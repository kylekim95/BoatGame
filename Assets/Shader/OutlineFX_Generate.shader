Shader "Custom/OutlineFX_Generate"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SubtractTex("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Blur.hlsl"

            struct appdata
            {
                float4 wpos :   POSITION;
                float2 uv   :   TEXCOORD0;
            };
            struct v2f
            {
                float2 uv   :   TEXCOORD0;
                float4 cpos :   SV_POSITION;
            };

            sampler2D _SubtractTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.cpos = UnityObjectToClipPos(v.wpos);

                o.uv = v.uv;
                return o;
            }
            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = fixed4(SampleGaussian(i.uv) - tex2D(_SubtractTex, i.uv).rgb, 1);
                return col;
            }
            ENDHLSL
        }
    }
}
