__kernel void
clProc(	__global const float A[],
		__const float B,
		__global float C[])
{
	int x=get_global_id(0);
	int y=get_global_id(1);

	C[y*10+x]=A[y*10+x]+B;
}