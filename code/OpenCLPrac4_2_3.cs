using System;
using System.Runtime.InteropServices;

//-----------------------------------------------------------------------
// class Program
class Program
{
    //-------------------------------------------------------------------
    // DllImport属性
    [DllImport("dll.dll")]
    private static extern void dllfloat(float inFloat);

    [DllImport("dll.dll")]
    private static extern void dlldouble(double inDouble);

    static void Main(string[] args)
    {
        float inFloat = 3.14159265358979323846f;    //float
        dllfloat(inFloat);

        Single inSingle = 3.14159265358979323846f;
        dllfloat(inSingle);

        double inDouble = 3.14159265358979323846d;  //double
        dlldouble(inDouble);
    }
}
