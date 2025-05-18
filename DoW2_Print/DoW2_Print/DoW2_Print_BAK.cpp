/*#include "stdafx.h"

#define XLIVE_CHECK_OFFSET 0x000ED690

#define CREATE_THREAD_ACCESS (PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ)

#pragma comment(lib, "C:\\Programme\\Microsoft SDKs\\Windows\\v6.0A\\Lib\\Psapi.Lib")

using namespace std;

bool InjectDLL(HANDLE hProc, std::wstring dllName);
bool SetDebugPrivileges();
bool PatchXLive(HANDLE hProcess, HMODULE hMod);
bool GetProcessByName(const wchar_t *procName, HANDLE *hTarget);
bool GetModuleByName(HANDLE hProcess, const wchar_t *modName, HMODULE *hModule);

const std::wstring PROCESS_NAME(L"dow2.exe");
const std::wstring XLIVE_NAME(L"xlive.dll");
const std::wstring DLL_NAME(L"cope.dll");

int _tmain(int argc, _TCHAR* argv[])
{
	HANDLE hProc;
	HMODULE hMod = 0;
	if (!SetDebugPrivileges())
		printf("Could not set debug privileges!\n");

	if (GetProcessByName(PROCESS_NAME.c_str(), &hProc))
	{
		printf("Process found!\n");
		if (true)
		{
			GetModuleByName(hProc, XLIVE_NAME.c_str(), &hMod);
			printf("Module Found!\n");
			if (PatchXLive(hProc, hMod))
			{
				if (InjectDLL(hProc, DLL_NAME.c_str()))
					printf("DLL injected!\n");
			}
		}
		else
		{
			printf("XLive not found!\n");
		}
		CloseHandle(hProc); 
		system("pause");
	}
	else
	{
		printf("Process couldn't be found!\n");
		system("pause");
	}
	return 0;
}

bool InjectDLL(HANDLE hProc, std::wstring dllName) 
{ 
   HMODULE hMod = 0;
   DWORD baseAddress = 0;
   LPVOID remoteString;

   // we need both a wchar_t and a normal char... sucks?
   // char *c_dllName = (char*)malloc(dllName.length());
   // wcstombs_s(NULL, c_dllName, dllName.length(),dllName.c_str(), dllName.length()*2);
   std::string c_dllName;
   c_dllName.assign(dllName.begin(), dllName.end());
   HMODULE kernel32 = GetModuleHandleW(_T("kernel32.dll"));

   remoteString = (LPVOID)VirtualAllocEx(hProc, NULL, c_dllName.length(), MEM_RESERVE|MEM_COMMIT, PAGE_READWRITE);
   WriteProcessMemory(hProc, (LPVOID)remoteString, (void*)c_dllName.c_str(), c_dllName.length(), NULL);
   HANDLE hT = CreateRemoteThread(hProc, NULL, 0, (LPTHREAD_START_ROUTINE)GetProcAddress(kernel32, "LoadLibraryA"), (LPVOID)remoteString, 0, NULL);
   WaitForSingleObject(hT, INFINITE); // wait for the thread to terminate
   if (!GetExitCodeThread(hT, &baseAddress))
	   printf("GetExitCodeThread failed!\n");
   GetModuleByName(hProc, dllName.c_str(), &hMod);
   printf("%d, %d\n", baseAddress, hMod);
   CloseHandle(hT);
   VirtualFreeEx(hProc, remoteString, sizeof(dllName), MEM_RELEASE); // free the memory used up by the library-name
   // Unload the library
   hT = CreateRemoteThread(hProc, NULL, NULL, (LPTHREAD_START_ROUTINE)GetProcAddress(kernel32, "FreeLibrary"), (void*)hMod, 0, NULL);
   WaitForSingleObject(hT, INFINITE);
   CloseHandle(hT);
   return true; 
}

bool SetDebugPrivileges()
{
	HANDLE hToken;
	TOKEN_PRIVILEGES tokenPriv;
	tokenPriv.PrivilegeCount = 1;

	if(!OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES, &hToken))
		return false;

	if(!LookupPrivilegeValue(NULL, SE_DEBUG_NAME, &tokenPriv.Privileges[0].Luid))
		return false;
	tokenPriv.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;

	if(!AdjustTokenPrivileges(hToken, false, &tokenPriv, sizeof(TOKEN_PRIVILEGES), NULL, NULL))
		return false;
	return true;
}

bool GetProcessByName(const wchar_t *procName, HANDLE *hTarget)
{
	DWORD processes[255];
	DWORD bytes = 0;
	EnumProcesses(processes, sizeof(processes), &bytes);

	if (bytes == 0)
		return false;
	for (unsigned int i=0;i < bytes / sizeof(DWORD); i++)
	{
		HANDLE hProcess;
		hProcess = OpenProcess(CREATE_THREAD_ACCESS,FALSE,processes[i]);
		if (hProcess == NULL)
			continue;
		wchar_t name[MAX_PATH] = L"";
		GetModuleBaseNameW(hProcess, NULL, name, sizeof(name));
		if (wcscmp(name, procName) == 0)
		{
			__asm
			{
				mov eax, hProcess;
				mov ecx, hTarget;
				mov [ecx], eax;
			}
			return true;
		}
		CloseHandle(hProcess);
	}
	return false;
}

bool GetModuleByName(HANDLE hProcess, const wchar_t* modName, HMODULE *hModule)
{
	HMODULE hMod[255];
	DWORD bytes = 0;

	EnumProcessModules(hProcess, hMod, sizeof(hMod), &bytes);

	if (bytes == 0)
		return false;

	for (unsigned int i=0;i < bytes / sizeof(DWORD); i++)
	{
		if (hProcess == NULL)
			continue;
		wchar_t name[MAX_PATH] = L"";
		GetModuleBaseName(hProcess, hMod[i], name, sizeof(name));
		if (wcscmp(name, modName) == 0)
		{
			HMODULE tmp = hMod[i];
			__asm
			{
				mov eax, hModule;
				mov ecx, tmp;
				mov [eax], ecx;
			}
			return true;
		}
	}
	return false;
}

bool PatchXLive(HANDLE hProcess, HMODULE hMod)
{
	BYTE retnC[3] = {0xC2, 0x0C, 0x00};
	SIZE_T bytes = 0;
	DWORD dwCodeProtection;
	if (VirtualProtectEx(hProcess, hMod+XLIVE_CHECK_OFFSET, sizeof(retnC), PAGE_EXECUTE_READWRITE, &dwCodeProtection))
		printf("Unprotected Memory!\n");
	if (WriteProcessMemory(hProcess, (LPVOID)(hMod+XLIVE_CHECK_OFFSET), (void*)retnC, sizeof(retnC), &bytes))
		printf("Xlive patched!\n");
	if (VirtualProtectEx(hProcess, hMod+XLIVE_CHECK_OFFSET, sizeof(retnC), dwCodeProtection, NULL))
		printf("Protected Memory!\n");
	return bytes != 0;
}*/