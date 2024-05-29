#version 330 core

in vec2 TexCoord;
in vec3 Normal;
in float FogDistance;

out vec4 FragColor;

uniform sampler2D texture1;

struct DirectionalLight
{
    vec3 Color;
    float AmbientIntensity;
    float DiffuseIntensity;
    vec3 Direction;
};

struct Material
{
    vec3 AmbientColor;
    vec3 DiffuseColor;
};

uniform DirectionalLight gDirectionalLight;
uniform Material gMaterial;
uniform float fogDensity = 0.02;


void main()
{
    vec4 AmbientColor = vec4(gDirectionalLight.Color, 1.0f) *
                        gDirectionalLight.AmbientIntensity *
                        vec4(gMaterial.AmbientColor, 1.0f);

    float DiffuseFactor = dot(normalize(Normal), -gDirectionalLight.Direction);

    vec4 DiffuseColor = vec4(0, 0, 0, 0);

    if (DiffuseFactor > 0) {
        DiffuseColor = vec4(gDirectionalLight.Color, 1.0f) *
                        gDirectionalLight.DiffuseIntensity *
                        vec4(gMaterial.DiffuseColor, 1.0f) *
                        DiffuseFactor;
    }

    vec3 ReflectDir = reflect(-gDirectionalLight.Direction, normalize(Normal));
    vec4 texColor = texture(texture1, TexCoord);
    vec3 finalColor = (AmbientColor.rgb + DiffuseColor.rgb) * vec3(texColor);
    float fogFactor = exp(- pow(fogDensity*FogDistance,2));
    fogFactor = clamp(fogFactor,0,1);
    vec3 fogColor = vec3(0.7, 0.7, 0.7);
    finalColor = mix(finalColor, fogColor, 1.0 - fogFactor);
    FragColor = vec4(finalColor, texColor.a);
}