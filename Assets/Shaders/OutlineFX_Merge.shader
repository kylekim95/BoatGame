Shader "Custom/OutlineFX_Merge"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MergeRT("MergeRT", 2D) = "white" {}
    }
    SubShader
    {
        Tags 
        {
            
        }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
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
            sampler2D _MergeRT;

            v2f vert(appdata v)
            {
                v2f o;
                o.cpos = UnityObjectToClipPos(v.wpos);
                o.uv = v.uv;
                return o;
            }
            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) + fixed4(tex2D(_MergeRT, i.uv).rgb,0.5);
                return col;
            }
            ENDHLSL
        }
    }
}
