using System;
using System.IO;
using System.Collections;

#if true
partial class Program
{
    private const string clName = "kernel.cl";
    private const string clProc = "lowPass";

    //----------------------------------------------------------------------
    // lowPass
    static int[] lowPass(int[] wav, int windowSize)
    {
        string[] src = { System.IO.File.ReadAllText(clName) };
        int[] result = new int[wav.Length];
        int numOfTotal = wav.Length / 2;

        IntPtr[] platformId = GetPlatformIDs();                 // get platform id
        IntPtr[] deviceID = GetDeviceIDs(platformId);           // get device id
        IntPtr context = CreateContext(platformId, deviceID);   // create Context 
        IntPtr queue = CreateCommandQueue(context, deviceID[0]);// create Command Queue
        IntPtr prog = CreatePragramWithSource(context, src);    // create program object
        BuildPragram(prog, deviceID);                           // build program
        IntPtr kernel = CreateKernel(prog, clProc);             // create kernel

        IntPtr memIn = CreateBuffer(context, wav, CL_MEM_READ_ONRY | CL_MEM_COPY_HOST_PTR);
        IntPtr memOut = CreateBuffer(context, result, CL_MEM_WRITE_ONRY);

        SetKernelAeg(kernel, 0, memIn);                         // set kernel parameters
        SetKernelAeg(kernel, 1, memOut);
        SetKernelAeg(kernel, 2, numOfTotal);
        SetKernelAeg(kernel, 3, windowSize);

        EnqueueTask(queue, kernel);                             // request execute kernel

        EnqueueReadBuffer(queue, memOut, result);

        clReleaseMemObject(memOut);
        clReleaseMemObject(memIn);
        clReleaseKernel(kernel);
        clReleaseProgram(prog);
        clReleaseCommandQueue(queue);
        clReleaseContext(context);

        return result;
    }

    //----------------------------------------------------------------------
    // read data
    private static int[] readData(string aName)
    {
        ArrayList aText = new ArrayList();

        string line;
        using (StreamReader sr = new StreamReader(aName))
            while ((line = sr.ReadLine()) != null)
                aText.Add(line);

        int[] intData = new int[aText.Count * 2];

        for (int i = 0; i < aText.Count; i++)
        {
            // split
            string[] split = aText[i].ToString().Split(new char[] { ',', '\t' });

            intData[i * 2 + 0] = Convert.ToInt32(split[0]);
            intData[i * 2 + 1] = Convert.ToInt32(split[1]);
        }

        return intData;

    }



    //----------------------------------------------------------------------
    // Main
    static void Main(string[] args)
    {
        try
        {
            if (args.Length != 2)                                   // check arguments
                throw new Exception("引数に[データファイル名]" +
                    " [ウィンドウサイズ]を指定してください.");

            int[] wav = readData(args[0]);                          // read data

            int windowSize = Convert.ToInt32(args[1].ToString());

            int[] result = lowPass(wav, windowSize);

            for (int i = 0; i < result.Length / 2; i++)
                Console.WriteLine("{0,6}, {1,6}", result[i * 2], result[i * 2 + 1]);
        }
        catch (Exception ex)
        {
            Console.WriteLine("{0}", ex.ToString());
        }
    }
}

#endif