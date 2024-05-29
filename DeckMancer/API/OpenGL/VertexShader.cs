using DeckMancer.Core;

namespace DeckMancer.API.OpenGL
{
    public class VertexShader
    {
        private static string Standard => ReadFile.ReadAllText(@"Resource\OpenGL\Shader\VertexShaderStandard.glsl");
        public string Shader { get; private set; }
        public VertexShader(string shader)
        {
            Shader = shader;
        }
        public VertexShader()
        {
            Shader = Standard;
        }
    }
}
