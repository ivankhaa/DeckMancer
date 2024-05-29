#version 330 core

const float waveAmplitude = 0.5;
const float waveFrequency = 1.0;
const float waveOffset = 0.2;

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec2 aTexCoord;

out float FogDistance;
out vec3 FragPos; 
out vec3 CameraPos; 
out vec2 TexCoord;

uniform mat4 ModelMatrix; 
uniform mat4 ViewMatrix;  
uniform mat4 ProjectionMatrix; 
uniform vec3 CameraPosition; 
uniform float time; 

void main()
{
    mat4 ModelViewProjectionMatrix = ProjectionMatrix * ViewMatrix * ModelMatrix;
                   

    vec3 animatedPosition = aPosition;
    animatedPosition.y += waveAmplitude * sin(waveFrequency * time + waveOffset * aPosition.x)* cos(waveFrequency * time + waveOffset * aPosition.x);
    
    FogDistance = length((ViewMatrix * ModelMatrix * vec4(animatedPosition, 1.0)).xyz);
    gl_Position = ModelViewProjectionMatrix * vec4(animatedPosition, 1.0);
    FragPos = vec3(ModelMatrix * vec4(animatedPosition, 1.0));
    CameraPos = CameraPosition;
    TexCoord = aTexCoord;
}
