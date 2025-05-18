#include "ProcExt.h"
#include <windows.h>
#include "LuaConfig.h" // needs a .LIB file of LuaConfig.dll !!!
#include "LuaStuff.h"
#include "Debug.h"

using namespace std;

typedef int (luaHandler)(lua_State*);
void* __handler;

ProcExt peThis(GetCurrentProcess());

void* GetLuaLoadfileAddress(HANDLE proc) {
	return 0;
}

bool PatchLUA()
{
	void* lfAddress = GetLuaLoadfileAddress(peThis.GetProcHandle());
	if (!peThis.Redirect(lfAddress, (void*)&luaLoadfile))
	{
		dbInternal::TimeStampedTracef("[COPE] Redirection failed, Error Code: %d", GetLastError());
		return false;
	}
	else
	{
		dbInternal::TimeStampedTracef("[COPE] Redirection: loadfile @%X to %X!", lfAddress, &luaLoadfile);
		return true;
	}
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
	lua_pushliteral(L, "Cope's implementation of Corsix' great code and ideas");
	return 1;
}


BOOL APIENTRY DllMain( HMODULE hModule, DWORD ul_reason_for_call, LPVOID lpReserved)
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
		{
			EnsureModuleNeverUnloaded(hModule);
			dbInternal::TimeStampedTracef("[COPE] LuaLibLoad.dll loaded @%X", hModule);
			if (PatchLUA())
			{
				dbInternal::TimeStampedTracef("[COPE] loadfile patched, use loadfile(\"LuaLibLoad\", \"luaSelfTest\")() to test the function");
			}
			break;
		}
	case DLL_THREAD_ATTACH:
		break;
	case DLL_THREAD_DETACH:
		break;
	case DLL_PROCESS_DETACH:
		break;
	}
	return true;
}