__kernel void
clCode(	__global const unsigned char in[],
		__global unsigned char out[],
		__const int width,
		__const int height)
{
	size_t x=get_global_id(0);
	size_t y=get_global_id(1);
	int pos=((width*y)+x)*4;

	out[pos+0]=(unsigned char)(255-in[pos+0]);	// b
	out[pos+1]=(unsigned char)(255-in[pos+1]);	// g
	out[pos+2]=(unsigned char)(255-in[pos+2]);	// r
}