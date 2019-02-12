﻿Shader "Unlit/NewUnlitShader"
{
	Properties
	{
		_MainTex ("Cool Texture", 2D) = "white" {}
		_TintColour ("Tint Colour", Color) = (1,1,1,1)
		_Transparency ("Transparency", Range(0,0.5)) = 0.25
		_CutoutThresh("Cutout Threshold", Range(0,1.0)) = 0.2
		_Distance("Distance", Float) = 1
		_Amplitude("Amplitude", Float) = 1
		_Speed("Speed", Float) = 1
		_Amnt("Amount", Range(0.0,35.0)) = 1
	}
	SubShader
	{
		Tags {"Queue"="Transparent" "RenderType"="Transparent" }
		LOD 1000

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _TintColour;
			float _Transparency;
			float _CutoutThresh;
			float _Distance;
			float _Amplitude;
			float _Speed;
			float _Amnt;
			
			v2f vert (appdata v)
			{
				v2f o;
				//v.vertex.x += sin(_Time.y* _Speed + v.vertex.y*_Amplitude) * _Distance *_Amnt;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv) + _TintColour;
				col.a = _Transparency;
				clip(col.r - _CutoutThresh);
				col *= 2;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
