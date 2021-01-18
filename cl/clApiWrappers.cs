using System;
using System.Runtime.InteropServices;

//--------------------------------------------------------------------------
// class Program
partial class Program
{
    //----------------------------------------------------------------------
    //GetPlatformIDs
    static IntPtr[] GetPlatformIDs()
    {
        IntPtr[] platformId = new IntPtr[1];

        uint platformCount;
        int status = clGetPlatformIDs(1, platformId, out platformCount);
        if (status != CL_SUCCESS || platformCount != 1)
            throw new Exception("cletPLatformIDs failed.");

        return platformId;
    }

    //----------------------------------------------------------------------
    //GetDeviceIDs
    static IntPtr[] GetDeviceIDs(IntPtr[] platformId, int device_type)
    {
        IntPtr[] deviceID = new IntPtr[1];
        uint deviceCount;
        int status = clGetDeviceIDs(platformId[0],
            device_type, 1, deviceID, out deviceCount);
        if (status != CL_SUCCESS)
            throw new Exception("clGetDeviceIDs failed.");

        return deviceID;
    }
    //GetDeviceIDs, no device type----------
    static IntPtr[] GetDeviceIDs(IntPtr[] platformId)
    {
        return GetDeviceIDs(platformId, CL_DEVICE_TYPE_DEFAULT);
    }

    //----------------------------------------------------------------------
    //CreateContext
    static IntPtr CreateContext(IntPtr[] platformId, IntPtr[] deviceID)
    {
        int errcode_ret;

        IntPtr mContext = clCreateContext(null, 1, deviceID,
            null, IntPtr.Zero, out errcode_ret);
        if (errcode_ret != CL_SUCCESS)
            throw new Exception("clCreateContext failed.");

        return mContext;
    }

    //----------------------------------------------------------------------
    //CreateCommandQueue
    static IntPtr CreateCommandQueue(IntPtr context, IntPtr deviceID)
    {
        int errcoed_ret;

        IntPtr queue = clCreateCommandQueue(context,
            deviceID, 0, out errcoed_ret);
        if (errcoed_ret != CL_SUCCESS)
            throw new Exception("clCreateCommandQueue failed.");

        return queue;
    }

    //----------------------------------------------------------------------
    //CreateProgramWithSource
    static IntPtr CreatePragramWithSource(IntPtr context, string[] src)
    {
        int errcode_ret;

        IntPtr prog = clCreateProgramWithSource(context,
            (uint)src.Length, src, null, out errcode_ret);
        if (errcode_ret != CL_SUCCESS)
            throw new Exception("clCreateProgramWithSource failed.");

        return prog;
    }

    //----------------------------------------------------------------------
    //BuildProgram
    static void BuildPragram(IntPtr prog, IntPtr[] deviceID)
    {
        int status = clBuildProgram(prog, 1, deviceID, null, null, IntPtr.Zero);
        if (status != CL_SUCCESS)
            throw new Exception("clBuildProgram failed.");
    }

    //----------------------------------------------------------------------
    //CreateKernel
    static IntPtr CreateKernel(IntPtr prog, string clName)
    {
        int errcode_ret;
        IntPtr kernel = clCreateKernel(prog, clName, out errcode_ret);
        if (errcode_ret != CL_SUCCESS)
            throw new Exception("clCreateKernel failed.");
        return kernel;
    }

    //----------------------------------------------------------------------
    //CreateBuffer, Array
    public static IntPtr CreateBuffer<T>(IntPtr context, T[] inData,
        uint flags) where T : struct
    {
        int errcode_ret;

        GCHandle handle = GCHandle.Alloc(inData, GCHandleType.Pinned);
        IntPtr hst_ptr = flags == CL_MEM_WRITE_ONRY ?
            IntPtr.Zero : handle.AddrOfPinnedObject();
        IntPtr memObj = clCreateBuffer(context, flags,
            (IntPtr)(Marshal.SizeOf(typeof(T)) * inData.Length),
            hst_ptr, out errcode_ret);
        handle.Free();

        if (errcode_ret != CL_SUCCESS)
            throw new Exception("clCreateBuffer failed.");

        return memObj;
    }
    //CreatBuffer, allocate memory----------
    public static IntPtr CreateBuffer(IntPtr context, int size, uint flags)
    {
        int errcode_ret;

        IntPtr memObj = clCreateBuffer(context, flags, (IntPtr)size,
            IntPtr.Zero, out errcode_ret);
        if (errcode_ret != CL_SUCCESS)
            throw new Exception("clCrateBuffer failed.");

        return memObj;
    }
    //dim2---------
    public static IntPtr CreateBuffer<T>(IntPtr context, T[,] inData,
        uint flags) where T : struct
    {
        int errcode_ret;

        GCHandle handle = GCHandle.Alloc(inData, GCHandleType.Pinned);
        IntPtr hsr_ptr = flags == CL_MEM_WRITE_ONRY ?
            IntPtr.Zero : handle.AddrOfPinnedObject();
        IntPtr memObj = clCreateBuffer(context, flags,
            (IntPtr)(Marshal.SizeOf(typeof(T)) * inData.Length),
            hsr_ptr, out errcode_ret);
        handle.Free();

        if (errcode_ret != CL_SUCCESS)
            throw new Exception("clCreateBuffer failed.");

        return memObj;
    }

    //----------------------------------------------------------------------
    //SetKernelArg
    static void SetKernelArg(IntPtr kernel, int argIndex, IntPtr mem)
    {
        int status = clSetKernelArg(kernel, (uint)argIndex,
            (IntPtr)(Marshal.SizeOf(typeof(IntPtr))), ref mem);

        if (status != CL_SUCCESS)
            throw new Exception("clSetKernelArg failed.");
    }
    static void SetKernelAeg<T>(IntPtr kernel, int argIndex, T value) where T : struct
    {
        GCHandle handle = GCHandle.Alloc(value, GCHandleType.Pinned);
        int status = clSetKernelArg(kernel, (uint)argIndex,
            (IntPtr)(Marshal.SizeOf(typeof(T))), handle.AddrOfPinnedObject());
        if (status != CL_SUCCESS)
            throw new Exception("clSetKernelArg failed.");
        handle.Free();
    }

    //----------------------------------------------------------------------
    //EnqueueTask
    public static void EnqueueTask(IntPtr queue, IntPtr kernel)
    {
        int status = clEnqueueTask(queue, kernel, 0, null, IntPtr.Zero);
        if (status != CL_SUCCESS)
            throw new Exception("clEnqueueTask failed.");
    }

    //----------------------------------------------------------------------
    //EnqueueNDRangeKernel
    public static void EnqueueNDRangeKernel(IntPtr queue, IntPtr kernel,
        uint work_dim, IntPtr[] globalSize)
    {
        int status = clEnqueueNDRangeKernel(queue, kernel, work_dim, null,
            globalSize, null, 0, null, IntPtr.Zero);
        if (status != CL_SUCCESS)
            throw new Exception("clEnqueueNDRangeKernel failed.");
    }

    //----------------------------------------------------------------------
    //EnqueueReadBuffer, Array
    public static void EnqueueReadBuffer<T>(IntPtr queue, IntPtr mem,
        T[] outData) where T : struct
    {
        GCHandle handle = GCHandle.Alloc(outData, GCHandleType.Pinned);
        int status = clEnqueueReadBuffer(queue, mem, CL_TRUE, (IntPtr)0,
            (IntPtr)(Marshal.SizeOf(typeof(T)) * outData.Length),
            handle.AddrOfPinnedObject(), 0, null, IntPtr.Zero);
        handle.Free();
        if (status != CL_SUCCESS)
            throw new Exception("clEnqueueReadBuffer failed.");
    }
    // dim2--------
    public static void EnqueueReadBuffer<T>(IntPtr queue, IntPtr mem,
        T[,] outData) where T : struct
    {
        GCHandle handle = GCHandle.Alloc(outData, GCHandleType.Pinned);
        int status = clEnqueueReadBuffer(queue, mem, CL_TRUE, (IntPtr)0,
            (IntPtr)(Marshal.SizeOf(typeof(T)) * outData.Length),
            handle.AddrOfPinnedObject(), 0, null, IntPtr.Zero);
        handle.Free();
        if (status != CL_SUCCESS)
            throw new Exception("clEnqueueReadBuffer failed.");
    }


    /********* image object ***********************************************/
    //----------------------------------------------------------------------
    //CreateImage
    private static IntPtr CreateImage<T>(IntPtr context, T[] inData,
        uint flags, ref cl_image_format image_format,
        ref cl_image_desc image_desc) where T : struct
    {
        int errcode_ret;

        GCHandle handle = GCHandle.Alloc(inData, GCHandleType.Pinned);
        IntPtr hst_ptr = flags == CL_MEM_WRITE_ONRY ?
            IntPtr.Zero : handle.AddrOfPinnedObject();
        IntPtr memObj = clCreateImage(context, flags, ref image_format,
            ref image_desc, hst_ptr, out errcode_ret);
        handle.Free();

        if (errcode_ret != CL_SUCCESS)
            throw new Exception("clCreateImage failed");

        return memObj;
    }
    private static IntPtr CreateImage(IntPtr context,
        uint flags, ref cl_image_format image_format,
        ref cl_image_desc image_desc)
    {
        int errcode_ret;

        IntPtr memObj = clCreateImage(context, flags, ref image_format,
            ref image_desc, IntPtr.Zero, out errcode_ret);
        if (errcode_ret != CL_SUCCESS)
            throw new Exception("clCreateImage failed.");

        return memObj;
    }

    //----------------------------------------------------------------------
    //EnqueueReadImage
    public static void EnqueueReadImage<T>(IntPtr queue, IntPtr mem,
        T[] outData, IntPtr[] origin, IntPtr[] region) where T : struct
    {
        GCHandle handle = GCHandle.Alloc(outData, GCHandleType.Pinned);
        int status = clEnqueueReadImage(queue, mem, CL_TRUE,
            origin, region, IntPtr.Zero, IntPtr.Zero,
            handle.AddrOfPinnedObject(), 0, null, IntPtr.Zero);
        handle.Free();
        if (status != CL_SUCCESS)
            throw new Exception("clEnqueueReadImage failed.");
    }

    //----------------------------------------------------------------------
    //EnqueueFillImage
    private static void EnqueueFillImage(IntPtr queue,IntPtr mem,
        float[] fill_color,IntPtr[] origin,IntPtr[] region)
    {
        GCHandle handle = GCHandle.Alloc(fill_color, GCHandleType.Pinned);
        int status = clEnqueueFillImage(queue, mem, handle.AddrOfPinnedObject(),
            origin, region, 0, null, IntPtr.Zero);
        handle.Free();
        if (status != CL_SUCCESS)
            throw new Exception("clEnqueueFillImage failed.");
    }
}
