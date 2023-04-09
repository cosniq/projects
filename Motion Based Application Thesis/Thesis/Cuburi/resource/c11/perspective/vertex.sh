#version 330 core
layout (location = 0) in vec3 position;
layout (location = 1) in vec2 textureCoord;
out vec2 TextureCoord;

uniform mat4  modelMatrix;
uniform mat4 viewMatrix;
uniform mat4 projectionMatrix;//PVM

void main()
{
	gl_Position = projectionMatrix*viewMatrix*modelMatrix*vec4(position, 1.0f);
  //gl_Position=viewMatrix*modelMatrix*vec4(position, 1.0f);;
	
//	gl_Position = projectionMatrix*vec4(position, 1.0f);
//   gl_Position = viewMatrix*modelMatrix*vec4(position, 1.0f);
	TextureCoord=vec2(textureCoord.s,1.f-textureCoord.t);
	////the image y-axis in opengl is inverted

}