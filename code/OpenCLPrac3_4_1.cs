using System;
using System.Runtime.InteropServices;

partial class Program
{
    private static string clProc = "clProc";


    //----------------------------------------------------------------------
    // clMain

    static void clMain(string[] src)
    {
        int status;

        // initialize array
        float B =2.2f;
        float[] A = new float[100], C = new float[100];
        for (int i = 0; i < 100; i++)
            A[i] = (float)(i + 100);

        // get platform id
        IntPtr[] platformId = new IntPtr[1];
        uint platformCount;
        status = clGetPlatformIDs(1, platformId, out platformCount);
        if (status != CL_SUCCESS)
            throw new Exception("clGetPlatformIDs failed.");

        // get device id
        IntPtr[] deviceID = new IntPtr[1];
        uint deviceCount;
        status = clGetDeviceIDs(platformId[0],
            CL_DEVICE_TYPE_DEFAULT, 1, deviceID, out deviceCount);
        if (status != CL_SUCCESS)
            throw new Exception("clGetDeviceIDs failed.");

        // create Context
        int errcode_ret;
        IntPtr context = clCreateContext(null, 1, deviceID,
            null, IntPtr.Zero, out errcode_ret);
        if (errcode_ret != CL_SUCCESS)
            throw new Exception("clCreateContext failed.");

        // create Command Queue
        IntPtr queue = clCreateCommandQueue(context,
            deviceID[0], 0, out errcode_ret);
        if (errcode_ret != CL_SUCCESS)
            throw new Exception("clCreateCommandQueue failed.");

        // create program object
        IntPtr prog = clCreateProgramWithSource(context,
            (uint)(src.Length), src, null, out errcode_ret);
        if (errcode_ret != CL_SUCCESS)
            throw new Exception("clCreateProgramWithSource failed.");

        // build program
        status = clBuildProgram(prog, 1, deviceID, null, null, IntPtr.Zero);
        if (status != CL_SUCCESS)
            throw new Exception("clBuildProgram failed.");

        //create kernel
        IntPtr kernel = clCreateKernel(prog, clProc, out errcode_ret);
        if (errcode_ret != CL_SUCCESS)
            throw new Exception("clCreateKernel failed.");

        // create memory object
        GCHandle handle;
        handle = GCHandle.Alloc(A, GCHandleType.Pinned);
        IntPtr memA = clCreateBuffer(context, CL_MEM_READ_ONRY | CL_MEM_COPY_HOST_PTR,
            (IntPtr)(Marshal.SizeOf(A[0]) * A.Length), handle.AddrOfPinnedObject(),
            out errcode_ret);
        handle.Free();
        if (errcode_ret != CL_SUCCESS)
            throw new Exception("clCreateBuffer A failed.");

        IntPtr memC = clCreateBuffer(context, CL_MEM_WRITE_ONRY,
            (IntPtr)(Marshal.SizeOf(C[0]) * C.Length), IntPtr.Zero, out errcode_ret);
        if (errcode_ret != CL_SUCCESS)
            throw new Exception("clCreateBuffer C failed.");



        // set kernel parameter
        status = clSetKernelArg(kernel, 0, (IntPtr)(Marshal.SizeOf(typeof(IntPtr))), ref memA);
        if (status != CL_SUCCESS)
            throw new Exception("clSetKernelArg A failed.");

        handle = GCHandle.Alloc(B, GCHandleType.Pinned);
        status = clSetKernelArg(kernel, 1, (IntPtr)(Marshal.SizeOf(B)), handle.AddrOfPinnedObject());
        handle.Free();
        if (status != CL_SUCCESS)
            throw new Exception("clSetKernelArg B failed.");
        status = clSetKernelArg(kernel, 2, (IntPtr)(Marshal.SizeOf(typeof(IntPtr))), ref memC);
        if (status != CL_SUCCESS)
            throw new Exception("clSetKernelArg C failed.");


        // request execute kernel
        IntPtr[] globalSize = { (IntPtr)C.Length };
        status = clEnqueueNDRangeKernel(queue, kernel, 1, null,
            globalSize, null, 0, null, IntPtr.Zero);
        if (status != CL_SUCCESS)
            throw new Exception("clEnqueueTask failed.");


        // obtain results
        handle = GCHandle.Alloc(C, GCHandleType.Pinned);
        status = clEnqueueReadBuffer(queue, memC, CL_TRUE, (IntPtr)0,
            (IntPtr)(Marshal.SizeOf(typeof(float)) * C.Length), handle.AddrOfPinnedObject(),
            0, null, IntPtr.Zero);

        handle.Free();
        if (status != CL_SUCCESS)
            throw new Exception("clEnqueueReadBuffer failed.");

        // list results
        Console.WriteLine("(A <?> B = C)\n");
        for (int i = 0; i < 10; i++)
            Console.WriteLine("{0:F4} <?> {1:F2} = {2:F2}", A[i], B, C[i]);


        //release resources
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
            // 引数チェック
            if (args.Length != 1)
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
