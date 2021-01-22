using System;
using System.Drawing;
#if true
partial class Program
{
    private const string clPloc = "clCode";

    //----------------------------------------------------------------------
    // effct
    static void effect(string clName, byte[] rgbMat, int width, int height)
    {
        string[] src = { System.IO.File.ReadAllText(clName) };

        IntPtr[] platformId = GetPlatformIDs();                 // get platform id
        IntPtr[] deviceID = GetDeviceIDs(platformId);           // get device id
        IntPtr context = CreateContext(platformId, deviceID);   // create Context
        IntPtr queue = CreateCommandQueue(context, deviceID[0]);// create CommandQueue
        IntPtr prog = CreatePragramWithSource(context, src);    // create program object
        BuildPragram(prog, deviceID);                           // build program
        IntPtr kernel = CreateKernel(prog, clPloc);             // create kernel

        // set image format
        cl_image_format format = new cl_image_format(CL_BGRA, CL_UNORM_INT8);
        cl_image_desc idesc = new cl_image_desc(CL_MEM_OBJECT_IMAGE2D,
            (IntPtr)width, (IntPtr)height);

        IntPtr memIn = CreateImage(context, rgbMat,
            CL_MEM_READ_ONRY | CL_MEM_USE_HOST_PTR, ref format, ref idesc);
        IntPtr memOut = CreateImage(context,
             CL_MEM_WRITE_ONRY, ref format, ref idesc);

        IntPtr[] origin = { (IntPtr)0, (IntPtr)0, (IntPtr)0 };  // obtain results
        IntPtr[] region = { (IntPtr)width, (IntPtr)height, (IntPtr)1 };

        float[] fill_color = { 0.0f, 0.0f, 0.0f, 255.0f };
        EnqueueFillImage(queue, memOut, fill_color, origin, region);

        SetKernelAeg(kernel, 0, memIn);                         // set kerbel parameters
        SetKernelAeg(kernel, 1, memOut);

        IntPtr[] globalSize = { (IntPtr)(width - 2), (IntPtr)(height - 2) };
        EnqueueNDRangeKernel(queue, kernel, 2, globalSize);     // request execute kernel

        EnqueueReadImage(queue, memOut, rgbMat, origin, region);

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