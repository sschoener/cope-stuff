// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"
#include "Debug.h"
#include "ProcExt.h"

/*
	WARNING -- DIRTY CODE AHEAD!
 */

const unsigned int TeamWeaponHookOffset1_entity = 0x2EE2E8;
const unsigned int TeamWeaponHookOffset1 = 0x2EE546; // old: 0x2C5492
const unsigned int TeamWeaponPatchOffset1 = 0x2EE540; // old: 0x2C5491
const unsigned int TeamWeaponHookOffset2 = 0x2ED927; // old: 0x2C4A07
const unsigned int TeamWeaponPatchOffset2 = 0x2ED927; // old: 0x2C4A07
bool bHookInstalled;
void* PGM_GetGroup; // SimEngine.dll
void* PGM_GetInstance; // SimEngine.dll
void* Entity_GetBlueprintName; // SimEngine.dll
void* EntityGroupTemp_Peek; // SimEngine.dll
void* LuaState;
void* lua_getfield; // LuaConfig.dll
void* lua_call; // LuaConfig.dll
void* lua_tolstring; // LuaConfig.dll
void* lua_pushinteger; // LuaConfig.dll
void* lua_pushstring; // LuaConfig.dll
void* lua_remove; // LuaConfig.dll

void* entity; // temporary storage

const char* LuaCallback = "GetTeamWeaponSquad"; // name of the lua callback
const char* InfoString = "[Cope FactionFix] - Capturing TeamWeapon: @%#010x";
const char* EntityInfo = "[Cope FactionFix] - Entity @%#010x";
const opcode snop[6] = {0x50, 0x90, 0x90, 0x90, 0x90, 0x90};// push eax, nop, nop, nop, nop, nop

const unsigned int sub_3A9F0_offset = 0x3DA00; // originally 0x3A9F0
void* sub_3A9F0; // function used by TeamWeaponHook2

BOOL APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
		dbTracefAux("[Cope FactionFix] - DLL loaded");
		LoadLibrary(L"LuaExtCore.dll"); // apply loadfile patch
		break;
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}

void __stdcall print(const char* c) {
	dbTracefAux(c);
}

void __stdcall printPtr(const char* c, void* ptr) {
	dbTracefAux(c, ptr);
}

__declspec(naked) void GetEntityPtr() {
	__asm {
		mov entity, edi;
		retn;
	}
}

__declspec(naked) void* GetTeamWeaponSquad()
{
	// PLAN:
	// see if ECX > 3 -> return eax;
	// grab player + entity
	// get entity ID
	// push player + entity PGB path for SCAR
	// call SCAR function which will return the right PGB
	// use PropertyBagGroupManager to get the right group and return it
	// The entity we need is passed as a THIS to the __thiscall, we use another hook to save it
	__asm{
		cmp ecx, 4;
		jb VanillaFaction; // if it is a vanilla race: jump down
		push ebx;
		push edx;
		push edi;
		push esi;
		pushfd;
		mov ecx, eax; // EAX is the player
		mov ebx, [ecx+0x40]; // get player-ID, see SimEngine.dll for correct offset
		mov ecx, entity;
		call Entity_GetBlueprintName; // this ptr in ECX
		// now:
		//		EAX = Entity name
		//		EBX = Player-Race-ID
		//  get Lua-state using Corsix' loadfile (see function above)
		//	get Lua-function using lua_getglobal(char* name)
		push eax;
		push LuaCallback;
		push -10002; // global
		push LuaState;
		call lua_getfield;
		push LuaState;
		call lua_pushstring;
		push ebx;
		push LuaState;
		call lua_pushinteger;
		// call Lua-function using lua_call(...)
		push 1; // one return value
		push 2; // two args
		push LuaState;
		// lua func def:
		// function GetTeamWeaponSquad(raceID, entityID)
		call lua_call;
		// get result from Lua-stack
		push 0; // null pointer
		push -1;
		push LuaState;
		call lua_tolstring;
		// get PropertyBagGroup
		push eax; // arg0 for PGM_GetGroup = path to PropertyBagGroup returned by Lua-callback
		call PGM_GetInstance;
		mov ecx, eax;
		call PGM_GetGroup;
		mov ebx, eax;
		push -1;
		push LuaState;
		call lua_remove;
		mov eax, ebx;
		popfd;
		pop esi;
		pop edi;
		pop edx;
		pop ebx;
		retn;
VanillaFaction:
		lea     ecx, [edx+ecx*4]
		mov     eax, [ecx]
		retn;
	}
}

__declspec(naked) void* GetTeamWeaponSquad2() 
{
	/*
		EDI = Faction Index
		EAX = Player*
		ESI = EntityGroupTemp
	*/
	__asm{
		cmp	edi, 4;
		jb VanillaFaction;
		push ebx;
		mov eax, [eax+0x40]; // get player-ID
		mov edi, eax; // save player-ID
		mov ecx, esi;
		call EntityGroupTemp_Peek; // now eax has the entity*
		mov ecx, eax;
		call Entity_GetBlueprintName;
		push eax; // eax = entity-name
		push LuaCallback;
		push -10002; // global
		push LuaState;
		call lua_getfield;
		push LuaState;
		call lua_pushstring;
		push edi; // player-id
		push LuaState;
		call lua_pushinteger;
		// call Lua-function using lua_call(...)
		push 1; // one return value
		push 2; // two args
		push LuaState;
		call lua_call;
		// get result from Lua-stack
		push 0; // null pointer
		push -1;
		push LuaState;
		call lua_tolstring;
		push eax; // arg0 for PGM_GetGroup = path to PropertyBagGroup returned by Lua-callback
		call PGM_GetInstance;
		mov ecx, eax;
		call PGM_GetGroup;
		mov ebx, eax;
		push -1;
		push LuaState;
		call lua_remove;
		mov eax, ebx; // EAX = PBG
		pop ebx;
		retn;
VanillaFaction:
		mov	ecx, esi;
		call EntityGroupTemp_Peek;
		call sub_3A9F0;
		mov edx, [eax+0x2C];
		lea eax, [edx+edi*4];
		mov eax, [eax]; // EAX = PBG
		retn;
	}
}

// LUA functions are always stdcalls, so esp+4 contains luastate
extern "C" __declspec(dllexport) void __stdcall PrepareHook(void* state)
{
	dbTracefAux("[Cope FactionFix] - Trying to hook function");
	dbTracefAux("[Cope FactionFix] - Got LuaState @%#010x", state);
	LuaState = state;
	if (bHookInstalled) {
		dbTracefAux("[Cope FactionFix] - Hook already installed");
		return;
	}
	HMODULE sim = GetModuleHandle(L"SimEngine.dll");
	PGM_GetInstance = GetProcAddress(sim, "?Instance@PropertyBagGroupManager@@SGAAV1@XZ");
	PGM_GetGroup = GetProcAddress(sim, "?GetGroup@PropertyBagGroupManager@@QAEPBVPropertyBagGroup@@PBD@Z");
	Entity_GetBlueprintName = GetProcAddress(sim, "?GetBlueprintName@Entity@@QBEPBDXZ");
	EntityGroupTemp_Peek = GetProcAddress(sim, "?peek@EntityGroupTemp@@QBEPAVEntity@@XZ");
	HMODULE luaConfig = GetModuleHandle(L"LuaConfig.dll");
	lua_getfield = GetProcAddress(luaConfig, "_lua_getfield@12");
	lua_call = GetProcAddress(luaConfig, "_lua_call@12");
	lua_tolstring = GetProcAddress(luaConfig, "_lua_tolstring@12");
	lua_pushstring = GetProcAddress(luaConfig, "_lua_pushstring@8");
	lua_pushinteger = GetProcAddress(luaConfig, "_lua_pushinteger@8");
	lua_remove = GetProcAddress(luaConfig, "_lua_remove@8");
	
	HMODULE ww2 = GetModuleHandle(L"WW2Mod.dll");
	// compute the actual address of our helper function
	sub_3A9F0 = reinterpret_cast<char*>(ww2) + sub_3A9F0_offset;

	ProcExt proc = ProcExt(GetCurrentProcess());
	
	// apply helper hook
	void* entityHookAddress = reinterpret_cast<char*>(ww2) + TeamWeaponHookOffset1_entity;
	proc.InstallHookWithoutAny(entityHookAddress, (void*)GetEntityPtr, 6);
	dbTracefAux("[Cope FactionFix] - hooked @%#010x", entityHookAddress);

	
	// apply first patch
	void* patchAddress = reinterpret_cast<char*>(ww2) + TeamWeaponPatchOffset1;
	proc.PatchAt(patchAddress, (opcode*)snop, 6);
	dbTracefAux("[Cope FactionFix] - patched @%#010x", patchAddress);

	// apply first hook
	void* hookAddress = reinterpret_cast<char*>(ww2) + TeamWeaponHookOffset1;
	proc.InstallHookWithoutAny(hookAddress, (void*)GetTeamWeaponSquad, 6);
	dbTracefAux("[Cope FactionFix] - hooked @%#010x", hookAddress);

	// apply second patch
	patchAddress = reinterpret_cast<char*>(ww2) + TeamWeaponPatchOffset2;
	proc.VirtualProtect(patchAddress, 25, PAGE_EXECUTE_READWRITE);
	unsigned char* patch = (unsigned char*)patchAddress;
	for (int i = 0; i < 25; i++)
	{
		patch[i] = 0x90;
	}
	dbTracefAux("[Cope FactionFix] - patched @%#010x", patchAddress);

	// apply second hook
	hookAddress = reinterpret_cast<char*>(ww2) + TeamWeaponHookOffset2;
	proc.InstallHookWithoutAny(hookAddress, (void*)GetTeamWeaponSquad2, 6);
	dbTracefAux("[Cope FactionFix] - hooked @%#010x", hookAddress);
	

	bHookInstalled = true;
	dbTracefAux("[Cope FactionFix] - Hook installed");
}