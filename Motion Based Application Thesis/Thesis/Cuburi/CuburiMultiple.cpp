#include "resource/SOIL2/SOIL2.h"
#include <iostream>
#define GLEW_STATIC
#include <GL/glew.h>


//#include <glfw/glfw3.h>
#include <glfw/include/GLFW/glfw3.h>
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include<glm/ext/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>
#include <glm/gtc/random.hpp>
#include "resource/c10/perspective/Shader.h"
#include <array>

#include <iostream>
#include <stdlib.h> 
#include <fstream>

#include <Windows.h>

using namespace std;

GLfloat increaseX = 0;
GLfloat increaseY = 0;
GLfloat increaseZ = 0;
const GLuint WIDTH = 400, HEIGHT = 400;
GLfloat repetition = 0;
GLfloat j = 10.0;
float readDataX[24400];
float readDataY[24400];
float readDataZ[24400];
float readGyrX[24400];
float readGyrY[24400];
float readGyrZ[24400];
int numaram=0;
int incGyr = 0;
int total,totalGyr;
int numaram2 = 0;

void context()
{
	glfwInit();
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 1);
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_ANY_PROFILE);
	glfwWindowHint(GLFW_OPENGL_FORWARD_COMPAT, GL_TRUE);
	glfwWindowHint(GLFW_RESIZABLE, GL_FALSE);
}
glm::mat4 transform(GLchar axes)
{
	glm::mat4 transformMatrix = glm::mat4(1.0f);
	GLfloat step = -2;
	GLfloat angle = 0;
	glm::vec3 axesV;
	//transformMatrix = glm::translate(transformMatrix, glm::vec3(0.5f, 0.5f, 0.0f));
	if (axes == 'z')
	{
		axesV = glm::vec3(0.0f, 0.0f, 1.0f);
	}
	angle = (GLfloat)glfwGetTime() * step;
	transformMatrix = glm::rotate(transformMatrix, angle, axesV);
	transformMatrix = glm::translate(transformMatrix, glm::vec3(0.75f, 0.75f, 0.0f));
	return transformMatrix;
}
//glm::mat4 translateUser(glm::vec3 vect)
//{
//	glm::mat4 transformMatrix = glm::mat4(1.0f);
//	transformMatrix = glm::translate(transformMatrix, vect);
//	return transformMatrix;
//}
glm::mat4 transformI(GLchar axes)
{
	glm::mat4 transformMatrix = glm::mat4(1.0f);
	GLfloat step = 0.002;
	GLfloat angle = 0;
	glm::vec3 axesV;
	transformMatrix = glm::translate(transformMatrix, glm::vec3(1.f, 1.f, 0.0f));
	if (axes == 'z')
	{
		axesV = glm::vec3(0.0f, 0.0f, 1.0f);
	}
	angle = (GLfloat)repetition * step;
	repetition++;
	transformMatrix = glm::rotate(transformMatrix, angle, axesV);
	return transformMatrix;
}


int main()
{
	//Read displacement vectors for X axis
	ifstream source;                    // build a read-Stream

	source.open("DisplacementX.txt", ios_base::in);  // open data
	if (!source) {                     // if it does not work
		cerr << "Can't open Data!\n";
	}
	for (std::string line; std::getline(source, line); )   //read stream line by line
	{
		std::istringstream in(line);      //make a stream for the line itself

		in >> total;
		while (numaram < total)
		{
			in >> readDataX[numaram];       //now read the whitespace-separated floats

			cout << readDataX[numaram] << " ";
			numaram++;
		}
	}
	source.close();
	cout << endl;
	//Read displacement vectors for Y axis
	numaram = 0;
	source.open("DisplacementY.txt", ios_base::in);  // open data
	if (!source) {                     // if it does not work
		cerr << "Can't open Data!\n";
	}
	for (std::string line; std::getline(source, line); )   //read stream line by line
	{
		std::istringstream in(line);      //make a stream for the line itself

		in >> total;
		while (numaram < total)
		{
			in >> readDataY[numaram];       //now read the whitespace-separated floats

			cout << readDataY[numaram] << " ";
			numaram++;
		}
	}
	source.close();
	cout << endl;
	//Read displacement vectors for Z axis
	numaram = 0;
	source.open("DisplacementZ.txt", ios_base::in);  // open data
	if (!source) {                     // if it does not work
		cerr << "Can't open Data!\n";
	}
	for (std::string line; std::getline(source, line); )   //read stream line by line
	{
		std::istringstream in(line);      //make a stream for the line itself

		in >> total;
		while (numaram < total)
		{
			in >> readDataZ[numaram];       //now read the whitespace-separated floats

			cout << readDataZ[numaram] << " ";
			numaram++;
		}
	}
	source.close();
	cout << endl;

	//Read gyroscope vectors for X axis
	incGyr = 0;
	cout << "GyrX data:";
	source.open("AllGyrX.txt", ios_base::in);  // open data
	if (!source) {                     // if it does not work
		cerr << "Can't open Data!\n";
	}
	for (std::string line; std::getline(source, line); )   //read stream line by line
	{
		std::istringstream in(line);      //make a stream for the line itself

		in >> totalGyr;
		while (incGyr < totalGyr)
		{
			in >> readGyrX[incGyr];       //now read the whitespace-separated floats

			cout << readGyrX[incGyr] << " ";
			incGyr++;
		}
	}
	source.close();
	cout << endl;

	//Read gyroscope vectors for X axis
	incGyr = 0;
	cout << "GyrY data:";
	source.open("AllGyrY.txt", ios_base::in);  // open data
	if (!source) {                     // if it does not work
		cerr << "Can't open Data!\n";
	}
	for (std::string line; std::getline(source, line); )   //read stream line by line
	{
		std::istringstream in(line);      //make a stream for the line itself

		in >> totalGyr;
		while (incGyr < totalGyr)
		{
			in >> readGyrY[incGyr];       //now read the whitespace-separated floats

			cout << readGyrY[incGyr] << " ";
			incGyr++;
		}
	}
	source.close();
	cout << endl;

	//Read gyroscope vectors for X axis
	incGyr = 0;
	cout << "GyrZ data:";
	source.open("AllGyrZ.txt", ios_base::in);  // open data
	if (!source) {                     // if it does not work
		cerr << "Can't open Data!\n";
	}
	for (std::string line; std::getline(source, line); )   //read stream line by line
	{
		std::istringstream in(line);      //make a stream for the line itself

		in >> totalGyr;
		while (incGyr < totalGyr)
		{
			in >> readGyrZ[incGyr];       //now read the whitespace-separated floats

			cout << readGyrZ[incGyr] << " ";
			incGyr++;
		}
	}
	source.close();
	cout << endl;
	/*GLfloat vertexArray[] =
	{
		// Positions         // Colors
		-0.75f, -0.25f, 1.0f,   1.0f, 0.0f, 0.0f,  0.0f,
		-0.25f, -0.25f, 1.0f,   0.0f, 1.0f, 0.0f,0.0f,
		-0.25f, -0.75f, 1.0f,   0.0f, 0.0f, 1.0f, 0.0f,
		+0.25f, +0.25f, 1.0f,   1.0f, 0.0f, 0.0f,  0.0f,
		+0.25f, +0.75f, 1.0f,   0.0f, 1.0f, 0.0f,0.0f,
		0.75f, +0.25f,  1.0f,    0.0f, 0.0f, 1.0f, 0.0f,
	};
	*/
	/*GLfloat vertexArray[] =
	{
		// Positions          // Colors           // Texture Coords
		0.5f,  0.5f, 0.0f,   1.0f, 0.0f, 0.0f,   1.0f, 1.0f, //
		0.5f, -0.5f, 0.0f,   1.0f, 1.0f, 0.0f,  1.0f, 0.0f, //
		-0.5f, -0.5f, 0.0f,   1.0f, 0.0f, .0f,   0.0f, 0.0f, //
		-0.5f,  0.5f, 0.0f,   1.0f, 1.0f, 0.0f,   0.0f,1.0f  //
	};*/


	GLfloat vertexArray[] = {
		// Positions        // Texture Coords
	  /* -0.75f, -0.75f, -0.75f,  0.0f, 0.0f,
		0.75f, -0.75f, -0.75f,  1.0f, 0.0f,
		0.75f,  0.75f, -0.75f,  1.0f, 1.0f,
		0.75f,  0.75f, -0.75f,  1.0f, 1.0f,
	   -0.75f,  0.75f, -0.75f,  0.0f, 1.0f,
	   -0.75f, -0.75f, -0.75f,  0.0f, 0.0f,*/

	   //back
		   0.75f, -0.75f, -0.75f,  0.0f, 0.0f,
	   -0.75f, -0.75f, -0.75f,  1.0f, 0.0f,
	   -0.75f,  0.75f, -0.75f,  1.0f, 1.0f,
		0.75f, -0.75f, -0.75f,  0.0f, 0.0f,
		0.75f, 0.75f, -0.75f,  0.0f, 1.0f,
	   -0.75f,  0.75f, -0.75f,  1.0f, 1.0f,
	   //front
			  -0.75f, -0.75f,  0.75f,  0.0f, 0.0f,
			   0.75f, -0.75f,  0.75f,  1.0f, 0.0f,
			   0.75f,  0.75f,  0.75f,  1.0f, 1.0f,
			   0.75f,  0.75f,  0.75f,  1.0f, 1.0f,
			  -0.75f,  0.75f,  0.75f,  0.0f, 1.0f,
			  -0.75f, -0.75f,  0.75f,  0.0f, 0.0f,

			  //right
					 0.75f,  0.75f,  0.75f,  0.0f, 1.0f,
					 0.75f,  0.75f, -0.75f,  1.0f, 1.0f,
					 0.75f, -0.75f, -0.75f,  1.0f, 0.0f,
					 0.75f, -0.75f, -0.75f,  1.0f, 0.0f,
					 0.75f, -0.75f,  0.75f,  0.0f, 0.0f,
					 0.75f,  0.75f,  0.75f,  0.0f, 1.0f,

					 //left
					 -0.75f,  0.75f,  0.75f,  1.0f, 1.0f,
					 -0.75f,  -0.75f, +0.75f, 1.0f, 0.0f,
					 -0.75f, -0.75f, -0.75f,  0.0f, 0.0f,
					 -0.75f, -0.75f, -0.75f,  0.0f, 0.0f,
					 -0.75f, +0.75f,  -0.75f,  0.0f, 1.0f,
					 -0.75f,  0.75f,  0.75f,  1.0f, 1.0f,
					 //up
							 -0.75f,  0.75f, -0.75f,  0.0f, 1.0f,
							 0.75f,  0.75f, -0.75f,  1.0f, 1.0f,
							 0.75f,  0.75f,  0.75f,  1.0f, 0.0f,
							 0.75f,  0.75f,  0.75f,  1.0f, 0.0f,
							-0.75f,  0.75f,  0.75f,  0.0f, 0.0f,
							-0.75f,  0.75f, -0.75f,  0.0f, 1.0f,
							//down
								 -0.75f, -0.75f, -0.75f,  1.0f, 1.0f,
								  0.75f, -0.75f, -0.75f,  0.0f, 1.0f,
								  0.75f, -0.75f,  0.75f,  0.0f, 0.0f,
								  0.75f, -0.75f,  0.75f,  0.0f, 0.0f,
								 -0.75f, -0.75f,  0.75f,  1.0f, 0.0f,
								 -0.75f, -0.75f, -0.75f,  1.0f, 1.0f,


	};
	//
	//GLuint indices[] =
	//{
	//	0, 1, 3, // Primul triunghi
	//	1, 2, 3  // Al doilea triunghi
	//};

	GLuint VBO, VAO, EBO;
	int screenWidth, screenHeight;

	context();
	GLFWwindow* window = glfwCreateWindow(1080, 750, "OpenGL - perspective", nullptr, nullptr);
	glfwGetFramebufferSize(window, &screenWidth, &screenHeight);
	cout << "Screen Width= " << screenWidth;
	if (nullptr == window)
	{
		std::cout << "Failed to create GLFW window" << std::endl;
		glfwTerminate();

		return EXIT_FAILURE;
	}
	glfwMakeContextCurrent(window);
	glewExperimental = GL_TRUE;
	if (GLEW_OK != glewInit())
	{
		std::cout << "Failed to initialize GLEW" << std::endl;
		return EXIT_FAILURE;
	}
	glEnable(GL_DEPTH_TEST);
	glEnable(GL_BLEND);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
	//glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
	//glBlendFunc(GL_SRC_COLOR, GL_ONE_MINUS_SRC_COLOR);
// glBlendFunc(GL_SRC_ALPHA, GL_DST_ALPHA);  
//	glBlendFunc(GL_SRC_ALPHA, GL_DST_ALPHA);
//	glBlendFunc(GL_ONE_MINUS_SRC_COLOR, GL_DST_COLOR);
//	glBlendFunc(GL_ONE, GL_ONE);
	/*glBlendFunc(GL_ZERO, GL_ONE);
	glBlendColor(1, 0, 0, 0.5);
	glBlendFunc(GL_CONSTANT_COLOR, GL_ONE_MINUS_CONSTANT_COLOR);*/
	Shader userShader("resource/c11/perspective/vertex.sh", "resource/c11/perspective/fragment.sh");
	glGenVertexArrays(1, &VAO);
	glGenBuffers(1, &VBO);
	//glGenBuffers(1, &EBO);
	glBindVertexArray(VAO);
	glBindBuffer(GL_ARRAY_BUFFER, VBO);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertexArray), vertexArray, GL_STATIC_DRAW);
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 5 * sizeof(GLfloat), (GLvoid*)0);
	glEnableVertexAttribArray(0);
	glVertexAttribPointer(1, 2, GL_FLOAT, GL_FALSE, 5 * sizeof(GLfloat), (GLvoid*)(3 * sizeof(GLfloat)));
	glEnableVertexAttribArray(1);
	glBindVertexArray(0);
	GLuint texture0, texture1;
	int width, height;

	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);// GL_LINEAR);
	GLuint texture[6];
	array<unsigned char*, 6> imageInfo;
	//array<const char *, 6> filePath = {
	//	"resource/images/example4.jpg",
	//	"resource/images/example5.jpg",
	//	"resource/images/example6.jfif",
	//	"resource/images/example8.jpg",
	//	"resource/images/example10.jpg",
	//	"resource/images/example15.jpg"
	//};
	array<const char*, 6> filePath = {
	"resource/images/red_.jpg",//back
	"resource/images/green_.jpg",//front
	"resource/images/blue_.jpg",//right
	"resource/images/yellow_.jpg",//left
	"resource/images/pink_.jpg",//up
	"resource/images/grey_.jpg"//down
	};

	for (int i = 0; i < 6; i++)
	{

		glGenTextures(1, &texture[i]);
		glBindTexture(GL_TEXTURE_2D, texture[i]);
		imageInfo[i] = SOIL_load_image(filePath[i], &width, &height, 0, SOIL_LOAD_RGBA);
		glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, width, height, 0, GL_RGBA, GL_UNSIGNED_BYTE, imageInfo[i]);
		glGenerateMipmap(GL_TEXTURE_2D);
		SOIL_free_image_data(imageInfo[i]);
		glBindTexture(GL_TEXTURE_2D, 0);
	}



	//projectionMatrix = glm::ortho(0.0f, (GLfloat)screenWidth, 0.0f, (GLfloat)screenHeight, 0.1f, 1000.0f);
	glm::mat4 projectionMatrix;
	projectionMatrix = glm::perspective(glm::radians(80.0f), (GLfloat)screenWidth / (GLfloat)screenHeight, 0.1f, 150.0f);
	//generating the position for all the cubes
	array<glm::vec3, 6> position;
	for (int k = 0; k < 6; k++)
	{
		position[k] = glm::sphericalRand(5.0f);
		//	position[k] = glm::vec3(glm::diskRand(15.0f),0.);
		  //  position[k] = glm::ballRand(10.0f);
	}
	glActiveTexture(GL_TEXTURE0);
	while (!glfwWindowShouldClose(window))
	{
		glfwPollEvents();
		glClearColor(0.4f, 0.0f, 0.0f, 1.f);
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
		userShader.Use();

		glm::mat4 modelMatrix = glm::mat4(1.0f), viewMatrix = glm::mat4(1.0f), viewMatrixRef = glm::mat4(1.0f);
		modelMatrix = glm::rotate(modelMatrix, 0.f, glm::vec3(1.f, 0.f, 1.f));
		
		int distance = 50;
		glm::vec3 center = glm::vec3(0., 0., 0.);
		//	glm::vec3 center = glm::vec3(10., 10., 10.);

		//viewMatrix = glm::lookAt(glm::vec3(sin(glfwGetTime()) * distance, 0, cos(glfwGetTime()) * distance), center, glm::vec3(0, 1, 0));
		viewMatrix = glm::lookAt(glm::vec3(sin(3.14) * distance, 0, cos(3.14) * distance), center, glm::vec3(0, 0, 1));


		GLint transformLocation;
		transformLocation = glGetUniformLocation(userShader.Program, "modelMatrix");
		glUniformMatrix4fv(transformLocation, 1, GL_FALSE, glm::value_ptr(modelMatrix));
		transformLocation = glGetUniformLocation(userShader.Program, "viewMatrix");
		glUniformMatrix4fv(transformLocation, 1, GL_FALSE, glm::value_ptr(viewMatrix));
		transformLocation = glGetUniformLocation(userShader.Program, "projectionMatrix");
		glUniformMatrix4fv(transformLocation, 1, GL_FALSE, glm::value_ptr(projectionMatrix));
		glBindVertexArray(VAO);

		for (int p = 0; p < 6; p++)//6 cuburi
		{
			if (p == 0) viewMatrixRef = glm::translate(viewMatrix, glm::vec3(0.f, 0.f, 0.f));
			else
			{
				if (numaram2 == 0) Sleep(10000);
				//increaseX += 0.0000001;		increaseY += 0.0000001;		increaseZ += 0.0000001;
				//cout <<increaseX << " " << p<< " "<< position[p].x << " " << position[p].y << " " << position[p].z << endl;
				if (numaram2 < numaram)
				{
					/*if (readDataX[numaram2] < readDataX[numaram2 + 1])
					{
						for (float increment = readDataX[numaram2]; increment < readDataX[numaram2 + 1]; increment += 0.0000001)
							position[p].x += increment;
					}*/
					position[p].x = readDataX[numaram2]/10;
					position[p].y = readDataY[numaram2]/10;
					position[p].z = readDataZ[numaram2]/10; 
					
				}
				viewMatrixRef = glm::translate(viewMatrix, position[p]);
				
				//viewMatrix = glm::translate(viewMatrix, glm::vec3(0.f, 0.f, 2.f));
				transformLocation = glGetUniformLocation(userShader.Program, "viewMatrix");
				glUniformMatrix4fv(transformLocation, 1, GL_FALSE, glm::value_ptr(viewMatrixRef));	
				
			   //aplica 3 rotatii diferite pe model matrix de aici modelMatrix = glm::rotate(modelMatrix, unghi.xf, glm::vec3(1.f, 0.f, 0.f));
				modelMatrix = glm::rotate(modelMatrix, readGyrX[numaram2], glm::vec3(1.f, 0.f, 0.f));
				modelMatrix = glm::rotate(modelMatrix, readGyrY[numaram2], glm::vec3(0.f, 1.f, 0.f));
				modelMatrix = glm::rotate(modelMatrix, readGyrZ[numaram2], glm::vec3(0.f, 0.f, 1.f));
			    transformLocation = glGetUniformLocation(userShader.Program, "modelMatrix");
				glUniformMatrix4fv(transformLocation, 1, GL_FALSE, glm::value_ptr(modelMatrix));
				Sleep(250);
				if (numaram2 == 0) Sleep(10000);
				numaram2++;
			}
			for (int i = 0; i < 6; i++) {

				glBindTexture(GL_TEXTURE_2D, texture[i]);
				glUniform1i(glGetUniformLocation(userShader.Program, "userTextureOut"), 0);
				glDrawArrays(GL_TRIANGLES, i * 6, 6);
			}
			viewMatrixRef = viewMatrix;

		}

		//	viewMatrix = glm::translate(viewMatrix, glm::vec3(-position.x, -position.y, -position.z));



		//primitiva grafica, numarul de elemente de desenat, tipul indicilor, offset-ul primului indice considerat
		//glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, 0);
		glBindVertexArray(0);
		glBindTexture(GL_TEXTURE_2D, 0);
		glfwSwapBuffers(window);
	}
	glViewport(0, 0, screenWidth, screenHeight);

	glDeleteVertexArrays(1, &VAO);
	glDeleteBuffers(1, &VBO);
	//glDeleteBuffers(1, &EBO);
	glfwTerminate();
	return EXIT_SUCCESS;
}