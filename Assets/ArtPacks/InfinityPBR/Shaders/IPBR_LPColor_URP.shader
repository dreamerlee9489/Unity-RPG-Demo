// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "InfinityPBR/LPColor"
{
	Properties
	{
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[ASEBegin]_Metal("Metal", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0.2
		_MainTex("ColorID", 2D) = "white" {}
		_ColorIDRange("ColorIDRange", Range( 0 , 1)) = 0
		_ColorIDFuzziness("ColorIDFuzziness", Range( 0 , 1)) = 0
		_Color0("Color 0", Color) = (1,0,0,0)
		_Color1("Color 1", Color) = (0,1,0,0)
		_Color2("Color 2", Color) = (0,0,1,0)
		_Color3("Color 3", Color) = (1,1,0,0)
		_Color4("Color 4", Color) = (1,0,1,0)
		_Color5("Color 5", Color) = (0,1,1,0)
		_Color6("Color 6", Color) = (1,0.5019608,0,0)
		_Color7("Color 7", Color) = (1,0,0.5019608,0)
		_Color8("Color 8", Color) = (0.5019608,1,0,0)
		_Color9("Color 9", Color) = (0,1,0.5019608,0)
		_Color10("Color 10", Color) = (0.5019608,0,1,0)
		_Color11("Color 11", Color) = (0,0.5019608,1,0)
		_Color12("Color 12", Color) = (1,0.5019608,0.5019608,0)
		_Color13("Color 13", Color) = (0.5019608,1,0.5019608,0)
		_Color14("Color 14", Color) = (0.5019608,0.5019608,1,0)
		_Color15("Color 15", Color) = (1,1,0.5019608,0)
		_Color16("Color 16", Color) = (1,0.5019608,1,0)
		_Color17("Color 17", Color) = (0.5019608,1,1,0)
		_Color18("Color 18", Color) = (0.3921569,0,0,0)
		_Color19("Color 19", Color) = (0,0.3921569,0,0)
		_Color20("Color 20", Color) = (0,0,0.3921569,0)
		_Color21("Color 21", Color) = (0.3921569,0.3921569,0,0)
		_Color22("Color 22", Color) = (0.3921569,0,0.3921569,0)
		_Color23("Color 23", Color) = (0,0.3921569,0.3921569,0)
		_Color24("Color 24", Color) = (0.1960784,0,0,0)
		_Color25("Color 25", Color) = (0,0.1960784,0,0)
		_Color26("Color 26", Color) = (0,0,0.1960784,0)
		_Color27("Color 27", Color) = (0.1960784,0.1960784,0,0)
		_Color28("Color 28", Color) = (0.1960784,0,0.1960784,0)
		_Color29("Color 29", Color) = (0,0.1960784,0.1960784,0)
		_Color30("Color 30", Color) = (0,0.1960784,0.1960784,0)
		_Color31("Color 31", Color) = (0,0.1960784,0.1960784,0)
		_Color32("Color 32", Color) = (0,0.1960784,0.1960784,0)
		_Color33("Color 33", Color) = (0,0.1960784,0.1960784,0)
		_Color34("Color 34", Color) = (0,0.1960784,0.1960784,0)
		_Color35("Color 35", Color) = (0,0.1960784,0.1960784,0)
		_Color36("Color 36", Color) = (0,0.1960784,0.1960784,0)
		_Color37("Color 37", Color) = (0,0.1960784,0.1960784,0)
		_Color38("Color 38", Color) = (0,0.1960784,0.1960784,0)
		_Color39("Color 39", Color) = (0,0.1960784,0.1960784,0)
		_Color40("Color 40", Color) = (0,0.1960784,0.1960784,0)
		_Color41("Color 41", Color) = (0,0.1960784,0.1960784,0)
		_Color42("Color 42", Color) = (0,0.1960784,0.1960784,0)
		_Color43("Color 43", Color) = (0,0.1960784,0.1960784,0)
		_Color44("Color 44", Color) = (0,0.1960784,0.1960784,0)
		_Color45("Color 45", Color) = (0,0.1960784,0.1960784,0)
		_Color47("Color 47", Color) = (0,0.1960784,0.1960784,0)
		_Color48("Color 48", Color) = (0,0.1960784,0.1960784,0)
		_Color46("Color 46", Color) = (0,0.1960784,0.1960784,0)
		_H0("H0", Range( 0 , 1)) = 0.5
		_S0("S0", Range( 0 , 1)) = 0.2559484
		_V0("V0", Range( 0 , 1)) = 0.5
		_H1("H1", Range( 0 , 1)) = 0.5
		_S1("S1", Range( 0 , 1)) = 0.5
		_V1("V1", Range( 0 , 1)) = 0.5
		_H2("H2", Range( 0 , 1)) = 0.5
		_S2("S2", Range( 0 , 1)) = 0.5
		_V2("V2", Range( 0 , 1)) = 0.5
		_H3("H3", Range( 0 , 1)) = 0.5
		_S3("S3", Range( 0 , 1)) = 0.5
		_V3("V3", Range( 0 , 1)) = 0.5
		_H4("H4", Range( 0 , 1)) = 0.5
		_S4("S4", Range( 0 , 1)) = 0.5
		_V4("V4", Range( 0 , 1)) = 0.5
		_H5("H5", Range( 0 , 1)) = 0.5
		_S5("S5", Range( 0 , 1)) = 0.5
		_V5("V5", Range( 0 , 1)) = 0.5
		_H6("H6", Range( 0 , 1)) = 0.5
		_S6("S6", Range( 0 , 1)) = 0.5
		_V6("V6", Range( 0 , 1)) = 0.5
		_H7("H7", Range( 0 , 1)) = 0.5
		_S7("S7", Range( 0 , 1)) = 0.5
		_V7("V7", Range( 0 , 1)) = 0.5
		_H8("H8", Range( 0 , 1)) = 0.5
		_S8("S8", Range( 0 , 1)) = 0.5
		_V8("V8", Range( 0 , 1)) = 0.5
		_H9("H9", Range( 0 , 1)) = 0.5
		_S9("S9", Range( 0 , 1)) = 0.5
		_V9("V9", Range( 0 , 1)) = 0.5
		_H10("H10", Range( 0 , 1)) = 0.5
		_V10("V10", Range( 0 , 1)) = 0.5
		_S10("S10", Range( 0 , 1)) = 0.5
		_H11("H11", Range( 0 , 1)) = 0.5
		_S11("S11", Range( 0 , 1)) = 0.5
		_V11("V11", Range( 0 , 1)) = 0.5
		_H12("H12", Range( 0 , 1)) = 0.5
		_S12("S12", Range( 0 , 1)) = 0.5
		_V12("V12", Range( 0 , 1)) = 0.5
		_H13("H13", Range( 0 , 1)) = 0.5
		_S13("S13", Range( 0 , 1)) = 0.5
		_V13("V13", Range( 0 , 1)) = 0.5
		_H14("H14", Range( 0 , 1)) = 0.5
		_S14("S14", Range( 0 , 1)) = 0.5
		_V14("V14", Range( 0 , 1)) = 0.5
		_H15("H15", Range( 0 , 1)) = 0.5
		_S15("S15", Range( 0 , 1)) = 0.5
		_V15("V15", Range( 0 , 1)) = 0.5
		_H16("H16", Range( 0 , 1)) = 0.5
		_S16("S16", Range( 0 , 1)) = 0.5
		_V16("V16", Range( 0 , 1)) = 0.5
		_H17("H17", Range( 0 , 1)) = 0.5
		_S17("S17", Range( 0 , 1)) = 0.5
		_V17("V17", Range( 0 , 1)) = 0.5
		_H18("H18", Range( 0 , 1)) = 0.5
		_S18("S18", Range( 0 , 1)) = 0.5
		_V18("V18", Range( 0 , 1)) = 0.5
		_H19("H19", Range( 0 , 1)) = 0.5
		_S19("S19", Range( 0 , 1)) = 0.5
		_V19("V19", Range( 0 , 1)) = 0.5
		_H20("H20", Range( 0 , 1)) = 0.5
		_S20("S20", Range( 0 , 1)) = 0.5
		_V20("V20", Range( 0 , 1)) = 0.5
		_H21("H21", Range( 0 , 1)) = 0.5
		_S21("S21", Range( 0 , 1)) = 0.5
		_V21("V21", Range( 0 , 1)) = 0.5
		_H22("H22", Range( 0 , 1)) = 0.5
		_S22("S22", Range( 0 , 1)) = 0.5
		_V22("V22", Range( 0 , 1)) = 0.5
		_H23("H23", Range( 0 , 1)) = 0.5
		_S23("S23", Range( 0 , 1)) = 0.5
		_V23("V23", Range( 0 , 1)) = 0.5
		_H24("H24", Range( 0 , 1)) = 0.5
		_S24("S24", Range( 0 , 1)) = 0.5
		_V24("V24", Range( 0 , 1)) = 0.5
		_H25("H25", Range( 0 , 1)) = 0.5
		_S25("S25", Range( 0 , 1)) = 0.5
		_V25("V25", Range( 0 , 1)) = 0.5
		_H26("H26", Range( 0 , 1)) = 0.5
		_S26("S26", Range( 0 , 1)) = 0.5
		_V26("V26", Range( 0 , 1)) = 0.5
		_H27("H27", Range( 0 , 1)) = 0.5
		_S27("S27", Range( 0 , 1)) = 0.5
		_V27("V27", Range( 0 , 1)) = 0.5
		_H28("H28", Range( 0 , 1)) = 0.5
		_S28("S28", Range( 0 , 1)) = 0.5
		_V28("V28", Range( 0 , 1)) = 0.5
		_H38("H38", Range( 0 , 1)) = 0.5
		_H34("H34", Range( 0 , 1)) = 0.5
		_H35("H35", Range( 0 , 1)) = 0.5
		_H37("H37", Range( 0 , 1)) = 0.5
		_H33("H33", Range( 0 , 1)) = 0.5
		_H40("H40", Range( 0 , 1)) = 0.5
		_H39("H39", Range( 0 , 1)) = 0.5
		_H36("H36", Range( 0 , 1)) = 0.5
		_H32("H32", Range( 0 , 1)) = 0.5
		_H43("H43", Range( 0 , 1)) = 0.5
		_H41("H41", Range( 0 , 1)) = 0.5
		_H47("H47", Range( 0 , 1)) = 0
		_H45("H45", Range( 0 , 1)) = 0
		_H42("H42", Range( 0 , 1)) = 0.5
		_H46("H46", Range( 0 , 1)) = 0
		_H48("H48", Range( 0 , 1)) = 0
		_H44("H44", Range( 0 , 1)) = 0
		_H31("H31", Range( 0 , 1)) = 0.5
		_H29("H29", Range( 0 , 1)) = 0.5
		_H30("H30", Range( 0 , 1)) = 0.5
		_S37("S37", Range( 0 , 1)) = 0.5
		_S44("S44", Range( 0 , 1)) = 0.5
		_S48("S48", Range( 0 , 1)) = 0.5
		_S41("S41", Range( 0 , 1)) = 0.5
		_S42("S42", Range( 0 , 1)) = 0.5
		_S43("S43", Range( 0 , 1)) = 0.5
		_S46("S46", Range( 0 , 1)) = 0.5
		_S45("S45", Range( 0 , 1)) = 0.5
		_S47("S47", Range( 0 , 1)) = 0.7404315
		_S40("S40", Range( 0 , 1)) = 0.5
		_S29("S29", Range( 0 , 1)) = 0.5
		_S39("S39", Range( 0 , 1)) = 0.5
		_S38("S38", Range( 0 , 1)) = 0.5
		_S32("S32", Range( 0 , 1)) = 0.5
		_S30("S30", Range( 0 , 1)) = 0.5
		_S31("S31", Range( 0 , 1)) = 0.5
		_S33("S33", Range( 0 , 1)) = 0.5
		_S35("S35", Range( 0 , 1)) = 0.5
		_S34("S34", Range( 0 , 1)) = 0.5
		_S36("S36", Range( 0 , 1)) = 0.5
		_V36("V36", Range( 0 , 1)) = 0.5
		_V33("V33", Range( 0 , 1)) = 0.5
		_V40("V40", Range( 0 , 1)) = 0.5
		_V31("V31", Range( 0 , 1)) = 0.5
		_V39("V39", Range( 0 , 1)) = 0.5
		_V34("V34", Range( 0 , 1)) = 0.5
		_V38("V38", Range( 0 , 1)) = 0.5
		_V41("V41", Range( 0 , 1)) = 0.5
		_V47("V47", Range( 0 , 1)) = 0.5
		_V46("V46", Range( 0 , 1)) = 0.5
		_V45("V45", Range( 0 , 1)) = 0.5
		_V44("V44", Range( 0 , 1)) = 0.5
		_V43("V43", Range( 0 , 1)) = 0.5
		_V42("V42", Range( 0 , 1)) = 0.5
		_V48("V48", Range( 0 , 1)) = 0.5
		_V37("V37", Range( 0 , 1)) = 0.5
		_V35("V35", Range( 0 , 1)) = 0.5
		_V30("V30", Range( 0 , 1)) = 0.5
		_V29("V29", Range( 0 , 1)) = 0.5
		[ASEEnd]_V32("V32", Range( 0 , 1)) = 0.5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

		//_TransmissionShadow( "Transmission Shadow", Range( 0, 1 ) ) = 0.5
		//_TransStrength( "Trans Strength", Range( 0, 50 ) ) = 1
		//_TransNormal( "Trans Normal Distortion", Range( 0, 1 ) ) = 0.5
		//_TransScattering( "Trans Scattering", Range( 1, 50 ) ) = 2
		//_TransDirect( "Trans Direct", Range( 0, 1 ) ) = 0.9
		//_TransAmbient( "Trans Ambient", Range( 0, 1 ) ) = 0.1
		//_TransShadow( "Trans Shadow", Range( 0, 1 ) ) = 0.5
		//_TessPhongStrength( "Tess Phong Strength", Range( 0, 1 ) ) = 0.5
		//_TessValue( "Tess Max Tessellation", Range( 1, 32 ) ) = 16
		//_TessMin( "Tess Min Distance", Float ) = 10
		//_TessMax( "Tess Max Distance", Float ) = 25
		//_TessEdgeLength ( "Tess Edge length", Range( 2, 50 ) ) = 16
		//_TessMaxDisp( "Tess Max Displacement", Float ) = 25
	}

	SubShader
	{
		LOD 0

		

		Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Opaque" "Queue"="Geometry" }
		Cull Back
		AlphaToMask Off
		HLSLINCLUDE
		#pragma target 2.0

		float4 FixedTess( float tessValue )
		{
			return tessValue;
		}
		
		float CalcDistanceTessFactor (float4 vertex, float minDist, float maxDist, float tess, float4x4 o2w, float3 cameraPos )
		{
			float3 wpos = mul(o2w,vertex).xyz;
			float dist = distance (wpos, cameraPos);
			float f = clamp(1.0 - (dist - minDist) / (maxDist - minDist), 0.01, 1.0) * tess;
			return f;
		}

		float4 CalcTriEdgeTessFactors (float3 triVertexFactors)
		{
			float4 tess;
			tess.x = 0.5 * (triVertexFactors.y + triVertexFactors.z);
			tess.y = 0.5 * (triVertexFactors.x + triVertexFactors.z);
			tess.z = 0.5 * (triVertexFactors.x + triVertexFactors.y);
			tess.w = (triVertexFactors.x + triVertexFactors.y + triVertexFactors.z) / 3.0f;
			return tess;
		}

		float CalcEdgeTessFactor (float3 wpos0, float3 wpos1, float edgeLen, float3 cameraPos, float4 scParams )
		{
			float dist = distance (0.5 * (wpos0+wpos1), cameraPos);
			float len = distance(wpos0, wpos1);
			float f = max(len * scParams.y / (edgeLen * dist), 1.0);
			return f;
		}

		float DistanceFromPlane (float3 pos, float4 plane)
		{
			float d = dot (float4(pos,1.0f), plane);
			return d;
		}

		bool WorldViewFrustumCull (float3 wpos0, float3 wpos1, float3 wpos2, float cullEps, float4 planes[6] )
		{
			float4 planeTest;
			planeTest.x = (( DistanceFromPlane(wpos0, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[0]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.y = (( DistanceFromPlane(wpos0, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[1]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.z = (( DistanceFromPlane(wpos0, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[2]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.w = (( DistanceFromPlane(wpos0, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[3]) > -cullEps) ? 1.0f : 0.0f );
			return !all (planeTest);
		}

		float4 DistanceBasedTess( float4 v0, float4 v1, float4 v2, float tess, float minDist, float maxDist, float4x4 o2w, float3 cameraPos )
		{
			float3 f;
			f.x = CalcDistanceTessFactor (v0,minDist,maxDist,tess,o2w,cameraPos);
			f.y = CalcDistanceTessFactor (v1,minDist,maxDist,tess,o2w,cameraPos);
			f.z = CalcDistanceTessFactor (v2,minDist,maxDist,tess,o2w,cameraPos);

			return CalcTriEdgeTessFactors (f);
		}

		float4 EdgeLengthBasedTess( float4 v0, float4 v1, float4 v2, float edgeLength, float4x4 o2w, float3 cameraPos, float4 scParams )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;
			tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
			tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
			tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
			tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			return tess;
		}

		float4 EdgeLengthBasedTessCull( float4 v0, float4 v1, float4 v2, float edgeLength, float maxDisplacement, float4x4 o2w, float3 cameraPos, float4 scParams, float4 planes[6] )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;

			if (WorldViewFrustumCull(pos0, pos1, pos2, maxDisplacement, planes))
			{
				tess = 0.0f;
			}
			else
			{
				tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
				tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
				tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
				tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			}
			return tess;
		}
		ENDHLSL

		
		Pass
		{
			
			Name "Forward"
			Tags { "LightMode"="UniversalForward" }
			
			Blend One Zero, One Zero
			ZWrite On
			ZTest LEqual
			Offset 0 , 0
			ColorMask RGBA
			

			HLSLPROGRAM
			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma multi_compile _ LOD_FADE_CROSSFADE
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define ASE_SRP_VERSION 80200

			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x

			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS
			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
			#pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
			#pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
			#pragma multi_compile _ _SHADOWS_SOFT
			#pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE
			
			#pragma multi_compile _ DIRLIGHTMAP_COMBINED
			#pragma multi_compile _ LIGHTMAP_ON

			#pragma vertex vert
			#pragma fragment frag

			#define SHADERPASS_FORWARD

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			
			#if ASE_SRP_VERSION <= 70108
			#define REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
			#endif

			#if defined(UNITY_INSTANCING_ENABLED) && defined(_TERRAIN_INSTANCED_PERPIXEL_NORMAL)
			    #define ENABLE_TERRAIN_PERPIXEL_NORMAL
			#endif

			

			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_tangent : TANGENT;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord : TEXCOORD0;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				float4 lightmapUVOrVertexSH : TEXCOORD0;
				half4 fogFactorAndVertexLight : TEXCOORD1;
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
				float4 shadowCoord : TEXCOORD2;
				#endif
				float4 tSpace0 : TEXCOORD3;
				float4 tSpace1 : TEXCOORD4;
				float4 tSpace2 : TEXCOORD5;
				#if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
				float4 screenPos : TEXCOORD6;
				#endif
				float4 ase_texcoord7 : TEXCOORD7;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Color27;
			float4 _Color42;
			float4 _Color7;
			float4 _Color16;
			float4 _Color31;
			float4 _Color8;
			float4 _Color41;
			float4 _Color21;
			float4 _Color9;
			float4 _Color40;
			float4 _Color32;
			float4 _Color10;
			float4 _Color20;
			float4 _Color22;
			float4 _Color39;
			float4 _Color19;
			float4 _Color12;
			float4 _Color38;
			float4 _Color33;
			float4 _Color13;
			float4 _Color37;
			float4 _Color18;
			float4 _Color14;
			float4 _Color34;
			float4 _Color36;
			float4 _Color15;
			float4 _Color17;
			float4 _Color11;
			float4 _Color6;
			float4 _Color35;
			float4 _Color43;
			float4 _Color26;
			float4 _Color29;
			float4 _Color45;
			float4 _Color3;
			float4 _Color1;
			float4 _Color24;
			float4 _MainTex_ST;
			float4 _Color2;
			float4 _Color0;
			float4 _Color4;
			float4 _Color46;
			float4 _Color44;
			float4 _Color48;
			float4 _Color23;
			float4 _Color5;
			float4 _Color30;
			float4 _Color25;
			float4 _Color28;
			float4 _Color47;
			float _S34;
			float _V34;
			float _S28;
			float _H35;
			float _S35;
			float _V28;
			float _H34;
			float _S29;
			float _V33;
			float _S33;
			float _H31;
			float _H33;
			float _V29;
			float _V32;
			float _S32;
			float _H30;
			float _S30;
			float _H32;
			float _V30;
			float _S31;
			float _H29;
			float _V31;
			float _V38;
			float _H36;
			float _S43;
			float _V43;
			float _H44;
			float _S44;
			float _V44;
			float _H45;
			float _S45;
			float _V45;
			float _H46;
			float _S46;
			float _V46;
			float _H47;
			float _S47;
			float _V47;
			float _H48;
			float _S48;
			float _V48;
			float _H43;
			float _V35;
			float _V42;
			float _H42;
			float _S36;
			float _V36;
			float _H37;
			float _S37;
			float _V37;
			float _H38;
			float _S38;
			float _H28;
			float _H39;
			float _S39;
			float _V39;
			float _H40;
			float _S40;
			float _V40;
			float _H41;
			float _S41;
			float _V41;
			float _S42;
			float _H0;
			float _S24;
			float _S27;
			float _H7;
			float _S7;
			float _V7;
			float _H8;
			float _S8;
			float _V8;
			float _H9;
			float _S9;
			float _V9;
			float _H10;
			float _S10;
			float _V10;
			float _H11;
			float _S11;
			float _V11;
			float _H12;
			float _S12;
			float _V6;
			float _S6;
			float _H6;
			float _V5;
			float _S0;
			float _V0;
			float _ColorIDRange;
			float _ColorIDFuzziness;
			float _H1;
			float _S1;
			float _V1;
			float _H2;
			float _V12;
			float _S2;
			float _H3;
			float _S3;
			float _V3;
			float _H4;
			float _S4;
			float _V4;
			float _H5;
			float _S5;
			float _V2;
			float _H13;
			float _S13;
			float _V13;
			float _S21;
			float _V21;
			float _H22;
			float _S22;
			float _V22;
			float _H23;
			float _S23;
			float _V23;
			float _H21;
			float _H24;
			float _V24;
			float _H25;
			float _S25;
			float _V25;
			float _H26;
			float _S26;
			float _V26;
			float _H27;
			float _Metal;
			float _V27;
			float _V20;
			float _H20;
			float _H14;
			float _S14;
			float _V14;
			float _H15;
			float _S15;
			float _V15;
			float _H16;
			float _S16;
			float _S20;
			float _V16;
			float _S17;
			float _V17;
			float _H18;
			float _S18;
			float _V18;
			float _H19;
			float _S19;
			float _V19;
			float _H17;
			float _Smoothness;
			#ifdef _TRANSMISSION_ASE
				float _TransmissionShadow;
			#endif
			#ifdef _TRANSLUCENCY_ASE
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D _MainTex;


			float3 HSVToRGB( float3 c )
			{
				float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
				float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
				return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
			}
			

			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.ase_texcoord7.xy = v.texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord7.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float3 positionVS = TransformWorldToView( positionWS );
				float4 positionCS = TransformWorldToHClip( positionWS );

				VertexNormalInputs normalInput = GetVertexNormalInputs( v.ase_normal, v.ase_tangent );

				o.tSpace0 = float4( normalInput.normalWS, positionWS.x);
				o.tSpace1 = float4( normalInput.tangentWS, positionWS.y);
				o.tSpace2 = float4( normalInput.bitangentWS, positionWS.z);

				OUTPUT_LIGHTMAP_UV( v.texcoord1, unity_LightmapST, o.lightmapUVOrVertexSH.xy );
				OUTPUT_SH( normalInput.normalWS.xyz, o.lightmapUVOrVertexSH.xyz );

				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					o.lightmapUVOrVertexSH.zw = v.texcoord;
					o.lightmapUVOrVertexSH.xy = v.texcoord * unity_LightmapST.xy + unity_LightmapST.zw;
				#endif

				half3 vertexLight = VertexLighting( positionWS, normalInput.normalWS );
				#ifdef ASE_FOG
					half fogFactor = ComputeFogFactor( positionCS.z );
				#else
					half fogFactor = 0;
				#endif
				o.fogFactorAndVertexLight = half4(fogFactor, vertexLight);
				
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
				VertexPositionInputs vertexInput = (VertexPositionInputs)0;
				vertexInput.positionWS = positionWS;
				vertexInput.positionCS = positionCS;
				o.shadowCoord = GetShadowCoord( vertexInput );
				#endif
				
				o.clipPos = positionCS;
				#if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
				o.screenPos = ComputeScreenPos(positionCS);
				#endif
				return o;
			}
			
			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_tangent : TANGENT;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_tangent = v.ase_tangent;
				o.texcoord = v.texcoord;
				o.texcoord1 = v.texcoord1;
				
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_tangent = patch[0].ase_tangent * bary.x + patch[1].ase_tangent * bary.y + patch[2].ase_tangent * bary.z;
				o.texcoord = patch[0].texcoord * bary.x + patch[1].texcoord * bary.y + patch[2].texcoord * bary.z;
				o.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag ( VertexOutput IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition( IN.clipPos.xyz, unity_LODFade.x );
				#endif

				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					float2 sampleCoords = (IN.lightmapUVOrVertexSH.zw / _TerrainHeightmapRecipSize.zw + 0.5f) * _TerrainHeightmapRecipSize.xy;
					float3 WorldNormal = TransformObjectToWorldNormal(normalize(SAMPLE_TEXTURE2D(_TerrainNormalmapTexture, sampler_TerrainNormalmapTexture, sampleCoords).rgb * 2 - 1));
					float3 WorldTangent = -cross(GetObjectToWorldMatrix()._13_23_33, WorldNormal);
					float3 WorldBiTangent = cross(WorldNormal, -WorldTangent);
				#else
					float3 WorldNormal = normalize( IN.tSpace0.xyz );
					float3 WorldTangent = IN.tSpace1.xyz;
					float3 WorldBiTangent = IN.tSpace2.xyz;
				#endif
				float3 WorldPosition = float3(IN.tSpace0.w,IN.tSpace1.w,IN.tSpace2.w);
				float3 WorldViewDirection = _WorldSpaceCameraPos.xyz  - WorldPosition;
				float4 ShadowCoords = float4( 0, 0, 0, 0 );
				#if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
				float4 ScreenPos = IN.screenPos;
				#endif

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
					ShadowCoords = IN.shadowCoord;
				#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
					ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
				#endif
	
				WorldViewDirection = SafeNormalize( WorldViewDirection );

				float3 hsvTorgb78 = HSVToRGB( float3(_H0,_S0,_V0) );
				float2 uv_MainTex = IN.ase_texcoord7.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode2 = tex2D( _MainTex, uv_MainTex );
				float3 lerpResult267 = lerp( hsvTorgb78 , hsvTorgb78 , saturate( ( 1.0 - ( ( distance( _Color0.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb90 = HSVToRGB( float3(_H1,_S1,_V1) );
				float3 lerpResult86 = lerp( lerpResult267 , hsvTorgb90 , saturate( ( 1.0 - ( ( distance( _Color1.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb91 = HSVToRGB( float3(_H2,_S2,_V2) );
				float3 lerpResult107 = lerp( lerpResult86 , hsvTorgb91 , saturate( ( 1.0 - ( ( distance( _Color2.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb98 = HSVToRGB( float3(_H3,_S3,_V3) );
				float3 lerpResult108 = lerp( lerpResult107 , hsvTorgb98 , saturate( ( 1.0 - ( ( distance( _Color3.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb99 = HSVToRGB( float3(_H4,_S4,_V4) );
				float3 lerpResult109 = lerp( lerpResult108 , hsvTorgb99 , saturate( ( 1.0 - ( ( distance( _Color4.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb106 = HSVToRGB( float3(_H5,_S5,_V5) );
				float3 lerpResult110 = lerp( lerpResult109 , hsvTorgb106 , saturate( ( 1.0 - ( ( distance( _Color5.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb114 = HSVToRGB( float3(_H6,_S6,_V6) );
				float3 lerpResult115 = lerp( lerpResult110 , hsvTorgb114 , saturate( ( 1.0 - ( ( distance( _Color6.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb120 = HSVToRGB( float3(_H7,_S7,_V7) );
				float3 lerpResult124 = lerp( lerpResult115 , hsvTorgb120 , saturate( ( 1.0 - ( ( distance( _Color7.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb123 = HSVToRGB( float3(_H8,_S8,_V8) );
				float3 lerpResult128 = lerp( lerpResult124 , hsvTorgb123 , saturate( ( 1.0 - ( ( distance( _Color8.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb132 = HSVToRGB( float3(_H9,_S9,_V9) );
				float3 lerpResult134 = lerp( lerpResult128 , hsvTorgb132 , saturate( ( 1.0 - ( ( distance( _Color9.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb133 = HSVToRGB( float3(_H10,_S10,_V10) );
				float3 lerpResult135 = lerp( lerpResult134 , hsvTorgb133 , saturate( ( 1.0 - ( ( distance( _Color10.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb138 = HSVToRGB( float3(_H11,_S11,_V11) );
				float3 lerpResult139 = lerp( lerpResult135 , hsvTorgb138 , saturate( ( 1.0 - ( ( distance( _Color11.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb142 = HSVToRGB( float3(_H12,_S12,_V12) );
				float3 lerpResult141 = lerp( lerpResult139 , hsvTorgb142 , saturate( ( 1.0 - ( ( distance( _Color12.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb150 = HSVToRGB( float3(_H13,_S13,_V13) );
				float3 lerpResult154 = lerp( lerpResult141 , hsvTorgb150 , saturate( ( 1.0 - ( ( distance( _Color13.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb153 = HSVToRGB( float3(_H14,_S14,_V14) );
				float3 lerpResult158 = lerp( lerpResult154 , hsvTorgb153 , saturate( ( 1.0 - ( ( distance( _Color14.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb162 = HSVToRGB( float3(_H15,_S15,_V15) );
				float3 lerpResult164 = lerp( lerpResult158 , hsvTorgb162 , saturate( ( 1.0 - ( ( distance( _Color15.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb163 = HSVToRGB( float3(_H16,_S16,_V16) );
				float3 lerpResult165 = lerp( lerpResult164 , hsvTorgb163 , saturate( ( 1.0 - ( ( distance( _Color16.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb168 = HSVToRGB( float3(_H17,_S17,_V17) );
				float3 lerpResult169 = lerp( lerpResult165 , hsvTorgb168 , saturate( ( 1.0 - ( ( distance( _Color17.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb172 = HSVToRGB( float3(_H18,_S18,_V18) );
				float3 lerpResult171 = lerp( lerpResult169 , hsvTorgb172 , saturate( ( 1.0 - ( ( distance( _Color18.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb180 = HSVToRGB( float3(_H19,_S19,_V19) );
				float3 lerpResult184 = lerp( lerpResult171 , hsvTorgb180 , saturate( ( 1.0 - ( ( distance( _Color19.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb183 = HSVToRGB( float3(_H20,_S20,_V20) );
				float3 lerpResult188 = lerp( lerpResult184 , hsvTorgb183 , saturate( ( 1.0 - ( ( distance( _Color20.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb192 = HSVToRGB( float3(_H21,_S21,_V21) );
				float3 lerpResult194 = lerp( lerpResult188 , hsvTorgb192 , saturate( ( 1.0 - ( ( distance( _Color21.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb193 = HSVToRGB( float3(_H22,_S22,_V22) );
				float3 lerpResult195 = lerp( lerpResult194 , hsvTorgb193 , saturate( ( 1.0 - ( ( distance( _Color22.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb198 = HSVToRGB( float3(_H23,_S23,_V23) );
				float3 lerpResult199 = lerp( lerpResult195 , hsvTorgb198 , saturate( ( 1.0 - ( ( distance( _Color23.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb202 = HSVToRGB( float3(_H24,_S24,_V24) );
				float3 lerpResult201 = lerp( lerpResult199 , hsvTorgb202 , saturate( ( 1.0 - ( ( distance( _Color24.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb210 = HSVToRGB( float3(_H25,_S25,_V25) );
				float3 lerpResult214 = lerp( lerpResult201 , hsvTorgb210 , saturate( ( 1.0 - ( ( distance( _Color25.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb213 = HSVToRGB( float3(_H26,_S26,_V26) );
				float3 lerpResult218 = lerp( lerpResult214 , hsvTorgb213 , saturate( ( 1.0 - ( ( distance( _Color26.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb222 = HSVToRGB( float3(_H27,_S27,_V27) );
				float3 lerpResult224 = lerp( lerpResult218 , hsvTorgb222 , saturate( ( 1.0 - ( ( distance( _Color27.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb223 = HSVToRGB( float3(_H28,_S28,_V28) );
				float3 lerpResult225 = lerp( lerpResult224 , hsvTorgb223 , saturate( ( 1.0 - ( ( distance( _Color28.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb228 = HSVToRGB( float3(_H29,_S29,_V29) );
				float3 lerpResult229 = lerp( lerpResult225 , hsvTorgb228 , saturate( ( 1.0 - ( ( distance( _Color29.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb383 = HSVToRGB( float3(_H30,_S30,_V30) );
				float3 lerpResult384 = lerp( lerpResult229 , hsvTorgb383 , saturate( ( 1.0 - ( ( distance( _Color30.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb386 = HSVToRGB( float3(_H31,_S31,_V31) );
				float3 lerpResult387 = lerp( lerpResult384 , hsvTorgb386 , saturate( ( 1.0 - ( ( distance( _Color31.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb391 = HSVToRGB( float3(_H32,_S32,_V32) );
				float3 lerpResult392 = lerp( lerpResult387 , hsvTorgb391 , saturate( ( 1.0 - ( ( distance( _Color32.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb396 = HSVToRGB( float3(_H33,_S33,_V33) );
				float3 lerpResult397 = lerp( lerpResult392 , hsvTorgb396 , saturate( ( 1.0 - ( ( distance( _Color33.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb401 = HSVToRGB( float3(_H34,_S34,_V34) );
				float3 lerpResult402 = lerp( lerpResult397 , hsvTorgb401 , saturate( ( 1.0 - ( ( distance( _Color34.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb406 = HSVToRGB( float3(_H35,_S35,_V35) );
				float3 lerpResult407 = lerp( lerpResult402 , hsvTorgb406 , saturate( ( 1.0 - ( ( distance( _Color35.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb411 = HSVToRGB( float3(_H36,_S36,_V36) );
				float3 lerpResult412 = lerp( lerpResult407 , hsvTorgb411 , saturate( ( 1.0 - ( ( distance( _Color36.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb416 = HSVToRGB( float3(_H37,_S37,_V37) );
				float3 lerpResult417 = lerp( lerpResult412 , hsvTorgb416 , saturate( ( 1.0 - ( ( distance( _Color37.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb421 = HSVToRGB( float3(_H38,_S38,_V38) );
				float3 lerpResult422 = lerp( lerpResult417 , hsvTorgb421 , saturate( ( 1.0 - ( ( distance( _Color38.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb426 = HSVToRGB( float3(_H39,_S39,_V39) );
				float3 lerpResult427 = lerp( lerpResult422 , hsvTorgb426 , saturate( ( 1.0 - ( ( distance( _Color39.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb431 = HSVToRGB( float3(_H40,_S40,_V40) );
				float3 lerpResult432 = lerp( lerpResult427 , hsvTorgb431 , saturate( ( 1.0 - ( ( distance( _Color40.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb436 = HSVToRGB( float3(_H41,_S41,_V41) );
				float3 lerpResult437 = lerp( lerpResult432 , hsvTorgb436 , saturate( ( 1.0 - ( ( distance( _Color41.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb446 = HSVToRGB( float3(_H42,_S42,_V42) );
				float3 lerpResult447 = lerp( lerpResult437 , hsvTorgb446 , saturate( ( 1.0 - ( ( distance( _Color42.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb453 = HSVToRGB( float3(_H43,_S43,_V43) );
				float3 lerpResult454 = lerp( lerpResult447 , hsvTorgb453 , saturate( ( 1.0 - ( ( distance( _Color43.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb460 = HSVToRGB( float3(_H44,_S44,_V44) );
				float3 lerpResult461 = lerp( lerpResult454 , hsvTorgb460 , saturate( ( 1.0 - ( ( distance( _Color44.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb467 = HSVToRGB( float3(_H45,_S45,_V45) );
				float3 lerpResult468 = lerp( lerpResult461 , hsvTorgb467 , saturate( ( 1.0 - ( ( distance( _Color45.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb474 = HSVToRGB( float3(_H46,_S46,_V46) );
				float3 lerpResult475 = lerp( lerpResult468 , hsvTorgb474 , saturate( ( 1.0 - ( ( distance( _Color46.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb481 = HSVToRGB( float3(_H47,_S47,_V47) );
				float3 lerpResult482 = lerp( lerpResult475 , hsvTorgb481 , saturate( ( 1.0 - ( ( distance( _Color47.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb488 = HSVToRGB( float3(_H48,_S48,_V48) );
				float3 lerpResult489 = lerp( lerpResult482 , hsvTorgb488 , saturate( ( 1.0 - ( ( distance( _Color48.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				
				float3 Albedo = lerpResult489;
				float3 Normal = float3(0, 0, 1);
				float3 Emission = 0;
				float3 Specular = 0.5;
				float Metallic = _Metal;
				float Smoothness = _Smoothness;
				float Occlusion = 1;
				float Alpha = 1;
				float AlphaClipThreshold = 0.5;
				float AlphaClipThresholdShadow = 0.5;
				float3 BakedGI = 0;
				float3 RefractionColor = 1;
				float RefractionIndex = 1;
				float3 Transmission = 1;
				float3 Translucency = 1;

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				InputData inputData;
				inputData.positionWS = WorldPosition;
				inputData.viewDirectionWS = WorldViewDirection;
				inputData.shadowCoord = ShadowCoords;

				#ifdef _NORMALMAP
					#if _NORMAL_DROPOFF_TS
					inputData.normalWS = TransformTangentToWorld(Normal, half3x3( WorldTangent, WorldBiTangent, WorldNormal ));
					#elif _NORMAL_DROPOFF_OS
					inputData.normalWS = TransformObjectToWorldNormal(Normal);
					#elif _NORMAL_DROPOFF_WS
					inputData.normalWS = Normal;
					#endif
					inputData.normalWS = NormalizeNormalPerPixel(inputData.normalWS);
				#else
					inputData.normalWS = WorldNormal;
				#endif

				#ifdef ASE_FOG
					inputData.fogCoord = IN.fogFactorAndVertexLight.x;
				#endif

				inputData.vertexLighting = IN.fogFactorAndVertexLight.yzw;
				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					float3 SH = SampleSH(inputData.normalWS.xyz);
				#else
					float3 SH = IN.lightmapUVOrVertexSH.xyz;
				#endif

				inputData.bakedGI = SAMPLE_GI( IN.lightmapUVOrVertexSH.xy, SH, inputData.normalWS );
				#ifdef _ASE_BAKEDGI
					inputData.bakedGI = BakedGI;
				#endif
				half4 color = UniversalFragmentPBR(
					inputData, 
					Albedo, 
					Metallic, 
					Specular, 
					Smoothness, 
					Occlusion, 
					Emission, 
					Alpha);

				#ifdef _TRANSMISSION_ASE
				{
					float shadow = _TransmissionShadow;

					Light mainLight = GetMainLight( inputData.shadowCoord );
					float3 mainAtten = mainLight.color * mainLight.distanceAttenuation;
					mainAtten = lerp( mainAtten, mainAtten * mainLight.shadowAttenuation, shadow );
					half3 mainTransmission = max(0 , -dot(inputData.normalWS, mainLight.direction)) * mainAtten * Transmission;
					color.rgb += Albedo * mainTransmission;

					#ifdef _ADDITIONAL_LIGHTS
						int transPixelLightCount = GetAdditionalLightsCount();
						for (int i = 0; i < transPixelLightCount; ++i)
						{
							Light light = GetAdditionalLight(i, inputData.positionWS);
							float3 atten = light.color * light.distanceAttenuation;
							atten = lerp( atten, atten * light.shadowAttenuation, shadow );

							half3 transmission = max(0 , -dot(inputData.normalWS, light.direction)) * atten * Transmission;
							color.rgb += Albedo * transmission;
						}
					#endif
				}
				#endif

				#ifdef _TRANSLUCENCY_ASE
				{
					float shadow = _TransShadow;
					float normal = _TransNormal;
					float scattering = _TransScattering;
					float direct = _TransDirect;
					float ambient = _TransAmbient;
					float strength = _TransStrength;

					Light mainLight = GetMainLight( inputData.shadowCoord );
					float3 mainAtten = mainLight.color * mainLight.distanceAttenuation;
					mainAtten = lerp( mainAtten, mainAtten * mainLight.shadowAttenuation, shadow );

					half3 mainLightDir = mainLight.direction + inputData.normalWS * normal;
					half mainVdotL = pow( saturate( dot( inputData.viewDirectionWS, -mainLightDir ) ), scattering );
					half3 mainTranslucency = mainAtten * ( mainVdotL * direct + inputData.bakedGI * ambient ) * Translucency;
					color.rgb += Albedo * mainTranslucency * strength;

					#ifdef _ADDITIONAL_LIGHTS
						int transPixelLightCount = GetAdditionalLightsCount();
						for (int i = 0; i < transPixelLightCount; ++i)
						{
							Light light = GetAdditionalLight(i, inputData.positionWS);
							float3 atten = light.color * light.distanceAttenuation;
							atten = lerp( atten, atten * light.shadowAttenuation, shadow );

							half3 lightDir = light.direction + inputData.normalWS * normal;
							half VdotL = pow( saturate( dot( inputData.viewDirectionWS, -lightDir ) ), scattering );
							half3 translucency = atten * ( VdotL * direct + inputData.bakedGI * ambient ) * Translucency;
							color.rgb += Albedo * translucency * strength;
						}
					#endif
				}
				#endif

				#ifdef _REFRACTION_ASE
					float4 projScreenPos = ScreenPos / ScreenPos.w;
					float3 refractionOffset = ( RefractionIndex - 1.0 ) * mul( UNITY_MATRIX_V, WorldNormal ).xyz * ( 1.0 - dot( WorldNormal, WorldViewDirection ) );
					projScreenPos.xy += refractionOffset.xy;
					float3 refraction = SHADERGRAPH_SAMPLE_SCENE_COLOR( projScreenPos ) * RefractionColor;
					color.rgb = lerp( refraction, color.rgb, color.a );
					color.a = 1;
				#endif

				#ifdef ASE_FINAL_COLOR_ALPHA_MULTIPLY
					color.rgb *= color.a;
				#endif

				#ifdef ASE_FOG
					#ifdef TERRAIN_SPLAT_ADDPASS
						color.rgb = MixFogColor(color.rgb, half3( 0, 0, 0 ), IN.fogFactorAndVertexLight.x );
					#else
						color.rgb = MixFog(color.rgb, IN.fogFactorAndVertexLight.x);
					#endif
				#endif
				
				return color;
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "ShadowCaster"
			Tags { "LightMode"="ShadowCaster" }

			ZWrite On
			ZTest LEqual
			AlphaToMask Off

			HLSLPROGRAM
			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma multi_compile _ LOD_FADE_CROSSFADE
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define ASE_SRP_VERSION 80200

			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x

			#pragma vertex vert
			#pragma fragment frag

			#define SHADERPASS_SHADOWCASTER

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"

			

			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Color27;
			float4 _Color42;
			float4 _Color7;
			float4 _Color16;
			float4 _Color31;
			float4 _Color8;
			float4 _Color41;
			float4 _Color21;
			float4 _Color9;
			float4 _Color40;
			float4 _Color32;
			float4 _Color10;
			float4 _Color20;
			float4 _Color22;
			float4 _Color39;
			float4 _Color19;
			float4 _Color12;
			float4 _Color38;
			float4 _Color33;
			float4 _Color13;
			float4 _Color37;
			float4 _Color18;
			float4 _Color14;
			float4 _Color34;
			float4 _Color36;
			float4 _Color15;
			float4 _Color17;
			float4 _Color11;
			float4 _Color6;
			float4 _Color35;
			float4 _Color43;
			float4 _Color26;
			float4 _Color29;
			float4 _Color45;
			float4 _Color3;
			float4 _Color1;
			float4 _Color24;
			float4 _MainTex_ST;
			float4 _Color2;
			float4 _Color0;
			float4 _Color4;
			float4 _Color46;
			float4 _Color44;
			float4 _Color48;
			float4 _Color23;
			float4 _Color5;
			float4 _Color30;
			float4 _Color25;
			float4 _Color28;
			float4 _Color47;
			float _S34;
			float _V34;
			float _S28;
			float _H35;
			float _S35;
			float _V28;
			float _H34;
			float _S29;
			float _V33;
			float _S33;
			float _H31;
			float _H33;
			float _V29;
			float _V32;
			float _S32;
			float _H30;
			float _S30;
			float _H32;
			float _V30;
			float _S31;
			float _H29;
			float _V31;
			float _V38;
			float _H36;
			float _S43;
			float _V43;
			float _H44;
			float _S44;
			float _V44;
			float _H45;
			float _S45;
			float _V45;
			float _H46;
			float _S46;
			float _V46;
			float _H47;
			float _S47;
			float _V47;
			float _H48;
			float _S48;
			float _V48;
			float _H43;
			float _V35;
			float _V42;
			float _H42;
			float _S36;
			float _V36;
			float _H37;
			float _S37;
			float _V37;
			float _H38;
			float _S38;
			float _H28;
			float _H39;
			float _S39;
			float _V39;
			float _H40;
			float _S40;
			float _V40;
			float _H41;
			float _S41;
			float _V41;
			float _S42;
			float _H0;
			float _S24;
			float _S27;
			float _H7;
			float _S7;
			float _V7;
			float _H8;
			float _S8;
			float _V8;
			float _H9;
			float _S9;
			float _V9;
			float _H10;
			float _S10;
			float _V10;
			float _H11;
			float _S11;
			float _V11;
			float _H12;
			float _S12;
			float _V6;
			float _S6;
			float _H6;
			float _V5;
			float _S0;
			float _V0;
			float _ColorIDRange;
			float _ColorIDFuzziness;
			float _H1;
			float _S1;
			float _V1;
			float _H2;
			float _V12;
			float _S2;
			float _H3;
			float _S3;
			float _V3;
			float _H4;
			float _S4;
			float _V4;
			float _H5;
			float _S5;
			float _V2;
			float _H13;
			float _S13;
			float _V13;
			float _S21;
			float _V21;
			float _H22;
			float _S22;
			float _V22;
			float _H23;
			float _S23;
			float _V23;
			float _H21;
			float _H24;
			float _V24;
			float _H25;
			float _S25;
			float _V25;
			float _H26;
			float _S26;
			float _V26;
			float _H27;
			float _Metal;
			float _V27;
			float _V20;
			float _H20;
			float _H14;
			float _S14;
			float _V14;
			float _H15;
			float _S15;
			float _V15;
			float _H16;
			float _S16;
			float _S20;
			float _V16;
			float _S17;
			float _V17;
			float _H18;
			float _S18;
			float _V18;
			float _H19;
			float _S19;
			float _V19;
			float _H17;
			float _Smoothness;
			#ifdef _TRANSMISSION_ASE
				float _TransmissionShadow;
			#endif
			#ifdef _TRANSLUCENCY_ASE
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			

			
			float3 _LightDirection;

			VertexOutput VertexFunction( VertexInput v )
			{
				VertexOutput o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				o.worldPos = positionWS;
				#endif
				float3 normalWS = TransformObjectToWorldDir(v.ase_normal);

				float4 clipPos = TransformWorldToHClip( ApplyShadowBias( positionWS, normalWS, _LightDirection ) );

				#if UNITY_REVERSED_Z
					clipPos.z = min(clipPos.z, clipPos.w * UNITY_NEAR_CLIP_VALUE);
				#else
					clipPos.z = max(clipPos.z, clipPos.w * UNITY_NEAR_CLIP_VALUE);
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = clipPos;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif
				o.clipPos = clipPos;
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag(VertexOutput IN  ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );
				
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif
				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				
				float Alpha = 1;
				float AlphaClipThreshold = 0.5;
				float AlphaClipThresholdShadow = 0.5;

				#ifdef _ALPHATEST_ON
					#ifdef _ALPHATEST_SHADOW_ON
						clip(Alpha - AlphaClipThresholdShadow);
					#else
						clip(Alpha - AlphaClipThreshold);
					#endif
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition( IN.clipPos.xyz, unity_LODFade.x );
				#endif
				return 0;
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "DepthOnly"
			Tags { "LightMode"="DepthOnly" }

			ZWrite On
			ColorMask 0
			AlphaToMask Off

			HLSLPROGRAM
			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma multi_compile _ LOD_FADE_CROSSFADE
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define ASE_SRP_VERSION 80200

			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x

			#pragma vertex vert
			#pragma fragment frag

			#define SHADERPASS_DEPTHONLY

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"

			

			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Color27;
			float4 _Color42;
			float4 _Color7;
			float4 _Color16;
			float4 _Color31;
			float4 _Color8;
			float4 _Color41;
			float4 _Color21;
			float4 _Color9;
			float4 _Color40;
			float4 _Color32;
			float4 _Color10;
			float4 _Color20;
			float4 _Color22;
			float4 _Color39;
			float4 _Color19;
			float4 _Color12;
			float4 _Color38;
			float4 _Color33;
			float4 _Color13;
			float4 _Color37;
			float4 _Color18;
			float4 _Color14;
			float4 _Color34;
			float4 _Color36;
			float4 _Color15;
			float4 _Color17;
			float4 _Color11;
			float4 _Color6;
			float4 _Color35;
			float4 _Color43;
			float4 _Color26;
			float4 _Color29;
			float4 _Color45;
			float4 _Color3;
			float4 _Color1;
			float4 _Color24;
			float4 _MainTex_ST;
			float4 _Color2;
			float4 _Color0;
			float4 _Color4;
			float4 _Color46;
			float4 _Color44;
			float4 _Color48;
			float4 _Color23;
			float4 _Color5;
			float4 _Color30;
			float4 _Color25;
			float4 _Color28;
			float4 _Color47;
			float _S34;
			float _V34;
			float _S28;
			float _H35;
			float _S35;
			float _V28;
			float _H34;
			float _S29;
			float _V33;
			float _S33;
			float _H31;
			float _H33;
			float _V29;
			float _V32;
			float _S32;
			float _H30;
			float _S30;
			float _H32;
			float _V30;
			float _S31;
			float _H29;
			float _V31;
			float _V38;
			float _H36;
			float _S43;
			float _V43;
			float _H44;
			float _S44;
			float _V44;
			float _H45;
			float _S45;
			float _V45;
			float _H46;
			float _S46;
			float _V46;
			float _H47;
			float _S47;
			float _V47;
			float _H48;
			float _S48;
			float _V48;
			float _H43;
			float _V35;
			float _V42;
			float _H42;
			float _S36;
			float _V36;
			float _H37;
			float _S37;
			float _V37;
			float _H38;
			float _S38;
			float _H28;
			float _H39;
			float _S39;
			float _V39;
			float _H40;
			float _S40;
			float _V40;
			float _H41;
			float _S41;
			float _V41;
			float _S42;
			float _H0;
			float _S24;
			float _S27;
			float _H7;
			float _S7;
			float _V7;
			float _H8;
			float _S8;
			float _V8;
			float _H9;
			float _S9;
			float _V9;
			float _H10;
			float _S10;
			float _V10;
			float _H11;
			float _S11;
			float _V11;
			float _H12;
			float _S12;
			float _V6;
			float _S6;
			float _H6;
			float _V5;
			float _S0;
			float _V0;
			float _ColorIDRange;
			float _ColorIDFuzziness;
			float _H1;
			float _S1;
			float _V1;
			float _H2;
			float _V12;
			float _S2;
			float _H3;
			float _S3;
			float _V3;
			float _H4;
			float _S4;
			float _V4;
			float _H5;
			float _S5;
			float _V2;
			float _H13;
			float _S13;
			float _V13;
			float _S21;
			float _V21;
			float _H22;
			float _S22;
			float _V22;
			float _H23;
			float _S23;
			float _V23;
			float _H21;
			float _H24;
			float _V24;
			float _H25;
			float _S25;
			float _V25;
			float _H26;
			float _S26;
			float _V26;
			float _H27;
			float _Metal;
			float _V27;
			float _V20;
			float _H20;
			float _H14;
			float _S14;
			float _V14;
			float _H15;
			float _S15;
			float _V15;
			float _H16;
			float _S16;
			float _S20;
			float _V16;
			float _S17;
			float _V17;
			float _H18;
			float _S18;
			float _V18;
			float _H19;
			float _S19;
			float _V19;
			float _H17;
			float _Smoothness;
			#ifdef _TRANSMISSION_ASE
				float _TransmissionShadow;
			#endif
			#ifdef _TRANSLUCENCY_ASE
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			

			
			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;
				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float4 positionCS = TransformWorldToHClip( positionWS );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				o.worldPos = positionWS;
				#endif

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = positionCS;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif
				o.clipPos = positionCS;
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag(VertexOutput IN  ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif
				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				
				float Alpha = 1;
				float AlphaClipThreshold = 0.5;

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition( IN.clipPos.xyz, unity_LODFade.x );
				#endif
				return 0;
			}
			ENDHLSL
		}

		
		Pass
		{
			
			Name "Meta"
			Tags { "LightMode"="Meta" }

			Cull Off

			HLSLPROGRAM
			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma multi_compile _ LOD_FADE_CROSSFADE
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define ASE_SRP_VERSION 80200

			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x

			#pragma vertex vert
			#pragma fragment frag

			#define SHADERPASS_META

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/MetaInput.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"

			

			#pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				float4 ase_texcoord2 : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Color27;
			float4 _Color42;
			float4 _Color7;
			float4 _Color16;
			float4 _Color31;
			float4 _Color8;
			float4 _Color41;
			float4 _Color21;
			float4 _Color9;
			float4 _Color40;
			float4 _Color32;
			float4 _Color10;
			float4 _Color20;
			float4 _Color22;
			float4 _Color39;
			float4 _Color19;
			float4 _Color12;
			float4 _Color38;
			float4 _Color33;
			float4 _Color13;
			float4 _Color37;
			float4 _Color18;
			float4 _Color14;
			float4 _Color34;
			float4 _Color36;
			float4 _Color15;
			float4 _Color17;
			float4 _Color11;
			float4 _Color6;
			float4 _Color35;
			float4 _Color43;
			float4 _Color26;
			float4 _Color29;
			float4 _Color45;
			float4 _Color3;
			float4 _Color1;
			float4 _Color24;
			float4 _MainTex_ST;
			float4 _Color2;
			float4 _Color0;
			float4 _Color4;
			float4 _Color46;
			float4 _Color44;
			float4 _Color48;
			float4 _Color23;
			float4 _Color5;
			float4 _Color30;
			float4 _Color25;
			float4 _Color28;
			float4 _Color47;
			float _S34;
			float _V34;
			float _S28;
			float _H35;
			float _S35;
			float _V28;
			float _H34;
			float _S29;
			float _V33;
			float _S33;
			float _H31;
			float _H33;
			float _V29;
			float _V32;
			float _S32;
			float _H30;
			float _S30;
			float _H32;
			float _V30;
			float _S31;
			float _H29;
			float _V31;
			float _V38;
			float _H36;
			float _S43;
			float _V43;
			float _H44;
			float _S44;
			float _V44;
			float _H45;
			float _S45;
			float _V45;
			float _H46;
			float _S46;
			float _V46;
			float _H47;
			float _S47;
			float _V47;
			float _H48;
			float _S48;
			float _V48;
			float _H43;
			float _V35;
			float _V42;
			float _H42;
			float _S36;
			float _V36;
			float _H37;
			float _S37;
			float _V37;
			float _H38;
			float _S38;
			float _H28;
			float _H39;
			float _S39;
			float _V39;
			float _H40;
			float _S40;
			float _V40;
			float _H41;
			float _S41;
			float _V41;
			float _S42;
			float _H0;
			float _S24;
			float _S27;
			float _H7;
			float _S7;
			float _V7;
			float _H8;
			float _S8;
			float _V8;
			float _H9;
			float _S9;
			float _V9;
			float _H10;
			float _S10;
			float _V10;
			float _H11;
			float _S11;
			float _V11;
			float _H12;
			float _S12;
			float _V6;
			float _S6;
			float _H6;
			float _V5;
			float _S0;
			float _V0;
			float _ColorIDRange;
			float _ColorIDFuzziness;
			float _H1;
			float _S1;
			float _V1;
			float _H2;
			float _V12;
			float _S2;
			float _H3;
			float _S3;
			float _V3;
			float _H4;
			float _S4;
			float _V4;
			float _H5;
			float _S5;
			float _V2;
			float _H13;
			float _S13;
			float _V13;
			float _S21;
			float _V21;
			float _H22;
			float _S22;
			float _V22;
			float _H23;
			float _S23;
			float _V23;
			float _H21;
			float _H24;
			float _V24;
			float _H25;
			float _S25;
			float _V25;
			float _H26;
			float _S26;
			float _V26;
			float _H27;
			float _Metal;
			float _V27;
			float _V20;
			float _H20;
			float _H14;
			float _S14;
			float _V14;
			float _H15;
			float _S15;
			float _V15;
			float _H16;
			float _S16;
			float _S20;
			float _V16;
			float _S17;
			float _V17;
			float _H18;
			float _S18;
			float _V18;
			float _H19;
			float _S19;
			float _V19;
			float _H17;
			float _Smoothness;
			#ifdef _TRANSMISSION_ASE
				float _TransmissionShadow;
			#endif
			#ifdef _TRANSLUCENCY_ASE
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D _MainTex;


			float3 HSVToRGB( float3 c )
			{
				float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
				float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
				return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
			}
			

			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.ase_texcoord2.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord2.zw = 0;
				
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				o.worldPos = positionWS;
				#endif

				o.clipPos = MetaVertexPosition( v.vertex, v.texcoord1.xy, v.texcoord1.xy, unity_LightmapST, unity_DynamicLightmapST );
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = o.clipPos;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.texcoord1 = v.texcoord1;
				o.texcoord2 = v.texcoord2;
				o.ase_texcoord = v.ase_texcoord;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
				o.texcoord2 = patch[0].texcoord2 * bary.x + patch[1].texcoord2 * bary.y + patch[2].texcoord2 * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag(VertexOutput IN  ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif
				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				float3 hsvTorgb78 = HSVToRGB( float3(_H0,_S0,_V0) );
				float2 uv_MainTex = IN.ase_texcoord2.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode2 = tex2D( _MainTex, uv_MainTex );
				float3 lerpResult267 = lerp( hsvTorgb78 , hsvTorgb78 , saturate( ( 1.0 - ( ( distance( _Color0.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb90 = HSVToRGB( float3(_H1,_S1,_V1) );
				float3 lerpResult86 = lerp( lerpResult267 , hsvTorgb90 , saturate( ( 1.0 - ( ( distance( _Color1.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb91 = HSVToRGB( float3(_H2,_S2,_V2) );
				float3 lerpResult107 = lerp( lerpResult86 , hsvTorgb91 , saturate( ( 1.0 - ( ( distance( _Color2.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb98 = HSVToRGB( float3(_H3,_S3,_V3) );
				float3 lerpResult108 = lerp( lerpResult107 , hsvTorgb98 , saturate( ( 1.0 - ( ( distance( _Color3.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb99 = HSVToRGB( float3(_H4,_S4,_V4) );
				float3 lerpResult109 = lerp( lerpResult108 , hsvTorgb99 , saturate( ( 1.0 - ( ( distance( _Color4.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb106 = HSVToRGB( float3(_H5,_S5,_V5) );
				float3 lerpResult110 = lerp( lerpResult109 , hsvTorgb106 , saturate( ( 1.0 - ( ( distance( _Color5.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb114 = HSVToRGB( float3(_H6,_S6,_V6) );
				float3 lerpResult115 = lerp( lerpResult110 , hsvTorgb114 , saturate( ( 1.0 - ( ( distance( _Color6.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb120 = HSVToRGB( float3(_H7,_S7,_V7) );
				float3 lerpResult124 = lerp( lerpResult115 , hsvTorgb120 , saturate( ( 1.0 - ( ( distance( _Color7.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb123 = HSVToRGB( float3(_H8,_S8,_V8) );
				float3 lerpResult128 = lerp( lerpResult124 , hsvTorgb123 , saturate( ( 1.0 - ( ( distance( _Color8.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb132 = HSVToRGB( float3(_H9,_S9,_V9) );
				float3 lerpResult134 = lerp( lerpResult128 , hsvTorgb132 , saturate( ( 1.0 - ( ( distance( _Color9.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb133 = HSVToRGB( float3(_H10,_S10,_V10) );
				float3 lerpResult135 = lerp( lerpResult134 , hsvTorgb133 , saturate( ( 1.0 - ( ( distance( _Color10.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb138 = HSVToRGB( float3(_H11,_S11,_V11) );
				float3 lerpResult139 = lerp( lerpResult135 , hsvTorgb138 , saturate( ( 1.0 - ( ( distance( _Color11.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb142 = HSVToRGB( float3(_H12,_S12,_V12) );
				float3 lerpResult141 = lerp( lerpResult139 , hsvTorgb142 , saturate( ( 1.0 - ( ( distance( _Color12.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb150 = HSVToRGB( float3(_H13,_S13,_V13) );
				float3 lerpResult154 = lerp( lerpResult141 , hsvTorgb150 , saturate( ( 1.0 - ( ( distance( _Color13.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb153 = HSVToRGB( float3(_H14,_S14,_V14) );
				float3 lerpResult158 = lerp( lerpResult154 , hsvTorgb153 , saturate( ( 1.0 - ( ( distance( _Color14.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb162 = HSVToRGB( float3(_H15,_S15,_V15) );
				float3 lerpResult164 = lerp( lerpResult158 , hsvTorgb162 , saturate( ( 1.0 - ( ( distance( _Color15.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb163 = HSVToRGB( float3(_H16,_S16,_V16) );
				float3 lerpResult165 = lerp( lerpResult164 , hsvTorgb163 , saturate( ( 1.0 - ( ( distance( _Color16.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb168 = HSVToRGB( float3(_H17,_S17,_V17) );
				float3 lerpResult169 = lerp( lerpResult165 , hsvTorgb168 , saturate( ( 1.0 - ( ( distance( _Color17.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb172 = HSVToRGB( float3(_H18,_S18,_V18) );
				float3 lerpResult171 = lerp( lerpResult169 , hsvTorgb172 , saturate( ( 1.0 - ( ( distance( _Color18.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb180 = HSVToRGB( float3(_H19,_S19,_V19) );
				float3 lerpResult184 = lerp( lerpResult171 , hsvTorgb180 , saturate( ( 1.0 - ( ( distance( _Color19.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb183 = HSVToRGB( float3(_H20,_S20,_V20) );
				float3 lerpResult188 = lerp( lerpResult184 , hsvTorgb183 , saturate( ( 1.0 - ( ( distance( _Color20.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb192 = HSVToRGB( float3(_H21,_S21,_V21) );
				float3 lerpResult194 = lerp( lerpResult188 , hsvTorgb192 , saturate( ( 1.0 - ( ( distance( _Color21.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb193 = HSVToRGB( float3(_H22,_S22,_V22) );
				float3 lerpResult195 = lerp( lerpResult194 , hsvTorgb193 , saturate( ( 1.0 - ( ( distance( _Color22.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb198 = HSVToRGB( float3(_H23,_S23,_V23) );
				float3 lerpResult199 = lerp( lerpResult195 , hsvTorgb198 , saturate( ( 1.0 - ( ( distance( _Color23.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb202 = HSVToRGB( float3(_H24,_S24,_V24) );
				float3 lerpResult201 = lerp( lerpResult199 , hsvTorgb202 , saturate( ( 1.0 - ( ( distance( _Color24.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb210 = HSVToRGB( float3(_H25,_S25,_V25) );
				float3 lerpResult214 = lerp( lerpResult201 , hsvTorgb210 , saturate( ( 1.0 - ( ( distance( _Color25.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb213 = HSVToRGB( float3(_H26,_S26,_V26) );
				float3 lerpResult218 = lerp( lerpResult214 , hsvTorgb213 , saturate( ( 1.0 - ( ( distance( _Color26.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb222 = HSVToRGB( float3(_H27,_S27,_V27) );
				float3 lerpResult224 = lerp( lerpResult218 , hsvTorgb222 , saturate( ( 1.0 - ( ( distance( _Color27.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb223 = HSVToRGB( float3(_H28,_S28,_V28) );
				float3 lerpResult225 = lerp( lerpResult224 , hsvTorgb223 , saturate( ( 1.0 - ( ( distance( _Color28.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb228 = HSVToRGB( float3(_H29,_S29,_V29) );
				float3 lerpResult229 = lerp( lerpResult225 , hsvTorgb228 , saturate( ( 1.0 - ( ( distance( _Color29.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb383 = HSVToRGB( float3(_H30,_S30,_V30) );
				float3 lerpResult384 = lerp( lerpResult229 , hsvTorgb383 , saturate( ( 1.0 - ( ( distance( _Color30.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb386 = HSVToRGB( float3(_H31,_S31,_V31) );
				float3 lerpResult387 = lerp( lerpResult384 , hsvTorgb386 , saturate( ( 1.0 - ( ( distance( _Color31.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb391 = HSVToRGB( float3(_H32,_S32,_V32) );
				float3 lerpResult392 = lerp( lerpResult387 , hsvTorgb391 , saturate( ( 1.0 - ( ( distance( _Color32.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb396 = HSVToRGB( float3(_H33,_S33,_V33) );
				float3 lerpResult397 = lerp( lerpResult392 , hsvTorgb396 , saturate( ( 1.0 - ( ( distance( _Color33.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb401 = HSVToRGB( float3(_H34,_S34,_V34) );
				float3 lerpResult402 = lerp( lerpResult397 , hsvTorgb401 , saturate( ( 1.0 - ( ( distance( _Color34.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb406 = HSVToRGB( float3(_H35,_S35,_V35) );
				float3 lerpResult407 = lerp( lerpResult402 , hsvTorgb406 , saturate( ( 1.0 - ( ( distance( _Color35.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb411 = HSVToRGB( float3(_H36,_S36,_V36) );
				float3 lerpResult412 = lerp( lerpResult407 , hsvTorgb411 , saturate( ( 1.0 - ( ( distance( _Color36.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb416 = HSVToRGB( float3(_H37,_S37,_V37) );
				float3 lerpResult417 = lerp( lerpResult412 , hsvTorgb416 , saturate( ( 1.0 - ( ( distance( _Color37.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb421 = HSVToRGB( float3(_H38,_S38,_V38) );
				float3 lerpResult422 = lerp( lerpResult417 , hsvTorgb421 , saturate( ( 1.0 - ( ( distance( _Color38.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb426 = HSVToRGB( float3(_H39,_S39,_V39) );
				float3 lerpResult427 = lerp( lerpResult422 , hsvTorgb426 , saturate( ( 1.0 - ( ( distance( _Color39.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb431 = HSVToRGB( float3(_H40,_S40,_V40) );
				float3 lerpResult432 = lerp( lerpResult427 , hsvTorgb431 , saturate( ( 1.0 - ( ( distance( _Color40.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb436 = HSVToRGB( float3(_H41,_S41,_V41) );
				float3 lerpResult437 = lerp( lerpResult432 , hsvTorgb436 , saturate( ( 1.0 - ( ( distance( _Color41.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb446 = HSVToRGB( float3(_H42,_S42,_V42) );
				float3 lerpResult447 = lerp( lerpResult437 , hsvTorgb446 , saturate( ( 1.0 - ( ( distance( _Color42.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb453 = HSVToRGB( float3(_H43,_S43,_V43) );
				float3 lerpResult454 = lerp( lerpResult447 , hsvTorgb453 , saturate( ( 1.0 - ( ( distance( _Color43.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb460 = HSVToRGB( float3(_H44,_S44,_V44) );
				float3 lerpResult461 = lerp( lerpResult454 , hsvTorgb460 , saturate( ( 1.0 - ( ( distance( _Color44.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb467 = HSVToRGB( float3(_H45,_S45,_V45) );
				float3 lerpResult468 = lerp( lerpResult461 , hsvTorgb467 , saturate( ( 1.0 - ( ( distance( _Color45.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb474 = HSVToRGB( float3(_H46,_S46,_V46) );
				float3 lerpResult475 = lerp( lerpResult468 , hsvTorgb474 , saturate( ( 1.0 - ( ( distance( _Color46.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb481 = HSVToRGB( float3(_H47,_S47,_V47) );
				float3 lerpResult482 = lerp( lerpResult475 , hsvTorgb481 , saturate( ( 1.0 - ( ( distance( _Color47.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb488 = HSVToRGB( float3(_H48,_S48,_V48) );
				float3 lerpResult489 = lerp( lerpResult482 , hsvTorgb488 , saturate( ( 1.0 - ( ( distance( _Color48.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				
				
				float3 Albedo = lerpResult489;
				float3 Emission = 0;
				float Alpha = 1;
				float AlphaClipThreshold = 0.5;

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				MetaInput metaInput = (MetaInput)0;
				metaInput.Albedo = Albedo;
				metaInput.Emission = Emission;
				
				return MetaFragment(metaInput);
			}
			ENDHLSL
		}

		
		Pass
		{
			
			Name "Universal2D"
			Tags { "LightMode"="Universal2D" }

			Blend One Zero, One Zero
			ZWrite On
			ZTest LEqual
			Offset 0 , 0
			ColorMask RGBA

			HLSLPROGRAM
			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma multi_compile _ LOD_FADE_CROSSFADE
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define ASE_SRP_VERSION 80200

			#pragma enable_d3d11_debug_symbols
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x

			#pragma vertex vert
			#pragma fragment frag

			#define SHADERPASS_2D

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			
			

			#pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				float4 ase_texcoord2 : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Color27;
			float4 _Color42;
			float4 _Color7;
			float4 _Color16;
			float4 _Color31;
			float4 _Color8;
			float4 _Color41;
			float4 _Color21;
			float4 _Color9;
			float4 _Color40;
			float4 _Color32;
			float4 _Color10;
			float4 _Color20;
			float4 _Color22;
			float4 _Color39;
			float4 _Color19;
			float4 _Color12;
			float4 _Color38;
			float4 _Color33;
			float4 _Color13;
			float4 _Color37;
			float4 _Color18;
			float4 _Color14;
			float4 _Color34;
			float4 _Color36;
			float4 _Color15;
			float4 _Color17;
			float4 _Color11;
			float4 _Color6;
			float4 _Color35;
			float4 _Color43;
			float4 _Color26;
			float4 _Color29;
			float4 _Color45;
			float4 _Color3;
			float4 _Color1;
			float4 _Color24;
			float4 _MainTex_ST;
			float4 _Color2;
			float4 _Color0;
			float4 _Color4;
			float4 _Color46;
			float4 _Color44;
			float4 _Color48;
			float4 _Color23;
			float4 _Color5;
			float4 _Color30;
			float4 _Color25;
			float4 _Color28;
			float4 _Color47;
			float _S34;
			float _V34;
			float _S28;
			float _H35;
			float _S35;
			float _V28;
			float _H34;
			float _S29;
			float _V33;
			float _S33;
			float _H31;
			float _H33;
			float _V29;
			float _V32;
			float _S32;
			float _H30;
			float _S30;
			float _H32;
			float _V30;
			float _S31;
			float _H29;
			float _V31;
			float _V38;
			float _H36;
			float _S43;
			float _V43;
			float _H44;
			float _S44;
			float _V44;
			float _H45;
			float _S45;
			float _V45;
			float _H46;
			float _S46;
			float _V46;
			float _H47;
			float _S47;
			float _V47;
			float _H48;
			float _S48;
			float _V48;
			float _H43;
			float _V35;
			float _V42;
			float _H42;
			float _S36;
			float _V36;
			float _H37;
			float _S37;
			float _V37;
			float _H38;
			float _S38;
			float _H28;
			float _H39;
			float _S39;
			float _V39;
			float _H40;
			float _S40;
			float _V40;
			float _H41;
			float _S41;
			float _V41;
			float _S42;
			float _H0;
			float _S24;
			float _S27;
			float _H7;
			float _S7;
			float _V7;
			float _H8;
			float _S8;
			float _V8;
			float _H9;
			float _S9;
			float _V9;
			float _H10;
			float _S10;
			float _V10;
			float _H11;
			float _S11;
			float _V11;
			float _H12;
			float _S12;
			float _V6;
			float _S6;
			float _H6;
			float _V5;
			float _S0;
			float _V0;
			float _ColorIDRange;
			float _ColorIDFuzziness;
			float _H1;
			float _S1;
			float _V1;
			float _H2;
			float _V12;
			float _S2;
			float _H3;
			float _S3;
			float _V3;
			float _H4;
			float _S4;
			float _V4;
			float _H5;
			float _S5;
			float _V2;
			float _H13;
			float _S13;
			float _V13;
			float _S21;
			float _V21;
			float _H22;
			float _S22;
			float _V22;
			float _H23;
			float _S23;
			float _V23;
			float _H21;
			float _H24;
			float _V24;
			float _H25;
			float _S25;
			float _V25;
			float _H26;
			float _S26;
			float _V26;
			float _H27;
			float _Metal;
			float _V27;
			float _V20;
			float _H20;
			float _H14;
			float _S14;
			float _V14;
			float _H15;
			float _S15;
			float _V15;
			float _H16;
			float _S16;
			float _S20;
			float _V16;
			float _S17;
			float _V17;
			float _H18;
			float _S18;
			float _V18;
			float _H19;
			float _S19;
			float _V19;
			float _H17;
			float _Smoothness;
			#ifdef _TRANSMISSION_ASE
				float _TransmissionShadow;
			#endif
			#ifdef _TRANSLUCENCY_ASE
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D _MainTex;


			float3 HSVToRGB( float3 c )
			{
				float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
				float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
				return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
			}
			

			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				o.ase_texcoord2.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord2.zw = 0;
				
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float4 positionCS = TransformWorldToHClip( positionWS );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				o.worldPos = positionWS;
				#endif

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = positionCS;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif

				o.clipPos = positionCS;
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_texcoord = v.ase_texcoord;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag(VertexOutput IN  ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif
				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				float3 hsvTorgb78 = HSVToRGB( float3(_H0,_S0,_V0) );
				float2 uv_MainTex = IN.ase_texcoord2.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode2 = tex2D( _MainTex, uv_MainTex );
				float3 lerpResult267 = lerp( hsvTorgb78 , hsvTorgb78 , saturate( ( 1.0 - ( ( distance( _Color0.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb90 = HSVToRGB( float3(_H1,_S1,_V1) );
				float3 lerpResult86 = lerp( lerpResult267 , hsvTorgb90 , saturate( ( 1.0 - ( ( distance( _Color1.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb91 = HSVToRGB( float3(_H2,_S2,_V2) );
				float3 lerpResult107 = lerp( lerpResult86 , hsvTorgb91 , saturate( ( 1.0 - ( ( distance( _Color2.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb98 = HSVToRGB( float3(_H3,_S3,_V3) );
				float3 lerpResult108 = lerp( lerpResult107 , hsvTorgb98 , saturate( ( 1.0 - ( ( distance( _Color3.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb99 = HSVToRGB( float3(_H4,_S4,_V4) );
				float3 lerpResult109 = lerp( lerpResult108 , hsvTorgb99 , saturate( ( 1.0 - ( ( distance( _Color4.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb106 = HSVToRGB( float3(_H5,_S5,_V5) );
				float3 lerpResult110 = lerp( lerpResult109 , hsvTorgb106 , saturate( ( 1.0 - ( ( distance( _Color5.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb114 = HSVToRGB( float3(_H6,_S6,_V6) );
				float3 lerpResult115 = lerp( lerpResult110 , hsvTorgb114 , saturate( ( 1.0 - ( ( distance( _Color6.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb120 = HSVToRGB( float3(_H7,_S7,_V7) );
				float3 lerpResult124 = lerp( lerpResult115 , hsvTorgb120 , saturate( ( 1.0 - ( ( distance( _Color7.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb123 = HSVToRGB( float3(_H8,_S8,_V8) );
				float3 lerpResult128 = lerp( lerpResult124 , hsvTorgb123 , saturate( ( 1.0 - ( ( distance( _Color8.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb132 = HSVToRGB( float3(_H9,_S9,_V9) );
				float3 lerpResult134 = lerp( lerpResult128 , hsvTorgb132 , saturate( ( 1.0 - ( ( distance( _Color9.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb133 = HSVToRGB( float3(_H10,_S10,_V10) );
				float3 lerpResult135 = lerp( lerpResult134 , hsvTorgb133 , saturate( ( 1.0 - ( ( distance( _Color10.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb138 = HSVToRGB( float3(_H11,_S11,_V11) );
				float3 lerpResult139 = lerp( lerpResult135 , hsvTorgb138 , saturate( ( 1.0 - ( ( distance( _Color11.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb142 = HSVToRGB( float3(_H12,_S12,_V12) );
				float3 lerpResult141 = lerp( lerpResult139 , hsvTorgb142 , saturate( ( 1.0 - ( ( distance( _Color12.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb150 = HSVToRGB( float3(_H13,_S13,_V13) );
				float3 lerpResult154 = lerp( lerpResult141 , hsvTorgb150 , saturate( ( 1.0 - ( ( distance( _Color13.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb153 = HSVToRGB( float3(_H14,_S14,_V14) );
				float3 lerpResult158 = lerp( lerpResult154 , hsvTorgb153 , saturate( ( 1.0 - ( ( distance( _Color14.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb162 = HSVToRGB( float3(_H15,_S15,_V15) );
				float3 lerpResult164 = lerp( lerpResult158 , hsvTorgb162 , saturate( ( 1.0 - ( ( distance( _Color15.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb163 = HSVToRGB( float3(_H16,_S16,_V16) );
				float3 lerpResult165 = lerp( lerpResult164 , hsvTorgb163 , saturate( ( 1.0 - ( ( distance( _Color16.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb168 = HSVToRGB( float3(_H17,_S17,_V17) );
				float3 lerpResult169 = lerp( lerpResult165 , hsvTorgb168 , saturate( ( 1.0 - ( ( distance( _Color17.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb172 = HSVToRGB( float3(_H18,_S18,_V18) );
				float3 lerpResult171 = lerp( lerpResult169 , hsvTorgb172 , saturate( ( 1.0 - ( ( distance( _Color18.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb180 = HSVToRGB( float3(_H19,_S19,_V19) );
				float3 lerpResult184 = lerp( lerpResult171 , hsvTorgb180 , saturate( ( 1.0 - ( ( distance( _Color19.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb183 = HSVToRGB( float3(_H20,_S20,_V20) );
				float3 lerpResult188 = lerp( lerpResult184 , hsvTorgb183 , saturate( ( 1.0 - ( ( distance( _Color20.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb192 = HSVToRGB( float3(_H21,_S21,_V21) );
				float3 lerpResult194 = lerp( lerpResult188 , hsvTorgb192 , saturate( ( 1.0 - ( ( distance( _Color21.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb193 = HSVToRGB( float3(_H22,_S22,_V22) );
				float3 lerpResult195 = lerp( lerpResult194 , hsvTorgb193 , saturate( ( 1.0 - ( ( distance( _Color22.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb198 = HSVToRGB( float3(_H23,_S23,_V23) );
				float3 lerpResult199 = lerp( lerpResult195 , hsvTorgb198 , saturate( ( 1.0 - ( ( distance( _Color23.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb202 = HSVToRGB( float3(_H24,_S24,_V24) );
				float3 lerpResult201 = lerp( lerpResult199 , hsvTorgb202 , saturate( ( 1.0 - ( ( distance( _Color24.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb210 = HSVToRGB( float3(_H25,_S25,_V25) );
				float3 lerpResult214 = lerp( lerpResult201 , hsvTorgb210 , saturate( ( 1.0 - ( ( distance( _Color25.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb213 = HSVToRGB( float3(_H26,_S26,_V26) );
				float3 lerpResult218 = lerp( lerpResult214 , hsvTorgb213 , saturate( ( 1.0 - ( ( distance( _Color26.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb222 = HSVToRGB( float3(_H27,_S27,_V27) );
				float3 lerpResult224 = lerp( lerpResult218 , hsvTorgb222 , saturate( ( 1.0 - ( ( distance( _Color27.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb223 = HSVToRGB( float3(_H28,_S28,_V28) );
				float3 lerpResult225 = lerp( lerpResult224 , hsvTorgb223 , saturate( ( 1.0 - ( ( distance( _Color28.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb228 = HSVToRGB( float3(_H29,_S29,_V29) );
				float3 lerpResult229 = lerp( lerpResult225 , hsvTorgb228 , saturate( ( 1.0 - ( ( distance( _Color29.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb383 = HSVToRGB( float3(_H30,_S30,_V30) );
				float3 lerpResult384 = lerp( lerpResult229 , hsvTorgb383 , saturate( ( 1.0 - ( ( distance( _Color30.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb386 = HSVToRGB( float3(_H31,_S31,_V31) );
				float3 lerpResult387 = lerp( lerpResult384 , hsvTorgb386 , saturate( ( 1.0 - ( ( distance( _Color31.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb391 = HSVToRGB( float3(_H32,_S32,_V32) );
				float3 lerpResult392 = lerp( lerpResult387 , hsvTorgb391 , saturate( ( 1.0 - ( ( distance( _Color32.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb396 = HSVToRGB( float3(_H33,_S33,_V33) );
				float3 lerpResult397 = lerp( lerpResult392 , hsvTorgb396 , saturate( ( 1.0 - ( ( distance( _Color33.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb401 = HSVToRGB( float3(_H34,_S34,_V34) );
				float3 lerpResult402 = lerp( lerpResult397 , hsvTorgb401 , saturate( ( 1.0 - ( ( distance( _Color34.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb406 = HSVToRGB( float3(_H35,_S35,_V35) );
				float3 lerpResult407 = lerp( lerpResult402 , hsvTorgb406 , saturate( ( 1.0 - ( ( distance( _Color35.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb411 = HSVToRGB( float3(_H36,_S36,_V36) );
				float3 lerpResult412 = lerp( lerpResult407 , hsvTorgb411 , saturate( ( 1.0 - ( ( distance( _Color36.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb416 = HSVToRGB( float3(_H37,_S37,_V37) );
				float3 lerpResult417 = lerp( lerpResult412 , hsvTorgb416 , saturate( ( 1.0 - ( ( distance( _Color37.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb421 = HSVToRGB( float3(_H38,_S38,_V38) );
				float3 lerpResult422 = lerp( lerpResult417 , hsvTorgb421 , saturate( ( 1.0 - ( ( distance( _Color38.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb426 = HSVToRGB( float3(_H39,_S39,_V39) );
				float3 lerpResult427 = lerp( lerpResult422 , hsvTorgb426 , saturate( ( 1.0 - ( ( distance( _Color39.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb431 = HSVToRGB( float3(_H40,_S40,_V40) );
				float3 lerpResult432 = lerp( lerpResult427 , hsvTorgb431 , saturate( ( 1.0 - ( ( distance( _Color40.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb436 = HSVToRGB( float3(_H41,_S41,_V41) );
				float3 lerpResult437 = lerp( lerpResult432 , hsvTorgb436 , saturate( ( 1.0 - ( ( distance( _Color41.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb446 = HSVToRGB( float3(_H42,_S42,_V42) );
				float3 lerpResult447 = lerp( lerpResult437 , hsvTorgb446 , saturate( ( 1.0 - ( ( distance( _Color42.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb453 = HSVToRGB( float3(_H43,_S43,_V43) );
				float3 lerpResult454 = lerp( lerpResult447 , hsvTorgb453 , saturate( ( 1.0 - ( ( distance( _Color43.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb460 = HSVToRGB( float3(_H44,_S44,_V44) );
				float3 lerpResult461 = lerp( lerpResult454 , hsvTorgb460 , saturate( ( 1.0 - ( ( distance( _Color44.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb467 = HSVToRGB( float3(_H45,_S45,_V45) );
				float3 lerpResult468 = lerp( lerpResult461 , hsvTorgb467 , saturate( ( 1.0 - ( ( distance( _Color45.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb474 = HSVToRGB( float3(_H46,_S46,_V46) );
				float3 lerpResult475 = lerp( lerpResult468 , hsvTorgb474 , saturate( ( 1.0 - ( ( distance( _Color46.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb481 = HSVToRGB( float3(_H47,_S47,_V47) );
				float3 lerpResult482 = lerp( lerpResult475 , hsvTorgb481 , saturate( ( 1.0 - ( ( distance( _Color47.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				float3 hsvTorgb488 = HSVToRGB( float3(_H48,_S48,_V48) );
				float3 lerpResult489 = lerp( lerpResult482 , hsvTorgb488 , saturate( ( 1.0 - ( ( distance( _Color48.rgb , tex2DNode2.rgb ) - _ColorIDRange ) / max( _ColorIDFuzziness , 1E-05 ) ) ) ));
				
				
				float3 Albedo = lerpResult489;
				float Alpha = 1;
				float AlphaClipThreshold = 0.5;

				half4 color = half4( Albedo, Alpha );

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				return color;
			}
			ENDHLSL
		}
		
	}
	/*ase_lod*/
	CustomEditor "UnityEditor.ShaderGraph.PBRMasterGUI"
	Fallback "Hidden/InternalErrorShader"
	
}
/*ASEBEGIN
Version=18800
0;711;1180;688;5646.985;-10403.52;5.64487;True;True
Node;AmplifyShaderEditor.RangedFloatNode;84;-1983.864,1560.88;Inherit;False;Property;_V0;V0;56;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;62;-3946.711,12294.09;Inherit;False;Property;_ColorIDFuzziness;ColorIDFuzziness;4;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-3981.74,11895.64;Inherit;True;Property;_MainTex;ColorID;2;0;Create;False;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;271;-3954.406,12218.35;Inherit;False;Property;_ColorIDRange;ColorIDRange;3;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;83;-1981.258,1488.706;Inherit;False;Property;_S0;S0;55;0;Create;True;0;0;0;False;0;False;0.2559484;0.2559484;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;79;-1980.025,1418.274;Inherit;False;Property;_H0;H0;54;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;3;-3207.045,1494.981;Inherit;False;Property;_Color0;Color 0;5;0;Create;True;0;0;0;False;0;False;1,0,0,0;1,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;89;-1956.433,1657.155;Inherit;False;Property;_H1;H1;57;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;87;-1953.273,1801.762;Inherit;False;Property;_V1;V1;59;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1;-2929.704,1469.132;Inherit;True;Color Mask;-1;;33;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;78;-1662.063,1452.862;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;88;-1955.667,1730.588;Inherit;False;Property;_S1;S1;58;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;4;-3213.687,1736.927;Inherit;False;Property;_Color1;Color 1;6;0;Create;True;0;0;0;False;0;False;0,1,0,0;0,1,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;94;-1938.183,2025.878;Inherit;False;Property;_V2;V2;62;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;93;-1935.577,1953.704;Inherit;False;Property;_S2;S2;61;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;92;-1934.342,1883.271;Inherit;False;Property;_H2;H2;60;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;6;-2925.993,1715.488;Inherit;True;Color Mask;-1;;34;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;267;-1306.408,1425.637;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.HSVToRGBNode;90;-1636.471,1702.744;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;5;-3182.567,1965.116;Inherit;False;Property;_Color2;Color 2;7;0;Create;True;0;0;0;False;0;False;0,0,1,0;0,0,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;7;-2880.298,1950.642;Inherit;True;Color Mask;-1;;35;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;11;-3196.323,2199.957;Inherit;False;Property;_Color3;Color 3;8;0;Create;True;0;0;0;False;0;False;1,1,0,0;1,1,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;86;-1296.872,1654.447;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;95;-1937.702,2110.602;Inherit;False;Property;_H3;H3;63;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;91;-1616.381,1917.86;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;96;-1936.935,2184.035;Inherit;False;Property;_S3;S3;64;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;97;-1934.541,2255.208;Inherit;False;Property;_V3;V3;65;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;102;-1928.343,2481.755;Inherit;False;Property;_V4;V4;68;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;98;-1617.74,2156.191;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;100;-1926.418,2339.147;Inherit;False;Property;_H4;H4;66;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;10;-2878.638,2188.442;Inherit;True;Color Mask;-1;;36;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;101;-1927.652,2409.58;Inherit;False;Property;_S4;S4;67;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;12;-3193.531,2447.638;Inherit;False;Property;_Color4;Color 4;9;0;Create;True;0;0;0;False;0;False;1,0,1,0;1,0,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;107;-1251.953,1913.514;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;105;-1924.702,2711.086;Inherit;False;Property;_V5;V5;71;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;13;-3184.691,2683.756;Inherit;False;Property;_Color5;Color 5;10;0;Create;True;0;0;0;False;0;False;0,1,1,0;0,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.HSVToRGBNode;99;-1606.542,2373.736;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.FunctionNode;8;-2878.067,2434.127;Inherit;True;Color Mask;-1;;37;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;108;-1253.622,2165.978;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;104;-1927.096,2639.913;Inherit;False;Property;_S5;S5;70;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;103;-1927.862,2566.48;Inherit;False;Property;_H5;H5;69;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;17;-3212.631,2982.594;Inherit;False;Property;_Color6;Color 6;11;0;Create;True;0;0;0;False;0;False;1,0.5019608,0,0;1,0.5019608,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;112;-1917.165,2833.859;Inherit;False;Property;_H6;H6;72;0;Create;False;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;106;-1607.901,2612.069;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp;109;-1239.088,2379.824;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;111;-1916.398,2907.292;Inherit;False;Property;_S6;S6;73;0;Create;False;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;9;-2882.422,2669.281;Inherit;True;Color Mask;-1;;38;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;113;-1914.004,2978.465;Inherit;False;Property;_V6;V6;74;0;Create;False;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;114;-1597.203,2889.44;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;116;-1911.797,3070.047;Inherit;False;Property;_H7;H7;75;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;16;-2876.467,2998.43;Inherit;True;Color Mask;-1;;39;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;110;-1253.622,2612.356;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;118;-1908.637,3214.654;Inherit;False;Property;_V7;V7;77;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;117;-1911.031,3141.026;Inherit;False;Property;_S7;S7;76;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;18;-3201.504,3241.387;Inherit;False;Property;_Color7;Color 7;12;0;Create;True;0;0;0;False;0;False;1,0,0.5019608,0;1,0,0.5019608,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;122;-1901.248,3442.621;Inherit;False;Property;_V8;V8;80;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;115;-1311.084,2949.704;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.HSVToRGBNode;120;-1591.836,3115.636;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;121;-1897.407,3300.012;Inherit;False;Property;_H8;H8;78;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;19;-3195.443,3469.171;Inherit;False;Property;_Color8;Color 8;13;0;Create;True;0;0;0;False;0;False;0.5019608,1,0,0;0.5019608,1,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;119;-1898.641,3370.445;Inherit;False;Property;_S8;S8;79;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;14;-2866.592,3222.321;Inherit;True;Color Mask;-1;;40;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;126;-1897.606,3671.952;Inherit;False;Property;_V9;V9;83;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;15;-2870.947,3452.506;Inherit;True;Color Mask;-1;;41;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;23;-3186.972,3742.908;Inherit;False;Property;_Color9;Color 9;14;0;Create;True;0;0;0;False;0;False;0,1,0.5019608,0;0,1,0.5019608,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;127;-1900.766,3527.346;Inherit;False;Property;_H9;H9;81;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;125;-1900,3600.779;Inherit;False;Property;_S9;S9;82;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;123;-1579.446,3334.601;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp;124;-1288.139,3202.185;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;130;-1890.717,3826.324;Inherit;False;Property;_S10;S10;86;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;129;-1889.482,3755.891;Inherit;False;Property;_H10;H10;84;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;132;-1580.805,3572.935;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp;128;-1289.766,3422.708;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;24;-3178.624,3932.243;Inherit;False;Property;_Color10;Color 10;15;0;Create;True;0;0;0;False;0;False;0.5019608,0,1,0;0.5019608,0,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;131;-1891.407,3898.499;Inherit;False;Property;_V10;V10;85;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;22;-2869.287,3695.275;Inherit;True;Color Mask;-1;;42;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;140;-1887.766,4127.831;Inherit;False;Property;_V11;V11;89;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;133;-1569.607,3790.479;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;25;-3175.34,4190.589;Inherit;False;Property;_Color11;Color 11;16;0;Create;True;0;0;0;False;0;False;0,0.5019608,1,0;0,0.5019608,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;20;-2868.716,3940.96;Inherit;True;Color Mask;-1;;43;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;134;-1291.434,3675.172;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;136;-1890.16,4056.656;Inherit;False;Property;_S11;S11;88;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;137;-1890.927,3983.224;Inherit;False;Property;_H11;H11;87;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;21;-2873.071,4176.115;Inherit;True;Color Mask;-1;;44;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;143;-1890.812,4495.313;Inherit;False;Property;_V12;V12;92;0;Create;False;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;29;-3161.438,4484.956;Inherit;False;Property;_Color12;Color 12;17;0;Create;True;0;0;0;False;0;False;1,0.5019608,0.5019608,0;1,0.5019608,0.5019608,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;144;-1894.206,4424.141;Inherit;False;Property;_S12;S12;91;0;Create;False;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;135;-1276.901,3889.019;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;145;-1893.972,4350.707;Inherit;False;Property;_H12;H12;90;0;Create;False;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;138;-1570.965,4028.813;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.HSVToRGBNode;142;-1574.01,4396.296;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;147;-1887.839,4660.328;Inherit;False;Property;_S13;S13;94;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;30;-3141.976,4710.41;Inherit;False;Property;_Color13;Color 13;18;0;Create;True;0;0;0;False;0;False;0.5019608,1,0.5019608,0;0.5019608,1,0.5019608,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;28;-2849.31,4442.879;Inherit;True;Color Mask;-1;;45;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;146;-1888.604,4586.896;Inherit;False;Property;_H13;H13;93;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;148;-1885.445,4731.502;Inherit;False;Property;_V13;V13;95;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;139;-1291.435,4121.552;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;141;-1322.018,4398.549;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;31;-3155.364,4938.192;Inherit;False;Property;_Color14;Color 14;19;0;Create;True;0;0;0;False;0;False;0.5019608,0.5019608,1,0;0.5019608,0.5019608,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;152;-1878.055,4959.468;Inherit;False;Property;_V14;V14;98;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;149;-1875.448,4887.293;Inherit;False;Property;_S14;S14;97;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;150;-1568.643,4632.484;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.FunctionNode;26;-2844.73,4688.564;Inherit;True;Color Mask;-1;;46;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;151;-1874.214,4816.86;Inherit;False;Property;_H14;H14;96;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;155;-1876.807,5117.626;Inherit;False;Property;_S15;S15;100;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;156;-1874.414,5188.799;Inherit;False;Property;_V15;V15;101;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;153;-1556.253,4851.449;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;157;-1877.573,5044.193;Inherit;False;Property;_H15;H15;99;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;154;-1292.485,4628.547;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;27;-2853.094,4923.719;Inherit;True;Color Mask;-1;;47;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;35;-3180.233,5181.369;Inherit;False;Property;_Color15;Color 15;20;0;Create;True;0;0;0;False;0;False;1,1,0.5019608,0;1,1,0.5019608,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;34;-2851.434,5161.519;Inherit;True;Color Mask;-1;;48;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;159;-1866.29,5272.738;Inherit;False;Property;_H16;H16;102;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;160;-1867.524,5343.171;Inherit;False;Property;_S16;S16;103;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;161;-1868.214,5415.346;Inherit;False;Property;_V16;V16;104;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;162;-1557.612,5089.782;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp;158;-1294.111,4849.071;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;36;-3146.879,5417.936;Inherit;False;Property;_Color16;Color 16;21;0;Create;True;0;0;0;False;0;False;1,0.5019608,1,0;1,0.5019608,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;167;-1867.734,5500.071;Inherit;False;Property;_H17;H17;105;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;164;-1295.78,5101.535;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;170;-1864.573,5644.677;Inherit;False;Property;_V17;V17;107;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;166;-1866.968,5573.504;Inherit;False;Property;_S17;S17;106;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;32;-2863.137,5382.658;Inherit;True;Color Mask;-1;;49;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;37;-3157.488,5656.833;Inherit;False;Property;_Color17;Color 17;22;0;Create;True;0;0;0;False;0;False;0.5019608,1,1,0;0.5019608,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.HSVToRGBNode;163;-1546.414,5307.327;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;173;-1867.557,5965.937;Inherit;False;Property;_V18;V18;110;0;Create;False;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;165;-1281.247,5315.381;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;41;-3226.871,5935.702;Inherit;False;Property;_Color18;Color 18;23;0;Create;True;0;0;0;False;0;False;0.3921569,0,0,0;0.3921568,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;174;-1869.951,5894.764;Inherit;False;Property;_S18;S18;109;0;Create;False;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;168;-1547.772,5545.66;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;175;-1870.717,5821.331;Inherit;False;Property;_H18;H18;108;0;Create;False;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;33;-2855.219,5642.359;Inherit;True;Color Mask;-1;;50;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;40;-2902.195,5901.869;Inherit;True;Color Mask;-1;;51;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;172;-1550.756,5866.92;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;178;-1862.19,6202.126;Inherit;False;Property;_V19;V19;113;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;177;-1864.584,6130.952;Inherit;False;Property;_S19;S19;112;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;176;-1865.35,6057.519;Inherit;False;Property;_H19;H19;111;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;42;-3242.093,6166.622;Inherit;False;Property;_Color19;Color 19;24;0;Create;True;0;0;0;False;0;False;0,0.3921569,0,0;0,0.3921568,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;169;-1295.78,5547.913;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;43;-3208.248,6397.182;Inherit;False;Property;_Color20;Color 20;25;0;Create;True;0;0;0;False;0;False;0,0,0.3921569,0;0,0,0.3921568,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;179;-1852.194,6357.917;Inherit;False;Property;_S20;S20;115;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;38;-2901.624,6147.554;Inherit;True;Color Mask;-1;;52;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;181;-1850.96,6287.484;Inherit;False;Property;_H20;H20;114;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;171;-1313.885,5864.853;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;182;-1854.8,6430.092;Inherit;False;Property;_V20;V20;116;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;180;-1545.388,6103.108;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp;184;-1292.992,6051.649;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;185;-1853.553,6588.25;Inherit;False;Property;_S21;S21;118;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;39;-2905.979,6382.708;Inherit;True;Color Mask;-1;;53;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;47;-3230.337,6643.137;Inherit;False;Property;_Color21;Color 21;26;0;Create;True;0;0;0;False;0;False;0.3921569,0.3921569,0,0;0.3921568,0.3921568,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.HSVToRGBNode;183;-1532.998,6322.073;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;187;-1854.319,6514.817;Inherit;False;Property;_H21;H21;117;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;186;-1851.159,6659.423;Inherit;False;Property;_V21;V21;119;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;46;-2904.319,6620.508;Inherit;True;Color Mask;-1;;54;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;191;-1844.96,6885.97;Inherit;False;Property;_V22;V22;122;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;190;-1844.269,6813.795;Inherit;False;Property;_S22;S22;121;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;189;-1843.035,6743.362;Inherit;False;Property;_H22;H22;120;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;188;-1270.857,6319.695;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.HSVToRGBNode;192;-1534.357,6560.406;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;48;-3213.655,6890.818;Inherit;False;Property;_Color22;Color 22;27;0;Create;True;0;0;0;False;0;False;0.3921569,0,0.3921569,0;0.3921568,0,0.3921568,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;194;-1272.526,6572.159;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;44;-2903.748,6866.194;Inherit;True;Color Mask;-1;;55;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;193;-1523.159,6777.951;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;196;-1843.713,7044.128;Inherit;False;Property;_S23;S23;124;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;49;-3210.372,7115.823;Inherit;False;Property;_Color23;Color 23;28;0;Create;True;0;0;0;False;0;False;0,0.3921569,0.3921569,0;0,0.3921568,0.3921568,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;200;-1841.319,7115.301;Inherit;False;Property;_V23;V23;125;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;197;-1844.479,6970.695;Inherit;False;Property;_H23;H23;123;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;45;-2908.103,7101.349;Inherit;True;Color Mask;-1;;56;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;195;-1257.992,6786.005;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;53;-3209.489,7359.834;Inherit;False;Property;_Color24;Color 24;29;0;Create;True;0;0;0;False;0;False;0.1960784,0,0,0;0.1960783,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;203;-1833.729,7445.957;Inherit;False;Property;_V24;V24;128;0;Create;False;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;198;-1524.518,7016.284;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;204;-1836.123,7374.784;Inherit;False;Property;_S24;S24;127;0;Create;False;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;205;-1836.889,7301.351;Inherit;False;Property;_H24;H24;126;0;Create;False;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;199;-1272.526,7018.537;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;52;-2913.226,7338.08;Inherit;True;Color Mask;-1;;57;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;208;-1828.362,7682.146;Inherit;False;Property;_V25;V25;131;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;207;-1830.756,7610.972;Inherit;False;Property;_S25;S25;130;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;206;-1831.522,7537.539;Inherit;False;Property;_H25;H25;129;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;54;-3239.47,7608.91;Inherit;False;Property;_Color25;Color 25;30;0;Create;True;0;0;0;False;0;False;0,0.1960784,0,0;0,0.1960783,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.HSVToRGBNode;202;-1516.928,7346.94;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;209;-1818.366,7837.937;Inherit;False;Property;_S26;S26;133;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;55;-3236.187,7836.694;Inherit;False;Property;_Color26;Color 26;31;0;Create;True;0;0;0;False;0;False;0,0,0.1960784,0;0,0,0.1960783,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;212;-1820.972,7910.11;Inherit;False;Property;_V26;V26;134;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;201;-1264.935,7349.193;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;50;-2943.455,7587.065;Inherit;True;Color Mask;-1;;58;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;210;-1511.56,7583.128;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;211;-1817.132,7767.504;Inherit;False;Property;_H26;H26;132;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;51;-2947.81,7822.221;Inherit;True;Color Mask;-1;;59;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;213;-1499.17,7802.093;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp;214;-1235.401,7579.191;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;216;-1817.331,8139.442;Inherit;False;Property;_V27;V27;137;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;59;-3258.278,8079.87;Inherit;False;Property;_Color27;Color 27;32;0;Create;True;0;0;0;False;0;False;0.1960784,0.1960784,0,0;0.1960783,0.1960783,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;217;-1820.491,7994.836;Inherit;False;Property;_H27;H27;135;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;215;-1819.725,8068.269;Inherit;False;Property;_S27;S27;136;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;222;-1500.529,8040.425;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;220;-1810.441,8293.813;Inherit;False;Property;_S28;S28;139;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;58;-2946.15,8060.02;Inherit;True;Color Mask;-1;;60;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;219;-1809.207,8223.381;Inherit;False;Property;_H28;H28;138;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;60;-3261.042,8319.218;Inherit;False;Property;_Color28;Color 28;33;0;Create;True;0;0;0;False;0;False;0.1960784,0,0.1960784,0;0.1960783,0,0.1960783,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;221;-1811.132,8365.988;Inherit;False;Property;_V28;V28;140;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;218;-1237.029,7799.715;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;226;-1809.885,8524.146;Inherit;False;Property;_S29;S29;171;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;224;-1238.698,8052.178;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;61;-3252.203,8555.337;Inherit;False;Property;_Color29;Color 29;34;0;Create;True;0;0;0;False;0;False;0,0.1960784,0.1960784,0;0,0.1960783,0.1960783,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.HSVToRGBNode;223;-1489.331,8257.97;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;230;-1807.491,8595.319;Inherit;False;Property;_V29;V29;199;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;227;-1810.651,8450.714;Inherit;False;Property;_H29;H29;159;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;56;-2945.579,8305.707;Inherit;True;Color Mask;-1;;61;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;381;-1794.141,8751.834;Inherit;False;Property;_S30;S30;175;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;356;-3230.954,8832.405;Inherit;False;Property;_Color30;Color 30;35;0;Create;True;0;0;0;False;0;False;0,0.1960784,0.1960784,0;0.7686275,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;225;-1224.163,8266.023;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.HSVToRGBNode;228;-1490.69,8496.303;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.FunctionNode;57;-2946.346,8558.144;Inherit;True;Color Mask;-1;;62;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;385;-1794.907,8678.401;Inherit;False;Property;_H30;H30;160;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;382;-1794.855,8823.007;Inherit;False;Property;_V30;V30;198;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;369;-2946.724,8802.306;Inherit;True;Color Mask;-1;;76;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;229;-1241.136,8513.187;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;389;-1782.907,9052.84;Inherit;False;Property;_V31;V31;184;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;390;-1789.176,8908.234;Inherit;False;Property;_H31;H31;158;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;388;-1785.301,8981.667;Inherit;False;Property;_S31;S31;176;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;383;-1474.946,8723.99;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;357;-3219.897,9064.924;Inherit;False;Property;_Color31;Color 31;36;0;Create;True;0;0;0;False;0;False;0,0.1960784,0.1960784,0;0,0.7686275,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;384;-1222.954,8726.243;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;358;-3229.998,9327.605;Inherit;False;Property;_Color32;Color 32;37;0;Create;True;0;0;0;False;0;False;0,0.1960784,0.1960784,0;0,0,0.7686275,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.HSVToRGBNode;386;-1466.106,8953.823;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;393;-1779.407,9211.504;Inherit;False;Property;_S32;S32;174;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;370;-2947.491,9054.742;Inherit;True;Color Mask;-1;;77;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;395;-1780.173,9138.071;Inherit;False;Property;_H32;H32;149;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;394;-1777.013,9282.677;Inherit;False;Property;_V32;V32;200;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;399;-1765.227,9524.298;Inherit;False;Property;_V33;V33;182;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;391;-1460.212,9183.66;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;359;-3240.102,9575.138;Inherit;False;Property;_Color33;Color 33;38;0;Create;True;0;0;0;False;0;False;0,0.1960784,0.1960784,0;0.7686275,0.7686275,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;371;-2946.722,9312.525;Inherit;True;Color Mask;-1;;78;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;400;-1768.387,9379.692;Inherit;False;Property;_H33;H33;145;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;387;-1214.114,8956.076;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;398;-1767.621,9453.125;Inherit;False;Property;_S33;S33;177;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;396;-1448.426,9425.281;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;405;-1768.387,9621.313;Inherit;False;Property;_H34;H34;142;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;403;-1767.621,9694.745;Inherit;False;Property;_S34;S34;179;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;392;-1208.22,9185.913;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;372;-2947.489,9564.962;Inherit;True;Color Mask;-1;;79;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;360;-3240.103,9802.466;Inherit;False;Property;_Color34;Color 34;39;0;Create;True;0;0;0;False;0;False;0,0.1960784,0.1960784,0;0.7686275,0,0.7686275,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;404;-1765.227,9765.918;Inherit;False;Property;_V34;V34;186;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;361;-3230,10065.15;Inherit;False;Property;_Color35;Color 35;40;0;Create;True;0;0;0;False;0;False;0,0.1960784,0.1960784,0;0,0.7686275,0.7686275,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;397;-1196.434,9427.534;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;373;-2936.62,9802.534;Inherit;True;Color Mask;-1;;80;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;401;-1448.426,9666.901;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;408;-1767.62,9930.472;Inherit;False;Property;_S35;S35;178;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;410;-1768.386,9857.04;Inherit;False;Property;_H35;H35;143;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;409;-1765.227,10001.64;Inherit;False;Property;_V35;V35;197;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;415;-1756.599,10092.77;Inherit;False;Property;_H36;H36;148;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;406;-1448.426,9902.628;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;413;-1755.833,10166.2;Inherit;False;Property;_S36;S36;180;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;414;-1753.44,10237.37;Inherit;False;Property;_V36;V36;181;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;402;-1196.434,9669.154;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;374;-2937.387,10054.97;Inherit;True;Color Mask;-1;;81;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;362;-3240.103,10292.47;Inherit;False;Property;_Color36;Color 36;41;0;Create;True;0;0;0;False;0;False;0,0.1960784,0.1960784,0;0.7686275,0.1960784,0.1960784,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;407;-1196.433,9904.881;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;420;-1747.76,10331.45;Inherit;False;Property;_H37;H37;144;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;363;-3235.052,10545.06;Inherit;False;Property;_Color37;Color 37;42;0;Create;True;0;0;0;False;0;False;0,0.1960784,0.1960784,0;0.1960784,0.7686275,0.1960783,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;375;-2936.621,10287.5;Inherit;True;Color Mask;-1;;82;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;419;-1744.601,10476.05;Inherit;False;Property;_V37;V37;196;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;418;-1746.994,10404.88;Inherit;False;Property;_S37;S37;161;0;Create;True;0;0;0;False;0;False;0.5;0.9865491;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;411;-1436.639,10138.36;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.HSVToRGBNode;416;-1427.8,10377.04;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp;412;-1184.646,10140.61;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;364;-3235.051,10792.59;Inherit;False;Property;_Color38;Color 38;43;0;Create;True;0;0;0;False;0;False;0,0.1960784,0.1960784,0;0.1960784,0.1960783,0.7686275,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;425;-1750.706,10576.02;Inherit;False;Property;_H38;H38;141;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;423;-1749.94,10649.45;Inherit;False;Property;_S38;S38;173;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;424;-1747.547,10720.62;Inherit;False;Property;_V38;V38;187;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;376;-2937.387,10539.93;Inherit;True;Color Mask;-1;;83;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;377;-2931.569,10782.56;Inherit;True;Color Mask;-1;;84;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;365;-3224.948,11055.27;Inherit;False;Property;_Color39;Color 39;44;0;Create;True;0;0;0;False;0;False;0,0.1960784,0.1960784,0;0.7686275,0.7686275,0.1960783,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;417;-1175.807,10379.29;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.HSVToRGBNode;421;-1430.746,10621.61;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;430;-1750.707,10817.64;Inherit;False;Property;_H39;H39;147;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;428;-1749.941,10891.07;Inherit;False;Property;_S39;S39;172;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;429;-1747.548,10962.24;Inherit;False;Property;_V39;V39;185;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;434;-1735.762,11203.86;Inherit;False;Property;_V40;V40;183;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;435;-1738.921,11059.26;Inherit;False;Property;_H40;H40;146;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;422;-1178.753,10623.86;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;433;-1738.155,11132.69;Inherit;False;Property;_S40;S40;170;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;378;-2932.334,11034.99;Inherit;True;Color Mask;-1;;85;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;366;-3224.948,11302.81;Inherit;False;Property;_Color40;Color 40;45;0;Create;True;0;0;0;False;0;False;0,0.1960784,0.1960784,0;0.7686275,0.1960783,0.7686275,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.HSVToRGBNode;426;-1430.747,10863.23;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.FunctionNode;379;-2931.57,11297.82;Inherit;True;Color Mask;-1;;86;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;427;-1178.754,10865.48;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.HSVToRGBNode;431;-1418.961,11104.85;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;438;-1744.048,11374.31;Inherit;False;Property;_S41;S41;164;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;440;-1744.814,11300.88;Inherit;False;Property;_H41;H41;151;0;Create;True;0;0;0;False;0;False;0.5;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;439;-1741.655,11445.48;Inherit;False;Property;_V41;V41;188;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;367;-3261.682,11543.9;Inherit;False;Property;_Color41;Color 41;46;0;Create;True;0;0;0;False;0;False;0,0.1960784,0.1960784,0;0.1960784,0.7686275,0.7686275,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;432;-1166.968,11107.1;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;380;-2969.069,11553.92;Inherit;True;Color Mask;-1;;87;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;445;-1745.385,11709.58;Inherit;False;Property;_V42;V42;194;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;436;-1424.854,11346.47;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;442;-3265.411,11808;Inherit;False;Property;_Color42;Color 42;47;0;Create;True;0;0;0;False;0;False;0,0.1960784,0.1960784,0;0.1960784,0.7686275,0.7686275,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;443;-1748.544,11564.97;Inherit;False;Property;_H42;H42;154;0;Create;True;0;0;0;False;0;False;0.5;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;444;-1747.778,11638.4;Inherit;False;Property;_S42;S42;165;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;452;-1745.385,11977.73;Inherit;False;Property;_V43;V43;193;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;441;-2972.798,11818.01;Inherit;True;Color Mask;-1;;88;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;450;-1748.544,11833.12;Inherit;False;Property;_H43;H43;150;0;Create;True;0;0;0;False;0;False;0.5;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;446;-1428.584,11610.56;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp;437;-1172.861,11348.72;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;448;-3265.41,12076.15;Inherit;False;Property;_Color43;Color 43;48;0;Create;True;0;0;0;False;0;False;0,0.1960784,0.1960784,0;0.1960784,0.7686275,0.7686275,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;451;-1747.778,11906.55;Inherit;False;Property;_S43;S43;166;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;459;-1749.059,12223.84;Inherit;False;Property;_V44;V44;192;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;455;-3269.084,12322.26;Inherit;False;Property;_Color44;Color 44;49;0;Create;True;0;0;0;False;0;False;0,0.1960784,0.1960784,0;0.1960784,0.7686275,0.7686275,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;458;-1751.452,12152.66;Inherit;False;Property;_S44;S44;162;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;447;-1176.591,11612.81;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.HSVToRGBNode;453;-1428.584,11878.71;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.FunctionNode;449;-2972.797,12086.16;Inherit;True;Color Mask;-1;;89;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;457;-1752.218,12079.23;Inherit;False;Property;_H44;H44;157;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;454;-1176.591,11880.96;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;464;-1748.545,12321.67;Inherit;False;Property;_H45;H45;153;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;462;-3265.41,12564.69;Inherit;False;Property;_Color45;Color 45;50;0;Create;True;0;0;0;False;0;False;0,0.1960784,0.1960784,0;0.1960784,0.7686275,0.7686275,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;466;-1745.386,12466.27;Inherit;False;Property;_V45;V45;191;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;465;-1747.779,12395.09;Inherit;False;Property;_S45;S45;168;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;460;-1432.258,12124.82;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.FunctionNode;456;-2976.471,12332.27;Inherit;True;Color Mask;-1;;90;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;471;-1741.198,12582.46;Inherit;False;Property;_H46;H46;155;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;467;-1428.585,12367.25;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.FunctionNode;463;-2972.797,12574.7;Inherit;True;Color Mask;-1;;91;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;484;-3281.614,12830.82;Inherit;False;Property;_Color46;Color 46;53;0;Create;True;0;0;0;False;0;False;0,0.1960784,0.1960784,0;0.1960784,0.7686275,0.7686275,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;473;-1738.039,12727.06;Inherit;False;Property;_V46;V46;190;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;461;-1180.265,12127.07;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;472;-1740.432,12655.88;Inherit;False;Property;_S46;S46;167;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;468;-1176.592,12369.5;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;469;-3279.723,13096.24;Inherit;False;Property;_Color47;Color 47;51;0;Create;True;0;0;0;False;0;False;0,0.1960784,0.1960784,0;0.1960784,0.7686275,0.7686275,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;480;-1730.693,12987.86;Inherit;False;Property;_V47;V47;189;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;474;-1421.238,12628.04;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;478;-1733.852,12843.26;Inherit;False;Property;_H47;H47;152;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;479;-1733.086,12916.68;Inherit;False;Property;_S47;S47;169;0;Create;True;0;0;0;False;0;False;0.7404315;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;470;-2965.45,12835.49;Inherit;True;Color Mask;-1;;92;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;477;-3002.183,13092.63;Inherit;True;Color Mask;-1;;93;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;486;-1738.858,13171.19;Inherit;False;Property;_S48;S48;163;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;485;-1739.624,13097.77;Inherit;False;Property;_H48;H48;156;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;487;-1736.465,13245.39;Inherit;False;Property;_V48;V48;195;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;481;-1413.892,12888.84;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;476;-3286.672,13342.54;Inherit;False;Property;_Color48;Color 48;52;0;Create;True;0;0;0;False;0;False;0,0.1960784,0.1960784,0;0.1960784,0.7686275,0.7686275,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;475;-1169.245,12630.29;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;482;-1161.899,12891.09;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;483;-3007.955,13347.14;Inherit;True;Color Mask;-1;;94;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;488;-1419.664,13143.35;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;261;-879.4981,12829.4;Inherit;False;Property;_Metal;Metal;0;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;262;-875.7679,12906.49;Inherit;False;Property;_Smoothness;Smoothness;1;0;Create;True;0;0;0;False;0;False;0.2;0.2;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;489;-1189.364,13137.46;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;493;-494.068,12822.48;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;DepthOnly;0;3;DepthOnly;0;False;False;False;False;False;False;False;False;True;0;False;-1;True;0;False;-1;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;False;False;False;False;0;False;-1;False;False;False;False;True;1;False;-1;False;False;True;1;LightMode=DepthOnly;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;492;-494.068,12822.48;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;ShadowCaster;0;2;ShadowCaster;0;False;False;False;False;False;False;False;False;True;0;False;-1;True;0;False;-1;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;False;False;False;False;False;False;False;False;True;0;False;-1;False;False;False;False;False;False;True;1;False;-1;True;3;False;-1;False;True;1;LightMode=ShadowCaster;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;494;-494.068,12822.48;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;Meta;0;4;Meta;0;False;False;False;False;False;False;False;False;True;0;False;-1;True;0;False;-1;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;False;False;False;True;1;LightMode=Meta;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;490;-494.068,12822.48;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;ExtraPrePass;0;0;ExtraPrePass;5;False;False;False;False;False;False;False;False;True;0;False;-1;True;0;False;-1;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;True;1;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;False;False;False;False;False;False;False;False;True;0;False;-1;True;True;True;True;True;0;False;-1;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;0;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;491;-494.068,12822.48;Float;False;True;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;2;InfinityPBR/LPColor;94348b07e5e8bab40bd6c8a1e3df54cd;True;Forward;0;1;Forward;17;False;False;False;False;False;False;False;False;True;0;False;-1;True;0;False;-1;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;True;1;1;False;-1;0;False;-1;1;1;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;-1;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;LightMode=UniversalForward;False;0;Hidden/InternalErrorShader;0;0;Standard;36;Workflow;1;Surface;0;  Refraction Model;0;  Blend;0;Two Sided;1;Fragment Normal Space,InvertActionOnDeselection;0;Transmission;0;  Transmission Shadow;0.5,False,-1;Translucency;0;  Translucency Strength;1,False,-1;  Normal Distortion;0.5,False,-1;  Scattering;2,False,-1;  Direct;0.9,False,-1;  Ambient;0.1,False,-1;  Shadow;0.5,False,-1;Cast Shadows;1;  Use Shadow Threshold;0;Receive Shadows;1;GPU Instancing;1;LOD CrossFade;1;Built-in Fog;1;_FinalColorxAlpha;0;Meta Pass;1;Override Baked GI;0;Extra Pre Pass;0;DOTS Instancing;0;Tessellation;0;  Phong;0;  Strength;0.5,False,-1;  Type;0;  Tess;16,False,-1;  Min;10,False,-1;  Max;25,False,-1;  Edge Length;16,False,-1;  Max Displacement;25,False,-1;Vertex Position,InvertActionOnDeselection;1;0;6;False;True;True;True;True;True;False;;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;495;-494.068,12822.48;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;Universal2D;0;5;Universal2D;0;False;False;False;False;False;False;False;False;True;0;False;-1;True;0;False;-1;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;True;1;1;False;-1;0;False;-1;1;1;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;-1;False;False;False;False;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;LightMode=Universal2D;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
WireConnection;1;1;2;0
WireConnection;1;3;3;0
WireConnection;1;4;271;0
WireConnection;1;5;62;0
WireConnection;78;0;79;0
WireConnection;78;1;83;0
WireConnection;78;2;84;0
WireConnection;6;1;2;0
WireConnection;6;3;4;0
WireConnection;6;4;271;0
WireConnection;6;5;62;0
WireConnection;267;0;78;0
WireConnection;267;1;78;0
WireConnection;267;2;1;0
WireConnection;90;0;89;0
WireConnection;90;1;88;0
WireConnection;90;2;87;0
WireConnection;7;1;2;0
WireConnection;7;3;5;0
WireConnection;7;4;271;0
WireConnection;7;5;62;0
WireConnection;86;0;267;0
WireConnection;86;1;90;0
WireConnection;86;2;6;0
WireConnection;91;0;92;0
WireConnection;91;1;93;0
WireConnection;91;2;94;0
WireConnection;98;0;95;0
WireConnection;98;1;96;0
WireConnection;98;2;97;0
WireConnection;10;1;2;0
WireConnection;10;3;11;0
WireConnection;10;4;271;0
WireConnection;10;5;62;0
WireConnection;107;0;86;0
WireConnection;107;1;91;0
WireConnection;107;2;7;0
WireConnection;99;0;100;0
WireConnection;99;1;101;0
WireConnection;99;2;102;0
WireConnection;8;1;2;0
WireConnection;8;3;12;0
WireConnection;8;4;271;0
WireConnection;8;5;62;0
WireConnection;108;0;107;0
WireConnection;108;1;98;0
WireConnection;108;2;10;0
WireConnection;106;0;103;0
WireConnection;106;1;104;0
WireConnection;106;2;105;0
WireConnection;109;0;108;0
WireConnection;109;1;99;0
WireConnection;109;2;8;0
WireConnection;9;1;2;0
WireConnection;9;3;13;0
WireConnection;9;4;271;0
WireConnection;9;5;62;0
WireConnection;114;0;112;0
WireConnection;114;1;111;0
WireConnection;114;2;113;0
WireConnection;16;1;2;0
WireConnection;16;3;17;0
WireConnection;16;4;271;0
WireConnection;16;5;62;0
WireConnection;110;0;109;0
WireConnection;110;1;106;0
WireConnection;110;2;9;0
WireConnection;115;0;110;0
WireConnection;115;1;114;0
WireConnection;115;2;16;0
WireConnection;120;0;116;0
WireConnection;120;1;117;0
WireConnection;120;2;118;0
WireConnection;14;1;2;0
WireConnection;14;3;18;0
WireConnection;14;4;271;0
WireConnection;14;5;62;0
WireConnection;15;1;2;0
WireConnection;15;3;19;0
WireConnection;15;4;271;0
WireConnection;15;5;62;0
WireConnection;123;0;121;0
WireConnection;123;1;119;0
WireConnection;123;2;122;0
WireConnection;124;0;115;0
WireConnection;124;1;120;0
WireConnection;124;2;14;0
WireConnection;132;0;127;0
WireConnection;132;1;125;0
WireConnection;132;2;126;0
WireConnection;128;0;124;0
WireConnection;128;1;123;0
WireConnection;128;2;15;0
WireConnection;22;1;2;0
WireConnection;22;3;23;0
WireConnection;22;4;271;0
WireConnection;22;5;62;0
WireConnection;133;0;129;0
WireConnection;133;1;130;0
WireConnection;133;2;131;0
WireConnection;20;1;2;0
WireConnection;20;3;24;0
WireConnection;20;4;271;0
WireConnection;20;5;62;0
WireConnection;134;0;128;0
WireConnection;134;1;132;0
WireConnection;134;2;22;0
WireConnection;21;1;2;0
WireConnection;21;3;25;0
WireConnection;21;4;271;0
WireConnection;21;5;62;0
WireConnection;135;0;134;0
WireConnection;135;1;133;0
WireConnection;135;2;20;0
WireConnection;138;0;137;0
WireConnection;138;1;136;0
WireConnection;138;2;140;0
WireConnection;142;0;145;0
WireConnection;142;1;144;0
WireConnection;142;2;143;0
WireConnection;28;1;2;0
WireConnection;28;3;29;0
WireConnection;28;4;271;0
WireConnection;28;5;62;0
WireConnection;139;0;135;0
WireConnection;139;1;138;0
WireConnection;139;2;21;0
WireConnection;141;0;139;0
WireConnection;141;1;142;0
WireConnection;141;2;28;0
WireConnection;150;0;146;0
WireConnection;150;1;147;0
WireConnection;150;2;148;0
WireConnection;26;1;2;0
WireConnection;26;3;30;0
WireConnection;26;4;271;0
WireConnection;26;5;62;0
WireConnection;153;0;151;0
WireConnection;153;1;149;0
WireConnection;153;2;152;0
WireConnection;154;0;141;0
WireConnection;154;1;150;0
WireConnection;154;2;26;0
WireConnection;27;1;2;0
WireConnection;27;3;31;0
WireConnection;27;4;271;0
WireConnection;27;5;62;0
WireConnection;34;1;2;0
WireConnection;34;3;35;0
WireConnection;34;4;271;0
WireConnection;34;5;62;0
WireConnection;162;0;157;0
WireConnection;162;1;155;0
WireConnection;162;2;156;0
WireConnection;158;0;154;0
WireConnection;158;1;153;0
WireConnection;158;2;27;0
WireConnection;164;0;158;0
WireConnection;164;1;162;0
WireConnection;164;2;34;0
WireConnection;32;1;2;0
WireConnection;32;3;36;0
WireConnection;32;4;271;0
WireConnection;32;5;62;0
WireConnection;163;0;159;0
WireConnection;163;1;160;0
WireConnection;163;2;161;0
WireConnection;165;0;164;0
WireConnection;165;1;163;0
WireConnection;165;2;32;0
WireConnection;168;0;167;0
WireConnection;168;1;166;0
WireConnection;168;2;170;0
WireConnection;33;1;2;0
WireConnection;33;3;37;0
WireConnection;33;4;271;0
WireConnection;33;5;62;0
WireConnection;40;1;2;0
WireConnection;40;3;41;0
WireConnection;40;4;271;0
WireConnection;40;5;62;0
WireConnection;172;0;175;0
WireConnection;172;1;174;0
WireConnection;172;2;173;0
WireConnection;169;0;165;0
WireConnection;169;1;168;0
WireConnection;169;2;33;0
WireConnection;38;1;2;0
WireConnection;38;3;42;0
WireConnection;38;4;271;0
WireConnection;38;5;62;0
WireConnection;171;0;169;0
WireConnection;171;1;172;0
WireConnection;171;2;40;0
WireConnection;180;0;176;0
WireConnection;180;1;177;0
WireConnection;180;2;178;0
WireConnection;184;0;171;0
WireConnection;184;1;180;0
WireConnection;184;2;38;0
WireConnection;39;1;2;0
WireConnection;39;3;43;0
WireConnection;39;4;271;0
WireConnection;39;5;62;0
WireConnection;183;0;181;0
WireConnection;183;1;179;0
WireConnection;183;2;182;0
WireConnection;46;1;2;0
WireConnection;46;3;47;0
WireConnection;46;4;271;0
WireConnection;46;5;62;0
WireConnection;188;0;184;0
WireConnection;188;1;183;0
WireConnection;188;2;39;0
WireConnection;192;0;187;0
WireConnection;192;1;185;0
WireConnection;192;2;186;0
WireConnection;194;0;188;0
WireConnection;194;1;192;0
WireConnection;194;2;46;0
WireConnection;44;1;2;0
WireConnection;44;3;48;0
WireConnection;44;4;271;0
WireConnection;44;5;62;0
WireConnection;193;0;189;0
WireConnection;193;1;190;0
WireConnection;193;2;191;0
WireConnection;45;1;2;0
WireConnection;45;3;49;0
WireConnection;45;4;271;0
WireConnection;45;5;62;0
WireConnection;195;0;194;0
WireConnection;195;1;193;0
WireConnection;195;2;44;0
WireConnection;198;0;197;0
WireConnection;198;1;196;0
WireConnection;198;2;200;0
WireConnection;199;0;195;0
WireConnection;199;1;198;0
WireConnection;199;2;45;0
WireConnection;52;1;2;0
WireConnection;52;3;53;0
WireConnection;52;4;271;0
WireConnection;52;5;62;0
WireConnection;202;0;205;0
WireConnection;202;1;204;0
WireConnection;202;2;203;0
WireConnection;201;0;199;0
WireConnection;201;1;202;0
WireConnection;201;2;52;0
WireConnection;50;1;2;0
WireConnection;50;3;54;0
WireConnection;50;4;271;0
WireConnection;50;5;62;0
WireConnection;210;0;206;0
WireConnection;210;1;207;0
WireConnection;210;2;208;0
WireConnection;51;1;2;0
WireConnection;51;3;55;0
WireConnection;51;4;271;0
WireConnection;51;5;62;0
WireConnection;213;0;211;0
WireConnection;213;1;209;0
WireConnection;213;2;212;0
WireConnection;214;0;201;0
WireConnection;214;1;210;0
WireConnection;214;2;50;0
WireConnection;222;0;217;0
WireConnection;222;1;215;0
WireConnection;222;2;216;0
WireConnection;58;1;2;0
WireConnection;58;3;59;0
WireConnection;58;4;271;0
WireConnection;58;5;62;0
WireConnection;218;0;214;0
WireConnection;218;1;213;0
WireConnection;218;2;51;0
WireConnection;224;0;218;0
WireConnection;224;1;222;0
WireConnection;224;2;58;0
WireConnection;223;0;219;0
WireConnection;223;1;220;0
WireConnection;223;2;221;0
WireConnection;56;1;2;0
WireConnection;56;3;60;0
WireConnection;56;4;271;0
WireConnection;56;5;62;0
WireConnection;225;0;224;0
WireConnection;225;1;223;0
WireConnection;225;2;56;0
WireConnection;228;0;227;0
WireConnection;228;1;226;0
WireConnection;228;2;230;0
WireConnection;57;1;2;0
WireConnection;57;3;61;0
WireConnection;57;4;271;0
WireConnection;57;5;62;0
WireConnection;369;1;2;0
WireConnection;369;3;356;0
WireConnection;369;4;271;0
WireConnection;369;5;62;0
WireConnection;229;0;225;0
WireConnection;229;1;228;0
WireConnection;229;2;57;0
WireConnection;383;0;385;0
WireConnection;383;1;381;0
WireConnection;383;2;382;0
WireConnection;384;0;229;0
WireConnection;384;1;383;0
WireConnection;384;2;369;0
WireConnection;386;0;390;0
WireConnection;386;1;388;0
WireConnection;386;2;389;0
WireConnection;370;1;2;0
WireConnection;370;3;357;0
WireConnection;370;4;271;0
WireConnection;370;5;62;0
WireConnection;391;0;395;0
WireConnection;391;1;393;0
WireConnection;391;2;394;0
WireConnection;371;1;2;0
WireConnection;371;3;358;0
WireConnection;371;4;271;0
WireConnection;371;5;62;0
WireConnection;387;0;384;0
WireConnection;387;1;386;0
WireConnection;387;2;370;0
WireConnection;396;0;400;0
WireConnection;396;1;398;0
WireConnection;396;2;399;0
WireConnection;392;0;387;0
WireConnection;392;1;391;0
WireConnection;392;2;371;0
WireConnection;372;1;2;0
WireConnection;372;3;359;0
WireConnection;372;4;271;0
WireConnection;372;5;62;0
WireConnection;397;0;392;0
WireConnection;397;1;396;0
WireConnection;397;2;372;0
WireConnection;373;1;2;0
WireConnection;373;3;360;0
WireConnection;373;4;271;0
WireConnection;373;5;62;0
WireConnection;401;0;405;0
WireConnection;401;1;403;0
WireConnection;401;2;404;0
WireConnection;406;0;410;0
WireConnection;406;1;408;0
WireConnection;406;2;409;0
WireConnection;402;0;397;0
WireConnection;402;1;401;0
WireConnection;402;2;373;0
WireConnection;374;1;2;0
WireConnection;374;3;361;0
WireConnection;374;4;271;0
WireConnection;374;5;62;0
WireConnection;407;0;402;0
WireConnection;407;1;406;0
WireConnection;407;2;374;0
WireConnection;375;1;2;0
WireConnection;375;3;362;0
WireConnection;375;4;271;0
WireConnection;375;5;62;0
WireConnection;411;0;415;0
WireConnection;411;1;413;0
WireConnection;411;2;414;0
WireConnection;416;0;420;0
WireConnection;416;1;418;0
WireConnection;416;2;419;0
WireConnection;412;0;407;0
WireConnection;412;1;411;0
WireConnection;412;2;375;0
WireConnection;376;1;2;0
WireConnection;376;3;363;0
WireConnection;376;4;271;0
WireConnection;376;5;62;0
WireConnection;377;1;2;0
WireConnection;377;3;364;0
WireConnection;377;4;271;0
WireConnection;377;5;62;0
WireConnection;417;0;412;0
WireConnection;417;1;416;0
WireConnection;417;2;376;0
WireConnection;421;0;425;0
WireConnection;421;1;423;0
WireConnection;421;2;424;0
WireConnection;422;0;417;0
WireConnection;422;1;421;0
WireConnection;422;2;377;0
WireConnection;378;1;2;0
WireConnection;378;3;365;0
WireConnection;378;4;271;0
WireConnection;378;5;62;0
WireConnection;426;0;430;0
WireConnection;426;1;428;0
WireConnection;426;2;429;0
WireConnection;379;1;2;0
WireConnection;379;3;366;0
WireConnection;379;4;271;0
WireConnection;379;5;62;0
WireConnection;427;0;422;0
WireConnection;427;1;426;0
WireConnection;427;2;378;0
WireConnection;431;0;435;0
WireConnection;431;1;433;0
WireConnection;431;2;434;0
WireConnection;432;0;427;0
WireConnection;432;1;431;0
WireConnection;432;2;379;0
WireConnection;380;1;2;0
WireConnection;380;3;367;0
WireConnection;380;4;271;0
WireConnection;380;5;62;0
WireConnection;436;0;440;0
WireConnection;436;1;438;0
WireConnection;436;2;439;0
WireConnection;441;1;2;0
WireConnection;441;3;442;0
WireConnection;441;4;271;0
WireConnection;441;5;62;0
WireConnection;446;0;443;0
WireConnection;446;1;444;0
WireConnection;446;2;445;0
WireConnection;437;0;432;0
WireConnection;437;1;436;0
WireConnection;437;2;380;0
WireConnection;447;0;437;0
WireConnection;447;1;446;0
WireConnection;447;2;441;0
WireConnection;453;0;450;0
WireConnection;453;1;451;0
WireConnection;453;2;452;0
WireConnection;449;1;2;0
WireConnection;449;3;448;0
WireConnection;449;4;271;0
WireConnection;449;5;62;0
WireConnection;454;0;447;0
WireConnection;454;1;453;0
WireConnection;454;2;449;0
WireConnection;460;0;457;0
WireConnection;460;1;458;0
WireConnection;460;2;459;0
WireConnection;456;1;2;0
WireConnection;456;3;455;0
WireConnection;456;4;271;0
WireConnection;456;5;62;0
WireConnection;467;0;464;0
WireConnection;467;1;465;0
WireConnection;467;2;466;0
WireConnection;463;1;2;0
WireConnection;463;3;462;0
WireConnection;463;4;271;0
WireConnection;463;5;62;0
WireConnection;461;0;454;0
WireConnection;461;1;460;0
WireConnection;461;2;456;0
WireConnection;468;0;461;0
WireConnection;468;1;467;0
WireConnection;468;2;463;0
WireConnection;474;0;471;0
WireConnection;474;1;472;0
WireConnection;474;2;473;0
WireConnection;470;1;2;0
WireConnection;470;3;484;0
WireConnection;470;4;271;0
WireConnection;470;5;62;0
WireConnection;477;1;2;0
WireConnection;477;3;469;0
WireConnection;477;4;271;0
WireConnection;477;5;62;0
WireConnection;481;0;478;0
WireConnection;481;1;479;0
WireConnection;481;2;480;0
WireConnection;475;0;468;0
WireConnection;475;1;474;0
WireConnection;475;2;470;0
WireConnection;482;0;475;0
WireConnection;482;1;481;0
WireConnection;482;2;477;0
WireConnection;483;1;2;0
WireConnection;483;3;476;0
WireConnection;483;4;271;0
WireConnection;483;5;62;0
WireConnection;488;0;485;0
WireConnection;488;1;486;0
WireConnection;488;2;487;0
WireConnection;489;0;482;0
WireConnection;489;1;488;0
WireConnection;489;2;483;0
WireConnection;491;0;489;0
WireConnection;491;3;261;0
WireConnection;491;4;262;0
ASEEND*/
//CHKSM=6CA899E4D6F280579EBF2728062BEE43D96D5C55