Shader "Original/Post Outline" {

    Properties {

        _MainTex("Main Texture",2D)="black"{}
        _SceneTex("Scene Texture",2D)="black"{}
        _OutlineWidth ("Outline Width", Range(0, 10)) = 1
        _kernel("Gauss Kernel",Vector)=(0.05399097, 0.2419707, 0.3989423, 0.2419707)
        _kernelWidth("Gauss Kernel Width",Float)=1

    }

    Subshader {
Cull Front

        CGPROGRAM

        #pragma surface surf Standard

        struct Input {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;
        
        void surf(Input IN, inout SurfaceOutputStandard o) {
            o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
            o.Alpha = tex2D (_MainTex, IN.uv_MainTex).a;
        }

        ENDCG

        Pass {

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            struct appdata {
                float4 vertex : POSITION;
	            float3 normal : NORMAL;
            };

            struct v2f {
	            float4 pos : SV_POSITION;
	            float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float2 _MainTex_TexelSize;
            half _OutlineWidth;

            v2f vert(appdata v) {

                v2f o;
                v.vertex.xyz += v.normal * _OutlineWidth;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = o.pos.xy / 2 + 0.5;
                return o;
                //float4 clipPosition = UnityObjectToClipPos(position);
                //float3 clipNormal = mul((float3x3) UNITY_MATRIX_VP, mul((float3x3) UNITY_MATRIX_M, normal));
                //float2 offset = normalize(clipNormal.xy) / _ScreenParams.xy * _OutlineWidth * clipPosition.w * 2;
                //clipPosition.xy += offset;
                //clipPosition.xyz += normalize(clipNormal) * _OutlineWidth;

                //return clipPosition;

            }

            float _kernel[21];
            float _kernelWidth;

            float4 frag(v2f i) : COLOR {

                int NumberOfIterations=_kernelWidth;
				
                float TX_x=_MainTex_TexelSize.x;
                float TX_y=_MainTex_TexelSize.y;
                float ColorIntensityInRadius=0;

                //for every iteration we need to do horizontally
                for(int k=0;k<NumberOfIterations;k+=1)
                {
                    ColorIntensityInRadius+=_kernel[k]*tex2D(_MainTex, float2(i.uv.x+(k-NumberOfIterations/2)*TX_x, i.uv.y)).r;
                }
                return ColorIntensityInRadius;
              
            }

            ENDCG

        }

        GrabPass{}

        Pass {

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            struct appdata {
                float4 vertex : POSITION;
	            float3 normal : NORMAL;
            };

            struct v2f {
	            float4 pos : SV_POSITION;
	            float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            sampler2D _SceneTex;
            sampler2D _GrabTexture;
            float2 _GrabTexture_TexelSize;
            half _OutlineWidth;

            v2f vert(appdata v) {

                v2f o;
                v.vertex.xyz += v.normal * _OutlineWidth;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = o.pos.xy / 2 + 0.5;
                return o;
            }

            float _kernel[21];
            float _kernelWidth;

            float4 frag(v2f i) : COLOR {

                if(tex2D(_MainTex,i.uv.xy).r>0)
                {
                    return tex2D(_SceneTex,i.uv.xy);
                }

                int NumberOfIterations=_kernelWidth;
				
                float TX_x=_GrabTexture_TexelSize.x;
                float TX_y=_GrabTexture_TexelSize.y;
                float ColorIntensityInRadius=0;

                //for every iteration we need to do vertically
                for(int k=0;k<NumberOfIterations;k+=1)
                {
                    ColorIntensityInRadius+=_kernel[k]*tex2D(_GrabTexture, float2(i.uv.x, 1-i.uv.y+(k-NumberOfIterations/2)*TX_y));
                }
                half4 color= tex2D(_SceneTex,i.uv.xy)+ColorIntensityInRadius*half4(0,1,1,1);
                return ColorIntensityInRadius;
              
            }

            ENDCG

        }

    }

}