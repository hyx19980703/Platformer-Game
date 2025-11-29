Shader "Custom/RadialMask"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PlayerPosition ("Player Position (World Space)", Vector) = (0,0,0)
        _Radius ("Light Radius", Float) = 5.0
        _Softness ("Soft Edge Falloff", Float) = 1.0
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha // 启用alpha混合

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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 worldPos : TEXCOORD1; // 传递世界空间位置
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float3 _PlayerPosition;
            float _Radius;
            float _Softness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex); // 计算世界位置
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 计算当前像素世界位置与玩家世界位置的距离
                // 已修正：将变量名 'distance' 改为 'dist'
                float dist = distance(i.worldPos.xy, _PlayerPosition.xy);

                // 如果在光照范围内，alpha为0（透明），否则为1（黑色）
                // 使用smoothstep实现平滑过渡
                // 建议：将变量名 'alpha' 改为更具描述性的名称，如 'maskAlpha'
                float maskAlpha = smoothstep(_Radius - _Softness, _Radius, dist);

                // 返回黑色，alpha值由上面计算得出
                return fixed4(0, 0, 0, maskAlpha);
            }
            ENDCG
        }
    }
}