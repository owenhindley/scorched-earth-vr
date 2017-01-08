Shader "Unlit/LandscapeShader"
{
	Properties
	{
		_Main ("Main", Color) = (1,1,1,1)
		_Secondary ("Secondary", Color) = (1,1,1,1)
		_Cliff ("Cliff", Color) = (1,1,1,1)
		_yMax ("YMax", Float) = 1.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

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
				float4 normal : TEXCOORD1;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 normal : TEXCOORD2;
				float4 orig : TEXCOORD3;
			};

			float4 _Main;
			float4 _Secondary;
			float _yMax;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.orig = v.vertex;
				o.normal = v.normal;
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				float4 col;
				col = lerp(_Main, _Secondary, i.orig.y * _yMax);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				// col = float4(i.normal);
				return col;
			}
			ENDCG
		}
	}
}
