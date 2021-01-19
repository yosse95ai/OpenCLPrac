__kernel void
lowPass(__global const int in[],
		__global int out[] ,
		__const int windowSize)
{
	unsigned int x=get_global_id(0);

	int value=0;
	for (int i=0; i<x+windowSize; i++)
		value+=in[i];

	out[x+windowSize-1]=value/windowSize;
}