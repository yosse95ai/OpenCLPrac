#include <windows.h>
#include <stdio.h>

//-----------------------------------------------------------------------
// DLLエクスポート関数プロトタイプ宣言
#ifdef __cplusplus
extern "C"
{
#endif // __cplusplus 
	void __declspec(dllexport) __stdcall dllString(const char* constString);
	void __declspec(dllexport) __stdcall dllRetString(char* string);


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
// const char*
void __declspec(dllexport) __stdcall dllString(const char* constString)
{
	printf("受け取った文字列=[%s]\n", constString);
}

//-----------------------------------------------------------------------
// return String
void __declspec(dllexport) __stdcall dllRetString(char* string)
{
	strcpy_s(string, sizeof("DLLからC#へ文字列を返す."), "DLLからC#へ文字列を返す.");
}