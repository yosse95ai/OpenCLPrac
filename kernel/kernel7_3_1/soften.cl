__kernel void
clCode(__global const unsigned char in[],
		__global unsigned char out[],
		__const int width)
{
	size_t x=get_global_id(0);
	size_t y=get_global_id(1);

	int stride=4*width;

	for(int rgb=0; rgb<3;rgb++)
	{
		int data=0;
		for(int dY=0; dY<3; dY++)
			for(int dX=0; dX<3; dX++)
				data+=(int)in[(y+dY)*stride+(x+dX)*4+rgb];

		data/=9;
		out[stride*(y+1)+(x+1)*4+rgb]=data;
	}
}