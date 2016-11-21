Shader "HIDDEN/Unlit/Transparent Colored Shine 1" 
 {
 		Properties
        {
	        _MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}
			 _percent ("_percent", Range(-4, 3)) = 0 
			_maxBrightness("_maxBrightness", Range(0, 1)) = 0.5 
        }
        
        SubShader
        {
	        LOD 200

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

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                        
                #include "UnityCG.cginc"
                
                sampler2D _MainTex;
                float _percent; 
   				float _maxBrightness;
   				
   				float4 _ClipRange0 = float4(0.0, 0.0, 1.0, 1.0);
				float2 _ClipArgs0 = float2(1000.0, 1000.0);

                struct appdata_t
                {
	                float4 vertex : POSITION;
	                fixed4 color : COLOR;
	                float2 texcoord : TEXCOORD0;	          
                };

                struct v2f
                {
	                float4 vertex : POSITION;
	                fixed4 color : COLOR;
	                half2 texcoord : TEXCOORD0;
	                float2 worldPos : TEXCOORD1; 
                };
                        
                v2f vert (appdata_t v)
                {
	                v2f o;
	                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);	            
	                o.color = v.color;
	                o.texcoord = v.texcoord;
	                o.worldPos = v.vertex.xy * _ClipRange0.zw + _ClipRange0.xy;
	                return o;
                }
                        
                fixed4 frag (v2f i) : COLOR
                {
                    fixed4 col;
                
                	col = tex2D(_MainTex, i.texcoord) * i.color;

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
				
					// Softness factor
					float2 factor = (float2(1.0, 1.0) - abs(i.worldPos)) * _ClipArgs0;
					col.a *= clamp( min(factor.x, factor.y), 0.0, 1.0);
				
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