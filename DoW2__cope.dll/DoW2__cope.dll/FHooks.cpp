#include "stdafx.h"
#include "Hooks.h"
#ifndef _DEBUG_H
	#include "Debug.h"
#endif

using namespace std;

const char *msgPlayerAdded = "[COPELOGGER] Player added: &Player = %X";
const char *msgPlayerRemoved = "[COPELOGGER] Player removed: PlayerID = %d";
const char *msgScreenShown = "[COPELOGGER] Showing screen: %X, %X";

void PlayerAdded()
{
	__asm{
		mov		eax, [esp+44];	// we want to get the parameter passed to the AddPlayer(Player*) function
								// PUSHAD and PUSHFD are used in the Hooking-function, thus decreasing ESP by 8 * 4 + 4 bytes
								// this function here doesn't decrement the ESP (no local vars) but 'call' pushes EIP to the stack, again 4 bytes less
								// [esp+4] usually is the first argument to a function
								// [esp + 4 (EIP) + 4 (first argument) + 4 (PUSHFD) + 8 * 4 (PUSHAD)] = [esp+44] :)
		mov		ecx, dbInternal::TimeStampedTracef;
		push	eax;
		push	msgPlayerAdded;
		call	ecx;
		pop		eax;
		pop		ecx;
	};
}

void PlayerRemoved()
{
	__asm{
		mov		eax, [esp+44];
		mov		ecx, dbInternal::TimeStampedTracef;
		push	eax;
		push	msgPlayerRemoved;
		call	ecx;
		pop		eax;
		pop		ecx;
	};
}

void ScreenShown()
{
	__asm{
		mov		eax, [esp+44];
		push	eax;
		mov		eax, [esp+48];
		push	eax;
		mov		ecx, dbInternal::TimeStampedTracef;
		push	msgScreenShown;
		call	ecx;
		pop		eax;
		pop		eax;
		pop		ecx;
	};
}