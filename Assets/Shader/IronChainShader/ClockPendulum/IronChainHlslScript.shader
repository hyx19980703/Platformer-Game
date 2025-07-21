Shader "Custom/ChainPivotRotation"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PivotWorldPos ("Pivot World Position", Vector) = (0, 0, 0, 0)
        _Amplitude ("Swing Amplitude", Float) = 0.3
        _Speed ("Swing Speed", Float) = 2.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float2 _PivotWorldPos;
                float _Amplitude;
                float _Speed;
            CBUFFER_END

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            Varyings vert (Attributes v)
            {
                Varyings o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);

                // 1. 获取顶点世界坐标
                float3 worldPos = TransformObjectToWorld(v.positionOS.xyz);

                // 2. 计算局部偏移向量
                float2 offset = worldPos.xy - _PivotWorldPos;

                // 3. 计算旋转角度（钟摆运动）
                float angle = _Amplitude * sin(_Time.y * _Speed);

                // 4. 2D旋转矩阵
                float2x2 rot = float2x2(
                    cos(angle), -sin(angle),
                    sin(angle),  cos(angle)
                );

                // 5. 旋转局部偏移
                offset = mul(rot, offset);

                // 6. 转回世界坐标
                worldPos.xy = _PivotWorldPos + offset;

                // 7. 输出裁剪空间坐标
                o.positionCS = TransformWorldToHClip(worldPos);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);
                return SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
            }
            ENDHLSL
        }
    }
}