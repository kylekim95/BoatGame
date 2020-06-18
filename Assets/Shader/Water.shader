Shader "Custom/Water"
{
    Properties
    {
        _Ambient("ambient", Float) = .2
        _Diffuse("diffuse", Float) = .4
        _Specular("Specular", Float) = .4
        _Power("power(glossiness)", Float) = 5

        _MainTex("Texture", 2D) = "white" {}
        _Wave0("Wave 0 (dir, steepness, wavelength)", Vector) = (1,1,0,1)
        _Wave1("Wave 1 (dir, steepness, wavelength)", Vector) = (1,1,0,1)

        _Ripple0Time("Ripple 0 time since ripple start", Float) = 0
        _Ripple1Time("Ripple 1 time since ripple start", Float) = 0
        _Ripple2Time("Ripple 2 time since ripple start", Float) = 0
        _Ripple3Time("Ripple 3 time since ripple start", Float) = 0
        _Ripple4Time("Ripple 4 time since ripple start", Float) = 0

        _Ripple0Origin("Ripple 0 origin", Vector) = (0,0,0,0)
        _Ripple1Origin("Ripple 1 origin", Vector) = (0,0,0,0)
        _Ripple2Origin("Ripple 2 origin", Vector) = (0,0,0,0)
        _Ripple3Origin("Ripple 3 origin", Vector) = (0,0,0,0)
        _Ripple4Origin("Ripple 4 origin", Vector) = (0,0,0,0)

        _RippleMaxDuration("Ripple max duration", Float) = 3
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" }
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
            };
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float3 wPos : TEXCOORD1;
            };

            float3 GerstnerWave(float4 wave, float3 p, inout float3 tangent, inout float3 binormal)
            {
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
            float ripple(float3 origin, float3 wPos, float timeSinceStart, float maxDuration) {
                float x = abs(wPos.x - origin.x);
                float z = abs(wPos.z - origin.z);
                float r = 5 * clamp(timeSinceStart,0,maxDuration) / maxDuration;
                float a = 0.9 * clamp(maxDuration - timeSinceStart, 0, maxDuration)/maxDuration;
                float val = a * sin(r - (x + z));

                if (r*r >= x*x + z*z) {
                    return -val;
                }
                else return 0;
            }

            sampler2D _MainTex;
            float4 _Wave0;
            float4 _Wave1;

            float _Ripple0Time;
            float _Ripple1Time;
            float _Ripple2Time;
            float _Ripple3Time;
            float _Ripple4Time;

            float3 _Ripple0Origin;
            float3 _Ripple1Origin;
            float3 _Ripple2Origin;
            float3 _Ripple3Origin;
            float3 _Ripple4Origin;

            float _RippleMaxDuration;

            float _Ambient;
            float _Diffuse;
            float _Specular;
            float _Power;

            v2f vert(appdata v)
            {
                v2f o;

                float3 gridPoint = v.vertex.xyz;
                float3 tangent = float3(1, 0, 0);
                float3 binormal = float3(0, 0, 1);
                float3 p = gridPoint;
                p += GerstnerWave(_Wave0, gridPoint, tangent, binormal);
                p += GerstnerWave(_Wave1, gridPoint, tangent, binormal);
                float3 normal = normalize(cross(binormal, tangent));

                float3 wPos = mul(unity_ObjectToWorld, v.vertex);
                
                float val = ripple(_Ripple0Origin, wPos, _Ripple0Time, _RippleMaxDuration);
                p.y += val;
                normal.y += val;
                val = ripple(_Ripple1Origin, wPos, _Ripple1Time, _RippleMaxDuration);
                p.y += val;
                normal.y += val;
                val = ripple(_Ripple2Origin, wPos, _Ripple2Time, _RippleMaxDuration);
                p.y += val;
                normal.y += val;
                val = ripple(_Ripple3Origin, wPos, _Ripple3Time, _RippleMaxDuration);
                p.y += val;
                normal.y += val;
                val = ripple(_Ripple4Origin, wPos, _Ripple4Time, _RippleMaxDuration);
                p.y += val;
                normal.y += val;

                o.vertex = UnityObjectToClipPos(float4(p.x,p.y,p.z,1));
                o.uv = v.uv;
                o.normal = normal;
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
                col.a = 0.5f;   
                return col;
            }
            ENDHLSL
        }
    }
}
