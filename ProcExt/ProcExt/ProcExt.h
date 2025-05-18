// needs PSAPI.dll
#ifndef PROC_EXT
	#define PROC_EXT

#include <map>
#include <vector>
#include <windows.h>
#include <psapi.h>

using namespace std;

typedef unsigned char opcode;

class ProcExt
{
	class Hook;
	friend class Hook;
	HANDLE m_hProc;
	HANDLE m_hThread;
	std::map<unsigned int, Hook*> m_hooks;
	std::vector<std::pair<DWORD, DWORD>> m_redirections;
	unsigned int m_uHookCounter;
	PROCESS_INFORMATION m_processInfo;

	static const opcode abs_jump[2]; // JMP ds:m32
	static const opcode rel_jump = 0xE9; // JUMP rel32
	static const opcode abs_call[2]; // CALL ds:m32
	static const opcode rel_call = 0xE8;
	static const opcode pushall = 0x60; // PUSHA - push multi-purpose registers
	static const opcode pushf = 0x9C; // PUSHF - push flags register
	static const opcode popall = 0x61; // POPA - pop mpr
	static const opcode popf = 0x9D; // POPF - pop fr
	static const opcode pushEDI = 0x57;
	static const opcode pushEAX = 0x50;
	static const opcode popEDI = 0x5F;
	static const opcode popEAX = 0x58;
	static const opcode movEAXEDI[2];

	public:

		// launches the specified process and returns a proc_ext belonging to it
		static ProcExt LaunchProcess(const std::wstring& pname, std::wstring parameters, DWORD flags);
		
		// Returns true on success, otherwise false; htarget will be the HANDLE of the process with the name pname.
		// Opens it up with PROCESS_ALL_ACCESS
		static bool GetProcessByName(const std::wstring& pname, HANDLE& htarget);

		// Returns true on success, otherwise false; htarget will be the HANDLE of the module with the name pname.
		static bool GetModuleByName(HANDLE hproc, const std::wstring& mname, HMODULE& hmod);

		HANDLE GetProcHandle();
		HANDLE GetThreadHandle();

		void SetProcHandle(HANDLE hproc);
		void SetThreadHandle(HANDLE hthread);

		DWORD SuspendMainThread();
		DWORD ResumeMainThread();

		// Sets the process of this proc_ext to the process with the specified name; returns false if there's no such process.
		bool GetProcessByName(const std::wstring& pname);

		// Returns the base-address of the module with the specified name.
		// Returns NULL if there's no such module.
		HMODULE GetModuleByName(const std::wstring& mname);

		// self explaining, just wraps the GetModuleInformation-function from psapi.dll
		bool GetModuleInfo(HMODULE hmod, LPMODULEINFO lpmodinfo);
		bool GetModuleInfo(const std::wstring& mname, LPMODULEINFO lpmodinfo);
		FARPROC GetProcAddr(HMODULE hmod, const std::string& pname);
		FARPROC GetProcAddr(const std::wstring& mname, const std::string& pname);
		FARPROC GetProcAddr(const std::string& pname);

		// returns the address of the first occurence of the given byte-sequence
		// this one searches the whole memory from from_ to to_
		unsigned long FindSequence(opcode *bytes, SIZE_T length, unsigned long from = 0x00000000, unsigned long to = 0x7FFFFFFF);
		// these two just scan the memory of one specified module
		unsigned long FindSequence(HMODULE hmod, opcode *bytes, SIZE_T length);
		unsigned long FindSequence(const wchar_t *mname, opcode *bytes, SIZE_T length);

		// Injects the specified DLL into the Process using the CreateRemoteThread-method
		bool InjectDll(const std::wstring& dName);
		bool InjectDll(const std::string& dName);

		// Patches the process at the specified offset
		bool PatchAt(void *vOffset, const opcode *vNewContent, SIZE_T length = 1);

		// Patches the process at the specified offset; mname is the name of the Module which urelative_offset is relative to
		bool PatchAt(const std::wstring&, unsigned int uRelativeOffset, const opcode *vNewContent, SIZE_T length = 1);

		// Replaces a sequence of bytes with another; if vnew_bytes < vold_bytes only the first bytes of vold_bytes will be patched.
		// Only the first count_ occurences will be patched, set to 0 to patch all occurences
		// Returns the number of replacements
		unsigned int Replace(const std::wstring&, opcode *vold_bytes, SIZE_T old_length, opcode *vnew_bytes, SIZE_T new_length, unsigned int count = 0);
		unsigned int Replace(HMODULE hmod, opcode *vold_bytes, SIZE_T old_length, opcode* vnew_bytes, SIZE_T new_length, unsigned int count = 0);
		DWORD VirtualProtect(void* offset, SIZE_T length, DWORD codeProtection);

		// This ctor should only be called when you're absolutely sure that there is a process with that name (pname = name of the exe).
		ProcExt(const std::wstring&);
		ProcExt(HANDLE hproc);
		ProcExt();

		unsigned int InstallHook(void *offset, void *function, unsigned int relevantBytes = 5);
		unsigned int InstallHook(const std::wstring&, unsigned int offset, void *function, unsigned int relevantBytes = 5);
		unsigned int InstallHook(HMODULE module, unsigned int offset, void* function, unsigned int relevantBytes = 5);
		unsigned int InstallHook(HMODULE module, unsigned int offset, void (__stdcall *p)(), unsigned int relevantBytes = 5);
		unsigned int InstallSetEAXHook(void *offset, void *function, unsigned int relevantBytes = 5);
		unsigned int InstallHookWithoutPushAD(void *offset, void *function, unsigned int relevantBytes = 5);
		unsigned int InstallHookWithoutAny(void *offset, void *function, unsigned int relevantBytes = 5);
		void RemoveHook(unsigned int index);
		void ChangeHookTarget(unsigned int index, void *new_target);

		// redirects an offset to the function of your choice (that is, it writes a jump to *offset pointing to *function)
		bool Redirect(void *offset, void *function);

		~ProcExt();
};

class ProcExt::Hook{
	void *_offset;
	void *_back;
	const opcode *_trmpptr;
	const opcode *_pointer;
	opcode _trampoline[40];
	ProcExt *_parent;

	Hook();
public:
	// Changes the offset to jump to
	void ChangeTarget(void *function);
	// p1: where to hook, p2: where to jump to, p3: number of bytes that need to be preserved (must be >= 6)
	// function is the pointer to a function; it should not have any arguments as they're getting ignored anyway
	// of course you can still get parameters using [EBP+8] etc...
	// to get the original EIP calling the hook use the following (only for GetHook):
	// OIP = [ebp+4] + 4 + relevantBytes
	// OIP = [OIP] - 6
	static Hook* GetHook(ProcExt *parent, void *offset, void *function, unsigned int relevantBytes);
	static Hook* GetSetEAXHook(ProcExt *parent, void *offset, void *function, unsigned int relevantBytes);
	static Hook* GetHookWithoutPushAD(ProcExt *parent, void *offset, void *function, unsigned int relevantBytes);
	static Hook* GetHookWithoutAny(ProcExt *parent, void *offset, void *function, unsigned int relevantOpcodes);
	~Hook();
};

#endif