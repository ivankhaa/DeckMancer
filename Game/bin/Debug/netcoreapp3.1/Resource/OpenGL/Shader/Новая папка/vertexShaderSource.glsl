#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec2 aTexCoord;
layout(location = 2) in vec3 aNormal;
               
out vec2 TexCoord;
out vec3 Normal;
out float FogDistance;
               
uniform mat4 ModelMatrix; 
uniform mat4 ViewMatrix;  
uniform mat4 ProjectionMatrix; 
               
void main()
{
    mat4 ModelViewProjectionMatrix = ProjectionMatrix * ViewMatrix * ModelMatrix;
    gl_Position = ModelViewProjectionMatrix * vec4(aPosition, 1.0);
                   
    TexCoord = aTexCoord;
    Normal = mat3(transpose(inverse(ModelMatrix))) * aNormal;;
    FogDistance = length((ViewMatrix * ModelMatrix * vec4(aPosition, 1.0)).xyz);
}