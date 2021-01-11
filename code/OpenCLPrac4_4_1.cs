using System;
using System.Text;
using System.Runtime.InteropServices;

//-----------------------------------------------------------------------
// class Program
class Program
{
    //-------------------------------------------------------------------
    // DllImport属性
    [DllImport("dll.dll", CharSet = CharSet.Ansi)]
    private static extern void dllString(String inString);

    [DllImport("dll.dll", CharSet = CharSet.Ansi)]
    private static extern void dllRetString(StringBuilder inStringBuilder);

    static void Main(string[] args)
    {
        String inString = "C#へ文字列を渡し例です.";
        dllString(inString);

        StringBuilder inStringBuilder = new StringBuilder(255);
        dllRetString(inStringBuilder);
        Console.WriteLine(inStringBuilder);
    }
}
