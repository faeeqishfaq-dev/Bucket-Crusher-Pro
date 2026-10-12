Shader "Voodoo/WaterUnlit" {
	Properties {
		_MainColor ("Main Color", Vector) = (0.1132075,0.7023883,1,1)
		_FoamColor ("Foam Color", Vector) = (1,1,1,1)
		_ShadowColor ("ShadowColor", Vector) = (0.2242791,0.372331,0.6792453,0.5764706)
		_MainTexture ("Main Texture", 2D) = "white" {}
		_WorldScale ("World Scale", Range(0.001, 1)) = 1
		_Sharpness ("Sharpness", Range(0, 1)) = 0.5
		_SecondaryFoam ("Secondary Foam", Range(0, 1)) = 0.5
		_FadeDistance ("Fade Distance", Range(0, 100)) = 10
		_HSpeed ("H Speed", Range(-1, 1)) = 0.1
		_VSpeed ("V Speed", Range(-1, 1)) = 0.1
		[Toggle(_DISTORTION_ON)] _Distortion ("Texture Ripples", Float) = 0
		_DistortionScale ("Distortion Scale", Range(0.001, 5)) = 1
		_DistortionIntensity ("Distortion Intensity", Range(0, 1)) = 0
		[HideInInspector] __dirty ("", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_ObjectToWorld;
			float4x4 unity_MatrixVP;

			struct Vertex_Stage_Input
			{
				float4 pos : POSITION;
			};

			struct Vertex_Stage_Output
			{
				float4 pos : SV_POSITION;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input)
			{
				Vertex_Stage_Output output;
				output.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, input.pos));
				return output;
			}

			float4 frag(Vertex_Stage_Output input) : SV_TARGET
			{
				return float4(1.0, 1.0, 1.0, 1.0); // RGBA
			}

			ENDHLSL
		}
	}
	Fallback "Diffuse"
	//CustomEditor "ASEMaterialInspector"
}