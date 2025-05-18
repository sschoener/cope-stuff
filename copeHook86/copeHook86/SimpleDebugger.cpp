#include "stdafx.h"
#include "SimpleDebugger.h"

bool GetProcessByName(const wchar_t *pname, HANDLE &hTarget)
{
	DWORD processes[255];
	DWORD bytesReturned = 0;
	EnumProcesses(processes, sizeof(processes), &bytesReturned);

	if (bytesReturned == 0)
		return false;
	for (unsigned int i = 0; i < bytesReturned / sizeof(DWORD); i++)
	{
		HANDLE hProcess;
		hProcess = OpenProcess(PROCESS_ALL_ACCESS, FALSE, processes[i]);
		if (hProcess == NULL)
			continue;
		wchar_t name[MAX_PATH] = L"";
		GetModuleBaseNameW(hProcess, NULL, name, sizeof(name));
		if (wcscmp(name, pname) == 0)
		{
			hTarget = hProcess;
			return true;
		}
		CloseHandle(hProcess);
	}
	return false;
}

void Debugger::DebuggerLoop()
{
	DEBUG_EVENT debugEvent= {0};

	for(;;)
	{
		if (!WaitForDebugEvent(&debugEvent, INFINITE))
			return;
		DWORD continueStatus = DBG_CONTINUE;
		bool continueLoop = HandleDebugEvent(debugEvent, continueStatus);
		ContinueDebugEvent(debugEvent.dwProcessId, debugEvent.dwThreadId, continueStatus);
		if (!continueLoop)
			break;
	}
}

bool Debugger::HandleDebugEvent(const DEBUG_EVENT &debugEvent, DWORD &continueStatus)
{
	switch(debugEvent.dwDebugEventCode)
	{
	case CREATE_PROCESS_DEBUG_EVENT:
		{
			m_processInfo = debugEvent.u.CreateProcessInfo;
			CloseHandle(m_processInfo.hFile);
		}
	case CREATE_THREAD_DEBUG_EVENT:
		{
			DWORD threadId = debugEvent.dwThreadId;
			HANDLE hThread = debugEvent.u.CreateThread.hThread;
			m_threads.insert(pair<DWORD,HANDLE>(threadId, hThread));
		}
	case EXIT_THREAD_DEBUG_EVENT:
		{
			m_threads.erase(debugEvent.dwThreadId);
		}
	case OUTPUT_DEBUG_STRING_EVENT:
		{
			OnDebugOutput(debugEvent.u.DebugString);
		}
	case EXCEPTION_DEBUG_EVENT:
		{
			if (debugEvent.u.Exception.ExceptionRecord.ExceptionCode == EXCEPTION_BREAKPOINT)
			{
				CONTEXT lpContext;
				lpContext.ContextFlags = CONTEXT_ALL;
				
				HANDLE hThread = m_threads.find(debugEvent.dwThreadId)->second;
				GetThreadContext(hThread, &lpContext);

				// handle breakpoint
				m_lastBreakpoint = debugEvent.u.Exception.ExceptionRecord.ExceptionAddress;
				

				lpContext.Eip--;
				lpContext.EFlags |= 0x100; // enable single step
				SetThreadContext(hThread, &lpContext);

				// rewrite code
				opcode op = m_breakpoints.find(m_lastBreakpoint)->second;
				DWORD writeSize;
				WriteProcessMemory(m_processInfo.hProcess, m_lastBreakpoint, &op, 1, &writeSize);
				FlushInstructionCache(m_processInfo.hProcess, m_lastBreakpoint, 1);
				return true;
			}
			if (debugEvent.u.Exception.ExceptionRecord.ExceptionCode == EXCEPTION_SINGLE_STEP)
			{
				// rewrite breakpoint
				DWORD writeSize;
				WriteProcessMemory(m_processInfo.hProcess, m_lastBreakpoint, &OP_BREAKPOINT, 1, &writeSize);
				return true;
			}
			continueStatus = DBG_EXCEPTION_NOT_HANDLED;
		}
	case EXIT_PROCESS_DEBUG_EVENT:
		{
			return false;
		}
	case LOAD_DLL_DEBUG_EVENT:
		{
			CloseHandle(debugEvent.u.LoadDll.hFile);
		}
	default:
		{
		}
	}
	return true;
}

bool Debugger::RemoveBreakpoint(void* address)
{
	auto iter = m_breakpoints.find(address);
	if (iter == m_breakpoints.end())
		return false;
	opcode original = iter->second;
	DWORD writeSize;
	WriteProcessMemory(m_processInfo.hProcess, address, &original, 1, &writeSize);
	m_breakpoints.erase(address);
	return true;
}

bool Debugger::SetBreakpoint(void* address)
{
	auto iter = m_breakpoints.find(address);
	if (iter != m_breakpoints.end())
		return false;
	opcode original;
	DWORD numRead;
	ReadProcessMemory(m_processInfo.hProcess, address, &original, 1, &numRead);

	DWORD numWritten;
	WriteProcessMemory(m_processInfo.hProcess, address, &OP_BREAKPOINT, 1, &numWritten);
	FlushInstructionCache(m_processInfo.hProcess, address, 1);
	m_breakpoints.insert(pair<void*, opcode> (address, original));
	return true;
}

bool Debugger::AttachToProcess(const std::wstring name)
{
	if (m_bAttached)
		return false;
	HANDLE hProc;
	if (!GetProcessByName(name.c_str(), hProc))
		return false;
	m_processId = GetProcessId(hProc);
	DebugActiveProcess(m_processId);
}

bool Debugger::AttachToProcess(const HANDLE process)
{
	if (m_bAttached)
		return false;
	m_processId = GetProcessId(process);
	DebugActiveProcess(m_processId);
}

bool Debugger::AttachToProcess(const DWORD procid)
{
	if (m_bAttached)
		return false;
	m_processId = procid;
	DebugActiveProcess(m_processId);
}