const sampler_t samp=CLK_NORMALIZED_COORDS_FALSE |
					CLK_ADDRESS_NONE | CLK_FILTER_NEAREST;

__kernel void
clCode(__read_only image2d_t in,
		__write_only image2d_t out)
{
	size_t x=get_global_id(0)+1;
	size_t y=get_global_id(1)+1;

	float4 data=(float4)0.0f;

	for(int yy=0; yy<3; yy++)
		for(int xx=0; xx<3; xx++)
		{
			int2 coord=(int2)((x-1+xx), (y-1+yy));
			if(yy==1 && xx==1)
				data+=(read_imagef(in, samp, coord)*9.0f);
			else
				data-=read_imagef(in,samp,coord);
		}

	data.w=1.0f;

	write_imagef(out, (int2)(x, y), data);
}