using DeckMancer.Core;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;

namespace DeckMancer.API.OpenGL
{
    public class VertexLayout
    {
        public static readonly VertexLayout Default;
        public static readonly VertexLayout Standard;
        public List<VertexAttribute> Attributes { get; private set; }

        static VertexLayout()
        {
            Default = new VertexLayout(
                new List<VertexAttribute>
                {
                    new VertexAttribute(VertexAttributeType.Vertices, 0, 3, VertexAttribPointerType.Float, false, 12, 0),
                });
            Standard = new VertexLayout(
                new List<VertexAttribute>
                {
                    new VertexAttribute(VertexAttributeType.Vertices, 0, 3, VertexAttribPointerType.Float, false, 12, 0),
                    new VertexAttribute(VertexAttributeType.Texture, 1, 2, VertexAttribPointerType.Float, false, 8, 0),
                });
        }
        private VertexLayout(List<VertexAttribute> attributes)
        {
            Attributes = attributes;
        }
    }
}
