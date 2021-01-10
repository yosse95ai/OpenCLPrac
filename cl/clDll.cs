using System;
using System.Text;
using System.Runtime.InteropServices;

//--------------------------------------------------------------------------
// class Program
partial class Program
{
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
}
