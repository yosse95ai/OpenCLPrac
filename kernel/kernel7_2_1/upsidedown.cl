__kernel void
clCode(	__global const unsigned char in[],
		__global unsigned char out[],
		__const int width,
		__const int height)
{
	size_t x=get_global_id(0);
	size_t y=get_global_id(1);
	
	unsigned int upper=(width*y+x)*4;
	unsigned int lower=(width*(height-y-1)+x)*4;

	out[upper+0]=in[lower+0];		// b
	out[upper+1]=in[lower+1];		// g
	out[upper+2]=in[lower+2];		// r

}