#include "ProcExt.h"

using namespace std;

const opcode ProcExt::abs_jump[2] = {0xFF, 0x25}; // JMP ds:m32
const opcode ProcExt::abs_call[2] = {0xFF, 0x15}; // CALL ds:m32
const opcode ProcExt::movEAXEDI[2] = {0x8B, 0xC7};

ProcExt ProcExt::LaunchProcess(const std::wstring& pname, std::wstring parameters, DWORD flags)
{
	if (parameters.size() != 0)
	{
		if (parameters[0] != L' ')
		{
			parameters.insert(0,L" ");
		}
	}

	std::wstring tmp_string = pname;
	unsigned int pos = tmp_string.find_last_of(L"\\");
	tmp_string.erase(0, pos +1);
	parameters = tmp_string.append(parameters);

	STARTUPINFOW startup_info;
	PROCESS_INFORMATION process_info;
	memset(&startup_info, 0, sizeof(startup_info));
	memset(&process_info, 0, sizeof(process_info));
	startup_info.cb = sizeof(startup_info);

	LPCWSTR procName = const_cast<LPCWSTR>(pname.c_str());
	LPWSTR procParam = const_cast<LPWSTR>(parameters.c_str());
	if (CreateProcess(procName, procParam, 0, 0, false,
		flags, 0, 0, &startup_info, &process_info) != 0)
	{
		return ProcExt((HANDLE)NULL);
	}

	ProcExt tmp_proc(process_info.hProcess);
	tmp_proc.SetThreadHandle(process_info.hThread);
	return tmp_proc;
}

HANDLE ProcExt::GetThreadHandle()
{
	return m_hThread;
}

HANDLE ProcExt::GetProcHandle()
{
	return m_hProc;
}

void ProcExt::SetProcHandle(HANDLE hproc)
{
	m_hProc = hproc;
}

void ProcExt::SetThreadHandle(HANDLE hthread)
{
	m_hThread = hthread;
}

DWORD ProcExt::SuspendMainThread()
{
	return SuspendThread(m_hThread);
}

DWORD ProcExt::ResumeMainThread()
{
	return ResumeThread(m_hThread);
}

bool ProcExt::GetProcessByName(const std::wstring& pname, HANDLE& htarget)
{
	DWORD processes[255];
	DWORD opcodes_written = 0;
	EnumProcesses(processes, sizeof(processes), &opcodes_written);

	if (opcodes_written == 0)
		return false;
	const wchar_t* procName = pname.c_str();
	for (unsigned int i=0;i < opcodes_written / sizeof(DWORD); i++)
	{
		HANDLE hprocess;
		hprocess = OpenProcess(PROCESS_ALL_ACCESS,FALSE,processes[i]);
		if (hprocess == NULL)
			continue;
		wchar_t name[MAX_PATH] = L"";
		GetModuleBaseNameW(hprocess, NULL, name, sizeof(name));
		
		if (wcscmp(name, procName) == 0)
		{
			htarget = hprocess;
			return true;
		}
		CloseHandle(hprocess);
	}
	return false;
}

bool ProcExt::GetModuleByName(HANDLE hproc, const std::wstring& mname, HMODULE& hmod)
{
	HMODULE modules[255];
	DWORD opcodes_written = 0;

	EnumProcessModules(hproc, modules, sizeof(modules), &opcodes_written);

	if (opcodes_written == 0)
		return false;
	const wchar_t* modName = mname.c_str();
	for (unsigned int i=0;i < opcodes_written / sizeof(DWORD); i++)
	{
		if (hproc == NULL)
			continue;
		wchar_t name[MAX_PATH] = L"";
		GetModuleBaseName(hproc, modules[i], name, sizeof(name));
		if (wcscmp(name, modName) == 0)
		{
			hmod = modules[i];
			return true;
		}
	}
	return false;
}

bool ProcExt::GetProcessByName(const std::wstring &pname)
{
	return GetProcessByName(pname, m_hProc);
}

HMODULE ProcExt::GetModuleByName(const std::wstring &mname)
{
	HMODULE htmp;
	if (GetModuleByName(m_hProc, mname, htmp))
		return htmp;
	return NULL;
}

bool ProcExt::GetModuleInfo(HMODULE hmod, LPMODULEINFO lpmodinfo)
{
	return (bool)GetModuleInformation(m_hProc, hmod, lpmodinfo, sizeof(MODULEINFO));
}
bool ProcExt::GetModuleInfo(const std::wstring& mname, LPMODULEINFO lpmodinfo)
{
	HMODULE module = GetModuleByName(mname);
	return (bool)GetModuleInformation(m_hProc, module, lpmodinfo, sizeof(MODULEINFO));
}
FARPROC ProcExt::GetProcAddr(const std::wstring& mname, const std::string& pname)
{
	return GetProcAddress(GetModuleByName(mname), pname.c_str());
}
FARPROC ProcExt::GetProcAddr(HMODULE hmod, const std::string& pname)
{
	return GetProcAddress(hmod, pname.c_str());
}
FARPROC ProcExt::GetProcAddr(const std::string& pname)
{
	return GetProcAddress((HINSTANCE)m_hProc, pname.c_str());
}
bool ProcExt::InjectDll(const std::string& dName)
{
	HMODULE kernel32, hmod = 0;
	DWORD baseAddress;
	void* dllNameOffset;

	kernel32 = GetModuleHandleW(L"kernel32.dll");

	dllNameOffset = VirtualAllocEx(m_hProc, NULL, dName.length(), MEM_RESERVE|MEM_COMMIT, PAGE_READWRITE);
	WriteProcessMemory(m_hProc, dllNameOffset, dName.c_str(), dName.length(), NULL);
	HANDLE ht = CreateRemoteThread(m_hProc, NULL, 0, (LPTHREAD_START_ROUTINE)GetProcAddress(kernel32, "LoadLibraryA"), dllNameOffset, 0, NULL);
	WaitForSingleObject(ht, INFINITE); // wait for the thread to terminate
	GetExitCodeThread(ht, &baseAddress);
	CloseHandle(ht);
	VirtualFreeEx(m_hProc, dllNameOffset, dName.length(), MEM_RELEASE); // free the memory used up by the library-name
	// Unload the library
	ht = CreateRemoteThread(m_hProc, NULL, NULL, (LPTHREAD_START_ROUTINE)GetProcAddress(kernel32, "FreeLibrary"), (void*)baseAddress, 0, NULL);
	WaitForSingleObject(ht, INFINITE);
	CloseHandle(ht);
	return true;
}
bool ProcExt::InjectDll(const std::wstring& dName)
{
	std::string c_dName;
	c_dName.assign(dName.begin(), dName.end());
	return InjectDll(c_dName);
}
bool ProcExt::PatchAt(void *vOffset, const opcode *vNewContent, SIZE_T length)
{
	DWORD dwCodeProtection;
	VirtualProtectEx(m_hProc, vOffset, length, PAGE_EXECUTE_READWRITE, &dwCodeProtection);
	bool success = WriteProcessMemory(m_hProc, vOffset, (void*)vNewContent, length, NULL);
	VirtualProtectEx(m_hProc, vOffset, length, dwCodeProtection, NULL);
	return success;
}
bool ProcExt::PatchAt(const std::wstring& mName, unsigned int uRelativeOffset,
					  const opcode *vNewContent, SIZE_T length)
{
	void* module = (void*)((unsigned int)GetModuleByName(mName)+uRelativeOffset);
	return PatchAt(module, vNewContent, length);
}
unsigned int ProcExt::Replace(HMODULE hmod, opcode *vold_opcodes, SIZE_T old_length, opcode *vnew_opcodes, SIZE_T new_length, unsigned int count)
{
	unsigned int retval = 0;
	MODULEINFO minfo;

	if (!GetModuleInfo(hmod, &minfo))
		return retval;

	ULONG position = (ULONG)hmod; // the location in the RAM we're currently reading
	unsigned int current_opcode = 0; // points to the the first opcode of the vOldopcodes array
	opcode read_opcode;
	while(position <= (ULONG)((unsigned int)minfo.SizeOfImage + (unsigned int)hmod))
	{
		if (!ReadProcessMemory(m_hProc, (void*)position, (void*)&read_opcode, 1, NULL))
		{
			position++;
			continue;
		}
		if (read_opcode == vold_opcodes[current_opcode++])
		{
			if (current_opcode == old_length)
			{
				if (PatchAt((void*)((unsigned int)position - current_opcode + 1), vnew_opcodes, 3))
					++retval;
				if (retval == count && count != 0)
					return retval;
			}
		}
		else
			current_opcode = 0;
		position++;
	}
	return retval;
};
unsigned int ProcExt::Replace(const std::wstring& mname, opcode *vold_opcodes, SIZE_T old_length, opcode *vnew_opcodes, SIZE_T new_length, unsigned int count)
{
	HMODULE hmod = GetModuleByName(mname);
	return Replace(hmod, vold_opcodes, old_length, vnew_opcodes, new_length ,count);
};

DWORD ProcExt::VirtualProtect(void* vOffset, SIZE_T length, DWORD codeProtection)
{
	DWORD dwCodeProtection;
	VirtualProtectEx(m_hProc, vOffset, length, codeProtection, &dwCodeProtection);
	return dwCodeProtection;
}

ProcExt::ProcExt(const std::wstring& pname)
{
	GetProcessByName(pname, m_hProc);
	m_uHookCounter = 0;
	m_hThread = NULL;
}

ProcExt::ProcExt(HANDLE hproc)
{
	m_hProc = hproc;
	m_uHookCounter = 0;
	m_hThread = NULL;
}
ProcExt::ProcExt()
{
	m_uHookCounter = 0;
	m_hThread = NULL;
}

unsigned int ProcExt::InstallHook(const std::wstring& mname, unsigned int moffset,
								  void *function, unsigned int relevantBytes)
{
	HMODULE module = GetModuleByName(mname);
	void* offset = (void*)((unsigned int)module + moffset);
	return InstallHook(offset, function, relevantBytes);
}

unsigned int ProcExt::InstallHook(void *offset, void *function, unsigned int relevantBytes)
{
	Hook *k = Hook::GetHook(this, offset, function, relevantBytes);
	m_hooks.insert(pair<unsigned int, Hook*>(m_uHookCounter,k));
	return m_uHookCounter++;
}

unsigned int ProcExt::InstallHook(HMODULE module, unsigned int moffset, void *function, unsigned int relevantBytes)
{
	return InstallHook((void*)((unsigned int)module + moffset), function, relevantBytes);
}

unsigned int ProcExt::InstallHook(HMODULE module, unsigned int moffset, void (__stdcall *p)(), unsigned int relevantBytes)
{
	return InstallHook((void*)((unsigned int)module + moffset), reinterpret_cast<void*>(p), relevantBytes);
}

unsigned int ProcExt::InstallSetEAXHook(void *offset, void *function, unsigned int relevantBytes)
{
	Hook *k = Hook::GetSetEAXHook(this, offset, function, relevantBytes);
	m_hooks.insert(pair<unsigned int, Hook*>(m_uHookCounter,k));
	return m_uHookCounter++;
}

unsigned int ProcExt::InstallHookWithoutPushAD(void *offset, void *function, unsigned int relevantBytes)
{
	Hook *k = Hook::GetHookWithoutPushAD(this, offset, function, relevantBytes);
	m_hooks.insert(pair<unsigned int, Hook*>(m_uHookCounter,k));
	return m_uHookCounter++;
}

unsigned int ProcExt::InstallHookWithoutAny(void *offset, void *function, unsigned int relevantBytes)
{
	Hook *k = Hook::GetHookWithoutAny(this, offset, function, relevantBytes);
	m_hooks.insert(pair<unsigned int, Hook*>(m_uHookCounter,k));
	return m_uHookCounter++;
}

void ProcExt::RemoveHook(unsigned int index)
{
	m_hooks.find(index)->second->~Hook();
	m_hooks.erase(index);
}

void ProcExt::ChangeHookTarget(unsigned int index, void *new_target)
{
	m_hooks.find(index)->second->ChangeTarget(new_target);
}
bool ProcExt::Redirect(void *offset, void *function)
{
	// TODO: absolute / relative
	DWORD *jmp_address = 0;
	bool success = PatchAt(offset, (opcode*)abs_jump, 2);
	if (!success)
		return success;
	m_redirections.push_back(std::pair<DWORD, DWORD>(reinterpret_cast<DWORD>(jmp_address), reinterpret_cast<DWORD>(function)));
	m_redirections.back().first = (DWORD)&m_redirections.back().second;
	success &= PatchAt((void*)((unsigned long)offset + 2), (opcode*)&m_redirections.back().first, 4);
	return success;
}

ProcExt::~ProcExt()
{
	if (m_hProc != NULL)
		CloseHandle(m_hProc);
	if (m_hThread != NULL)
		CloseHandle(m_hThread);
	typedef std::map<unsigned int, Hook*>::iterator iter;
	for (iter i = m_hooks.begin(); i != m_hooks.end(); i++) {
		delete i->second;
	}
}

ProcExt::Hook::Hook() {
}

ProcExt::Hook* ProcExt::Hook::GetSetEAXHook(ProcExt* parent, void *offset, void *function, unsigned int relevantBytes)
{
	Hook* h = new Hook();
	h->_offset = offset;
	h->_back = (void*)((ULONG)offset + relevantBytes);
	h->_parent = parent;
	h->_pointer = (opcode*)new opcode*((opcode*)function);
	h->_trmpptr = (opcode*)&h->_trampoline;

	// get the relevant opcodes
	ReadProcessMemory(h->_parent->m_hProc, h->_offset, (void*)(h->_trampoline + 0x10), relevantBytes, NULL);
	// write a jump to our trampoline - 6 bytes
	opcode* ptr = (opcode*)&h->_trmpptr;
	h->_parent->PatchAt(h->_offset, (opcode*)abs_jump, 2);
	h->_parent->PatchAt((void*)((unsigned long)h->_offset + 2), (opcode*)&ptr, sizeof(DWORD));


	// write the trampoline:
	h->_parent->PatchAt((void*)h->_trampoline, &pushEDI);
	h->_parent->PatchAt((void*)(h->_trampoline + 1), &pushall);
	h->_parent->PatchAt((void*)(h->_trampoline + 2), &pushf); // push all to ensure that the registers don't change
	h->_parent->PatchAt((void*)(h->_trampoline + 3), (opcode*)abs_call, 2);
	// write the call within in the trampoline to the function
	h->_parent->PatchAt((void*)(h->_trampoline + 5), (opcode*)&h->_pointer, sizeof(DWORD));
	h->_parent->PatchAt((void*)(h->_trampoline + 9), &popf);
	h->_parent->PatchAt((void*)(h->_trampoline + 0xA), &popEDI);
	h->_parent->PatchAt((void*)(h->_trampoline + 0xB), &pushEAX);
	h->_parent->PatchAt((void*)(h->_trampoline + 0xC), &popall);
	h->_parent->PatchAt((void*)(h->_trampoline + 0xD), (opcode*)movEAXEDI, 2);
	h->_parent->PatchAt((void*)(h->_trampoline + 0xF), &popEDI);

	// now the jump back;
	h->_parent->PatchAt((void*)(h->_trampoline + 0x10 + relevantBytes), (opcode*)abs_jump, 2);
	ptr = (opcode*)&h->_back;
	h->_parent->PatchAt((void*)(h->_trampoline + 0x12 + relevantBytes), (opcode*)&ptr, sizeof(DWORD));
	return h;
}

ProcExt::Hook* ProcExt::Hook::GetHookWithoutAny(ProcExt *parent, void *offset, void *function, unsigned int relevantBytes)
{
	Hook* h = new Hook();
	h->_offset = offset;
	h->_back = (void*)((ULONG)offset + relevantBytes);
	h->_parent = parent;
	h->_pointer = (opcode*)new opcode*((opcode*)function);
	h->_trmpptr = (opcode*)&h->_trampoline;

	ReadProcessMemory(h->_parent->m_hProc, h->_offset, (void*)(h->_trampoline + 6), relevantBytes, NULL);
	// write a jump to our trampoline
	opcode* ptr = (opcode*)&h->_trmpptr;
	h->_parent->PatchAt(h->_offset, (opcode*)abs_jump, 2);
	h->_parent->PatchAt((void*)((unsigned long)h->_offset + 2), (opcode*)&ptr, sizeof(DWORD));

	// write the trampoline:
	h->_parent->PatchAt((void*)(h->_trampoline), (opcode*)abs_call, 2);
	// write the call within in the trampoline to the function
	h->_parent->PatchAt((void*)(h->_trampoline + 2), (opcode*)&h->_pointer, sizeof(DWORD));
	// now the jump back;
	h->_parent->PatchAt((void*)(h->_trampoline + 6 + relevantBytes), (opcode*)abs_jump, 2);
	ptr = (opcode*)&h->_back;
	h->_parent->PatchAt((void*)(h->_trampoline + 8 + relevantBytes), (opcode*)&ptr, sizeof(DWORD));
	return h;
}

ProcExt::Hook* ProcExt::Hook::GetHookWithoutPushAD(ProcExt *parent, void *offset, void *function, unsigned int relevantBytes)
{
	Hook* h = new Hook();
	h->_offset = offset;
	h->_back = (void*)((ULONG)offset + relevantBytes);
	h->_parent = parent;
	h->_pointer = (opcode*)new opcode*((opcode*)function);
	h->_trmpptr = (opcode*)&h->_trampoline;

	ReadProcessMemory(h->_parent->m_hProc, h->_offset, (void*)(h->_trampoline + 8), relevantBytes, NULL);
	// write a jump to our trampoline
	opcode* ptr = (opcode*)&h->_trmpptr;
	h->_parent->PatchAt(h->_offset, (opcode*)abs_jump, 2);
	h->_parent->PatchAt((void*)((unsigned long)h->_offset + 2), (opcode*)&ptr, sizeof(DWORD));

	// write the trampoline:
	h->_parent->PatchAt((void*)(h->_trampoline ), &pushf); // push all to ensure that the registers don't change
	h->_parent->PatchAt((void*)(h->_trampoline + 1), (opcode*)abs_call, 2);
	// write the call within in the trampoline to the function
	h->_parent->PatchAt((void*)(h->_trampoline + 3), (opcode*)&h->_pointer, sizeof(DWORD));
	h->_parent->PatchAt((void*)(h->_trampoline + 7), &popf);
	// now the jump back;
	h->_parent->PatchAt((void*)(h->_trampoline + 8 + relevantBytes), (opcode*)abs_jump, 2);
	ptr = (opcode*)&h->_back;
	h->_parent->PatchAt((void*)(h->_trampoline + 10 + relevantBytes), (opcode*)&ptr, sizeof(DWORD));
	return h;
}

ProcExt::Hook* ProcExt::Hook::GetHook(ProcExt *parent, void *offset, void *function, unsigned int relevantBytes)
{
	Hook* h = new Hook();
	h->_offset = offset;
	h->_back = (void*)((ULONG)offset + relevantBytes);
	h->_parent = parent;
	h->_pointer = (opcode*)new opcode*((opcode*)function);
	h->_trmpptr = (opcode*)&h->_trampoline;

	ReadProcessMemory(h->_parent->m_hProc, h->_offset, (void*)(h->_trampoline + 10), relevantBytes, NULL);
	// write a jump to our trampoline
	opcode* ptr = (opcode*)&h->_trmpptr;
	h->_parent->PatchAt(h->_offset, (opcode*)abs_jump, 2);
	h->_parent->PatchAt((void*)((unsigned long)h->_offset + 2), (opcode*)&ptr, sizeof(DWORD));

	// write the trampoline:
	h->_parent->PatchAt((void*)h->_trampoline, &pushall);
	h->_parent->PatchAt((void*)(h->_trampoline + 1), &pushf); // push all to ensure that the registers don't change
	h->_parent->PatchAt((void*)(h->_trampoline + 2), (opcode*)abs_call, 2);
	// write the call within in the trampoline to the function
	h->_parent->PatchAt((void*)(h->_trampoline + 4), (opcode*)&h->_pointer, sizeof(DWORD));
	h->_parent->PatchAt((void*)(h->_trampoline + 8), &popf);
	h->_parent->PatchAt((void*)(h->_trampoline + 9), &popall);
	// now the jump back;
	h->_parent->PatchAt((void*)(h->_trampoline + 10 + relevantBytes), (opcode*)abs_jump, 2);
	ptr = (opcode*)&h->_back;
	h->_parent->PatchAt((void*)(h->_trampoline + 12 + relevantBytes), (opcode*)&ptr, sizeof(DWORD));
	return h;
}

void ProcExt::Hook::ChangeTarget(void *function)
{
	// TODO: implementieren
	_pointer = (opcode*)&function;
	_parent->PatchAt((void*)((unsigned long)_trampoline + 4), (opcode*)_pointer);
}

ProcExt::Hook::~Hook()
{
	opcode old_opcodes[20];
	ReadProcessMemory(_parent->m_hProc, (void*)((unsigned long)_trampoline + 0x0A), old_opcodes, ((ULONG)_back - (ULONG)_offset), NULL);
	_parent->PatchAt(_offset, old_opcodes, ((ULONG)_back - (ULONG)_offset));
	delete _pointer;
}

unsigned long ProcExt::FindSequence(const wchar_t *mname, opcode *bytes, SIZE_T length)
{
	return ProcExt::FindSequence(GetModuleByName(mname), bytes, length);
}
unsigned long ProcExt::FindSequence(HMODULE hmod, opcode *bytes, SIZE_T length)
{
	MODULEINFO minfo;

	if (!GetModuleInfo(hmod, &minfo))
		return NULL;

	return FindSequence(bytes, length, (unsigned long)hmod, ((unsigned long)minfo.SizeOfImage + (unsigned long)hmod));
}
unsigned long ProcExt::FindSequence(opcode *bytes, SIZE_T length, unsigned long from, unsigned long to)
{
	unsigned int retval = 0;

	ULONG position = from; // the location in the RAM we're currently reading
	unsigned int currentByte = 0; // points to the the first byte of the bytes array

	const int BUFFER_SIZE = 1024;
	opcode bytesRead[BUFFER_SIZE];
	int currentSize = BUFFER_SIZE;
	while(position <= to)
	{
		if (to - position < BUFFER_SIZE)
			currentSize = to - position;
		if (!ReadProcessMemory(m_hProc, (void*)position, (void*)&bytesRead, currentSize, NULL))
		{
			position++;
			continue;
		}
		for (int i = 0; i < currentSize; i++) {
			if (bytesRead[i] == bytes[currentByte++])
			{
				if (currentByte == length)
				{
					return position + i - currentByte + 1;
				}
			}
			else
				currentByte = 0;
		}
		position += currentSize;
	}
	return retval;
};