#version 110

#ifdef VERTEX_SHADER
uniform mat4 ProjectionMatrix;

attribute vec2 InPosition;
attribute vec2 InTexCoord;

varying vec2 TexCoord;
varying vec4 ShadowCoord;

void main( void )
{
	gl_Position = ProjectionMatrix * vec4( InPosition, 0.0, 1.0 );
	TexCoord = InTexCoord;
}
#endif

#ifdef FRAGMENT_SHADER
uniform sampler2D ShadowMap;
uniform mat4      ShadowMapMatrix;

varying vec2 TexCoord;
varying vec4 ShadowCoord;

void main( void )
{
	/*vec4 coord = ShadowCoord / ShadowCoord.w;
	coord.z += 0.0005;
	float distanceFromLight = texture2D( ShadowMap, coord.st ).z;
 	if( ShadowCoord.w > 0.0 || distanceFromLight >= coord.z )
		discard;
  	gl_FragColor = vec4( 0.5 );*/
	
	if( gl_FragCoord.z  texture2D( ShadowMap, TexCoord );
	
	/*gl_FragColor=vec4(1.0);
	gl_FragColor.g = (texture2D( ShadowMap, TexCoord ).z-0.4)*5.0;
	if( texture2D( ShadowMap, TexCoord ).z > 0.999 )
		discard;*/

	//gl_FragColor = vec4(c.x,c.y,c.z,1.0);
	//if(c.x==1.0 && c.y==1.0 && c.z==1.0)
	//	gl_FragColor = vec4(1.0,1.0,1.0,1.0);
	//else
	//	gl_FragColor = vec4(0.0,0.0,0.0,1.0);
	//gl_FragColor = vec4(v.x,v.y,v.z,1.0);
}
#endif
