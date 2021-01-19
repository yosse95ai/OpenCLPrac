__kernel void
lowPass(__global const int2 in[],
		__global int2 out[],
		__const int numOfTotal,
		__const int windowSize)
{
	for(int i=0; i<windowSize-1; i++)
		out[i]=0;

	int2 value=(int2)0;
	for(int i=0; i<windowSize; i++)
		value+=in[i];

	out[windowSize-1]=value/windowSize;
	for(int i=windowSize; i<numOfTotal; i++)
	{
		value+=(in[i]-in[i-windowSize]);
		out[i]=value/(int2)windowSize;
	}
}