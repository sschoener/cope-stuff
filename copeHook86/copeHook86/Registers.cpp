#include "stdafx.h"

void* RegisterCallback;

extern "C" __declspec(dllexport,naked) int GetEAX()
{
	__asm{
		ret;
	}
}

extern "C" __declspec(dllexport,naked) int GetEBX()
{
	__asm{
		mov eax, ebx;
		ret;
	}
}

extern "C" __declspec(dllexport,naked) int GetECX()
{
	__asm{
		mov eax, ecx;
		ret;
	}
}

extern "C" __declspec(dllexport,naked) int GetEDX()
{
	__asm{
		mov eax, edx;
		ret;
	}
}

extern "C" __declspec(dllexport,naked) int GetESI()
{
	__asm{
		mov eax, esi;
		ret;
	}
}

extern "C" __declspec(dllexport,naked) int GetEDI()
{
	__asm{
		mov eax, edi;
		ret;
	}
}

extern "C" __declspec(dllexport,naked) int GetESP()
{
	__asm{
		mov eax, esp;
		ret;
	}
}

extern "C" __declspec(dllexport,naked) int GetEBP()
{
	__asm{
		mov eax, ebp;
		ret;
	}
}

extern "C" __declspec(dllexport,naked) int GetStack(int offset)
{
	__asm{
		push ebx;
		mov eax, [esp + 4];
		add esp, eax;
		mov ebx, [esp];
		sub esp, eax;
		mov eax, ebx;
		pop ebx;
		ret;
	}
}

extern "C" __declspec(dllexport,naked) void SetEAX(int eax)
{
	__asm{
		mov eax, [esp+4];
		ret;
	}
}

extern "C" __declspec(dllexport,naked) void SetEBX(int ebx)
{
	__asm{
		mov ebx, [esp+4];
		ret;
	}
}

extern "C" __declspec(dllexport,naked) void SetECX(int ecx)
{
	__asm{
		mov ecx, [esp+4];
		ret;
	}
}

extern "C" __declspec(dllexport,naked) void SetEDX(int edx)
{
	__asm{
		mov edx, [esp+4];
		ret;
	}
}

extern "C" __declspec(dllexport,naked) void SetESI(int esi)
{
	__asm{
		mov esi, [esp+4];
		ret;
	}
}

extern "C" __declspec(dllexport,naked) void SetEDI(int edi)
{
	__asm{
		mov edi, [esp+4];
		ret;
	}
}

extern "C" __declspec(dllexport,naked) void SetStack(int offset, int value)
{
	__asm{
		push ebx;
		push eax;
		mov ebx, [esp+4];
		mov eax, [esp+8];
		add esp, ebx;
		mov [esp], eax;
		pop eax;
		pop ebx;
		ret;
	}
}

// pushes the registers to the stack and calls the RegisterCallback with a pointer to the stack as arg1
extern "C" __declspec(dllexport,naked) void GetRegisters()
{
	__asm {
		push eax;
		push ebx;
		push ecx;
		push edx;
		push esi;
		push edi;
		mov eax, esp;
		add eax, 0x18;
		push eax;
		push ebp;
		mov eax, esp;
		push eax;
		call RegisterCallback;
		add esp, 0x1C;
		pop eax;
	}
}

extern "C" __declspec(dllexport,naked) void SetRegisterCallback(void* callback)
{
	__asm {
		push eax;
		mov eax, [esp+8];
		mov RegisterCallback, eax;
		pop eax;
	}
}