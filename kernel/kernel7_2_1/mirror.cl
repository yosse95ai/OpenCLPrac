__kernel void
clCode(	__global const unsigned char in[],
		__global unsigned char out[],
		__const int width,
		__const int height)
{
	size_t x=get_global_id(0);
	size_t y=get_global_id(1);
	
	unsigned int dst=(width*y+x)*4;
	unsigned int src=((width*y)+width-x-1)*4;

	out[dst+0]=in[src+0];		// b
	out[dst+1]=in[src+1];		// g
	out[dst+2]=in[src+2];		// r

}