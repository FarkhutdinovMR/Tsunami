// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/VertexDeform"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _AmountTime("Time", float) = 30
        _Amount1("Extrusion Amount 1", Range(0,10)) = 0.5
        _Amount2("Extrusion Amount 2", Range(0,10)) = 0.5
        _Amount3("Extrusion Amount 3", Range(0,10)) = 0.5
        _Wave1Direction("Wave Direction", Range(0,1)) = 0.5
        [HDR] _Emission("Emission", color) = (0,0,0)
        _FresnelColor("Fresnel Color", Color) = (1,1,1,1)
        [PowerSlider(4)] _FresnelExponent("Fresnel Exponent", Range(0.25, 4)) = 1
    }
    SubShader
    {
 /*       Tags { "RenderType"="Opaque" }
        LOD 100*/
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
            float3 viewDir;
            INTERNAL_DATA
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _AmountTime;
        float _Amount1;
        float _Amount2;
        float _Amount3;
        float _Wave1Direction;
        float3 _FresnelColor;
        float _FresnelExponent;
        half3 _Emission;

        void vert(inout appdata_full v) {
            //v.vertex.xyz += v.normal * _Amount;

            float phase = _Time * _AmountTime;
            float4 wpos = mul(unity_ObjectToWorld, v.vertex);
            float offset = (wpos.x + (wpos.z * _Amount1)) * _Amount2;
            //wpos.x += sin(phase + offset) * _Amount3 * v.color.r;
            float2 dir1 = float2(sin(phase + offset), cos(phase + offset));
            wpos.xz += dir1 * _Amount3 * v.color.r;
            v.vertex = mul(unity_WorldToObject, wpos);
        }

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;

            //get the dot product between the normal and the view direction
            float fresnel = dot(IN.worldNormal, IN.viewDir);
            //invert the fresnel so the big values are on the outside
            fresnel = saturate(1 - fresnel);
            //raise the fresnel value to the exponents power to be able to adjust it
            fresnel = pow(fresnel, _FresnelExponent);
            //combine the fresnel value with a color
            float3 fresnelColor = fresnel * _FresnelColor;
            //apply the fresnel value to the emission
            o.Emission = _Emission + fresnelColor;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
