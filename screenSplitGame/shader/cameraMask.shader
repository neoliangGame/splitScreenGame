Shader "neo/cameraMask"
{
	SubShader
	{
		Tags{ "Queue" = "Background-1"}
		LOD 100

		Pass
		{
			ZTest On
			ZWrite On
			Cull Off
			ColorMask 0
		}
	}
}
