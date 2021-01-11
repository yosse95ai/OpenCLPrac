using System;
using System.Runtime.InteropServices;

//-----------------------------------------------------------------------
// class Program
class Program
{
    //-------------------------------------------------------------------
    // DllImport属性
    [DllImport("dll.dll")]
    private static extern void dllint(int inInt);

    [DllImport("dll.dll")]
    private static extern void dlluint(uint inUint);

    [DllImport("dll.dll")]
    private static extern void dllshort(short inShort);

    [DllImport("dll.dll")]
    private static extern void dlllong(long inLong);

    static void Main(string[] args)
    {
        int inInt = -3;             //int
        dllint(inInt);

        uint inUint = 32767;        //uint
        dlluint(inUint);

        short inShort = -444;       //short
        dllshort(inShort);

        Int16 inInt16 = 333;
        dllshort(inInt16);

        long inLong = -32767;       //long
        dlllong(inLong);
    }
}
