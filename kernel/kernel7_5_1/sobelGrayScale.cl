/*
* grayscale, in=color, out=grayscale
*
*/
const sampler_t samp=CLK_NORMALIZED_COORDS_FALSE |
					CLK_ADDRESS_NONE | CLK_FILTER_NEAREST;
__kernel void
grayscale(__read_only image2d_t rgb,
		__write_only image2d_t gs)
{
	size_t x=get_global_id(0);
	size_t y=get_global_id(1);

	int2 coord=(int2)(x, y);
	float4 data=read_imagef(rgb, samp,coord);

	float gsData=data.s0*0.114478f			// blue
					+data.s1*0.586611f		// green
						+data.s2*0.298912f;	// red
	write_imagef(gs,coord,(float4)(gsData,gsData,gsData,data.s3));
}
/*
* Prewitt filter
*
* 		| -1  0  1 |
* 	hx=	| -2  0  2 |
*		| -1  0  1 |
*
* 		| -1 -2 -1 |
* 	hy=	|  0  0  0 |
*		|  1  2  1 |
*
*			____________
*		   /
* hxy=	  / hx^2 + hy^2
*		 V
*
*/
__kernel void
clCode(__read_only image2d_t gs,
		__write_only image2d_t rgb)
{
	size_t x=get_global_id(0)+1;
	size_t y=get_global_id(1)+1;

	float4 hx=
		-read_imagef(gs, samp, (int2)(x-1,y-1))
			+read_imagef(gs, samp, (int2)(x+1,y-1))
		-read_imagef(gs, samp, (int2)(x-1,y))*2
			+read_imagef(gs, samp, (int2)(x+1,y))*2
		-read_imagef(gs, samp, (int2)(x-1,y+1))
			+read_imagef(gs, samp, (int2)(x+1,y+1));

	float4 hy=
		-read_imagef(gs, samp, (int2)(x-1, y-1))
			-read_imagef(gs, samp, (int2)(x, y-1))*2
				-read_imagef(gs, samp, (int2)(x+1, y-1))

		+read_imagef(gs, samp, (int2)(x-1, y+1))
			+read_imagef(gs, samp, (int2)(x, y+1))*2
				+read_imagef(gs, samp, (int2)(x+1, y+1));

	float4 data=hypot(hx,hy);

	data.w=1.0f;

	write_imagef(rgb, (int2)(x, y), data);
}