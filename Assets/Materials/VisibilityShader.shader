Shader "Custom/VisibilityShader"
{

	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _Threshold("Threshold", float) = 1
        _EdgeColor("Edge color", Color) = (0,0.5,0.5,1)

	}
	SubShader
	{
		Tags{"Queue" = "Transparent" "RenderType" = "Transparent"}

		Pass
		{
			ColorMask 0

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				return o;
			}

			float4 frag(v2f i) : SV_Target
			{
				return float4(1, 1, 1, 1);
			}
			ENDCG
		}

		
	}
}