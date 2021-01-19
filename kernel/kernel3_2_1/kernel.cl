__kernel void
lowPass(__global const int2 *in,
		__global int2 *out,
		__const int windowSize)
{
	size_t x=get_global_id(0);

	int2 value=0;
	for (int i=0; i<x+windowSize; i++)
		value+=in[i];

	out[x+windowSize-1]=value/(int2)windowSize;
}