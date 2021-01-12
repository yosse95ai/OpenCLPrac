using System;
using System.Text;
using System.Runtime.InteropServices;

//-----------------------------------------------------------------------
// class Program
class Program
{

    //-------------------------------------------------------------------
    // struct

    //-------------------------------------------------------------------
    // Dll
    [DllImport("dll.dll")]
    private static extern void dllByteArray(byte[] buffer, int size);

    [DllImport("dll.dll")]
    private static extern void dllFloatArray(float[] buffer, int size);
    static void Main(string[] args)
    {
        // byte[]/ unsigned char[] ...................
        byte[] byteArray = new byte[10];
        for (int i = 0; i < byteArray.Length; i++)
            byteArray[i] = (byte)i;

        Console.WriteLine("=====< byte[]/ unsigned char[]>=====");
        dllByteArray(byteArray, byteArray.Length);

        Console.WriteLine("----------");

        for (int i = 0; i < byteArray.Length; i++)
            Console.WriteLine("byteAeeay[{0}] = {1}", i, byteArray[i]);

        // float[] ...................................
        float[] floatArray = new float[10];
        for (int i = 0; i < floatArray.Length; i++)
            floatArray[i] = (float) i + 0.12f;

        Console.WriteLine("=====< float[] >====================");
        dllFloatArray(floatArray, floatArray.Length);

        Console.WriteLine("----------");

        for (int i = 0; i < floatArray.Length; i++)
            Console.WriteLine("dllFloatArray[{0}] = {1,2:F2}", i, floatArray[i]);



    }
}
