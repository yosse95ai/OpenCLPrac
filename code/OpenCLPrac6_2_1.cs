using System;
using System.IO;
using System.Collections;

#if true
partial class Program
{
    //----------------------------------------------------------------------
    // lowPass
    static int[] lowPass(int[] wav, int windowSize)
    {
        int[] result = new int[wav.Length];

        for (int i = 0; i < windowSize - 1; i++)
            result[i] = 0;

        int value = 0;                                      // calc first value
        for (int i = 0; i < windowSize; i++)
            value += wav[i];

        result[windowSize - 1] = value / windowSize;        // do moving average

        for (int i = windowSize; i < wav.Length; i++)
        {
            value += (wav[i] - wav[i - windowSize]);
            result[i] = value / windowSize;
        }
        return result;
    }

    //----------------------------------------------------------------------
    // read data
    private static int[] readData(string aName)
    {
        ArrayList aText = new ArrayList();

        string line;
        using (StreamReader sr = new StreamReader(aName))
        {
            while ((line = sr.ReadLine()) != null)
                aText.Add(line);

            int[] intData = new int[aText.Count];
            for (int i = 0; i < aText.Count; i++)
                intData[i] = Convert.ToInt32(aText[i].ToString());

            return intData;
        }

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
            
            for(int i = 0; i < result.Length; i++)
                Console.WriteLine("{0,6}", result[i]);
        }
        catch (Exception ex)
        {
            Console.WriteLine("{0}", ex.ToString());
        }
    }
}

#endif