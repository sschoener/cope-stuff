// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"

void EnsureModuleNeverUnloaded(HMODULE hModule)
{
  size_t iNameLength = MAX_PATH;
  char* sNameBuffer = new char[iNameLength + 1];
  while(GetModuleFileNameA(hModule, sNameBuffer, (DWORD)iNameLength) == iNameLength)
  {
	delete[] sNameBuffer;
	iNameLength *= 2;
	sNameBuffer = new char[iNameLength + 1];
  }
  LoadLibraryA(sNameBuffer);
  delete[] sNameBuffer;
}

BOOL APIENTRY DllMain( HMODULE hModule,
					   DWORD  ul_reason_for_call,
					   LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
		EnsureModuleNeverUnloaded((HMODULE)hModule);
		DisableThreadLibraryCalls((HMODULE)hModule);
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}

