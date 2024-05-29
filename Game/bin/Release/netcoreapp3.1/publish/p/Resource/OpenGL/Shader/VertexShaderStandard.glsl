#version 330 core

layout(location = 0) in vec3 Position;
layout(location = 1) in vec2 TexCoord;

out vec2 _TexCoord;

uniform mat4 ModelMatrix; 
uniform mat4 ViewMatrix;  
uniform mat4 ProjectionMatrix; 

void main()
{
    mat4 ModelViewProjectionMatrix = ProjectionMatrix * ViewMatrix * ModelMatrix;
    gl_Position = ModelViewProjectionMatrix * vec4(Position, 1.0);
    _TexCoord = TexCoord;
}