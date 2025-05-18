#include "stdafx.h"

typedef unsigned char opcode;

class HookData {
public:
	const void *m_jumpBackPtr;
	const void *m_trampolinePtr;
	const void *m_functionPtr;
	opcode m_trampoline[40];
};

extern "C" __declspec(dllexport) HookData* Hook_CreateHookData(const void* jumpBackPtr, const void* functionPtr) {
	HookData *hd = new HookData();
	hd->m_jumpBackPtr = jumpBackPtr;
	hd->m_functionPtr = functionPtr;
	hd->m_trampolinePtr = &hd->m_trampoline;
	return hd;
};

extern "C" __declspec(dllexport) const void Hook_DestroyHookData(HookData* hd) {delete hd;};
extern "C" __declspec(dllexport) const void* Hook_GetJumpBackPtr(HookData* hd) {return hd->m_jumpBackPtr;};
extern "C" __declspec(dllexport) const void** Hook_GetJumpBackPtrPtr(HookData* hd) {return &hd->m_jumpBackPtr;};
extern "C" __declspec(dllexport) const void* Hook_GetTrampolinePtr(HookData* hd) {return hd->m_trampolinePtr;};
extern "C" __declspec(dllexport) const void** Hook_GetTrampolinePtrPtr(HookData* hd) {return &hd->m_trampolinePtr;};
extern "C" __declspec(dllexport) const void* Hook_GetFunctionPtr(HookData* hd) {return hd->m_functionPtr;};
extern "C" __declspec(dllexport) const void** Hook_GetFunctionPtrPtr(HookData* hd) {return &hd->m_functionPtr;};
extern "C" __declspec(dllexport) const void Hook_SetFunctionPtr(HookData* hd, void* ptr) {hd->m_functionPtr = ptr;};
extern "C" __declspec(dllexport) const void Hook_SetTrampoline(HookData* hd, unsigned char* trampoline, int trampLength) {
	for (int i = 0; i < trampLength; i++) {
		hd->m_trampoline[i] = trampoline[i];
	}
};
