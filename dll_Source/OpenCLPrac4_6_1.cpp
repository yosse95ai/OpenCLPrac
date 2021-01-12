#include <windows.h>
#include <stdio.h>

//-----------------------------------------------------------------------
// struct

//-----------------------------------------------------------------------
// DLLエクスポート関数プロトタイプ宣言
#ifdef __cplusplus
extern "C"
{
#endif // __cplusplus 
	void __declspec(dllexport) __stdcall dllByteArray(unsigned char buffer[],
		int size);
	void __declspec(dllexport) __stdcall dllFloatArray(float buffer[], int size);

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
// get/ret unsigned char[]
void __declspec(dllexport) __stdcall dllByteArray(unsigned char buffer[],
	int size)
{
	for (int i = 0; i < size; i++)
		printf("buffer[%d] = %d\n", i, (int)buffer[i]);

	for (int i = 0; i < size; i++)
		buffer[i] += (unsigned char)1;
}

//-----------------------------------------------------------------------
// get/ret float[]
void __declspec(dllexport) __stdcall dllFloatArray(float buffer[], int size)
{
	for (int i = 0; i < size; i++)
		printf("buffer[%d] = %f\n", i, buffer[i]);

	for (int i = 0; i < size; i++)
		buffer[i] += 1.1f;
}