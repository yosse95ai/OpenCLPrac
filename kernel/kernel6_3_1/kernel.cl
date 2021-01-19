__kernel void
lowPass(__global const int in[],
		__global int out[],
		__const int numOfTotal,
		__const int windowSize)
{
	for(int i=0; i<windowSize-1; i++)
		out[i]=0;

	int value=0;
	for(int i=0; i<windowSize; i++)
		value+=in[i];

	out[windowSize-1]=value/windowSize;
	for(int i=windowSize; i<numOfTotal; i++)
	{
		value+=(in[i]-in[i-windowSize]);
		out[i]=value/windowSize;
	}
}