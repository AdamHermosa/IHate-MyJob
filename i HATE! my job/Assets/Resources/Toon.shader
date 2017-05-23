﻿Shader "Cel Shader" 
{
	Properties
	{
		_Color("Diffuse Material Color", Color) = (1,1,1,1)
		_UnlitColor("Unlit Color", Color) = (0.5,0.5,0.5,1)
		_DiffuseThreshold("Lighting Threshold", Range(-1.1,1)) = 0.1
		_SpecColor("Specular Material Color", Color) = (1,1,1,1)
		_Shininess("Shininess", Range(0.5,1)) = 1
		_OutlineThickness("Outline Thickness", Range(0,1)) = 0.1
	}

	SubShader
	{
		Pass{
		Tags{ "LightMode" = "ForwardBase" }

		CGPROGRAM

		#pragma vertex vert
		#pragma fragment frag

		uniform float4 _Color;
		uniform float4 _UnlitColor;
		uniform float _DiffuseThreshold;
		uniform float4 _SpecColor;
		uniform float _Shininess;
		uniform float _OutlineThickness;

		uniform float4 _LightColor0;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;

		struct vertexInput
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float4 texcoord : TEXCOORD0;
		};

		struct vertexOutput 
		{
			float4 pos : SV_POSITION;
			float3 normalDir : TEXCOORD1;
			float4 lightDir : TEXCOORD2;
			float3 viewDir : TEXCOORD3;
			float2 uv : TEXCOORD0;
		};

		vertexOutput vert(vertexInput input)
		{
			vertexOutput output;

			//normal direction
			output.normalDir = normalize(mul(float4(input.normal, 0.0), unity_WorldToObject).xyz);

			//world position
			float4 posWorld = mul(unity_ObjectToWorld, input.vertex);

			//view direction
			output.viewDir = normalize(_WorldSpaceCameraPos.xyz - posWorld.xyz);

			float3 fragmentToLightSource = (_WorldSpaceCameraPos.xyz - posWorld.xyz);
			output.lightDir = float4(
				normalize(lerp(_WorldSpaceLightPos0.xyz , fragmentToLightSource, _WorldSpaceLightPos0.w)),
				lerp(1.0 , 1.0 / length(fragmentToLightSource), _WorldSpaceLightPos0.w)
				);

			output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
			output.uv = input.texcoord;

			return output;
		}

		float4 frag(vertexOutput input) : COLOR
		{
			float nDotL = saturate(dot(input.normalDir, input.lightDir.xyz));

			float diffuseCutoff = saturate((max(_DiffuseThreshold, nDotL) - _DiffuseThreshold) * 1000);

			float specularCutoff = saturate(max(_Shininess, dot(reflect(-input.lightDir.xyz, input.normalDir), input.viewDir)) - _Shininess) * 1000;
			//float specularCutoff = saturate(max(_Shininess, dot(reflect(-input.lightDir.xyz, input.normalDir), input.viewDir))) * 1000;

			// my shitty "outline" calculation
			float outlineStrength = saturate((dot(input.normalDir, input.viewDir) - _OutlineThickness) * 500);

			// color calculation
			float3 ambientLight = (1 - diffuseCutoff) * _UnlitColor.xyz;
			float3 diffuseReflection = (1 - specularCutoff) * _Color.xyz * diffuseCutoff;
			float3 specularReflection = _SpecColor.xyz * specularCutoff;

			float3 combinedLight = (ambientLight + diffuseReflection) * outlineStrength + specularReflection;

			return float4(combinedLight, 1.0); 
		}

		ENDCG

		}
	}
	
	Fallback "Ambient"

}