// DON'T FORGET TO EDIT THE IMPORT-TABLES!!!
// 1) the methods are NOT static and NOT public, you need to edit the import-table of your DLL

#ifndef _DEBUG_H
	#define _DEBUG_H
#endif

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