Shader "Hidden/VignetteWithShake"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ShakeIntensity ("Shake Intensity", Range(0, 0.2)) = 0.05

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _ShakeIntensity;
            float _Health;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 shakeOffset = float2(
                    (sin(_Time.y * 30.0) + cos(_Time.y * 50.0)) * _ShakeIntensity * (1.0 / _Health),
                    (cos(_Time.y * 40.0) + sin(_Time.y * 60.0)) * _ShakeIntensity * (1.0 / _Health)
                );


                if (_Health <= 1)
                {
                    i.uv += shakeOffset * _ShakeIntensity;
                }

                fixed4 col = tex2D(_MainTex, i.uv);

                float intensity = abs(_SinTime.w * distance(i.uv.xy, float2(0.5, 0.5)));
                col = float4(col.x + intensity, col.y - intensity, col.z - intensity, col.w);
                
                return col;
            }
            ENDCG
        }
    }
}

