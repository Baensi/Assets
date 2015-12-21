Shader "Custom/Textures Mixer" {
        Properties {
                _MainTex ("Base (RGB A-mask)", 2D) = "white" {}
                _SecondTex ("Base (RGB)", 2D) = "white" {}
                _Threshold ("Threshold", Range(0,1))=0.5
        }
        SubShader
        {
                Tags { "RenderType"="Opaque" }
                LOD 200
               
                CGPROGRAM
                #pragma surface surf Lambert

                sampler2D _MainTex;
                sampler2D _SecondTex;
                fixed _Threshold;

                struct Input
                {
                        float2 uv_MainTex;
                };

                void surf (Input IN, inout SurfaceOutput o)
                {
                        fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
                        fixed4 c1 = tex2D (_SecondTex, IN.uv_MainTex);

                        if(c.a>_Threshold) c=c1;

                        o.Albedo = c.rgb;
                        o.Alpha = c.a;
                }
                ENDCG
        }
        FallBack "Diffuse"
}
