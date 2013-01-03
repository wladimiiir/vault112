#version 110

#ifdef VERTEX_SHADER
uniform mat4 ProjectionMatrix;

attribute vec2 InPosition;
attribute vec4 InColor;
attribute vec2 InTexCoord;
attribute vec2 InTexEggCoord;

varying vec4 Color;
varying vec2 TexCoord;
varying vec2 TexEggCoord;

void main( void )
{
	gl_Position = ProjectionMatrix * vec4( InPosition, 0.0, 1.0 );
	Color = InColor;
	TexCoord = InTexCoord;
	TexEggCoord = InTexEggCoord;
}
#endif

#ifdef FRAGMENT_SHADER
uniform sampler2D ColorMap;
uniform vec4      ColorMapSize;
uniform sampler2D EggMap;
uniform float     ZoomFactor;

varying vec4 Color;
varying vec2 TexCoord;
varying vec2 TexEggCoord;

void main( void )
{
	vec4 texColor;
	if( ZoomFactor != 1.0 )
	{
		vec4 tl = texture2D( ColorMap, TexCoord );
		if( tl.a < 1.0 ) discard;
		vec4 tr = texture2D( ColorMap, TexCoord + vec2( ColorMapSize[ 2 ], 0 ) );
		if( tr.a < 1.0 ) tr = tl;
		vec4 bl = texture2D( ColorMap, TexCoord + vec2( 0, ColorMapSize[ 3 ] ) );
		if( bl.a < 1.0 ) bl = tl;
		vec4 br = texture2D( ColorMap, TexCoord + vec2( ColorMapSize[ 2 ] , ColorMapSize[ 3 ] ) );
		if( br.a < 1.0 ) br = tl;
		
		vec2 f = fract( TexCoord.xy * ColorMapSize[ 0 ] );
		vec4 tA = mix( tl, tr, f.x );
		vec4 tB = mix( bl, br, f.x );
		texColor = mix( tA, tB, f.y );
	}
	else
	{
		texColor = texture2D( ColorMap, TexCoord );
		if( texColor.a < 1.0 ) discard;
	}
	
	gl_FragColor.rgb = ( texColor.rgb * Color.rgb ) * 2.0;
	gl_FragColor.a = texColor.a * Color.a;
	
	if( TexEggCoord.x != 0.0 )
		gl_FragColor.a *= texture2D( EggMap, TexEggCoord ).a;
}
#endif
