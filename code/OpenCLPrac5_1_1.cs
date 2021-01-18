using System;
using System.Runtime.InteropServices;

#if true
partial class Program
{
    private static string clProc = "clProc";


    //----------------------------------------------------------------------
    // clMain

    static void clMain(string[] src)
    {
        int status;

        // initialize array
        float[] A = new float[100], B = new float[100], C = new float[100];
        for (int i = 0; i < 100; i++)
        {
            A[i] = (float)(i + 100);
            B[i] = (float)(i + 100) + ((float)i / 10.0f);
        }

        IntPtr[] platformId = GetPlatformIDs();                     // get platform id
        IntPtr[] deviceID = GetDeviceIDs(platformId);               // get device id
        IntPtr context = CreateContext(platformId, deviceID);       // create Context
        IntPtr queue = CreateCommandQueue(context, deviceID[0]);    // create Command Queue
        IntPtr prog = CreatePragramWithSource(context, src);        // create program object
        BuildPragram(prog, deviceID);                               // build program
        IntPtr kernel = CreateKernel(prog, clProc);                 // create kernel
                                                                    // create memory object
        IntPtr memA = CreateBuffer(context, A, CL_MEM_READ_ONRY | CL_MEM_COPY_HOST_PTR);
        IntPtr memB = CreateBuffer(context, B, CL_MEM_READ_ONRY | CL_MEM_COPY_HOST_PTR);
        IntPtr memC = CreateBuffer(context, Marshal.SizeOf(C[0])*C.Length,CL_MEM_WRITE_ONRY);

        SetKernelAeg(kernel, 0, memA);                              // set kernel parameters
        SetKernelAeg(kernel, 1, memB);
        SetKernelAeg(kernel, 2, memC);

        IntPtr[] globalSize = { (IntPtr)C.Length };                 // request execute kernel
        status = clEnqueueNDRangeKernel(queue, kernel, 1, null,
            globalSize, null, 0, null, IntPtr.Zero);

        EnqueueReadBuffer(queue, memC, C);                          // obtain results

        Console.WriteLine("(A <?> B = C)\n");                       // list results
        for (int i = 0; i < 10; i++)
            Console.WriteLine("{0:F2} <?> {1:F2} = {2:F2}", A[i], B[i], C[i]);

        clReleaseMemObject(memC);
        clReleaseMemObject(memB);
        clReleaseMemObject(memA);
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
            if (args.Length != 1)                                   // check arguments
                throw new Exception("引数に *.clを指定してください.");
            string[] src = { System.IO.File.ReadAllText(args[0].ToString()) };

            clMain(src);
        }
        catch(Exception ex)
        {
            Console.WriteLine("{0}{1}", ex.ToString(), ex.StackTrace.ToString());
        }
    }
}

#endif