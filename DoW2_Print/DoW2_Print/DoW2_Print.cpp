#include "stdafx.h"
#ifndef PROC_EXT
	#include "ProcExt.h"
#endif


using namespace std;
bool SetDebugPrivileges();

int _tmain(int argc, _TCHAR* argv[])
{
	cout << "Cope's DLL loader for Dawn of War 2\n\n";
	if (argc == 1)
	{
		cout << "No DLLs to load!\nPass the names of DLLs (which should be located in your DoW2-directory) as commandline parameters.\n";
		system("pause");
		return true;
	}
	// signature of the Xlive-memory-check
	BYTE xlivesig[20] = {0x8B, 0xFF, 0x55, 0x8B, 0xEC, 0x83, 0xEC, 0x20, 0x53, 0x56, 0x57, 0x8D, 0x45, 0xE0, 0x33, 0xF6, 0x50, 0xFF, 0x75, 0x0C};
	const BYTE retnC[3] = {0xC2, 0x0C, 0x00}; // retn 0x0C;
	
	ProcExt peDoW2 = new ProcExt();
	cout << "Searching for DoW2-process...\n";
	while (!peDoW2.GetProcessByName(L"dow2.exe"))
	{
		Sleep(1000);
	}
	cout << "DoW2 found!\n";
	cout << "Preparing DoW2-process... -- might take a minute or two...\n";
	peDoW2.Replace(L"xlive.dll", xlivesig, 20, (BYTE*)retnC, 3, 1);
	cout << "Preparation done!\n";
	cout << "Loading custom dlls...\n";
	for (int i = 1; i < argc; i++)
	{
		peDoW2.InjectDLL(argv[i]);
		wcout << argv[i] << " loaded!\n";
	}
	cout << "Custom dlls loaded!\n";

	system("pause");
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