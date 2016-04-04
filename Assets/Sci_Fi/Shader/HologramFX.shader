﻿/////////////////////////////////////////////////////////////////
/// EASY 2D SPRITES - Hologram 3 -1.2- by VETASOFT 2014
/////////////////////////////////////////////////////////////////

Shader "EasySprite2D/Hologram3_EasyS2D" {
Properties
{
_MainTex ("Base (RGB)", 2D) = "white" {}
_Size ("Size", Range(0,1)) = 0
_Distortion ("Distortion", Range(0,1)) = 0

_Alpha ("Alpha", Range (0,1)) = 1.0
_Red ("Red", Range (0,1)) = 0.5
_Green ("Green", Range (0,1)) = 0.5
_Blue ("Blue", Range (0,1)) = 0.5
}

SubShader
{
Tags {"Queue"="Transparent" "IgnoreProjector"="true" "RenderType"="Transparent"}
ZWrite Off Blend SrcAlpha One Cull Off



Pass
{

CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#pragma target 3.0
#include "UnityCG.cginc"


struct appdata_t
{
float4 vertex   : POSITION;
float4 color    : COLOR;
float2 texcoord : TEXCOORD0;
};

struct v2f
{
half2 texcoord  : TEXCOORD0;
float4 vertex   : POSITION;
fixed4 color    : COLOR;
};


sampler2D _MainTex;
float _Size;
float _Distortion;

float _Red;
float _Green;
float _Blue;
fixed _Alpha;

v2f vert(appdata_t IN)
{
v2f OUT;
OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
OUT.texcoord = IN.texcoord;
OUT.color = IN.color;

return OUT;
}


inline float mod(float x,float modu) {
return x - floor(x * (1.0 / modu)) * modu;
}

inline float noise(float2 p)
{
float _TimeX=_Time*20;
float sample = tex2D(_MainTex,float2(.2,0.2*cos(_TimeX))*_TimeX*8. + p*1.).x;
sample *= sample;
return sample;
}


inline float onOff(float a, float b, float c)
{
float _TimeX=_Time*20;
return step(c, sin(_TimeX + a*cos(_TimeX*b)));
}

inline float ramp(float y, float start, float end)
{
float inside = step(start,y) - step(end,y);
float fact = (y-start)/(end-start)*inside;
return (1.-fact) * inside;

}

inline float stripes(float2 uv)
{
float _TimeX=_Time*20;
float noi = noise(uv*float2(0.5,1.) + float2(6.,3.))*_Distortion*3;
return ramp(mod(uv.y*4. + _TimeX/2.+sin(_TimeX + sin(_TimeX*0.63)),1.),.5,.6)*noi;
}

inline float4 getVideo(float2 uv)
{
float _TimeX=_Time*20;
float2 look = uv;
float window = 4./(1.+20.*(look.y-mod(_TimeX/1.,1.))*(look.y-mod(_TimeX/10.,1.)));
look.x = look.x + sin(look.y*30. + _TimeX)/(50.*_Distortion)*onOff(1.,4.,.3)*(1.+cos(_TimeX*80.))*window;

float vShift = .4*onOff(2.,3.,.9)*(sin(_TimeX)*sin(_TimeX*200.) +
(0.5 + 0.1*sin(_TimeX*20.)*cos(_TimeX)));

look.y = mod(look.y + vShift, 1.);

float4 video;

video.r = tex2D(_MainTex,look-float2(.05,0.)*onOff(2.,1.5,.9)).r;
float4 videox=tex2D(_MainTex,look);
video.g = videox.g;
video.b = tex2D(_MainTex,look+float2(.05,0.)*onOff(2.,1.5,.9)).b;
video.a = videox.a;

return video;
}

inline float2 screenDistort(float2 uv)
{
uv -= float2(.5,.5);
uv = uv*4.2*(1./4.2+2.*uv.x*uv.x*uv.y*uv.y);
uv += float2(.5,.5);
return uv;
}

float4 frag (v2f i) : COLOR
{
float _TimeX=_Time*20;
float2 uv = i.texcoord.xy;
float alpha = tex2D(_MainTex,uv).a;

uv = screenDistort(uv);
fixed4 video = getVideo(uv);
float vigAmt = 3.+.3*sin(_TimeX + 5.*cos(_TimeX*5.));
float vignette = (1.-vigAmt*(uv.y-.5)*(uv.y-.5))*(1.-vigAmt*(uv.x-.5)*(uv.x-.5));

video += stripes(uv);
video += noise(uv*2.)/2.;
video.r *= vignette;

video.rgb=video.r;

video.r-=1-_Red;
video.g-=1-_Green;
video.b-=1-_Blue;

video *= (12.+mod(uv.y*30.+_TimeX,1.))/13.;
video.a=video.a+(frac(sin(dot(uv.xy*_TimeX,float2(12.9898,78.233))) * 43758.5453))*.5;
video.a=(video.a*.4)*alpha*vignette*4*(1-_Alpha);

return video;
}

ENDCG
}
}
Fallback "Sprites/Default"

}