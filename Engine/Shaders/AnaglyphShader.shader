Shader "Custom/AnaglyphShader" {
        Properties {
                _LeftTex ("Left Camera (RGB)", 2D) = "white" {}
                _RightTex ("Right Camera (RGB)", 2D) = "white" {}
        }
        SubShader {
                Tags { "RenderType"="Opaque" }
                LOD 200
               
                CGPROGRAM
                #pragma surface surf Lambert

                sampler2D _LeftTex;
                sampler2D _RightTex;

                struct Input {
                        float2 uv_LeftTex;
                        float2 uv_RightTex;
                };

                void surf (Input IN, inout SurfaceOutput o) {
                    float3 _left = tex2D(_LeftTex, IN.uv_LeftTex);
                    float3 _right = tex2D(_RightTex, IN.uv_RightTex);
                    o.Albedo = float3(0.0, 0.0, 0.0);
                    o.Emission = float3(_left.r, _right.g, _right.b);
                }
                ENDCG
        }
        FallBack "Diffuse"
}