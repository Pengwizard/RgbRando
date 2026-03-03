Shader "RbgRando/GrayScaler" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}

        RedPercent ("RedPercent", Float) = 1
        GreenPercent ("GreenPercent", Float) = 1
        BluePercent ("BluePercent", Float) = 1
        AlphaPercent ("AlphaPercent", Float) = 1
    }
    
    SubShader {
        Cull Off ZWrite Off ZTest Always
        
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            sampler2D _MainTex;

            float RedPercent;
            float GreenPercent;
            float BluePercent;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);
                // r = 0, g = 1, b = 0
                float gray = (col.r + col.g + col.b) / 3;
                // gray = 0.3
                // rp = 1.0, gp = 0, bp = 0
                float weightedGray = gray * (3 - (RedPercent + GreenPercent + BluePercent)) / 3
                // (0.3 * (2)) / 3 > 0
                float red = min(1, col.r * RedPercent + weightedGray)
                // red = 0 * ... + wg = wg
                float green = min(1, col.g * GreenPercent + weightedGray)
                // 1 * 0 + wg = wg
                float blue = min(1, col.b * BluePercent + weightedGray)
                // 0 * 0 + wg = wg
                col.rgba = float4(red, green, blue, col.a);
                
                return col;
            }
            ENDCG
        }
    }
}