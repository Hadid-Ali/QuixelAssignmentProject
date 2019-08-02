Shader "QuixelTest/StandardCircle" {
	Properties{
		_CircleColor("Circle Color", Color) = (1,1,1,1)
		_BoundaryColor("Boundary Color", Color) = (0,0,0,1)
		[HideInInspector]_CircleMask("Circle Mask", 2D) = "white" {}
		_CircleCutoff("Circle Cutoff", Range(0,1)) = 0.5
		_BoundaryCutoff("Boundary Cutoff", Range(0,1)) = 0.5
			_Radius("Radius",Range(0.1,1.9)) = 1
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
	#pragma surface surf Standard fullforwardshadows

			// Use shader model 3.0 target, to get nicer looking lighting
	#pragma target 3.0

			sampler2D _CircleMask;

		struct Input {
			float2 uv_CircleMask;
		};

		fixed4 _CircleColor;
		fixed4 _BoundaryColor;
		half _CircleCutoff;
		half _BoundaryCutoff;
		half _Radius;

		void surf(Input IN, inout SurfaceOutputStandard o) {

			fixed x = (-0.5 + IN.uv_CircleMask.x) * (2 / _Radius);
			fixed y = (-0.5 + IN.uv_CircleMask.y) * (2 / _Radius);

			// Albedo comes from a texture tinted by color
			fixed radius = 1 - sqrt(x * x + y * y);
			clip(radius - _BoundaryCutoff);
			o.Albedo = _BoundaryColor;//* _Radius;
			if (radius > _CircleCutoff) {
				o.Albedo = _CircleColor;// *_Radius;
			}
			o.Alpha = 1;
		}
		ENDCG
		}
			FallBack "Diffuse"
}