// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
// Modified from Workshop
Shader "Custom/water"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Wave1Amp ("Wave 1 Amp", Float) = 0.3
		_Wave1Xfreq ("Wave 1 X Frequency", Float) = 1.256
		_Wave1Zfreq ("Wave 1 Z Frequency", Float) = 1
		_Wave1XComp ("Wave 1 X Component", Float) = 0.3

		_Wave2Amp ("Wave 2 Amp", Float) = 0.2
		_Wave2Xfreq ("Wave 2 X Frequency", Float) = 1.256
		_Wave2Zfreq ("Wave 2 Z Frequency", Float) = 1.5

		_TimeFreq ("Time Freq", Float) = 1
		_ShawdowBlendValue ("Shadow Blend Value", Range(0,1)) = 0.3
	}
	SubShader
	{
		Pass
		{
			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase

			#include "UnityCG.cginc"
			#include "AutoLight.cginc"

			uniform sampler2D _MainTex;
			uniform float _Wave1Amp;
			uniform float _Wave1Xfreq;
			uniform float _Wave1Zfreq;
			uniform float _Wave1XComp;

			uniform float _Wave2Amp;
			uniform float _Wave2Xfreq;
			uniform float _Wave2Zfreq;

			uniform float _TimeFreq;
			uniform float _ShawdowBlendValue;

			struct vertIn
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct vertOut
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				SHADOW_COORDS(1)
				float4 pos : TEXCOORD2;
			};

			// Implementation of the vertex shader
			vertOut vert(vertIn v)
			{
				// Displace the original vertex in model space
				float wave1Y = _Wave1Amp*(_Wave1XComp*sin(_Wave1Xfreq*v.vertex.x + _TimeFreq*_Time.y)+(1.0f - _Wave1XComp)*sin(_Wave1Zfreq*v.vertex.z + _TimeFreq*_Time.y));
				float wave2Y = _Wave2Amp*(sin(_Wave2Xfreq*v.vertex.x + _Wave2Zfreq*v.vertex.z + _TimeFreq*_Time.y));
				float4 displacement = float4(0.0f, wave1Y + wave2Y, 0.0f, 0.0f);
				v.vertex += displacement;

				vertOut o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.uv += 0.01*_Time.y;

				o.pos = o.vertex;
				TRANSFER_VERTEX_TO_FRAGMENT(o)
				return o;
			}
			
			// Implementation of the fragment shader
			fixed4 frag(vertOut v) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, v.uv);
				float shadowIntensity = 1 - SHADOW_ATTENUATION(v);
				float textureIntensity = 1 - _ShawdowBlendValue*shadowIntensity;
				return textureIntensity*col;
			}
			ENDCG
		}
		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}