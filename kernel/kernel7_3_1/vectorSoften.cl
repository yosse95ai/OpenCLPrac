__kernel void
clCode(__global const uchar4 in[],
		__global  uchar4 out[],
		__const int width)
{
	int4 yx[3][3];

	size_t x=get_global_id(0);
	size_t y=get_global_id(1);

	for(int dY=0; dY<3; dY++)
		for(int dX=0; dX<3; dX++)
			yx[dY][dX]=convert_int4(in[(y+dY)*width+(x+dX)]);

	int4 bgra=  +yx[0][0] +yx[0][1] +yx[0][2]
				+yx[1][0] +yx[1][1] +yx[1][2]
				+yx[2][0] +yx[2][1] +yx[2][2];

	bgra/=9;

	bgra.w=in[(y+1)*width+(x+1)].w;		// copy alpha ch

	out[(y+1)*width+(x+1)]=convert_uchar4_sat(bgra);
}