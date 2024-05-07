Shader "Custom/StencilOutlineShader"
{
    Properties
    {
        _Color("Main Color", Color) = (1,1,1,1)
        _OutlineColor("Outline Color", Color) = (0,1,0,1)
        _Outline("Outline Width", Float) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            Stencil
            {
                Ref 1
                Comp always
                Pass replace
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            fixed4 _Color;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }

        // Outline pass
        Pass
        {
            Tags { "Queue"="Overlay" }  // Use Overlay or a high-value like 4000
            ZWrite Off
            ZTest Always  // Ignore depth test, render on top

            Stencil
            {
                Ref 1
                Comp notequal
                Pass keep
            }

            Offset 1, 1
            CGPROGRAM
            #pragma vertex vertOutline
            #pragma fragment fragOutline
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            fixed4 _OutlineColor;
            float _Outline;

            v2f vertOutline(appdata v)
            {
                float3 offset = _Outline * v.normal;
                v.vertex.xyz += offset;
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 fragOutline(v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }
    }
}
