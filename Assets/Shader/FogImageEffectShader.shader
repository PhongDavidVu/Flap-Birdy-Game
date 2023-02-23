Shader "Hidden/FogImageEffectShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_FogColor("Fog Color", Color) = (1,1,1,1)
		_FogDensity("Fog Density", Range(0, 1)) = 0.8
		_LinearFogBreak ("Linear Fog Break", Range(0, 0.5)) = 0.015
		_FogLogXParameter ("Fog Log Parameter", Float) = 100
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _CameraDepthTexture;
			fixed4 _FogColor;
			float _FogDensity;
			float _LinearFogBreak;
			float _FogLogXParameter;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			float combinationFunction (float x) {
				if (x < _LinearFogBreak) {
					return x;
				} else {
					return x + log10(1.0f + (x-_LinearFogBreak)*_FogLogXParameter);
				}
			}

			float greaterThanZeroTanh (float x) {
				float tanhX = tanh(x);
				if (x > 0.0f) {
					return tanhX;
				} else {
					return 0;
				}
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				float depth = UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, i.uv));
				float linearDepth = Linear01Depth(depth);
				float logarithDepth = combinationFunction(linearDepth);
				float fogIntensity = greaterThanZeroTanh(logarithDepth * _FogDensity);
				return lerp(col, _FogColor, fogIntensity);
			}
			ENDCG
		}
	}
}
