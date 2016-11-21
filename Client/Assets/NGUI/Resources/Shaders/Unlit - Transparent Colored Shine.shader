Shader "Unlit/Transparent Colored Shine" 
 {
 		Properties
        {
	        _MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}
			 _percent ("_percent", Range(-4, 3)) = 0 
			_maxBrightness("_maxBrightness", Range(0, 1)) = 0.5 
			_OffsetX_Start_1 ("_OffsetX start 1", Range(0, 1)) = 0 
			_OffsetY_Start_1 ("_OffsetY start 1", Range(0, 1)) = 0 
			_OffsetX_End_1 ("_OffsetX end 1", Range(0, 1)) = 0 
			_OffsetY_End_1 ("_OffsetY end 1", Range(0, 1)) = 0 
			
			_OffsetX_Start_2 ("_OffsetX start 2", Range(0, 1)) = 0 
			_OffsetY_Start_2 ("_OffsetY start 2", Range(0, 1)) = 0 
			_OffsetX_End_2 ("_OffsetX end 2", Range(0, 1)) = 0 
			_OffsetY_End_2 ("_OffsetY end 2", Range(0, 1)) = 0 
        }
        
        SubShader
        {
	        LOD 100


	        Tags
	        {
                "Queue" = "Transparent"
                "IgnoreProjector" = "True"
                "RenderType" = "Transparent"
	        }
	        
	        Cull Off
	        Lighting Off
	        ZWrite Off
	        Fog { Mode Off }
	        Offset -1, -1
	        Blend SrcAlpha OneMinusSrcAlpha


	        Pass
	        {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                        
                #include "UnityCG.cginc"

                struct appdata_t
                {
	                float4 vertex : POSITION;
	                float2 texcoord : TEXCOORD0;
	                fixed4 color : COLOR;
                };

                struct v2f
                {
	                float4 vertex : SV_POSITION;
	                half2 texcoord : TEXCOORD0;
	                fixed4 color : COLOR;
                };

                sampler2D _MainTex;
                float _percent; 
   				float _maxBrightness;
   				float _OffsetX_Start_1;
   				float _OffsetY_Start_1;
   				float _OffsetX_End_1;
   				float _OffsetY_End_1;
   				
   				float _OffsetX_Start_2;
   				float _OffsetY_Start_2;
   				float _OffsetX_End_2;
   				float _OffsetY_End_2;
                        
                v2f vert (appdata_t v)
                {
	                v2f o;
	                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
	                o.texcoord = v.texcoord;
	                o.color = v.color;
	                return o;
                }
                        
                fixed4 frag (v2f i) : COLOR
                {
                    fixed4 col;
                
                	col = tex2D(_MainTex, i.texcoord) * i.color;

					if((i.texcoord.x > _OffsetX_Start_1 && i.texcoord.y > _OffsetY_Start_1 && i.texcoord.x < _OffsetX_End_1 && i.texcoord.y < _OffsetY_End_1)
					||(i.texcoord.x > _OffsetX_Start_2 && i.texcoord.y > _OffsetY_Start_2 && i.texcoord.x < _OffsetX_End_2 && i.texcoord.y < _OffsetY_End_2))
					{
					    // 增大UV的值，UV值越大下面的lerp获取的值越小
					    fixed2 blink_uv = (i.texcoord + fixed2(_percent, _percent)) * 2;
					    // 旋转矩阵，旋转30度 
					    fixed2x2 rotMat = fixed2x2(0.866,0.5,-0.5,0.866); 
					    // 乘以旋转矩阵
					    blink_uv = mul(rotMat, blink_uv);
					    // 当y越靠近原点时，RGB值越大
					    fixed rgba = lerp(fixed(_maxBrightness),fixed(0),abs(blink_uv.x	));
					    // 叠加RGB值
					    col +=  fixed4(saturate(rgba), saturate(rgba), saturate(rgba), 0);
					}
					return col;

                }
                ENDCG
	        }
        }


        SubShader
        {
                LOD 100


                Tags
                {
			        "Queue" = "Transparent"
			        "IgnoreProjector" = "True"
			        "RenderType" = "Transparent"
                }
                
                Pass
                {
	                Cull Off
	                Lighting Off
	                ZWrite Off
	                Fog { Mode Off }
	                Offset -1, -1
	                ColorMask RGB
	                AlphaTest Greater .01
	                Blend SrcAlpha OneMinusSrcAlpha
	                ColorMaterial AmbientAndDiffuse
	                
	                SetTexture [_MainTex]
	                {
	                        Combine Texture * Primary
	                }
                }
        }
}