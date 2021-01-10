__kernel void
clProc(	__global const float A[],
		__global const float B[],
		__global float C[])
{
	for(int i=0;i<100;i++)
	{
		C[i]=A[i]+B[i];
	}
}