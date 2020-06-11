Shader "Custom/Waves"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WaveA("Wave A (dir, steepness, wavelength)", Vector) = (1,0,0.5,10)

        _RippleOrigin1("ripple origin", Vector) = (0,0,0,0)
        _TimeSinceRippleStart1("time", Float) = 0
        _RippleOrigin2("ripple origin", Vector) = (0,0,0,0)
        _TimeSinceRippleStart2("time", Float) = 0
        _MaxDuration("max duration", Float) = 3
    }
    SubShader
    {
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
            };
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
            };

            sampler2D _MainTex;
            float4 _WaveA;
            float3 GerstnerWave(
                float4 wave, float3 p, inout float3 tangent, inout float3 binormal
            ) {
                float steepness = wave.z;
                float wavelength = wave.w;
                float k = 2 * UNITY_PI / wavelength;
                float c = sqrt(9.8 / k);
                float2 d = normalize(wave.xy);
                float f = k * (dot(d, p.xz) - c * _Time.y);
                float a = steepness / k;

                tangent += float3(
                    -d.x * d.x * (steepness * sin(f)),
                    d.x * (steepness * cos(f)),
                    -d.x * d.y * (steepness * sin(f))
                    );
                binormal += float3(
                    -d.x * d.y * (steepness * sin(f)),
                    d.y * (steepness * cos(f)),
                    -d.y * d.y * (steepness * sin(f))
                    );
                return float3(
                    d.x * (a * cos(f)),
                    a * sin(f),
                    d.y * (a * cos(f))
                    );
            }

            float3 _RippleOrigin1;
            float _TimeSinceRippleStart1;
            float3 _RippleOrigin2;
            float _TimeSinceRippleStart2;

            float _MaxDuration;
            float ripple(float3 origin, float3 wPos, float timeSinceStart, float maxDuration) {
                float x = abs(wPos.x - origin.x);
                float z = abs(wPos.z - origin.z);
                float r = 9 * timeSinceStart;
                float a = clamp(maxDuration - timeSinceStart, 0, maxDuration) / (2 * maxDuration);
                float val = a * sin(r - (x + z));
                
                if (r >= x + z) {
                    return -val;
                }
                else return 0;
            }

            v2f vert (appdata v)
            {
                v2f o;

                float3 gridPoint = v.vertex.xyz;
                float3 tangent = float3(1, 0, 0);
                float3 binormal = float3(0, 0, 1);
                float3 p = gridPoint;
                p += GerstnerWave(_WaveA, gridPoint, tangent, binormal);
                float3 normal = normalize(cross(binormal, tangent));

                float3 wPos = mul(unity_ObjectToWorld, v.vertex);
                float val = ripple(_RippleOrigin1, wPos, _TimeSinceRippleStart1, _MaxDuration);
                p.y += val;
                normal.y += val;

                o.vertex = UnityObjectToClipPos(float4(p.x,p.y,p.z,1));
                o.uv = v.uv;
                o.normal = normal;

                return o;
            }
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col.rgb *= dot(i.normal, _WorldSpaceLightPos0);
                return col;
            }
            ENDHLSL
        }
    }
}
