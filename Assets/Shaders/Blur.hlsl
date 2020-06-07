#ifndef MAINTEX
#define MAINTEX
sampler2D _MainTex;
float4 _MainTex_TexelSize;
#endif

fixed3 Sample(float2 uv) 
{
	return tex2D(_MainTex, uv).rgb;
}
fixed3 SampleGaussian(float2 uv) 
{
	//3x3 gaussian kernel
	float4 texSize = _MainTex_TexelSize;
	fixed3 s =
		Sample(uv - float2(-texSize.x, -texSize.y))		* 1 + 
		Sample(uv - float2(-texSize.x, 0))				* 2 + 
		Sample(uv - float2(-texSize.x, texSize.y))		* 1 +

		Sample(uv - float2(0, -texSize.y))				* 2 + 
		Sample(uv - float2(0, 0))						* 4 + 
		Sample(uv - float2(0, texSize.y))				* 2 +

		Sample(uv - float2(texSize.x, -texSize.y))		* 1 + 
		Sample(uv - float2(texSize.x, 0))				* 2 + 
		Sample(uv - float2(texSize.x, texSize.y))		* 1;
	return s / 16;
}