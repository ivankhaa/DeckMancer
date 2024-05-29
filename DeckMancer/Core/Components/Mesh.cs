using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.Core
{
    [Serializable]
    public class Mesh : Component
    {
        public Vector3[] Vertices { get; set; }
        public Vector2[] UVs { get; set; }
        public uint[] Indices { get; set; }
        public Material Material { get; private set; }

        public Mesh()
        {
            Vertices = new Vector3[]
            {
                new Vector3(-1.0f, 1.0f, 0f),
                new Vector3(-1.0f, -1.0f, 0f),
                new Vector3(1.0f, -1.0f, 0f),
                new Vector3(1.0f, 1.0f, 0f)
            };

            UVs = new Vector2[]
            {
                new Vector2(0.0f, 0.0f),
                new Vector2(0.0f, 1.0f),
                new Vector2(1.0f, 1.0f),
                new Vector2(1.0f, 0.0f)
            };

            Indices = new uint[]
            {
                0, 1, 2,
                0, 2, 3
            };
            Material = new Material();
        }
    }
}
