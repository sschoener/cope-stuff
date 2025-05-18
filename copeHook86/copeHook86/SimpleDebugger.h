#include "stdafx.h"
#include <Psapi.h>
#include <string>
#include <map>

using namespace std;

typedef unsigned char opcode;

class Debugger 
{
private:
	static const opcode OP_BREAKPOINT = 0xCC;

	std::map<void*, opcode> m_breakpoints;
	std::map<DWORD,HANDLE> m_threads;
	void* m_lastBreakpoint;

	CREATE_PROCESS_DEBUG_INFO m_processInfo;
	DWORD m_processId;
	bool m_bAttached;

	void DebuggerLoop();
protected:
	virtual void OnBreakpoint(void* address, const CONTEXT &threadContext);
	virtual void OnDebugOutput(const OUTPUT_DEBUG_STRING_INFO &info);
	virtual bool HandleDebugEvent(const DEBUG_EVENT &debugEvent, DWORD &continueStatus);

public:

	// returns true on success
	bool AttachToProcess(const std::wstring name);
	bool AttachToProcess(const HANDLE process);
	bool AttachToProcess(const DWORD procid);

	// returns true on success
	bool SetBreakpoint(void* address);

	// returns true on success
	bool RemoveBreakpoint(void* address);
};
