#include <windows.h>
#include <stdio.h>

//-----------------------------------------------------------------------
// DLLエクスポート関数プロトタイプ宣言
#ifdef __cplusplus
extern "C"
{
#endif // __cplusplus 
	void __declspec(dllexport) __stdcall dllbyte(unsigned char* inByte);
	void __declspec(dllexport) __stdcall dllint(int* inInt);


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
// unsigned char*
void __declspec(dllexport) __stdcall dllbyte(unsigned char* inByte)
{
	printf("inByte=%d\n", (int)(*inByte));
	(*inByte) += 5;
}

//-----------------------------------------------------------------------
// int*
void __declspec(dllexport) __stdcall dllint(int* inInt)
{
	printf("inInt=%d\n", *inInt);
	(*inInt) += 24;
}