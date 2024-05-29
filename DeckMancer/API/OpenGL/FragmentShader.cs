using DeckMancer.Core;

namespace DeckMancer.API.OpenGL
{
    public class FragmentShader
    {
        private static string Standard => ReadFile.ReadAllText(@"Resource\OpenGL\Shader\FragmentShaderStandard.glsl");
        public string Shader { get; private set; }
        public FragmentShader(string shader) 
        {
            Shader = shader;
        }
        public FragmentShader()
        {
            Shader = Standard;
        }
    }
}
