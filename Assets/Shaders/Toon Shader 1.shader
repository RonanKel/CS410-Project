Shader "Unlit/Toon Shader 1"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _LightThresholds ("Light Thresholds", Vector) = (0.5, 0.75, 1, 1)
        _LightIntensity ("Light Intensity", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _LightThresholds;
            float _LightIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv) * _Color;

                // Compute the lighting
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float3 normal = normalize(i.worldNormal);
                float lightIntensity = max(0, dot(normal, lightDir)) * _LightIntensity;

                // Apply toon shading
                float3 lightColor;
                if (lightIntensity > _LightThresholds.x)
                    lightColor = float3(1, 1, 1);
                else if (lightIntensity > _LightThresholds.y)
                    lightColor = float3(0.7, 0.7, 0.7);
                else
                    lightColor = float3(0.4, 0.4, 0.4);

                fixed4 result = texColor * fixed4(lightColor, 1);
                return result;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
