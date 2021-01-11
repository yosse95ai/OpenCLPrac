#include <windows.h>
#include <stdio.h>

//-----------------------------------------------------------------------
// DLLエクスポート関数プロトタイプ宣言
#ifdef __cplusplus
extern "C"
{
#endif // __cplusplus 
	void __declspec(dllexport) __stdcall dllfloat(float inFloat);
	void __declspec(dllexport) __stdcall dlldouble(double inDouble);


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
// float
void __declspec(dllexport) __stdcall dllfloat(float inFloat)
{
	printf("inFloat=%.12lf\n", inFloat);
}

//-----------------------------------------------------------------------
// double
void __declspec(dllexport) __stdcall dlldouble(double inDouble)
{
	printf("inDouble=%.12lf\n", inDouble);
}