using OpenTK.Graphics.OpenGL4;

namespace DeckMancer.API.OpenGL
{
    public enum VertexAttributeType 
    {
        Vertices,
        Color,
        Normal,
        Texture,
        Indices
    }
    public struct VertexAttribute
    {
        public VertexAttributeType Name;
        public byte Id;
        public int Size;
        public VertexAttribPointerType Type;
        public bool Normalized;
        public int Stride;
        public int Offset;


        public VertexAttribute(VertexAttributeType name, byte id, int size, VertexAttribPointerType type, bool normalized, int stride, int offset)
        {
            Name = name;
            Id = id;
            Size = size;
            Type = type;
            Normalized = normalized;
            Stride = stride;
            Offset = offset;
        }
    }
}
