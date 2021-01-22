const sampler_t samp=CLK_NORMALIZED_COORDS_FALSE |
						CLK_ADDRESS_CLAMP | CLK_FILTER_LINEAR;

__kernel void
clCode(__read_only image2d_t in,
		__write_only image2d_t out,
		__const float degree)
{
	size_t x=get_global_id(0);
	size_t y=get_global_id(1);

	float radian=radians(degree);

	int oWidth=get_image_width(out);
	int oHeight=get_image_height(out);

	int iWidth=get_image_width(in);
	int iHeight=get_image_height(in);

	int oXc=oWidth/2;						// out x center
	int oYc=oHeight/2;						// out y center

	int iXc=iWidth/2;						// in x center
	int iYc=iHeight/2;						// in y center

	int oX=x-oXc;							// dest x coord
	int oY=y-oYc;							// dest y coord

	float iY=(float)(oX*sin(radian)+oY*cos(radian));
	float iX=(float)(oX*cos(radian)-oY*sin(radian));

	iY+=(float)iYc;
	iX+=(float)iXc;

	float2 coordr=(float2)(iX, iY);
	float4 data=read_imagef(in, samp, coordr);

	int2 coordw=(int2)(x, y);
	write_imagef(out, coordw, data);
}