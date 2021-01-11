__kernel void
clProc(	__global const float A[],
		__const float B,
		__global float C[])
{
	int i=get_global_id(0);

	C[i]=A[i]-B;
}