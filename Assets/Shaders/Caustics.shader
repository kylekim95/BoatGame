Shader "Custom/Caustics"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PlayerPos("PlayerPos", Vector) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 wPos : TEXCOORD1;
                float3 normal : NORMAL;
            };

            sampler2D _MainTex;
            float4 _PlayerPos;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.wPos = mul(unity_ObjectToWorld, v.vertex);
                o.normal = v.normal;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float cos_theta = clamp(dot(normalize(_WorldSpaceCameraPos), i.normal),0,1);

                fixed4 col2 = fixed4(.5, .7, 1, 1);
                fixed4 col = fixed4(.3, .5, 1, 1);

                return col * (1-cos_theta) + col2 * cos_theta;
            }
            ENDHLSL
        }
    }
}
