// SC Post Effects
// Staggart Creations
// http://staggart.xyz

#include "Sampling.hlsl"

//Always present in every shader
TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex); //Present in every shader
float4 _MainTex_TexelSize;

//-------------
// Generic functions
//-------------

float rand(float n) { return frac(sin(n) * 13758.5453123 * 0.01); }

float rand(float2 n) { return frac(sin(dot(n, float2(12.9898, 4.1414))) * 43758.5453); }

float2 RotateUV(float2 uv, float rotation) {
	float cosine = cos(rotation);
	float sine = sin(rotation);
	float2 pivot = float2(0.5, 0.5);
	float2 rotator = (mul(uv - pivot, float2x2(cosine, -sine, sine, cosine)) + pivot);
	return saturate(rotator);
}

float3 ChromaticAberration(TEXTURE2D_ARGS(tex, samplerTex), float4 texelSize, float2 uv, float amount) {
		float2 direction = normalize((float2(0.5, 0.5) - uv));
		float3 distortion = float3(-texelSize.x * amount, 0, texelSize.x * amount);

		float red = SAMPLE_TEXTURE2D(tex, samplerTex, uv + direction * distortion.r).r;
		float green = SAMPLE_TEXTURE2D(tex, samplerTex, uv + direction * distortion.g).g;
		float blue = SAMPLE_TEXTURE2D(tex, samplerTex, uv + direction * distortion.b).b;

		return float3(red, green, blue);
	}

half4 LuminanceThreshold(half4 color, half threshold)
{
	half br = Max3(color.r, color.g, color.b);

	half contrib = max(0, br - threshold);

	contrib /= max(br, 0.001);

	return color * contrib;
}

float Luminance(float3 color)
{
	return (color.r * 0.3 + color.g * 0.59 + color.b * 0.11);
}

/*
float3 PositionFromDepth(float depth, float2 uv, float4 inverseViewMatrix) {

	float4 clip = float4((uv.xy * 2.0f - 1.0f) * float2(1, -1), 0.0f, 1.0f);
	float3 worldDirection = mul(inverseViewMatrix, clip) - _WorldSpaceCameraPos;

	float3 worldspace = worldDirection * depth + _WorldSpaceCameraPos;

	return float3(frac((worldspace.rgb)) + float3(0, 0, 0.1));
}
*/

// (returns 1.0 when orthographic)
float CheckPerspective(float x)
{
	return lerp(x, 1.0, unity_OrthoParams.w);
}

// Reconstruct view-space position from UV and depth.
float3 ReconstructViewPos(float2 uv, float depth)
{
	float3 worldPos = float3(0, 0, 0);
	worldPos.xy = (uv.xy * 2.0 - 1.0 - float2(unity_CameraProjection._13, unity_CameraProjection._23)) / float2(unity_CameraProjection._11, unity_CameraProjection._22) * CheckPerspective(depth);
	worldPos.z = depth;
	return worldPos;
}

float2 FisheyeUV(half2 uv, half amount, half zoom)
{
	half2 center = uv.xy - half2(0.5, 0.5);
	half CdotC = dot(center, center);
	half f = 1.0 + CdotC * (amount * sqrt(CdotC));
	return f * zoom * center + 0.5;
}

float2 Distort(float2 uv)
{
#if DISTORT
	{
		uv = (uv - 0.5) * _Distortion_Amount.z + 0.5;
		float2 ruv = _Distortion_CenterScale.zw * (uv - 0.5 - _Distortion_CenterScale.xy);
		float ru = length(float2(ruv));

		UNITY_BRANCH
			if (_Distortion_Amount.w > 0.0)
			{
				float wu = ru * _Distortion_Amount.x;
				ru = tan(wu) * (1.0 / (ru * _Distortion_Amount.y));
				uv = uv + ruv * (ru - 1.0);
			}
			else
			{
				ru = (1.0 / ru) * _Distortion_Amount.x * atan(ru * _Distortion_Amount.y);
				uv = uv + ruv * (ru - 1.0);
			}
	}
#endif

	return uv;
}

//------------------------
// Common vertex functions
//------------------------

float4 _BlurOffsets;

struct v2fGaussian {
	float4 pos : POSITION;
	float2 uv : TEXCOORD0;

	float4 uv01 : TEXCOORD1;
	float4 uv23 : TEXCOORD2;
	float4 uv45 : TEXCOORD3;
};

v2fGaussian VertGaussian(AttributesDefault v) {
	v2fGaussian o;
	o.pos = float4(v.vertex.xy, 0, 1);

	o.uv.xy = TransformTriangleVertexToUV(o.pos.xy);

#if UNITY_UV_STARTS_AT_TOP
	o.uv = o.uv * float2(1.0, -1.0) + float2(0.0, 1.0);
#endif
	//UNITY_SINGLE_PASS_STEREO
	o.uv = TransformStereoScreenSpaceTex(o.uv, 1.0);

	o.uv01 = o.uv.xyxy + _BlurOffsets.xyxy * float4(1, 1, -1, -1);
	o.uv23 = o.uv.xyxy + _BlurOffsets.xyxy * float4(1, 1, -1, -1) * 2.0;
	o.uv45 = o.uv.xyxy + _BlurOffsets.xyxy * float4(1, 1, -1, -1) * 6.0;

	return o;
}

float4 FragBlurBox(VaryingsDefault i) : SV_Target
{
	return DownsampleBox4Tap(TEXTURE2D_PARAM(_MainTex, sampler_MainTex), i.texcoord, _BlurOffsets.xy).rgba;
}

float4 FragBlurGaussian(v2fGaussian i) : SV_Target
{
	half4 color = float4(0, 0, 0, 0);

	color += 0.40 * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
	color += 0.15 * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv01.xy);
	color += 0.15 * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv01.zw);
	color += 0.10 * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv23.xy);
	color += 0.10 * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv23.zw);
	color += 0.05 * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv45.xy);
	color += 0.05 * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv45.zw);

	return color;
}