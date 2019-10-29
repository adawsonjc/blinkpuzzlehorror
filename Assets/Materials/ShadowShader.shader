// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'



Shader "Custom/ShadowShader"
{
	Properties
	{
		_MainTex("Albedo Texture", 2D) = "white" {}
		_TintColor("Tint Color", Color) = (1,1,1,1)
		_Transparency("Transparency", Range(0.0,1)) = 0.25
		_Player_pos("_Player_pos", Vector) = (0,0,0,1)
		_ShadowRadius("Shadow Radius", float) = 1.0
		_MaxShadowRadius("max Shadow Radius", float) = 0.5

	}
	SubShader
	{
		Tags {"Queue" = "Transparent+1" "RenderType" = "Transparent" }
		LOD 100

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

	
			struct v2f
			{
				float4 pos :  SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			float powerForPos(float4 pos, float2 nearVertex);

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _TintColor;
			vector _Player_pos;
			float _Transparency;
			float _ShadowRadius;
			float _CutoutThresh;

			v2f vert(appdata_full v)
			{
				v2f o;
				o.pos  =  UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv) * _TintColor;

				float alpha = (1 - powerForPos(_Player_pos, i.pos));
				col.a = alpha;

				return col;
			}

			// return 0 if pos - nearVertex > shadowRadius
			float powerForPos(float4 pos, float2 nearVertex) {
				float atten = (_ShadowRadius - length(pos.xy - nearVertex.xy));
				return atten;
			}
			ENDCG
		}
	}
}