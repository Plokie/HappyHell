Shader "Custom/SwitchableParticle"
{
    Properties
    {
        _HappyTex ("Happy Texture", 2D) = "white" {}
        _HellTex ("Hell Texture", 2D) = "white" {}
        _CurrentMode ("Current Mode", int) = 0

        _MyArr("TEST", 2DArray) = ""{}
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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _HappyTex;
            float4 _HappyTex_ST;
            sampler2D _HellTex;
            float4 _HellTex_ST;
            
            int _CurrentMode = 0;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _HappyTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_HappyTex, i.uv);

                if(_CurrentMode == 1) {
                    col = tex2D(_HellTex, i.uv);
                }

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
