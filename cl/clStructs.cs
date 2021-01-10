using System;
using System.Text;
using System.Runtime.InteropServices;

//--------------------------------------------------------------------------
// class Program
partial class Program
{
    //----------------------------------------------------------------------
    //typedef struct _cl_image_format {
    //      cl_channel_order    image_channel_order;
    //      cl_channel_type     image_channel_data_type;
    //} cl_image_format;
    [StructLayout(LayoutKind.Sequential)]
    private struct cl_image_format
    {
        public uint image_channel_order;
        public uint image_channel_data_type;

        public cl_image_format(uint order, uint type)
        {
            image_channel_order = order;
            image_channel_data_type = type;
        }
    }

    //----------------------------------------------------------------------
    //typedef struct _cl_image_desc {
    //      cl_mem_object_type      image_type;
    //      size_t                  image_width;
    //      size_t                  image_height;
    //      size_t                  image_depth;
    //      size_t                  image_array_size;
    //      size_t                  image_row_pitch;
    //      size_t                  image_slice_pitch;
    //      cl_uint                 num_mip_levels;
    //      cl_uint                 num_samples;
    //      cl_mem                  buffer;
    //} cl_image_desc
    [StructLayout(LayoutKind.Sequential)]
    private struct cl_image_desc
    {
        public uint image_type;
        public IntPtr image_width;
        public IntPtr image_height;
        public IntPtr image_depth;
        public IntPtr image_array_size;
        public IntPtr image_row_pitch;
        public IntPtr image_slice_pitch;
        public uint num_mip_levels;
        public uint num_samples;
        public IntPtr buffer;

        public cl_image_desc(uint type, IntPtr width, IntPtr height)
        {
            image_type = type;
            image_width = width;
            image_height = height;
            image_depth = IntPtr.Zero;
            image_array_size = IntPtr.Zero;
            image_row_pitch = IntPtr.Zero;
            image_slice_pitch = IntPtr.Zero;
            num_mip_levels = 0;
            num_samples = 0;
            buffer = IntPtr.Zero;
        }
        public cl_image_desc(IntPtr idata)
        {
            image_type = (uint)idata;
            image_width = idata;
            image_height = idata;
            image_depth = idata;
            image_array_size = idata;
            image_row_pitch = idata;
            image_slice_pitch = idata;
            num_mip_levels = (uint)idata;
            num_samples = (uint)idata;
            buffer = idata;
        }
    }

    //----------------------------------------------------------------------
    //typedef struct _cl_buffer_region {
    //      size_t      origin;
    //      size_t      size;
    //} cl_buffer_region;
    [StructLayout(LayoutKind.Sequential)]
    private struct cl_buffer_region
    {
        public IntPtr origin;
        public IntPtr size;

        public cl_buffer_region(IntPtr inOrigin, IntPtr inSize)
        {
            origin = inOrigin;
            size = inSize;
        }
    }

}

