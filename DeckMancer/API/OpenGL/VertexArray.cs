using OpenTK.Graphics.OpenGL4;

namespace DeckMancer.API.OpenGL
{
    public class VertexArray
    {
        private int _vertexArrayId;
        public VertexArray()
        {
            _vertexArrayId = GL.GenVertexArray();
        }
        public void Bind()
        {
            GL.BindVertexArray(_vertexArrayId);
        }
    }
}
