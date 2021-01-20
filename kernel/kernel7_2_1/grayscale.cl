__kernel void
clCode(	__global const unsigned char in[],
		__global unsigned char out[],
		__const int width,
		__const int height)
{
	size_t x=get_global_id(0);
	size_t y=get_global_id(1);
	int pos=((width*y)+x)*4;

	int data=(int)((float)in[pos+0]*0.114478f					// blue
						+(float)in[pos+1]*0.586611f				// green
							+(float)in[pos+2]*0.298912f);		// red

	out[pos+0]=out[pos+1]=out[pos+2]=convert_uchar_sat(data);	// b, g, r

}