#version 330 core
in vec2 TextureCoord;
out vec4 color;
uniform sampler2D userTextureOut;

void main()
{
	color=texture(userTextureOut,TextureCoord);

  //  color.a=0.25;
	// color.a=0.75;
		color.a=1;
}