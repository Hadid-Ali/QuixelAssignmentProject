Shader "QuixelTest/SubtractionShader/Subtraction" 
//Declaring the Shader space
{
    Properties
    {
		_MainTex("Primary Texture", 2D) = "white" {}
		_SecondaryTex("Secondary Texture",2D) = "white"{}   
		//Declaring Secondary Texture with data type
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        #pragma target 2.0 
		
		sampler2D _MainTex, _SecondaryTex;
		//Re Declaring for CG-Part with shader data type to read
	    //sampler2D _SecondaryTex; 

//		sampler2D _RenderPaintTexture;

        struct Input
        {
			float2 uv_MainTex, uv_SecondaryTex;
			//Declaring the half fixed or half float type for UV channels

			float2 uv_RenderPaintTexture;
        };


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			fixed4 b = tex2D(_SecondaryTex, IN.uv_SecondaryTex); 
			//Calculating the RGBA channel or Vectpr for the secondary texture
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) - b; 
			//Subtracting the secondary RGBA Channel from the primary texture channel
		//	tex2D(_RenderPaintTexture, IN.uv_RenderPaintTexture) = c;
			o.Albedo =  c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
