Shader "Unlit/Outline" {

    Properties {

        _Color ("Color", Color) = (1, 1, 1, 1)
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Range(0, 10)) = 1

    }

    Subshader {

        Tags {
            "RenderType" = "Opaque"
        }

        CGPROGRAM

        #pragma surface surf Standard

        struct Input {
            float4 color : COLOR;
        };

        half4 _Color;

        void surf(Input IN, inout SurfaceOutputStandard o) {
            o.Albedo = _Color.rgb * IN.color.rgb;
            o.Alpha = _Color.a * IN.color.a;
        }

        ENDCG

        Pass {

            Cull Front

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            half _OutlineWidth;

            float4 vert(
                    float4 position : POSITION,
                    float3 normal : NORMAL) : SV_POSITION {

                position.xyz += normal * _OutlineWidth;

                return UnityObjectToClipPos(position);
                //float4 clipPosition = UnityObjectToClipPos(position);
                //float3 clipNormal = mul((float3x3) UNITY_MATRIX_VP, mul((float3x3) UNITY_MATRIX_M, normal));
                //float2 offset = normalize(clipNormal.xy) / _ScreenParams.xy * _OutlineWidth * clipPosition.w * 2;
                //clipPosition.xy += offset;
                //clipPosition.xyz += normalize(clipNormal) * _OutlineWidth;

                //return clipPosition;

            }

            half4 _OutlineColor;

            half4 frag() : SV_TARGET {
                return _OutlineColor;
            }

            ENDCG

        }

    }

}