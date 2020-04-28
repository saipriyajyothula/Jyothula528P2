Shader "Unlit/StarShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SphereRadius("Sphere Radius", Range(0.0, 5.0)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
 
            #include "UnityCG.cginc"
 
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2g
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct g2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _SphereRadius;
 
            v2g vert (appdata v)
            {
                v2g o;
                o.vertex = mul(UNITY_MATRIX_MV, v.vertex);
                o.uv = v.uv;
                return o;
            }
 
            [maxvertexcount(4)]
            void geom(triangle v2g IN[3], inout TriangleStream<g2f> triStream)
            {
                g2f o;

                float halfradius = _SphereRadius * 0.5;

                o.vertex = IN[0].vertex;
                o.vertex.xy += float2(halfradius, -halfradius);
                o.vertex = mul(UNITY_MATRIX_P, o.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.uv = IN[0].uv;
                o.uv.xy = float2(1.0,-1.0);
                o.uv = TRANSFORM_TEX(IN[0].uv, _MainTex);
                triStream.Append(o);

                o.vertex = IN[0].vertex;
                o.vertex.xy += float2(halfradius, halfradius);
                o.vertex = mul(UNITY_MATRIX_P, o.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.uv = IN[0].uv;
                o.uv.xy = float2(1.0,1.0);
                o.uv = TRANSFORM_TEX(IN[0].uv, _MainTex);
                triStream.Append(o);

                o.vertex = IN[0].vertex;
                o.vertex.xy += float2(-halfradius, -halfradius);
                o.vertex = mul(UNITY_MATRIX_P, o.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.uv = IN[0].uv;
                o.uv.xy = float2(-1.0,-1.0);
                o.uv = TRANSFORM_TEX(IN[0].uv, _MainTex);
                triStream.Append(o);

                o.vertex = IN[0].vertex;
                o.vertex.xy += float2(-halfradius, halfradius);
                o.vertex = mul(UNITY_MATRIX_P, o.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.uv = IN[0].uv;
                o.uv.xy = float2(-1.0,1.0);
                o.uv = TRANSFORM_TEX(IN[0].uv, _MainTex);
                triStream.Append(o);

                triStream.RestartStrip();
            }
 
            fixed4 frag (g2f i) : SV_Target
            {
                // sample the texture
                float x = i.uv.x;
                float y = i.uv.y;
                float zz = 1.0 - x*x - y*y;
                clip(zz);
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                col.a = pow(zz, 16);
                return col;
            }
            ENDCG
        }
    }
}