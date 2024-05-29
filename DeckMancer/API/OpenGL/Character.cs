using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.API.OpenGL
{
    public class Character
    {
        public uint TextureID { get; set; }
        public Vector2i Size { get; set; }
        public Vector2i Bearing { get; set; }
        public uint Advance { get; set; }
    }

}
