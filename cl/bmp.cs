using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;


//--------------------------------------------------------------------------
// class Program
partial class Program
{
    //----------------------------------------------------------------------
    // read Bitmap file
    private static Bitmap readBmp(string fName)
    {
        Image img = Image.FromFile(fName);
        return (Bitmap)img;
    }

    //----------------------------------------------------------------------
    // Bitmap to byte array
    private static Byte[] bmp2byteArray(Bitmap inBmp)
    {
        Rectangle rect = new Rectangle(0, 0, inBmp.Width, inBmp.Height);

        BitmapData bmpData = inBmp.LockBits(rect,        // lock
            ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

        Byte[] rgbaMat = new byte[bmpData.Stride * bmpData.Height];
        Marshal.Copy(bmpData.Scan0, rgbaMat, 0, bmpData.Stride * bmpData.Height);
        inBmp.UnlockBits(bmpData);                      // unlock

        return rgbaMat;
    }

    //----------------------------------------------------------------------
    // byte array to 24bpp Bitmap
    private static Bitmap byteArray2bmp(Bitmap inBmp, Byte[] rgbaMat)
    {
        Rectangle rect = new Rectangle(0, 0, inBmp.Width, inBmp.Height);

        BitmapData bmpData = inBmp.LockBits(rect,       // lock
            ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

        Marshal.Copy(rgbaMat, 0, bmpData.Scan0, bmpData.Stride * bmpData.Height);
        inBmp.UnlockBits(bmpData);                      // unlock

        // convert 32bpp to 24bpp        
        RectangleF cloneRect = new RectangleF(0, 0, inBmp.Width, inBmp.Height);
        Bitmap bmp24bpp = inBmp.Clone(cloneRect, PixelFormat.Format24bppRgb);

        return bmp24bpp;
    }

    //----------------------------------------------------------------------
    // byte array  to Bitmap
    private static Bitmap byteArray2bmp(Byte[] rgbaMat,int width,int height)
    {
        using(Bitmap bmp=new Bitmap(width, height, PixelFormat.Format32bppArgb))
        {
            return byteArray2bmp(bmp, rgbaMat);
        }
    }

    //----------------------------------------------------------------------
    // write bmp/jpg file
    private static void writeBmp(Bitmap bmp,string fName)
    {
        ImageFormat format = String.Compare(Path.GetExtension(fName),
            ".jpg", true) == 0 ? ImageFormat.Jpeg : ImageFormat.Bmp;

        bmp.Save(fName, format);
    }
}