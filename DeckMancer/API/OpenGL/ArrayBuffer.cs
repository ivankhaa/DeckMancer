using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.API.OpenGL
{
    public class ArrayBuffer
    {
        private int _arrayBufferId;
        private VertexLayout _vertexLayout;
        public VertexAttributeType AttributeType { get; private set; }

        public ArrayBuffer(VertexLayout vertexLayout, VertexAttributeType Type = VertexAttributeType.Vertices)
        {
            AttributeType = Type;
            _vertexLayout = vertexLayout;

            _arrayBufferId = GL.GenBuffer();
        }

        public int ArrayCount { get; private set; }

        public VertexLayout VertexLayout
        {
            get { return _vertexLayout; }
        }

        public void Bind()
        {
            if (AttributeType == VertexAttributeType.Indices)
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _arrayBufferId);
            else
                GL.BindBuffer(BufferTarget.ArrayBuffer, _arrayBufferId);
        }
        public void SetVertexAttribPointer(VertexAttribute attribute)
        {
                GL.VertexAttribPointer(
                    attribute.Id,
                    attribute.Size,
                    attribute.Type,
                    attribute.Normalized,
                    attribute.Stride,
                    attribute.Offset
                );
                GL.EnableVertexAttribArray(attribute.Id);
        }
        public bool SetArray<T>(T[] array) where T : struct
        {
            if (AttributeType == VertexAttributeType.Indices)
            {
                Bind();
                GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(uint) * array.Length, array, BufferUsageHint.StaticDraw);
                ArrayCount = array.Length;
                return true;
            }
            else
            {

                foreach (var attribute in _vertexLayout.Attributes)
                {
                    if (attribute.Name == AttributeType)
                    {
                        Bind();
                        GL.BufferData(BufferTarget.ArrayBuffer, attribute.Stride * array.Length, array, BufferUsageHint.StaticDraw);
                        SetVertexAttribPointer(attribute);
                        ArrayCount = array.Length;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
