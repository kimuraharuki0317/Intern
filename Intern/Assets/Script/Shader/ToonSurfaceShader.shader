/// <summary>
/// トゥーンシェーダー
/// </summary>
Shader "Unlit/ToonSurfaceShader"
{
    Properties
    {
        _MainTex ("テクスチャ", 2D) = "white" {}
        _MainColor ("色", Color) =  (1, 1, 1, 1)
        _Threshold_1 ("閾値(暗)", Range(0.0, 1.0)) = 0.01
        _Threshold_2 ("閾値(明)", Range(0.0, 1.0)) = 0.3
        _Brightness_1 ("完全に影になっている箇所の明るさ", Range(0.0, 1.0)) = 0.3
        _Brightness_2 ("少し影になっている箇所の明るさ", Range(0.0, 1.0)) = 0.6
        _Brightness_3 ("光が当たっている場所の明るさ", Range(0.0, 1.0)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainColor;
            float _Threshold_1;
            float _Threshold_2;
            float _Brightness_1;
            float _Brightness_2;
            float _Brightness_3;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // しきい値を設けて陰影を計算
                half nl = max(0, dot(i.normal, _WorldSpaceLightPos0.xyz));
                if (nl <= _Threshold_1) {
                    nl = _Brightness_1;
                } else if (nl <= _Threshold_2) {
                    nl = _Brightness_2;
                } else {
                    nl = _Brightness_3;
                }

                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                col *= nl * _MainColor;
                return col;
            }
            ENDCG
        }

        //他のオブジェクトに影を落とすようにする
        Pass
        {
            Name "ShadowCaster"
            Tags { "LightMode"="ShadowCaster" }

            ZWrite On
            ZTest LEqual

            HLSLPROGRAM
            #pragma vertex ShadowPassVertex
            #pragma fragment ShadowPassFragment

            #pragma shader_feature _ALPHATEST_ON
            #pragma shader_feature _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            #pragma multi_compile_instancing
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/ShadowCasterPass.hlsl"
            ENDHLSL
        }
    }
}
