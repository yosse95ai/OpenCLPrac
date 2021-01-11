#include <windows.h>
#include <stdio.h>

//-----------------------------------------------------------------------
// struct
typedef struct _structSample
{
	int		width;
	int		height;
	int		pixels;
}
structSample;

typedef structSample* pStructSample;

//-----------------------------------------------------------------------
// DLLエクスポート関数プロトタイプ宣言
#ifdef __cplusplus
extern "C"
{
#endif // __cplusplus 
	void __declspec(dllexport) __stdcall dllStruct(pStructSample format);

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
// get/ret struct
void __declspec(dllexport) __stdcall dllStruct(pStructSample format)
{
	fprintf(stdout, "img->width = %d\n", format->width);
	fprintf(stdout, "img->height = %d\n", format->height);
	fprintf(stdout, "img->pixels = %d\n", format->pixels);

	format->width = 320;
	format->height = 240;
	format->pixels = 32;
}