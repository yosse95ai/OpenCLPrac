using System;
using System.Text;
using System.Runtime.InteropServices;

//-----------------------------------------------------------------------
// class Program
class Program
{

    //-------------------------------------------------------------------
    // struct
    [StructLayout(LayoutKind.Sequential)]
    private struct structSample
    {
        public int width;
        public int height;
        public int pixels;
        public structSample(int init)
        {
            width = init;
            height = init;
            pixels = init;
        }
    }

    //-------------------------------------------------------------------
    // Dll
    [DllImport("dll.dll")]
    private static extern void dllStruct(ref structSample format);
    static void Main(string[] args)
    {
        structSample format = new structSample(5);

        dllStruct(ref format);

        Console.WriteLine("format.width  = {0}", format.width);
        Console.WriteLine("format.height = {0}", format.height);
        Console.WriteLine("format.pixels = {0}", format.pixels);

    }
}
