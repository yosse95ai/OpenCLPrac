#include <windows.h>
#include <stdio.h>

//-----------------------------------------------------------------------
// DLLエクスポート関数プロトタイプ宣言
#ifdef __cplusplus
extern "C"
{
#endif // __cplusplus 
	void __declspec(dllexport) __stdcall dllint(int inInt);
	void __declspec(dllexport) __stdcall dlluint(unsigned int inUint);
	void __declspec(dllexport) __stdcall dllshort(short inShort);
	// void __declspec(dllexport) __stdcall dlllong(long inLong);	//x86
	void __declspec(dllexport) __stdcall dlllong(long long inLong); //x64


#ifdef __cplusplus
}
#endif // __cplusplus

//-----------------------------------------------------------------------
// Entry Point
BOOL WINAPI DllMain(HINSTANCE hDll, DWORD dwReason, LPVOID lpReserved)
{
	return TRUE;
}

//-----------------------------------------------------------------------
// int
void __declspec(dllexport) __stdcall dllint(int inInt)
{
	printf("inInt=%d\n", inInt);
}

//-----------------------------------------------------------------------
// unsigned int
void __declspec(dllexport) __stdcall dlluint(unsigned int inUint)
{
	printf("inUint=%u\n", (int)inUint);
}

//-----------------------------------------------------------------------
// short
void __declspec(dllexport) __stdcall dllshort(short inShort)
{
	printf("inShort=%d\n", (int)inShort);
}

//-----------------------------------------------------------------------
// long long
void __declspec(dllexport) __stdcall dlllong(long long inLong)
{
	printf("inLong=%d\n", (int)inLong);
}