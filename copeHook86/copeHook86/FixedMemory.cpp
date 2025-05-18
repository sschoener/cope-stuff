#include "stdafx.h"

typedef unsigned char byte;

extern "C" __declspec(dllexport) byte* FixedMemory_Create(const int size)
{
	return new byte[size];
}

extern "C" __declspec(dllexport) void FixedMemory_Delete(byte* fixedMem)
{
	delete fixedMem;
}