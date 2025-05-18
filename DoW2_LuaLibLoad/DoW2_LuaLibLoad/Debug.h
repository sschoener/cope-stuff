// 1) the methods are NOT static and NOT public, you need to edit the import-table of your DLL
// ?TimeStampedTracef@dbInternal@@SAXPBDZZ <- WRONG
// ?TimeStampedTracef@dbInternal@@YAXPBDZZ <- RIGHT

#ifndef _DEBUG_H
	#define _DEBUG_H

class __declspec(dllimport) dbInternal
{
public:
	static void __cdecl TimeStampedTracef(const char *, ...);
	static void __cdecl Printf(const char *, ...);
	static const char* __stdcall FormatFileLineString(char const*, int);
};

class __declspec(dllimport) dbTrace
{
public:
	static void __stdcall Send(const char*);
};

#endif