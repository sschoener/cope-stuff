#include "stdafx.h"
#include "Hooks.h"
#ifndef _DEBUG_H
	#include "Debug.h"
#endif
#ifndef PROC_EXT
	#include "ProcExt.h"
#endif
#ifndef _LUACONFIG_H_
	#include "LuaConfig.h"
#endif

using namespace std;

ProcExt peThis(GetCurrentProcess());

void InitHooks()
{
	void* func = (void*)peThis.GetProcAddr(L"SimEngine.dll", "?AddPlayer@PlayerManager@@QAEXPAVPlayer@@@Z");
	peThis.InstallHook(func, &PlayerAdded);
	dbInternal::TimeStampedTracef("[COPE] Installed Hook: AddPlayer @%X to %X!", func, &PlayerAdded);
	func = (void*)peThis.GetProcAddr(L"SimEngine.dll", "?RmvPlayer@PlayerManager@@QAEXK@Z");
	peThis.InstallHook(func, &PlayerRemoved);
	dbInternal::TimeStampedTracef("[COPE] Installed Hook: RmvPlayer @%X to %X!", func, &PlayerRemoved);
	func = (void*)0x004C2BC0;
	peThis.InstallHook(func, &ScreenShown, 7);
	dbInternal::TimeStampedTracef("[COPE] Installed Hook: ShowScreen @%X to %X!", func, &ScreenShown);
	func = (void*)0x0045668D;
	peThis.InstallHook(func, &ScreenShown);
	dbInternal::TimeStampedTracef("[COPE] Installed Hook: ShowScreen @%X to %X!", func, &ScreenShown);
	func = (void*)0x004565EB;
	peThis.InstallHook(func, &ScreenShown);
	dbInternal::TimeStampedTracef("[COPE] Installed Hook: ShowScreen @%X to %X!", func, &ScreenShown);
}
void EnsureModuleNeverUnloaded(HMODULE hModule)
{
  size_t iNameLength = MAX_PATH;
  wchar_t* sNameBuffer = new wchar_t[iNameLength + 1];
  while(GetModuleFileName(hModule, sNameBuffer, (DWORD)iNameLength) == iNameLength)
  {
	delete[] sNameBuffer;
	iNameLength *= 2;
	sNameBuffer = new wchar_t[iNameLength + 1];
  }
  LoadLibrary(sNameBuffer);
  delete[] sNameBuffer;
}

extern "C" int __stdcall luaSelfTest(lua_State *L)
{
  dbInternal::TimeStampedTracef(luaL_optstring(L, 1, "self-test called"));
  lua_pushliteral(L, "Cope's implementation of Corsix' great code and ideas\n");
  return 1;
}

BOOL APIENTRY DllMain( HMODULE hModule, DWORD ul_reason_for_call, LPVOID lpReserved)
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
		{
			EnsureModuleNeverUnloaded(hModule);
			dbInternal::TimeStampedTracef("[COPE] cope.dll loaded @%X", hModule);
			InitHooks();
			break;
		}
	case DLL_THREAD_ATTACH:
		{
			break;
		}
	case DLL_THREAD_DETACH:
		break;
	case DLL_PROCESS_DETACH:
		break;
	}
	return true;
}