using System;
using System.Runtime.InteropServices;

//-----------------------------------------------------------------------
// class Program
class Program
{
    //-------------------------------------------------------------------
    // DllImport属性
    [DllImport("dll.dll")]
    private static extern void dllbyte(byte inByte);

    [DllImport("dll.dll")]
    private static extern void dllchar(char inChar);

    static void Main(string[] args)
    {
        byte data = 0x41;           // A, 65d
        dllbyte(data);

        Char inChar = 'B';
        dllchar(inChar);
    }
}
