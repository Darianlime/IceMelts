Shader "Unlit/RayMarching"
{
    Properties
    {
        _MAX_STEPS ("Max Step", Float) = 100 // Amount of times to march a ray until it is considered a miss
        _SURF_DIST ("Surface Distance", Float) = 0.01 // How close it needs to be to a surface to be considered a hit
        _MAX_DIST ("Max Distance", Float) = 100
        _TIME_SCALE ("Time Scale", Float) = 1
        _SHAPE_SIZE ("Shape Size", Float) = 1
        _SMOOTHNESS ("Smoothness", Float) = 1
        _POSITION_OFFSET ("Position Offset", Vector) = (0,0,0)
        _BRIGHTNESS ("Brightness", Float) = 1
        _COLOR ("Color", Color) = (1,1,1)
        _COLOR2 ("Color2", Color) = (1,1,1)
        _TEST1 ("TEST1", Float) = 1
        _TEST2 ("TEST2", Float) = 1
        _TEST3 ("TEST3", Float) = 1
        _ALPHA ("Alpha", Range(0.0, 1)) = 0.5
        _SCROLL_SPEED ("Scroll Speed", Vector) = (0,0,0)
        _SCALE ("Scale", Float) = 1
        _POWER_FRESNEL ("Power Fresnel", Float) = 1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType" = "Transparent"}
        Cull Off
        Blend SrcAlpha One
        
        //Blend SrcAlpha One
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

            //Two distances and returns the clostest, creates a combining affect
            float unioning(float a, float b) {
                return min(a, b);
            }

            float smoothUnion(float d1, float d2, float k) {
                float h = clamp(0.5 + 0.5*(d2-d1)/k, 0.0, 1.0);
                return lerp(d2, d1, h) - k*h*(1.0-h);
            }

            // Cuts one shape out of another
            float subtraction(float a, float b) {
                return max(-a, b);
            }

            float smoothSubtraction(float d1, float d2, float k) {
                float h = clamp(0.5 - 0.5*(d2+d1)/k, 0.0, 1.0);
                return lerp(d2, -d1, h) + k*h*(1.0-h);
            }

            // A shape were both overlap each other
            float intersection(float a, float b) {
                return max(a, b);
            }

            float smoothIntersection(float d1, float d2, float k) {
                float h = clamp(0.5 - 0.5*(d1-d2)/k, 0.0, 1.0);
                return lerp(d2, d1, h) + k*h*(1.0-h);
            }

            //Gets distance between camera pos and sphere position at the edge of the sphere radius
            float sdSphere(float3 pos, float3 spherePos, float radius) {
                return length(spherePos - pos) - radius;
            }

            float _TIME_SCALE;
            float _SHAPE_SIZE;
            float _SMOOTHNESS;
            float3 _POSITION_OFFSET;

            float GetDist(float3 pos) {
                float t = _Time * _TIME_SCALE;
                pos -= _POSITION_OFFSET;

                float3 shape1_pos = (float3(sin(t * 0.337), abs(sin(t * 0.428)), sin(t * -0.989)));
                float3 shape2_pos = (float3(sin(t * -0.214), abs(sin(t * -0.725)), sin(t * 0.56)));
                float3 shape3_pos = (float3(sin(t * -0.671), abs(sin(t * 0.272)), sin(t * 0.773)));

                float3 moveScale = float3(0.2, 3.0, 0.2);

                float sphere1 = sdSphere(pos, shape1_pos * moveScale, _SHAPE_SIZE * 0.5);
                float sphere2 = sdSphere(pos, shape2_pos * moveScale, _SHAPE_SIZE * 0.75);
                float sphere3 = sdSphere(pos, shape3_pos * moveScale, _SHAPE_SIZE);
                float sphere4 = sdSphere(pos, float3(0.0, 0.0, 0.0), 0.8);
                float spheres = smoothUnion(sphere1, sphere2, _SMOOTHNESS);
                spheres = smoothUnion(spheres, sphere3, _SMOOTHNESS);
                spheres = smoothUnion(spheres, sphere4, _SMOOTHNESS);

                return spheres;
            }

            float3 GetNormal(float3 pos) {
                float d = GetDist(pos);
                float2 e = float2(0.01, 0.0);
                float3 n = d - float3(GetDist(pos-e.xyy), GetDist(pos-e.yxy), GetDist(pos-e.yyx));
                return n;
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
            
            float2 unity_gradientNoise_dir(float2 p) {
                p = p % 289;
                float x = (34 * p.x + 1) * p.x % 289 + p.y;
                x = (34 * x + 1) * x % 289;
                x = frac(x / 41) * 2 - 1;
                return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
            }

            float unity_gradientNoise(float2 p) {
                float2 ip = floor(p);
                float2 fp = frac(p);
                float d00 = dot(unity_gradientNoise_dir(ip), fp);
                float d01 = dot(unity_gradientNoise_dir(ip + float2(0, 1)), fp - float2(0, 1));
                float d10 = dot(unity_gradientNoise_dir(ip + float2(1, 0)), fp - float2(1, 0));
                float d11 = dot(unity_gradientNoise_dir(ip + float2(1, 1)), fp - float2(1, 1));
                fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
                return lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x);
            }

            float Unity_GradientNoise_float(float2 UV, float Scale)
            {
                return unity_gradientNoise(UV * Scale) + 0.5;
            }

            float WaterGradient(float2 scrollSpeed, float scale, float2 normal) {
                float2 t = scrollSpeed * _Time;
                float2 tiling = normal * float2(1,1) + t;
                float gradient = Unity_GradientNoise_float(tiling, scale);
                return gradient;
            }

            float Unity_FresnelEffect_float(float3 n, float3 ViewDir, float Power)
            {
                return pow((1.0 - saturate(dot(normalize(n), normalize(ViewDir)))), Power);
            }

            fixed3 _COLOR;
            fixed3 _COLOR2;
            float _BRIGHTNESS;
            float _TEST1;
            float _TEST2;
            float _TEST3;
            float _ALPHA;
            float2 _SCROLL_SPEED;
            float _SCALE;
            float _POWER_FRESNEL;

            fixed4 frag (v2f i) : SV_Target
            {
                float3 viewDirection = normalize(i.wPos - _WorldSpaceCameraPos);
                float3 worldPosition = i.wPos;

                float d = RaymarchHit(worldPosition, viewDirection);
                float3 n = GetNormal(worldPosition + viewDirection * d);
                float waterGradient = WaterGradient(_SCROLL_SPEED, _SCALE, n);
                float4 fresnel = Unity_FresnelEffect_float(n, viewDirection, _POWER_FRESNEL) * fixed4(1,1,1,1);
                // float h = (worldPosition + viewDirection * d).y;
                // h = _TEST1 - (h - _TEST2) * _TEST3;
                // float f = dot(viewDirection, n);
                // float g = f * h;
                fixed3 color = lerp(_COLOR, _COLOR2, waterGradient * fresnel);
                return float4(color , (step(d, _MAX_DIST)) * _ALPHA); //returns 1 if _MAX_DIST greater or equal to d otherwise 0.
            }
            ENDCG
        }
    }
}
