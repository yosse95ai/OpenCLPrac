__kernel void
clCode(__global const unsigned char in[],
		__global unsigned char out[],
		__const int width)
{
	int yx[3][3];

	size_t x=get_global_id(0);
	size_t y=get_global_id(1);

	int stride=4*width;

	for(int dY=0; dY<3; dY++)
		for(int dX=0; dX<3; dX++)
			yx[dY][dX]=(y+dY)*stride+(x+dX)*4;

	for(int rgb=0; rgb<3;rgb++)
	{
		int data=(int)(
			-in[yx[0][0]+rgb] -in[yx[0][1]+rgb] -in[yx[0][2]+rgb]
			-in[yx[1][0]+rgb] +in[yx[1][1]+rgb]*8 -in[yx[1][2]+rgb]
			-in[yx[2][0]+rgb] -in[yx[2][1]+rgb] -in[yx[2][2]+rgb]
		);

		out[yx[1][1]+rgb]=convert_uchar_sat(data);
	}
}