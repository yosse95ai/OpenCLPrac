using System;
using System.Text;
using System.Runtime.InteropServices;

class Program
{
    //----------------------------------------------------------------------
    // constants
    // cl_device_type - bitfield
    private const int CL_DEVICE_TYPE_DEFAULT = (1 << 0);

    // cl_bool
    private const int CL_FALSE = 0;
    private const int CL_TRUE = 1;
    private const int CL_BLOCKING = CL_TRUE;
    private const int CL_NON_BLOCKING = CL_FALSE;

    // cl_mem_flags - bitfield
    private const int CL_MEM_READ_WRITE = (1 << 0);
    private const int CL_MEM_WRITE_ONRY = (1 << 1);
    private const int CL_MEM_READ_ONRY = (1 << 2);
    private const int CL_MEM_USE_HOST_PTR = (1 << 3);
    private const int CL_MEM_ALLOC_HOST_PTR = (1 << 4);
    private const int CL_MEM_COPY_HOST_PTR = (1 << 5);

    // Error Codes
    private const int CL_SUCCESS = 0;

    //----------------------------------------------------------------------
    // CALLBACKs
    //----------------------------------------------------------------------
    //----------------------------------------------------------------------
    public delegate void CL_CALLBACK_created_context(
        string errorInfo,
        IntPtr privateInfoSize,
        int cb,
        IntPtr useData);

    //----------------------------------------------------------------------
    public delegate void CL_CALLBACK_ProgramBuilt(
        IntPtr program,
        IntPtr userData);

    //----------------------------------------------------------------------
    // DLLs
    //----------------------------------------------------------------------
    private const String clPath = @"OpenCL.dll";

    //----------------------------------------------------------------------
    //cl_int clGetPlatformIDs ( cl_uint um_entries,
    //                          cl_platformid *platforms,
    //                          cl_uint *num_platforms )
    [DllImport(clPath)]
    public static extern int clGetPlatformIDs(
        uint num_entries,
        IntPtr[] platforms,
        out uint num_platforms
        );


    //----------------------------------------------------------------------
    //cl_int clGetDeviceIDs(cl_platform_id platform,
    //                      cl_device_type device_type,
    //                      cl_uint num_entries,
    //                      cl_device_id *devices,
    //                      cl_uint *num_devices )
    [DllImport(clPath)]
    public static extern int clGetDeviceIDs(
        IntPtr platform,
        int device_type,
        uint num_entries,
        IntPtr[] devices,
        out uint num_devices
        );

    //----------------------------------------------------------------------
    //cl_context clCreateContext (  const cl_context_properties *properties,
    //                              cl_uint num_devices,
    //                              const cl_device_id *devices,
    //                              void (CL_CALLBACK *pfn_notify)(
    //                                          const char *errinfo,
    //                                          const void *private_info,
    //                                          size_t cb,
    //                                          coid *user_data),
    //                              void *user_data,
    //                              cl_int *errcode_ret )
    [DllImport(clPath)]
    public static extern IntPtr clCreateContext(
        IntPtr[] properties,
        uint num_devices,
        IntPtr[] devices,
        CL_CALLBACK_created_context pfn_notify,
        IntPtr user_data,
        out int errcode_ret
        );

    //----------------------------------------------------------------------
    //cl_command_queue clCreateCommandQueue(cl_context context,
    //                                      cl_device_id device,
    //                                      cl_command_queue_properties properties,
    //                                      cl_int *errcode_ret )
    [DllImport(clPath)]
    public static extern IntPtr clCreateCommandQueue(
        IntPtr context,
        IntPtr device,
        long properties,
        out int errcode_ret
        );

    //----------------------------------------------------------------------
    //cl_program clCreateProgramWithSource (cl_context context,
    //                                      cl_uint count,
    //                                      const char **strings,
    //                                      const size_t *lengths,
    //                                      cl_int *errcode_ret )
    [DllImport(clPath)]
    public static extern IntPtr clCreateProgramWithSource(
        IntPtr context,
        uint count,
        string[] strings,
        IntPtr[] lengths,
        out int errcode_ret
        );

    //----------------------------------------------------------------------
    //cl_int clBuildProgram (   cl_program program,
    //                          cl_uint num_devices,
    //                          const cl_device_id *device_list,
    //                          const char *options,
    //                          void (CL_CALLBACK *pfn_notify)(
    //                                      cl_program program,
    //                                      void *user_data
    //                                ),
    //                          void *user_data )
    [DllImport(clPath)]
    public static extern int clBuildProgram(
        IntPtr program,
        uint num_devices,
        IntPtr[] device_list,
        string options,
        CL_CALLBACK_ProgramBuilt pnf_notify,
        IntPtr userData
        );

    //----------------------------------------------------------------------
    //cl_kernel clCreateKernel( cl_program program,
    //                          const char *kernel_name,
    //                          cl_int *errcode_ret )
    [DllImport(clPath)]
    public static extern IntPtr clCreateKernel(
        IntPtr program,
        string kernel_name,
        out int errcode_ret
        );

    //----------------------------------------------------------------------
    //cl_int clReleaseKernel (  cl_kernel kernel )
    [DllImport(clPath)]
    public static extern int clReleaseKernel(
        IntPtr kernel
        );

    //----------------------------------------------------------------------
    //cl_mem clCreateBuffer(cl_context context,
    //                      cl_mem_flags flags,
    //                      void *host_ptr,
    //                      cl_int *errcode_ret )
    [DllImport(clPath)]
    public static extern IntPtr clCreateBuffer(
        IntPtr context,
        uint flags,
        IntPtr size,
        IntPtr host_ptr,
        out int errcode_ret
        );

    //----------------------------------------------------------------------
    //cl_int clSetKernelArg(cl_kernel kernel,
    //                      cl_uint arg_index,
    //                      size_t arg_size,
    //                      const void *arg_value )
    [DllImport(clPath)]
    public static extern int clSetKernelArg(
        IntPtr kernel,
        uint arg_index,
        IntPtr arg_size,
        ref IntPtr arg_value
        );

    //----------------------------------------------------------------------
    //cl_int clEnqueueTask( cl_command_queue command_queue,
    //                      cl_kernel kernel,
    //                      cl_uint num_events_in_wait_list,
    //                      const cl_event *event_wait_list,
    //                      cl_event *event )
    [DllImport(clPath)]
    public static extern int clEnqueueTask(
        IntPtr command_queue,
        IntPtr kernel,
        uint num_events_in_wait_list,
        IntPtr[] event_wait_list,
        IntPtr eventObj
        );

    //----------------------------------------------------------------------
    //cl_int clEnqueueReadBuffer (  cl_command_queue command_queue,
    //                              cl_mem buffer,
    //                              cl_bool blocking_read,
    //                              size_t offset,
    //                              size_t size,
    //                              void *ptr,
    //                              cl_uint num_events_in_wait_list,
    //                              const cl_event *event_wait list,
    //                              cl_event *event )
    [DllImport(clPath)]
    public static extern int clEnqueueReadBuffer(
        IntPtr command_queue,
        IntPtr buffer,
        int blocking_read,
        IntPtr offset,
        IntPtr size,
        IntPtr ptr,
        uint num_events_in_wait_list,
        IntPtr[] event_wait_list,
        IntPtr eventObj
        );

    //----------------------------------------------------------------------
    //cl_int clReleaseMemObject(cl_mem memobj )
    [DllImport(clPath)]
    public static extern int clReleaseMemObject(
        IntPtr memObj
        );

    //----------------------------------------------------------------------
    //cl_int clReleaseProgram ( cl_program program )
    [DllImport(clPath)]
    public static extern int clReleaseProgram(
        IntPtr program
        );

    //----------------------------------------------------------------------
    //cl_int clReleaseCommandQueue (cl_command_queue command_queue )
    [DllImport(clPath)]
    public static extern int clReleaseCommandQueue(
        IntPtr command_queue
        );

    //----------------------------------------------------------------------
    //cl_int clReleaseContext ( cl_context context )
    [DllImport(clPath)]
    public static extern int clReleaseContext(
        IntPtr context
        );

    //----------------------------------------------------------------------
    // Main
    static void Main(string[] args)
    {
        try
        {
            int status;

            // initialize array
            float[] A = new float[100], B = new float[100], C = new float[100];
            for (int i = 0; i < 100; i++)
            {
                A[i] = i + 1000;
                B[i] = i / 10.0f;
            }


            // get device id
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
            string[] src = new string[]
            {
                "__kernel void\n"+
                "mul(__global const float A[],\n"+
                "    __global const float B[],\n"+
                "       __global float C[])\n"+
                "{\n"+
                "   for(int i=0;i<100;i++)\n" +
                "   {\n" +
                "       C[i]=A[i]*B[i];\n"+
                "   }\n" +
                "}\n"
            };
            IntPtr prog = clCreateProgramWithSource(context,
                (uint)src.Length, src, null, out errcode_ret);
            if (errcode_ret != CL_SUCCESS)
                throw new Exception("clCreateProgramWithSource failed.");

            // build program
            status = clBuildProgram(prog, 1, deviceID, null, null, IntPtr.Zero);
            if (status != CL_SUCCESS)
                throw new Exception("clBuildProgram failed.");

            //create kernel
            IntPtr kernel = clCreateKernel(prog, "mul", out errcode_ret);
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
            handle = GCHandle.Alloc(B, GCHandleType.Pinned);
            IntPtr memB = clCreateBuffer(context, CL_MEM_READ_ONRY | CL_MEM_COPY_HOST_PTR,
                (IntPtr)(Marshal.SizeOf(B[0]) * B.Length), handle.AddrOfPinnedObject(),
                out errcode_ret);
            handle.Free();
            if (errcode_ret != CL_SUCCESS)
                throw new Exception("clCreateBuffer B failed.");

            IntPtr memC = clCreateBuffer(context, CL_MEM_WRITE_ONRY,
                (IntPtr)(Marshal.SizeOf(C[0]) * C.Length), IntPtr.Zero, out errcode_ret);
            if (errcode_ret != CL_SUCCESS)
                throw new Exception("clCreateBuffer C failed.");



            // set kernel parameter
            status = clSetKernelArg(kernel, 0, (IntPtr)(Marshal.SizeOf(typeof(IntPtr))), ref memA);
            if (status != CL_SUCCESS)
                throw new Exception("clSetKernelArg A failed.");
            status = clSetKernelArg(kernel, 1, (IntPtr)(Marshal.SizeOf(typeof(IntPtr))), ref memB);
            if (status != CL_SUCCESS)
                throw new Exception("clSetKernelArg B failed.");
            status = clSetKernelArg(kernel, 2, (IntPtr)(Marshal.SizeOf(typeof(IntPtr))), ref memC);
            if (status != CL_SUCCESS)
                throw new Exception("clSetKernelArg C failed.");


            // request execute kernel
            status = clEnqueueTask(queue, kernel, 0, null, IntPtr.Zero);
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
            Console.WriteLine("(A * B = C)\n");
            for (int i = 0; i < 10; i++)
                Console.WriteLine("{0:F4} * {2:F4} = {2:F4}", A[i], B[i], C[i]);


            //release resources
            clReleaseMemObject(memC);
            clReleaseMemObject(memB);
            clReleaseMemObject(memA);
            clReleaseKernel(kernel);
            clReleaseProgram(prog);
            clReleaseCommandQueue(queue);
            clReleaseContext(context);


        }
        catch(Exception ex)
        {
            Console.WriteLine("{0}{1}", ex.ToString(), ex.StackTrace.ToString());
        }
    }
}
