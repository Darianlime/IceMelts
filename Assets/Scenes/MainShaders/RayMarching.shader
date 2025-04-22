Shader "Unlit/RayMarching"
{
    Properties
    {
        _MAX_STEPS ("Max Step", Float) = 100 // Amount of times to march a ray until it is considered a miss
        _SURF_DIST ("Surface Distance", Float) = 0.01 // How close it needs to be to a surface to be considered a hit
        _MAX_DIST ("Max Distance", Float) = 100
    }
    SubShader
    {
        Tags { "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
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
                float3 wPos : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.wPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            //Gets distance between 
            float sdSphere(float3 pos, float3 spherePos, float radius) {
                return length(spherePos - pos) - radius;
            }

            float GetDist(float3 pos) {
                float sphere = sdSphere(pos, float3(0, 0, 0), 1.0);
                return sphere;
            }

            float _MAX_STEPS;
            float _SURF_DIST;
            float _MAX_DIST;

            //Takes a ray world position and ray direction
            float3 RaymarchHit(float3 position, float3 direction) {
                float d = 0.0;
                for(int i = 0; i < _MAX_STEPS; i++)
                {
                    float3 pos = position + direction * d; //Step along ray
                    float dS = GetDist(pos);
                    d += dS;
                    if (d > _MAX_DIST || dS < _SURF_DIST) {
                        break;
                    }
                }

                return d;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 viewDirection = normalize(i.wPos - _WorldSpaceCameraPos);
                float3 worldPosition = i.wPos;

                float d = RaymarchHit(worldPosition, viewDirection);
                return fixed4(1,1,1, step(d, _MAX_DIST)); //returns 1 if _MAX_DIST greater or equal to d otherwise 0.
            }
            ENDCG
        }
    }
}
