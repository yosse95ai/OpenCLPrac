#include <windows.h>
#include <stdio.h>

//-----------------------------------------------------------------------
// DLLエクスポート関数プロトタイプ宣言
#ifdef __cplusplus
extern "C"
{
#endif // __cplusplus 
	void __declspec(dllexport) __stdcall dllbyte(unsigned char inByte);
	void __declspec(dllexport) __stdcall dllchar(char inDhar);
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
// unsigned char / byte
void __declspec(dllexport) __stdcall dllbyte(unsigned char inByte)
{
	printf("inByte=%C, %dd\n", inByte, (int)inByte);
}

//-----------------------------------------------------------------------
// Char受け取り
void __declspec(dllexport) __stdcall dllchar(char inChar)
{
	printf("inChar=%C\n", inChar);
}