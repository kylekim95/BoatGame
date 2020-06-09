Shader "Custom/Ripple"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RippleOrigin("ripple origin", Vector) = (0,0,0,0)
        _RippleOrigin2("ripple origin 2", Vector) = (0,0,0,0)

        _RippleRadius("ripple radius", Float) = 1

        _TimeSinceRippleStart("time", Float) = 0
        _TimeSinceRippleStart2("time 2", Float) = 0

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
                float3 normal : NORMAL;
            };

            sampler2D _MainTex;
            float3 _RippleOrigin;
            float3 _RippleOrigin2;
            float _RippleRadius;
            float _TimeSinceRippleStart;
            float _TimeSinceRippleStart2;

            float ripple(float3 origin, float3 wPos, float timeSinceStart, float maxDuration) {
                float x = abs(wPos.x - origin.x);
                float z = abs(wPos.z - origin.z);
                float r = timeSinceStart;
                float a = clamp(maxDuration - timeSinceStart, 0, maxDuration) / maxDuration;
                float val = a * sin(r - (x + z));
                return -val;
            }

            v2f vert (appdata v)
            {
                v2f o;

                float maxDuration = 3;
                float3 wPos = mul(unity_ObjectToWorld, v.vertex);
                v.vertex.y += ripple(_RippleOrigin, wPos, _TimeSinceRippleStart, maxDuration);
                v.vertex.y += ripple(_RippleOrigin2, wPos, _TimeSinceRippleStart2, maxDuration);

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = v.normal;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 dirLPos = _WorldSpaceLightPos0;
                col.rgb *= dot(i.normal, dirLPos);
                return col;
            }
            ENDHLSL
        }
    }
}
