Shader "Custom/CircleHealth" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainColor("MainColor", Color) = (1,1,1,1)
		_MainTex("Texture", 2D) = "white" {}
		_TransitionTex("Transition Texture", 2D) = "white" {}
		_Cutoff("_Cutoff", Range(0,1.01)) = 0.0
	}
		SubShader{
			Tags 
			{ 
			"RenderType" = "Transparent" 
			"Queue" = "Transparent" 
			"IgnoreProjector" = "True" 
			"PreviewType" = "Plane" 
			"CanUseSpriteAtlas" = "True" 
			}
			//LOD 200
			ZWrite Off
			Cull Off
			Lighting Off
			Blend One OneMinusSrcAlpha
			Pass
			{

			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			//#pragma surface surf Standard fullforwardshadows
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			// Use shader model 3.0 target, to get nicer looking lighting
			//#pragma target 3.0

			struct AppData {
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct VtoF {
		float2 uv : TEXCOORD0;
		float2 uv1 : TEXCOORD1;
		float4 vertex : SV_POSITION;
	};

	float4 _MainTex_TexelSize;
	VtoF vert(AppData i) {
		VtoF Output;
		Output.vertex = UnityObjectToClipPos(i.vertex);
		Output.uv = i.uv;
		Output.uv1 = i.uv;
	#if UNITY_UV_STARTS_AT_TOP
		if (_MainTex_TexelSize.y < 0) {
			Output.uv1.y = 1 - Output.uv1.y;
		}
	#endif
		return Output;
	}

	sampler2D _TransitionTex;
	sampler2D _MainTex;

	float _Cutoff;
	fixed4 _Color;
	fixed4 _MainColor;

	fixed4 frag(VtoF input) : SV_Target
	{
		fixed4 fill = tex2D(_TransitionTex, input.uv1);
		fixed4 color = tex2D(_MainTex, input.uv + _Cutoff * float2(0,0));
		if (fill.r < _Cutoff) {
			return color = lerp(color, _Color, 1);
		}
		color.rgb *= color.a;
		return color * _MainColor;
	}
	ENDCG
	}
	}
}