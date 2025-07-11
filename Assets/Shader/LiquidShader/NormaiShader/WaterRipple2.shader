Shader "Custom/WaterDistortion0" {
    Properties {
        _MainTex ("Water Texture", 2D) = "blue" {}
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _Distortion ("Distortion", Range(0, 0.2)) = 0.05
        _RippleSpeed ("Ripple Speed", Range(0, 5)) = 2.0
        _RippleSize ("Ripple Size", Range(0.01, 1)) = 0.2
        _WaveHeight ("Wave Height", Range(0, 0.5)) = 0.1
        _WaterColor ("Water Color", Color) = (0.2, 0.6, 1, 0.7)
        _FoamColor ("Foam Color", Color) = (1, 1, 1, 0.9)
        _FoamIntensity ("Foam Intensity", Range(0, 2)) = 1.0
        _UsualWaveSpeed ("UsualWaveSpeed",Range(0.1, 5))=2
        _UsualWaveFrequency("UsualWaveFrequency",Range(0.5, 20))=5
    }
    
    SubShader {
        Tags { 
            "Queue" = "Transparent" 
            "RenderType" = "Transparent" 
        }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            sampler2D _NormalMap;
            float4 _MainTex_ST;
            float _Distortion;
            float _RippleSpeed;
            float _RippleSize;
            float _WaveHeight;
            fixed4 _WaterColor;
            fixed4 _FoamColor;
            float _FoamIntensity;
            float _UsualWaveFrequency;
            float _UsualWaveSpeed;
            
            // 爆炸点参数（由脚本设置）
            float4 _ExplosionCenter;
            float _ExplosionIntensity;
            float _ExplosionTime;
            float _ExplosionRadius;

            v2f vert (appdata v) {
                v2f o;
                
                // 计算世界坐标
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                
                // 计算到爆炸点的距离
                float dist = distance(worldPos.xy, _ExplosionCenter.xy);
                float waveFactor = saturate(1 - dist / _ExplosionRadius);
                float wave0 = sin(worldPos.x * _UsualWaveFrequency + _Time.y * _UsualWaveSpeed);
                float wave = cos(worldPos.x * _UsualWaveFrequency * 0.8 + _Time.y * _UsualWaveSpeed * 1.2);
                // 顶点位移（创建波浪效果）
                if (dist < _ExplosionRadius) {
                    float elapsed = _Time.y - _ExplosionTime;
                    float wave1 = sin(dist * _RippleSize - elapsed * _RippleSpeed * 10);
                    v.vertex.y += (wave1+wave0+wave) * _WaveHeight * waveFactor * _ExplosionIntensity;
                }
                else{
                    v.vertex.y +=(wave0+wave*0.5)*_WaveHeight;
                    }

                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = worldPos;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // 计算到爆炸点的距离
                float dist = distance(i.worldPos.xy, _ExplosionCenter.xy);
                float waveFactor = saturate(1 - dist / _ExplosionRadius);
                
                // 计算时间因子
                float elapsed = _Time.y - _ExplosionTime;
                float timeFactor = saturate(1 - elapsed / 3.0);
                
                // 应用法线贴图
                float2 normalUV = i.uv + float2(_Time.x * 0.1, _Time.y * 0.05);
                float3 normal = UnpackNormal(tex2D(_NormalMap, normalUV));
                
                // 创建波纹效果
                if (dist < _ExplosionRadius) {
                    float wave = sin(dist * _RippleSize - elapsed * _RippleSpeed * 10);
                    waveFactor *= wave * timeFactor * _ExplosionIntensity;
                }
                
                // 应用UV偏移（扭曲效果）
                float2 uvOffset = normal.xy * _Distortion * waveFactor;
                fixed4 waterColor = tex2D(_MainTex, i.uv + uvOffset);
                
                // 混合水色
                waterColor.rgb = lerp(waterColor.rgb, _WaterColor.rgb, 0);
                
                // 添加水花泡沫效果
                float foam = saturate(waveFactor * 2) * timeFactor;
                if (foam > 0.3) {
                    float foamPattern = sin(i.worldPos.x * 10 + elapsed * 5) * 0.5 + 0.5;
                    foam *= foamPattern;
                    // waterColor.rgb = lerp(waterColor.rgb, _FoamColor.rgb, foam * _FoamIntensity);
                    waterColor.rgb = lerp(waterColor.rgb, _WaterColor.rgb, 0);
                }
                
                // 边缘发光效果
                waterColor.rgb += pow(waveFactor, 3) * fixed3(0.8, 0.9, 1.0) * 0.5;
                
                return waterColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}