﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

/*
 * By Martin Reintges 05/2016
 */

Shader "DistortionShaderPack/Legacy/GrabDistortionRingBlended"
{
	Properties
	{
		_RenderTexture ("RenderTexture", 2D) = "black" {}
		_DistortionStrength ("DistortionStrength", Range(-1,1)) = -1
		_DistortionSize ("DistortionSize", Range(0,0.99)) = 0.8
		_DistortionSizeMin ("DistortionSizeMin", Range(0,0.8)) = 0.0
	}
	SubShader 
	{
		// Subshader Tags
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		

		// This pass grabs the screen behind the object into a texture.
		// We can access the result in the next pass as _GrabTexture
		GrabPass 
		{
			Name "BASE"
			Tags { "LightMode" = "Always" }
		}

		// Render Pass
		Pass
		{
			CGPROGRAM
			#include "UnityCG.cginc"
			#include "DistortionCG.cginc"
			#pragma target 2.0
			
			#pragma fragmentoption ARB_precision_hint_fastest
			
			#pragma vertex vert
			#pragma fragment frag
	
	
		////// Uniform user variable definition	
			uniform sampler2D _GrabTexture;
			uniform float4 _GrabTexture_TexelSize;
			uniform sampler2D _LastCameraDepthTexture;

			uniform sampler2D _RenderTexture;
			uniform float _DistortionStrength;
			uniform float _DistortionSize;
			uniform float _DistortionSizeMin;

	
		////// Shader functions
			// Vertex shader
			Vert2Frag vert(VertexInput vertIn)
			{
				Vert2Frag output;
				
				output.pos = UnityObjectToClipPos(vertIn.vertex);
				output.posScreen = ComputeScreenPos(output.pos);
                output.uv = vertIn.texcoord0;
				
				return(output);
			}
			
			// Fragent shader
			float4 frag(Vert2Frag fragIn) : SV_Target
			{
				float4 uvScreen = UNITY_PROJ_COORD(fragIn.posScreen);
				float2 normal = float2(0,0);
				normal.x = sin((fragIn.uv.x-0.5)*2);
				normal.y = sin((fragIn.uv.y-0.5)*2);
				
				float dist = min(length(normal), _DistortionSize);
				
				float diff = _DistortionSize - _DistortionSizeMin;
				dist = max(0, dist - _DistortionSizeMin);
				dist = min(dist, min(diff*0.5, diff - dist));
				dist /= diff;
				
				normal *= _DistortionStrength * dist;

				float blend = getBlend(fragIn.posScreen, _LastCameraDepthTexture);

				float4 final = tex2Dproj( _GrabTexture, uvScreen+float4(normal.x*0.5,normal.y,0,0) );
				final.w = final.w*blend;
                return final;
			}
			ENDCG
		}
	} 
}