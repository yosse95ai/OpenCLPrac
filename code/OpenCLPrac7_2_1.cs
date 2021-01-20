using System;
using System.Drawing;

#if true
partial class Program
{
    private const string clPloc = "clCode";

    //----------------------------------------------------------------------
    // effct
    static void effect(string clName,byte[] rgbMat,int width,int height)
    {
        string[] src = { System.IO.File.ReadAllText(clName) };

        IntPtr[] platformId = GetPlatformIDs();                 // get platform id
        IntPtr[] deviceID = GetDeviceIDs(platformId);           // get device id
        IntPtr context = CreateContext(platformId, deviceID);   // create Context
        IntPtr queue = CreateCommandQueue(context, deviceID[0]);// create CommandQueue
        IntPtr prog = CreatePragramWithSource(context, src);    // create program object
        BuildPragram(prog, deviceID);                           // build program
        IntPtr kernel = CreateKernel(prog, clPloc);             // create kernel

        IntPtr memIn = CreateBuffer(context, rgbMat, CL_MEM_READ_ONRY | CL_MEM_USE_HOST_PTR);
        IntPtr memOut = CreateBuffer(context, rgbMat.Length, CL_MEM_WRITE_ONRY);

        SetKernelAeg(kernel, 0, memIn);                         // set kerbel parameters
        SetKernelAeg(kernel, 1, memOut);
        SetKernelAeg(kernel, 2, width);
        SetKernelAeg(kernel, 3, height);

        IntPtr[] globalSize = { (IntPtr)width, (IntPtr)height };
        EnqueueNDRangeKernel(queue, kernel, 2, globalSize);     // request execute kernel

        EnqueueReadBuffer(queue, memOut, rgbMat);               // obtain results

        clReleaseMemObject(memOut);
        clReleaseMemObject(memIn);
        clReleaseKernel(kernel);
        clReleaseProgram(prog);
        clReleaseCommandQueue(queue);
        clReleaseContext(context);

    }
    


    //----------------------------------------------------------------------
    // Main
    static void Main(string[] args)
    {
        try
        {
            if (args.Length != 3)                                   // check arguments
                throw new Exception("引数にi以下の値を指定してください．" +
                    " <入力ファイル> <出力ファイル> <*.cl>");
            Bitmap inBmp = readBmp(args[0]);

            Byte[] rgbMat = bmp2byteArray(inBmp);

            effect(args[2], rgbMat, inBmp.Width, inBmp.Height);
            byteArray2bmp(inBmp, rgbMat);

            rgbMat = null;
            writeBmp(inBmp, args[1]);
        }
        catch (Exception ex)
        {
            Console.WriteLine("{0}", ex.ToString());
        }
    }
}

#endif