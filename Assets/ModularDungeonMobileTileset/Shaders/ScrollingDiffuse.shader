Shader "Custom/ScrollingDiffuse" {
	Properties{
		_MainTex("Color (RGB) Alpha (A)", 2D) = "white"
	_XScrollSpeed("X Scroll Speed", Float) = 0
		_YScrollSpeed("Y Scroll Speed", Float) = 10
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
#pragma surface surf Lambert alpha

		sampler2D _MainTex;
	float _XScrollSpeed;
	float _YScrollSpeed;

	struct Input {
		float2 uv_MainTex;
	};

	void surf(Input IN, inout SurfaceOutput o) {
		fixed2 scrollUV = IN.uv_MainTex;
		fixed xScrollValue = _XScrollSpeed * _Time.x;
		fixed yScrollValue = _YScrollSpeed * _Time.x;
		scrollUV += fixed2(xScrollValue, yScrollValue);
		half4 c = tex2D(_MainTex, scrollUV);
		o.Albedo = c.rgb;
		o.Alpha = tex2D(_MainTex, IN.uv_MainTex).a;
	}
	ENDCG
	}
		FallBack "Diffuse"
}
