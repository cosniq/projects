#ifndef SHADER_H
#define SHADER_H
#include <string>
#include <fstream>
#include <sstream>
#include <iostream>
#include <GL/glew.h>
using namespace std;
class Shader
{
public:
	GLuint Program;
	Shader(const GLchar *vertexPath, const GLchar *fragmentPath)
	{

		GLuint vertexShader, fragmentShader;
		GLint success;
		string vertexCode,fragmentCode;
		ifstream vertexShaderFile,fragmentShaderFile;
		stringstream vertexShaderStream, fragmentShaderStream;
		const GLchar *vertexShaderCode,	 *fragmentShaderCode;
		//badbit: Read/writing error on i/o operation.
		vertexShaderFile.exceptions(ifstream::badbit);
		fragmentShaderFile.exceptions(ifstream::badbit);
		try
		{
			vertexShaderFile.open(vertexPath);
			fragmentShaderFile.open(fragmentPath);
		
			vertexShaderStream << vertexShaderFile.rdbuf();
			fragmentShaderStream << fragmentShaderFile.rdbuf();
			vertexShaderFile.close();
			fragmentShaderFile.close();
			vertexCode = vertexShaderStream.str();
			fragmentCode = fragmentShaderStream.str();
		}
		catch (ifstream::failure e)
		{
			cout << "Shader error reading file." << std::endl;
		}
		vertexShaderCode = vertexCode.c_str();
	    fragmentShaderCode = fragmentCode.c_str();
		// Vertez Shader creation, compilation
		vertexShader = glCreateShader(GL_VERTEX_SHADER);
		glShaderSource(vertexShader, 1, &vertexShaderCode, NULL);
		glCompileShader(vertexShader);
		this->errorMessage(vertexShader, GL_COMPILE_STATUS, "Vertex shader compilation failed");
		// Fragment Shader creation and compilation
		fragmentShader = glCreateShader(GL_FRAGMENT_SHADER);
		glShaderSource(fragmentShader, 1, &fragmentShaderCode, NULL);
		glCompileShader(fragmentShader);
		
		this->errorMessage(fragmentShader, GL_COMPILE_STATUS, "Fragment shader  compilation failed");
		
		// Shader Program - create, attach the shaders, linking, delete the shaders
		this->Program = glCreateProgram();
		glAttachShader(this->Program, vertexShader);
		glAttachShader(this->Program, fragmentShader);
		glLinkProgram(this->Program);
	
		this->errorMessage(this->Program, GL_LINK_STATUS, "Program shader linking failed");
		glDeleteShader(vertexShader);
		glDeleteShader(fragmentShader);

	}

	void Use()
	{
		glUseProgram(this->Program);
	}
private:
	void errorMessage(GLint shader, GLenum pname, const GLchar *message)
	{
		GLint success;
		GLchar logData[400];
		glGetProgramiv(shader, pname, &success);
		if (!success)
		{
			glGetProgramInfoLog(shader, 400, NULL, logData);
			std::cout << message<<"\n" << logData << std::endl;
		}
	}
};

#endif