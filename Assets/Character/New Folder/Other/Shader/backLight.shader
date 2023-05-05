// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/backLight" {
	
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		
		_Color ("Main Color", Color) = (0.1,0.2,1,1)
		
		_Modul("Modulus", Range (0.1, 20)) = 2.0
	}
	
	SubShader {
		//Tags { "Queue" = "AlphaTest"}
	    //Tags { "RenderType" = "TreeOpaque"}
	    
	    Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag 
			#include "UnityCG.cginc"
			
			float _Modul;
			float4 _Color;
			
			struct v2f {
			    float4 pos : SV_POSITION;
			    float3 color : COLOR0;
			    float alpha : COLOR1; 
			};
			
			v2f vert (appdata_base v)
			{
			    v2f o;
			    
			    
			    float3 viewDir = normalize(ObjSpaceViewDir (v.vertex ) );
			    
			    float parm = dot(v.normal, viewDir);
			    
			    parm = 1-parm;
			    parm = pow(parm,_Modul);
			    parm = max(parm,0.0);
			    o.pos = UnityObjectToClipPos (v.vertex);
			    o.color = _Color*parm;
			    o.alpha = parm;
			    return o;
			}
			
			half4 frag (v2f i) : COLOR
			{
			    //return half4 (i.color, i.alpha);
				return half4 (i.color, i.alpha);
			}
			ENDCG
						
			//ZWrite off
			
			//ZTest Greater
			
			//Blend SrcColor OneMinusSrcAlpha
			Blend One One
			
	    }
	   
	}
	
	
	 
	FallBack "Diffuse"
}
