using System;
using System.Text;
using System.Runtime.InteropServices;


class Program
{
    //------------------------------------------------
    // Error Codes
    private const int CL_SUCCESS = 0;

    //================================================
    // constants
    // cl_device_type - bitfield
    private const int CL_DEVICE_TYPE_DEFAULT = (1 << 0);

    // cl_device_info
    private const int CL_DEVICE_NAME = 0x102B;
    private const int CL_DEVICE_VENDOR = 0x102C;

    // cl_platform_info
    private const int CL_PLATFORM_PROFILE = 0x0900;
    private const int CL_PLATFORM_VERSION = 0x0901;

    //================================================
    // DLLs
    private const String clPath = @"OpenCL.dll";

    //------------------------------------------------
    //cl_int clGetPlatformIDs ( cl_uint um_entries,
    //                          cl_platformid *platforms,
    //                          cl_uint *num_platforms )
    [DllImport(clPath)]
    public static extern int clGetPlatformIDs(
        uint num_entries,
        IntPtr[] platforms,
        out uint num_platforms
        );

    //------------------------------------------------
    //cl_int clGetPlatformInfo( cl_platform_id platform,
    //                          cl_platform_info param_name,
    //                          size_t pram_value_size,
    //                          void *param_value,
    //                          size_t *param_value_size_ret )
    [DllImport(clPath)]
    public static extern int clGetPlatformInfo(
        IntPtr platform,
        int parameterName,
        IntPtr parameterValueSize,
        StringBuilder parameterValue,
        out IntPtr parameterValueSizeReturn
        );

    //------------------------------------------------
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
    //------------------------------------------------
    //cl_int clGetDeviceInfo(   cl_device_id device,
    //                          cl_device_info param_name,
    //                          size_t param_value__size,
    //                          void *param_value,
    //                          size_t *param_value_size_ret )
    [DllImport(clPath)]
    public static extern int clGetDeviceInfo(
        IntPtr device,
        int paramName,
        IntPtr paramValueSize,
        StringBuilder paramValue,
        out IntPtr paramValueSizeReturn
        );


    //================================================
    // Main
    static void Main(string[] args)
    {
        try
        {
            uint platformCount;
            int status;

            status = clGetPlatformIDs(0, null, out platformCount);
            if (status != CL_SUCCESS)
                throw new Exception("clGetPlatformIDs failed.");
            IntPtr[] platforms = new IntPtr[platformCount];
            clGetPlatformIDs(platformCount, platforms, out platformCount);

            foreach(var platform in platforms)
            {
                IntPtr valueSize = (IntPtr)0;
                StringBuilder result = new StringBuilder();

                // CL_PLATFORM_PROFILE
                status = clGetPlatformInfo(platform, CL_PLATFORM_PROFILE, (IntPtr)0, null, 
                    out valueSize);
                if (status != CL_SUCCESS)
                    throw new Exception("clGetPlatformInfo failed.");
                result.EnsureCapacity(valueSize.ToInt32());
                status = clGetPlatformInfo(platform, CL_PLATFORM_PROFILE, valueSize, result,
                    out valueSize);
                Console.WriteLine("Platform profile     : " + result.ToString());

                // CL_PLATFORM_VERSION
                status = clGetPlatformInfo(platform, CL_PLATFORM_VERSION, (IntPtr)0, null,
                    out valueSize);
                if (status != CL_SUCCESS)
                    throw new Exception("clGetPlatformInfo failed.");
                result.EnsureCapacity(valueSize.ToInt32());
                status = clGetPlatformInfo(platform, CL_PLATFORM_VERSION, valueSize, result,
                    out valueSize);
                Console.WriteLine("Platform version     : " + result.ToString());

                //get devices
                uint deviceCount;

                status = clGetDeviceIDs(platform, CL_DEVICE_TYPE_DEFAULT, 0u, null,
                    out deviceCount);
                if (status != CL_SUCCESS)
                    throw new Exception("clGetDeviceIDs failed.");
                IntPtr[] deviceId = new IntPtr[deviceCount];
                clGetDeviceIDs(platform, CL_DEVICE_TYPE_DEFAULT, deviceCount, deviceId,
                    out deviceCount);

                foreach(var device in deviceId)
                {
                    valueSize = (IntPtr)0;

                    // CL_DEVICE_VENDER
                    status = clGetDeviceInfo(device, CL_DEVICE_VENDOR, (IntPtr)0, null,
                        out valueSize);
                    if (status != CL_SUCCESS)
                        throw new Exception("clGetDeviceInfo failed.");
                    result.EnsureCapacity(valueSize.ToInt32());
                    clGetDeviceInfo(device, CL_DEVICE_VENDOR, valueSize, result, out valueSize);
                    Console.WriteLine("     Device vender   : "+result.ToString()); ;

                    // CL_DEVICE_NAME
                    status = clGetDeviceInfo(device, CL_DEVICE_NAME, (IntPtr)0, null,
                        out valueSize);
                    if (status != CL_SUCCESS)
                        throw new Exception("clGetDeviceInfo failed.");
                    result.EnsureCapacity(valueSize.ToInt32());
                    clGetDeviceInfo(device, CL_DEVICE_NAME, valueSize, result, out valueSize);
                    Console.WriteLine("     Device name     : " + result.ToString());
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("{0}{1}", ex.ToString(), ex.StackTrace.ToString());
        }
    }
}

