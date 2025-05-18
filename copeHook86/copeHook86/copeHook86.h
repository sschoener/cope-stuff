// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the COPEHOOK86_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// COPEHOOK86_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef COPEHOOK86_EXPORTS
#define COPEHOOK86_API __declspec(dllexport)
#else
#define COPEHOOK86_API __declspec(dllimport)
#endif

// This class is exported from the copeHook86.dll
class COPEHOOK86_API CcopeHook86 {
public:
	CcopeHook86(void);
	// TODO: add your methods here.
};

extern COPEHOOK86_API int ncopeHook86;

COPEHOOK86_API int fncopeHook86(void);
