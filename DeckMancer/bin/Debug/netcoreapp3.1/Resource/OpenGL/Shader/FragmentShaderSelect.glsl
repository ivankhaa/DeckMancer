#version 330 core

layout (location = 0) out uvec4 FragColor;

uniform uint ID;

void main()
{
	FragColor = uvec4(ID, 0.0, 0.0, 0xff);
}