Shader "Custom/OutlineFX"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _OutlineColor("OutlineColor", Color) = (0,0,0,0)
    }
    SubShader
    {
        Tags
        {
            "Outline" = "True"
        }

        Pass{
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 wpos :   POSITION;
                float2 uv   :   TEXCOORD0;
            };
            struct v2f
            {
                float4 cpos :   SV_POSITION;
                float2 uv   :   TEXCOORD0;
            };

            sampler2D _MainTex;
            fixed4 _OutlineColor;

            v2f vert(appdata v) 
            {
                v2f o;
                o.cpos = UnityObjectToClipPos(v.wpos);
                o.uv = v.uv;
                return o;
            }
            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = _OutlineColor;
                return col;
            }
            ENDHLSL
        }
    }
}
