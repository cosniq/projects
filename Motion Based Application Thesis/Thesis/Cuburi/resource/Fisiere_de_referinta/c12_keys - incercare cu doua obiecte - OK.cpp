//2 obiecte diferite, modificare a translatiei independenta pentru cuburi
#include "resource/SOIL2/SOIL2.h"
#include <iostream>
#define GLEW_STATIC
#include <GL/glew.h>
#include <GLFW/glfw3.h>
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>
#include <glm/gtc/random.hpp>
#include "resource/c10/perspective/Shader.h"
#include <array>

const GLuint WIDTH = 800, HEIGHT = 800;
GLfloat repetition = 0;
GLfloat j = 10.0;
GLfloat angle = glm::radians(45.f);
GLfloat xpos_ = 0;
glm::vec3 translateScene = glm::vec3(0.f, 0.f, 0.f);
glm::vec3 cameraPosition;
GLfloat distance = 30.0;
glm::vec3 center = glm::vec3(0.f, 0.f, 0.f);
void context()
{
	glfwInit();
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 1);
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_ANY_PROFILE);
	glfwWindowHint(GLFW_OPENGL_FORWARD_COMPAT, GL_TRUE);
	glfwWindowHint(GLFW_RESIZABLE, GL_FALSE);
}
// TEMA C12:...........EXPLICATI PREZENTA FUNCTIILOR go*, UNDE SE APElEAZA, 
// CE PARAMETRII MODIFICA SI CAND
void goLeft()
{
	translateScene.x-=0.25;
	cout << "   " << translateScene.x << endl;
}
void goRight()
{
	translateScene.x+=0.25;
	cout << "   " << translateScene.x << endl;
}
void goDown()
{
	translateScene.y-=0.25;
	cout << "   " << translateScene.y << endl;
}
void goUp()
{
	translateScene.y+=0.25;
	cout << "   " << translateScene.y << endl;
}
void goBack()
{
	translateScene.z -= 0.25;
	cout << "   " << translateScene.z << endl;
}
void goForward()
{
	translateScene.z += 0.25;
	cout << "   " << translateScene.z << endl;
}
// TEMA C12 :...........EXPLICATI PREZENTA FUNCTIEI KEY_CALLBACK, 
//UNDE SE APELEAZA, CE PARAMETRII MODIFICA SI CAND

void key_callback(GLFWwindow* window, int key, int scancode, int action, int mods)
{   
	
	//if (glfwGetKey(window, GLFW_KEY_A) == GLFW_PRESS)
	//if (key == GLFW_KEY_A && action == GLFW_PRESS)
	if (key == GLFW_KEY_A && action == GLFW_REPEAT)
		goLeft();
	if (glfwGetKey(window, GLFW_KEY_D) == GLFW_PRESS)
		goRight();
	if (glfwGetKey(window, GLFW_KEY_W) == GLFW_PRESS)
		goUp();
	if (glfwGetKey(window, GLFW_KEY_S) == GLFW_PRESS)
		goDown();
	if (glfwGetKey(window, GLFW_KEY_UP) == GLFW_PRESS)
		goBack();
	if (glfwGetKey(window, GLFW_KEY_DOWN) == GLFW_PRESS)
		goForward();

}
// TEMA C12 :...........EXPLICATI PREZENTA FUNCTIEI SCROOL_CALLBACK, 
//UNDE SE APELEAZA, CE PARAMETRII MODIFICA SI CAND
void scroll_callback(GLFWwindow* window, double xoffset, double yoffset)
{
	if (yoffset > 0) goUp();
	else goDown();
		/*if (xoffset > 0) goLeft();
	else goRight();*/

		
}
// TEMA C12 :...........EXPLICATI PREZENTA FUNCTIEI CURSOR_POSITION_CALLBACK, 
//UNDE SE APELEAZA, CE PARAMETRII MODIFICA SI CAND
void cursor_position_callback(GLFWwindow* window, double xpos, double ypos)
{
	std::cout << "cursor moved:   " << xpos << " | " << ypos << std::endl;
	if (xpos > xpos_)
	{
		
		if (glfwGetMouseButton(window, GLFW_MOUSE_BUTTON_LEFT) == GLFW_PRESS)
		{
			angle = angle - 0.01;
		}
		if (glfwGetMouseButton(window, GLFW_MOUSE_BUTTON_LEFT) == GLFW_RELEASE)
		{
			return;
		}
	}
	else 
		if (glfwGetMouseButton(window, GLFW_MOUSE_BUTTON_LEFT) == GLFW_PRESS)
		{
			angle = angle + 0.01;
		}
	if (glfwGetMouseButton(window, GLFW_MOUSE_BUTTON_LEFT) == GLFW_RELEASE)
	{
		return;
	}
	//comment and see the result	
	xpos_ = xpos;
	
}
int main()
{
	

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

	GLfloat vertexArrayPlan[] = {
								 -10.f, -10.f, -10.f,  1.0f, 1.0f,
								  10.f, -10.f, -10.f,  0.0f, 1.0f,
								  10.f, -10.f,  10.f,  0.0f, 0.0f,
								  10.f, -10.f,  10.f,  0.0f, 0.0f,
								 -10.f, -10.f,  10.f,  1.0f, 0.0f,
								 -10.f, -10.f, -10.f,  1.0f, 1.0f
	};

//	GLuint VBO, VAO, EBO, VBO1, VAO1;
	GLuint arrayVBO[2], arrayVAO[2];

	int screenWidth, screenHeight;

	context();
	GLFWwindow *window = glfwCreateWindow(WIDTH, HEIGHT, "OpenGL - perspective", nullptr, nullptr);
	glfwGetFramebufferSize(window, &screenWidth, &screenHeight);
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

	Shader userShader("resource/c12/plan/vertex.sh", "resource/c12/plan/fragment.sh");
	glGenVertexArrays(2,arrayVAO);
	glGenBuffers(2, arrayVBO);
	//glGenBuffers(1, &EBO);
	glBindVertexArray(arrayVAO[0]);
	glBindBuffer(GL_ARRAY_BUFFER, arrayVBO[0]);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertexArray), vertexArray, GL_STATIC_DRAW);
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 5 * sizeof(GLfloat), (GLvoid *)0);
	glEnableVertexAttribArray(0);
	glVertexAttribPointer(1, 2, GL_FLOAT, GL_FALSE, 5 * sizeof(GLfloat), (GLvoid *)(3 * sizeof(GLfloat)));
	glEnableVertexAttribArray(1);
	glBindVertexArray(0);

	GLuint texture0, texture1;
	int width, height;

	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);// GL_LINEAR);
	GLuint texture[7];
	array<unsigned char *, 7> imageInfo;

	/*array<const char *, 7> filePath = {
	"resource/images/red_.jpg",//back
	"resource/images/green_.jpg",//front
	"resource/images/blue_.jpg",//right
	"resource/images/yellow_.jpg",//left
	"resource/images/pink_.jpg",//up
	"resource/images/grey_.jpg",//down
	"resource/images/bluemarbleslabs-colormap.png"//marble

	};*/
	array<const char *, 7> filePath = {
	"resource/images/example9.jpg",//back
	"resource/images/example9.jpg",//front
	"resource/images/example9.jpg",//right
	"resource/images/example9.jpg",//left
	"resource/images/example9.jpg",//up
	"resource/images/gexample9.jpg",//down
	"resource/images/example14.jpg"//down

	};

	for (int i = 0; i < 7; i++)
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
	projectionMatrix = glm::perspective(glm::radians(45.0f), (GLfloat)screenWidth / (GLfloat)screenHeight, 0.1f, 100.0f);
	//generating the position for all the cubes
	array<glm::vec3, 6> position;
	for (int k = 0; k < 6; k++)
	{
		//position[k]= glm::sphericalRand(5.0f);
	   //position[k] = glm::vec3(glm::diskRand(5.0f),0.);
		position[k] = glm::ballRand(8.0f);
	}
	// TEMA C12 :...........EXPLICATI ROLUL URMATOARELOR TREI FUNCTII
	glfwSetKeyCallback(window, key_callback);
	glfwSetScrollCallback(window, scroll_callback);
	glfwSetCursorPosCallback(window, cursor_position_callback);
	
	while (!glfwWindowShouldClose(window))
	{
		glfwPollEvents();
		glClearColor(0.6f, 0.5f, 0.4f, 1.f);//0.6/0.5/0.4
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
		userShader.Use();
		//userShader2.Use();
		glm::mat4 modelMatrix = glm::mat4(1.0f), viewMatrixIni = glm::mat4(1.0f), viewMatrixRef = glm::mat4(1.0f);
		modelMatrix = glm::rotate(modelMatrix, 0.f, glm::vec3(1.f, 0.f, 1.f));
		glm::vec3 center = glm::vec3(0., 0., 0.);
		//glm::vec3 center = glm::vec3(10., 10., 10.);
		//viewMatrix = glm::lookAt(glm::vec3(sin(glfwGetTime()) *distance, 0, cos(glfwGetTime()) * distance), center, glm::vec3(0, 1, 0));
	
	  	viewMatrixIni = glm::lookAt(glm::vec3(sin(angle)*30, 0., cos(angle)*30), center, glm::vec3(0, 1, 0));
		projectionMatrix = glm::perspective(angle, (GLfloat)screenWidth / (GLfloat)screenHeight, 0.1f, 100.0f);
		
		GLint transformLocation;
		transformLocation = glGetUniformLocation(userShader.Program, "modelMatrix");
		glUniformMatrix4fv(transformLocation, 1, GL_FALSE, glm::value_ptr(modelMatrix));
		transformLocation = glGetUniformLocation(userShader.Program, "viewMatrix");
		glUniformMatrix4fv(transformLocation, 1, GL_FALSE, glm::value_ptr(viewMatrixIni));
		transformLocation = glGetUniformLocation(userShader.Program, "projectionMatrix");
		glUniformMatrix4fv(transformLocation, 1, GL_FALSE, glm::value_ptr(projectionMatrix));
	

		glBindVertexArray(arrayVAO[0]);
		glActiveTexture(GL_TEXTURE0);
	    
		
		glfwPollEvents();
		for (int p = 0; p < 6; p++)//6 cuburi
		{
			
			if (p == 0) viewMatrixRef = glm::translate(viewMatrixIni, glm::vec3(0.f, 0.f, -4.f)+translateScene);
			//else
			//{
				viewMatrixRef = glm::translate(viewMatrixIni, position[p]+translateScene);
				//viewMatrix = glm::translate(viewMatrix, glm::vec3(0.f, 0.f, 2.f));
				transformLocation = glGetUniformLocation(userShader.Program, "viewMatrix");
				glUniformMatrix4fv(transformLocation, 1, GL_FALSE, glm::value_ptr(viewMatrixRef));
		//	}
		
			for (int i = 0; i < 6; i++) {

				glBindTexture(GL_TEXTURE_2D, texture[i]);
				glUniform1i(glGetUniformLocation(userShader.Program, "userTextureOut"), 0);
				glDrawArrays(GL_TRIANGLES, i *6, 6);			
			}
			glBindTexture(GL_TEXTURE_2D, 0);
			viewMatrixRef = viewMatrixIni;
			if (p == 5)
			{
				glBindVertexArray(0);
				glBindVertexArray(arrayVAO[1]);
				glBindBuffer(GL_ARRAY_BUFFER, arrayVBO[1]);
				glBufferData(GL_ARRAY_BUFFER, sizeof(vertexArrayPlan), vertexArrayPlan, GL_STATIC_DRAW);
				glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 5 * sizeof(GLfloat), (GLvoid*)0);
				glEnableVertexAttribArray(0);
				glVertexAttribPointer(1, 2, GL_FLOAT, GL_FALSE, 5 * sizeof(GLfloat), (GLvoid*)(3 * sizeof(GLfloat)));
				glEnableVertexAttribArray(1);
			
				//translatia de mai jos actualizata cvu translateScene determina modificarea planseului
				//deodata cu cele cateva cuburi
				//viewMatrixRef = glm::translate(viewMatrixIni, glm::vec3(0.f, 0.f, -4.f) + translateScene);
				//eliminarea lui translateScene face ca modificarea sa afecteze doar cuburile nu si planseul
				viewMatrixRef = glm::translate(viewMatrixIni, glm::vec3(0.f, 0.f, -4.f));
				//viewMatrix = glm::translate(viewMatrix, glm::vec3(0.f, 0.f, 2.f));
				transformLocation = glGetUniformLocation(userShader.Program, "viewMatrix");
				glUniformMatrix4fv(transformLocation, 1, GL_FALSE, glm::value_ptr(viewMatrixRef));
				for (int i = 0; i < 6; i++) {

					glBindTexture(GL_TEXTURE_2D, texture[6]);
					glUniform1i(glGetUniformLocation(userShader.Program, "userTextureOut"), 0);
					glDrawArrays(GL_TRIANGLES, i * 6, 6);
				}
			}
		}
	

		//primitiva grafica, numarul de elemente de desenat, tipul indicilor, offset-ul primului indice considerat
		//glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, 0);
		
		glBindVertexArray(0);
		glBindTexture(GL_TEXTURE_2D, 0);
		glfwSwapBuffers(window);
	}
	glViewport(0, 0, screenWidth, screenHeight);

	glDeleteVertexArrays(2, arrayVAO);
	glDeleteBuffers(2, arrayVBO);
	//glDeleteBuffers(1, &EBO);
	glfwTerminate();
	return EXIT_SUCCESS;
}