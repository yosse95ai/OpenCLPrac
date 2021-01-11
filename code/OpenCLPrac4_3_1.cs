using System;
using System.Runtime.InteropServices;

//-----------------------------------------------------------------------
// class Program
class Program
{
    //-------------------------------------------------------------------
    // DllImport属性
    [DllImport("dll.dll")]
    private static extern void dllint(ref int inInt);

    [DllImport("dll.dll")]
    private static extern void dllbyte(ref byte inDouble);

    static void Main(string[] args)
    {
        // byte
        byte byteData = 65;

        Console.WriteLine("before dll call, data = {0}", byteData);
        dllbyte(ref byteData);
        Console.WriteLine("after dll call, data = {0}", byteData);

        Console.WriteLine("---------------");

        // int
        int intData = 1000;

        Console.WriteLine("before dll call, data = {0}", intData);
        dllint(ref intData);
        Console.WriteLine("after dll call, data = {0}", intData);
    }
}
