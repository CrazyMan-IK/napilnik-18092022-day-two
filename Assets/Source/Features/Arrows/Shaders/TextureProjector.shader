// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Projector/Texture" {
	Properties{
		_Texture("Cookie", 2D) = "gray" {}
	}
		Subshader{
			Tags {"Queue" = "Transparent"}
			Pass {
				ZWrite Off
				ColorMask RGB
				//Blend DstColor Zero
				Blend SrcAlpha OneMinusSrcAlpha
				Offset -1, -1

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "UnityCG.cginc"

				struct vertex_out {
					float4 uvShadow : TEXCOORD0;
					float4 uvFalloff : TEXCOORD1;
					UNITY_FOG_COORDS(2) // TEXCOORD2
					float4 pos : SV_POSITION;
					float intensity : TEXCOORD3; // additional intensity, based on normal orientation
				};

				float4x4 unity_Projector;
				float4x4 unity_ProjectorClip;

				vertex_out vert(float4 vertex : POSITION, float3 normal : NORMAL)
				{
					vertex_out o;

					o.intensity = sign(dot(float3(0.0, 1.0, 0.0), UnityObjectToWorldNormal(normal))); // 1.0 if pointing UP
					o.pos = UnityObjectToClipPos(vertex);
					o.uvShadow = mul(unity_Projector, vertex);
					o.uvFalloff = mul(unity_ProjectorClip, vertex);

					UNITY_TRANSFER_FOG(o,o.pos);
					return o;
				}

				sampler2D _Texture;

				float4 draw(float2 uv)
				{
					return 0;
				}

				fixed4 frag(vertex_out i) : SV_Target
				{
					fixed4 res = tex2D(_Texture, UNITY_PROJ_COORD(i.uvShadow));

					UNITY_APPLY_FOG_COLOR(i.fogCoord, res, fixed4(1,1,1,1));
					return res;
				}
				ENDCG
			}
	}
}