using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.Core
{
    [Serializable]
    public class Camera : Component
    {
        public float SIZE { get { return _size; } set { _size = value; } }
        public float NEAR { get { return _near; } set { _near = value; } }
        public float FAR { get { return _far; } set { _far = value; } }

        private float _width = 1600f;
        private float _height = 900f;
        private float _size = 10;
        private float _near = -100f;
        private float _far = 100f;
        public float Width { get { return _width; } set { if (_width != value) { _width = value; } } }
        public float Height { get { return _height; } set { if (_height != value) { _height = value; } } }
        public float AspectRatio => Width / Height;

        public Matrix4 Projection { get { return Matrix4.CreateOrthographic(SIZE * 1.0f * AspectRatio, SIZE * 1.0f, NEAR, FAR); } }

        public Camera()
        {

        }
    }
}
