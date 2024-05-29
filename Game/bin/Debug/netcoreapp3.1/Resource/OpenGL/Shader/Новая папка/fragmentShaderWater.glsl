#version 330 core

in float FogDistance;
in vec2 TexCoord;
in vec3 FragPos; 
in vec3 CameraPos; 

out vec4 FragColor;

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

uniform sampler2D reflectionTexture;

uniform DirectionalLight gDirectionalLight;
uniform Material gMaterial;
uniform float fogDensity = 0.02;


const float specularIntensity = 1; 

void main()
{
    vec4 AmbientColor = vec4(gDirectionalLight.Color, 1.0f) *
                        gDirectionalLight.AmbientIntensity *
                        vec4(gMaterial.AmbientColor, 1.0f);

    float DiffuseFactor = dot(normalize(vec3(0.0, 1.0, 0.0)), -gDirectionalLight.Direction); 

    vec4 DiffuseColor = vec4(0, 0, 0, 0);

    if (DiffuseFactor > 0) {
        DiffuseColor = vec4(gDirectionalLight.Color, 1.0f) *
                        gDirectionalLight.DiffuseIntensity *
                        vec4(gMaterial.DiffuseColor, 1.0f) *
                        DiffuseFactor;
    }

    vec3 viewDirection = normalize(CameraPos - FragPos); 
    vec3 reflectDir = reflect(-gDirectionalLight.Direction, normalize(vec3(0.0, 1.0, 0.0))); 
    float spec = pow(max(dot(viewDirection, reflectDir), 0.0), 16.0); 

    vec3 specularColor = specularIntensity * spec * gDirectionalLight.Color.rgb;
    vec4 texColor = texture(reflectionTexture, TexCoord);
    vec3 finalColor = (AmbientColor.rgb + DiffuseColor.rgb + specularColor) * vec3(texColor) * vec3(0.9,0.7,0.5);;

    float fogFactor = exp(-pow(fogDensity * FogDistance, 2));
    fogFactor = clamp(fogFactor, 0, 1);
    vec3 fogColor = vec3(0.7, 0.7, 0.7);

    finalColor = mix(finalColor, fogColor, 1.0 - fogFactor);

    FragColor = vec4(finalColor, 0.9);
}
