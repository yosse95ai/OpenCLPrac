using System;
using System.Text;
using System.Runtime.InteropServices;

//--------------------------------------------------------------------------
// class Program
partial class Program
{
    //----------------------------------------------------------------------
    // constants

    // Error Codes
    private const int CL_SUCCESS = 0;
    private const int CL_DEVICE_NOT_FOUND = -1;
    private const int CL_DEVICE_NOT_AVAILABLE = -2;
    private const int CL_COMPILER_NOT_AVAILACLE = -3;
    private const int CL_MEM_OBJEECT_ALLOCATION_FAILURE = -4;
    private const int CL_OUT_OF_RESOURCES = -5;
    private const int CL_OUT_OF_HOST_MEMORY = -6;
    private const int CL_PROFILING_INFO_NOT_AVAILABLE = -7;
    private const int CL_MEM_COPY_OVERLAP = -8;
    private const int CL_IMAGE_FORMAT_MISMATCH = -9;
    private const int CL_IMAGE_FORMAT_NOT_SUPPORTED = -10;
    private const int CL_BUILD_PROGRAM_FAILURE = -11;
    private const int CL_MAP_FAILURE = -12;
    private const int CL_MISALIGNED_SUB_BUFFER_OFFSET = -13;
    private const int CL_EXEC_STATUS_ERROR_FOR_EVENTS_IN_WAIT_LIST = -14;
    private const int CL_COMPILE_PROGRAM_FAILURE = -15;
    private const int CL_LINKER_NOT_AVAILABLE = -16;
    private const int CL_LINK_PROGRAM_FAILURE = -17;
    private const int CL_DEVICE_PARTITION_FAILED = -18;
    private const int CL_KERNEL_ARG_INFO_NOT_AVAILABLE = -19;

    private const int CL_INVALID_VALUE = -30;
    private const int CL_INVALID_DEVICE_TYPE = -31;
    private const int CL_INVALID_PLATFORM = -32;
    private const int CL_INVALID_DEVICE = -33;
    private const int CL_INVALID_CONTEXT = -34;


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


    private const int CL_COMMAND_BARRIER = 0x1205;
    private const int CL_COMMAND_MIGRATE_MEM_OBJECTS = 0x1206;
    private const int CL_COMMAND_FILL_BUFFER = 0x1207;
    private const int CL_COMMAND_FILE_IMAGE = 0x1208;

    // command execution status
    private const int CL_COMPLETE = 0x0;
    private const int CL_RUNNING = 0x1;
    private const int CL_SUBMITTED = 0x2;
    private const int CL_QUEUED = 0x3;

    // cl_buffer_create_type
    private const int CL_BUFFER_CREATE_TYPE_REGION = 0x1220;

    // cl_profiling_info
    private const int CL_PROFILING_COMMAND_QUEUED = 0x1280;
    private const int CL_PROFILING_COMMAND_SUBMIT = 0x1281;
    private const int CL_PROFILING_COMMAND_START = 0x1282;
    private const int CL_PROFILING_COMMAND_END = 0x1283;
}
