using System;
using System.Drawing;
#if true
partial class Program
{
    private const string clProc = "clCode";

    //----------------------------------------------------------------------
    // createOutBytes                  /*Size of ImageObject after change.*/
    static byte[] createOutBytes(int width, int height,
        out int outWidth, out int outHeight, float inDegree)
    {
        float degree = inDegree % 360;

        if (degree > 180.0f)
            degree -= 180.0f;
        if (degree > 90.0f)
            degree = 180.0f - degree;

        float radian = (float)Math.PI * degree / 180.0f;

        outWidth = (int)Math.Ceiling(((float)width * Math.Cos(radian)
            + (float)height * Math.Sin(radian)));
        outHeight = (int)Math.Ceiling(((float)width * Math.Sin(radian)
            + (float)height * Math.Cos(radian)));

        byte[] outBytes = new byte[outWidth * 4 * outHeight];

        return outBytes;
    }
    //----------------------------------------------------------------------
    // effct
    static Bitmap effect(string clName, byte[] rgbMat,
        int width, int height, float param)
    {
        string[] src = { System.IO.File.ReadAllText(clName) };

        int outWidth, outHeight;
        byte[] outRgb = createOutBytes(width, height,
            out outWidth, out outHeight, param);


        IntPtr[] platformId = GetPlatformIDs();                 // get platform id
        IntPtr[] deviceID = GetDeviceIDs(platformId);           // get device id
        IntPtr context = CreateContext(platformId, deviceID);   // create Context
        IntPtr queue = CreateCommandQueue(context, deviceID[0]);// create CommandQueue
        IntPtr prog = CreatePragramWithSource(context, src);    // create program object
        BuildPragram(prog, deviceID);                           // build program
        IntPtr kernel = CreateKernel(prog, clProc);             // create kernel

        // set image format
        cl_image_format format = new cl_image_format(CL_BGRA, CL_UNORM_INT8);
        cl_image_desc idesc = new cl_image_desc(CL_MEM_OBJECT_IMAGE2D,
            (IntPtr)width, (IntPtr)height);

        IntPtr memIn = CreateImage(context, rgbMat,
            CL_MEM_READ_ONLY | CL_MEM_USE_HOST_PTR, ref format, ref idesc);

        cl_image_desc odesc = new cl_image_desc(CL_MEM_OBJECT_IMAGE2D,
            (IntPtr)outWidth, (IntPtr)outHeight);

        IntPtr memOut = CreateImage(context,
            CL_MEM_WRITE_ONLY, ref format, ref odesc);

        IntPtr[] origin = { (IntPtr)0, (IntPtr)0, (IntPtr)0 };  // obtain results
        IntPtr[] region = { (IntPtr)outWidth, (IntPtr)outHeight, (IntPtr)1 };

        SetKernelAeg(kernel, 0, memIn);                      // set kerbel parameters
        SetKernelAeg(kernel, 1, memOut);
        SetKernelAeg(kernel, 2, param);

        IntPtr[] globalSize = { (IntPtr)outWidth, (IntPtr)outHeight };
        EnqueueNDRangeKernel(queue, kernel, 2, globalSize);   // request execute kernel

        EnqueueReadImage(queue, memOut, outRgb, origin, region);

        clReleaseMemObject(memIn);                              // release resources
        clReleaseMemObject(memOut);
        clReleaseKernel(kernel);
        clReleaseProgram(prog);
        clReleaseCommandQueue(queue);
        clReleaseContext(context);

        return byteArray2bmp(outRgb, (int)outWidth, (int)outHeight);
    }



    //----------------------------------------------------------------------
    // Main
    static void Main(string[] args)
    {
        try
        {
            if (args.Length != 4)                                   // check arguments
                throw new Exception("引数に以下の値を指定してください．" +
                    " <入力ファイル> <出力ファイル> <*.cl> <回転角度>");
            Bitmap inBmp = readBmp(args[0]);

            Byte[] rgbMat = bmp2byteArray(inBmp);

            float param = Convert.ToSingle(args[3]);

            using (Bitmap bmp = effect(args[2], rgbMat,
                inBmp.Width, inBmp.Height, param))
            {
                rgbMat = null;
                inBmp.Dispose();

                writeBmp(bmp, args[1]);
            };

        }
        catch (Exception ex)
        {
            Console.WriteLine("{0}", ex.ToString());
        }
    }
}

#endif