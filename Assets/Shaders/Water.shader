Shader "Custom/WorldTransparetWater"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Speed("Speed", float) = 30
        _Length("Wave length", Range(0,10)) = 0.5
        _Amplitude("Amplitude", Range(0,1)) = 0.5
        _FresnelColor("Fresnel Color", Color) = (1,1,1,1)
        [PowerSlider(4)] _FresnelExponent("Fresnel Exponent", Range(0.25, 4)) = 1
        _OpacityHeight("Opacity height", Range(0,10)) = 1
        _OpacityPosition("Opacity position", Range(-1,1)) = 0
        _Head("Head", Range(0,5)) = 1
        _HeadHeight("Head", Range(0,5)) = 2
    }
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            //"IgnoreProjector" = "True"
            "RenderType" = "Transparent"
        }

        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert

        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
            float3 viewDir;
            float4 color : Color;
            INTERNAL_DATA
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _Speed;
        float _Length;
        float _Amplitude;
        float3 _FresnelColor;
        float _FresnelExponent;
        float _OpacityHeight;
        float _OpacityPosition;
        float _Head;
        float _HeadHeight;

        void vert(inout appdata_full v)
        {
            float phase = _Time * _Speed;
            float offset = v.vertex.x * _Length;

            offset = sin(phase + offset) * _Amplitude;
            v.vertex.xz += (_Head - v.vertex.y) * _HeadHeight * offset;

            v.color.r = clamp((v.vertex.y - _OpacityPosition) * _OpacityHeight, 0, 1);
        }

        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a * IN.color.r;

            // Fresnel effect
            float fresnel = dot(IN.worldNormal, IN.viewDir);
            fresnel = saturate(1 - fresnel);
            fresnel = pow(fresnel, _FresnelExponent);
            float3 fresnelColor = fresnel * _FresnelColor;
            o.Emission = fresnelColor;
        }
        ENDCG
    }
    FallBack "Diffuse"
}