// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Voxel/Glow" {
Properties {


	//_Color ("Main Color", Color) = (1,1,1,1)
	//_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	//_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	//_MKGlowColor ("Glow Color", Color) = (1,1,1,1)
	_MKGlowPower ("Glow Power", Range(0.0,1.0)) = 1.0
	//_MKGlowTex ("Glow Texture", 2D) = "black" {}
	//_MKGlowTexColor ("Glow Texture Color", Color) = (1,1,1,1)
	//_MKGlowTexStrength ("Glow Texture Strength ", Range(0.0,1.0)) = 1.0
	_MKGlowOffSet ("Glow Width ", Range(0.0,0.0755)) = 0.0
}
SubShader {

	//Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="MKGlow"}
	Tags {"RenderType"="MKGlow"}
	LOD 150

CGPROGRAM
#pragma surface surf Lambert noforwardadd


uniform half _MKGlowPower;

struct Input {
	float2 uv_MainTex;
	fixed3 color : COLOR;
	float4 vertex : POSITION; 

};

void surf (Input IN, inout SurfaceOutput o) {
	//float2 uv= IN.uv_MainTex.x;
	//float ab = uv.x;
//	float a = ab;
	o.Albedo = IN.color ;// *  a;
	o.Emission = IN.color * IN.uv_MainTex.x *_MKGlowPower;
	o.Alpha = 1;

	//o.	 = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
}
ENDCG
}

Fallback "Mobile/VertexLit"
}
