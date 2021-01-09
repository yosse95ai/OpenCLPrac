using System;

class mul
{
    //------------------------------------------------
    // Main
    static void Main()
    {
        // initialize array
        float[] A = new float[100], B = new float[100], C = new float[100];
        for(int i = 0; i < 100; i++)
        {
            A[i] = (float)(i + 1000);
            B[i] = (float)i / 10.0f;
        }

        // calc.
        for(int i = 0; i < C.Length; i++)
        {
            C[i] = A[i] * B[i];
        }

        // list results
        Console.WriteLine("(A * B = C)\n");
        for(int i = 0; i < 10; i++)
        {
            Console.WriteLine("{0:F4} * {2:F4} = {2:F4}", A[i], B[i], C[i]);
        }
    }

}
