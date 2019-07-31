Shader "Quixel/SubtractionShader" {
	Properties{
		_MainTex("First Texture", 2D) = "white" {} //MainTexture Declaration for shader 
		_SecondTex("Second Texture", 2D) = "black" {} //MainTexture Declaration for shader 
	}
		SubShader
	{

		Pass
		{
			CGPROGRAM
#pragma vertex vert_img
#pragma fragment resultantTexture

#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform sampler2D _SecondTex;

			struct Input
			{
				float uv_MainTex, uv_SecondTex;
			};

			fixed4 resultantTexture(v2f_img i) : SV_Target{
				return tex2D(_MainTex, i.uv) - tex2D(_SecondTex, i.uv);
			}
				ENDCG
		}
	}
}