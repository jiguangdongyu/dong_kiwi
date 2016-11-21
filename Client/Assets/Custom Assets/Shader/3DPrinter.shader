//add by ljdong, 3D打印机效果

Shader "Custom/Dong/3DPrinter" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ConstructY ("ConstructY", float) = 0
		_ConstructColor ("ConstructColor", Color) = (255,255,255,255)
		_ConstructGap ("ConstructGap", float) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		Cull Off
		LOD 200
		
		CGPROGRAM
		//#pragma surface surf Lambert
		#pragma surface surf Unlit fullforwardshadows

		sampler2D _MainTex;
		half4 _ConstructColor;
		float _ConstructY;
		float _ConstructGap;
		float3 viewDir;
		int building;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
			float3 viewDir;
		};

		void surf (Input IN, inout SurfaceOutput o) {

			viewDir = IN.viewDir;

			float s = +sin((IN.worldPos.x * IN.worldPos.z) * 60 + _Time[3] + o.Normal) / 120;
 
        	if (IN.worldPos.y > _ConstructY + s + _ConstructGap)
                discard;

			if(IN.worldPos.y < _ConstructY)
			{
				half4 c = tex2D (_MainTex, IN.uv_MainTex);
				o.Albedo = c.rgb;
				o.Alpha = c.a;

				building = 0;
			}
			else
			{
				o.Albedo = _ConstructColor.rgb;
				o.Alpha = _ConstructColor.a;

				building = 1;
			}
		}

		inline half4 LightingUnlit (SurfaceOutput s, half3 lightDir, half atten)
		{
			if(building)
        		return _ConstructColor;

        	if(dot(s.Normal, viewDir) < 0 )
        		return _ConstructColor;

        	half4 c = half4(1,1,1,1);
        	c.rgb = s.Albedo;
        	c.a = s.Alpha;
        	return c;
		}

		ENDCG
	} 
	FallBack "Diffuse"
}
