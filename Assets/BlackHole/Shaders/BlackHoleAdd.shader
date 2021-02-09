Shader "Unlit/BlackHole/BlackHoleAdd"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
        SubShader
    {
        Tags { "RenderType" = "Transparent"  "IgnoreProjection" = "True"  "Queue" = "Transparent" }
        LOD 100

        Pass
        {
            Name "Forward"
            Tags {"LightMode" = "ForwardBase"}
            Blend one one
            Zwrite off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
        // make fog work
        #pragma multi_compile_fog
        #define UNITY_PASS_FORWARDBASE
        #pragma multi_compile_fwdbase
        #pragma onlyrenders d3d9 d3d11 glcore gles
        #pragma  target 3.0

        #include "UnityCG.cginc"

        sampler2D _MainTex;
        float4 _MainTex_ST;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 vertexColor : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 vertexColor : COLOR;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.vertexColor = v.vertexColor;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float4 baseColor = tex2D(_MainTex, TRANSFORM_TEX(i.uv, _MainTex));
                float3 emmision = (baseColor.rgb * i.vertexColor.rgb * i.vertexColor.a);
                float4 col = float4(emmision, 1);
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
