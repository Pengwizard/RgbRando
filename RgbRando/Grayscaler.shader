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
                float gray = (col.r + col.g + col.b) / 3;
                float weightedGray = gray * (3 - (RedPercent + GreenPercent + BluePercent)) / 3;
                float red = min(1, col.r * RedPercent + weightedGray);
                float green = min(1, col.g * GreenPercent + weightedGray);
                float blue = min(1, col.b * BluePercent + weightedGray);
                col.rgba = float4(red, green, blue, col.a);
                
                return col;
            }
            ENDCG
        }
    }
}