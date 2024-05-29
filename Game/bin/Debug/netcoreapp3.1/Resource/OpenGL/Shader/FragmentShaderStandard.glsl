#version 330 core

in vec2 _TexCoord;

layout (location = 0) out vec4 FragColor;

uniform vec4 Color;
uniform sampler2D Texture0;

void main()
{
	vec4 texColor = texture(Texture0, _TexCoord);

	vec4 finalColor = texColor * Color;
	FragColor = finalColor;
}