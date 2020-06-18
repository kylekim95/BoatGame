Shader "Unlit/Phong"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _Ambient("ambient", Float) = .1
        _Diffuse("diffuse", Float) = .45
        _Specular("Specular", Float) = .45
        _Power("power", Float) = 5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

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
                float3 normal : TEXCOORD1;
                float3 wPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            float _Ambient;
            float _Diffuse;
            float _Specular;
            float _Power;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = v.normal;
                o.wPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float3 H = normalize(_WorldSpaceLightPos0 + normalize(_WorldSpaceCameraPos - i.wPos));
                col.rgb = col.rgb * _Ambient +
                    col.rgb * _Diffuse * dot(i.normal, _WorldSpaceLightPos0) +
                    col.rgb * _Specular * pow(dot(i.normal, H),_Power);
                col.a = 0.8f;
                return col;
            }
            ENDHLSL
        }
    }
}
