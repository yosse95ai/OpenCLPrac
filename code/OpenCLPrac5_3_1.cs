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
        float B = 2.2f;
        float[,] A = new float[10, 5], C = new float[10, 5];

        Console.WriteLine("A.GetLength(0) = {0},A.GetLength(1) = {1}",
            A.GetLength(0), A.GetLength(1));
        Console.WriteLine("A.Length = {0}", A.Length);

        for (int y = 0; y < A.GetLength(0); y++)
            for (int x = 0; x < A.GetLength(1); x++)
            {
                A[y, x] = (float)(y * 1000 + x);
                C[y, x] = (float)(y * 10 + x);
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
        IntPtr memC = CreateBuffer(context, C, CL_MEM_WRITE_ONRY);

        SetKernelAeg(kernel, 0, memA);                              // set kernel parameters
        SetKernelAeg(kernel, 1, B);
        SetKernelAeg(kernel, 2, memC);
        // request execute kernel
        IntPtr[] globalSize = { (IntPtr)C.GetLength(0), (IntPtr)C.GetLength(1) };
        status = clEnqueueNDRangeKernel(queue, kernel, 2, null,
            globalSize, null, 0, null, IntPtr.Zero);

        EnqueueReadBuffer(queue, memC, C);                          // obtain results

        Console.WriteLine("(A <?> B = C)\n");                       // list results
        for (int y = 0; y < A.GetLength(0); y++)
            for (int x = 0; x < A.GetLength(1); x++)
                Console.WriteLine("{0,8:F2} <?> {1,8:F2} = {2,8:F2}"
                    , A[y, x], B, C[y, x]);

        clReleaseMemObject(memC);
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
        catch (Exception ex)
        {
            Console.WriteLine("{0}{1}", ex.ToString(), ex.StackTrace.ToString());
        }
    }
}

#endif