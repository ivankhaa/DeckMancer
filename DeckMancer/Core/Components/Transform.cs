using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.Core
{
    [Serializable]
    public class Transform : Component
    {
        private Vector3 _position;
        private Vector3 _rotation;
        private Vector3 _sale;
        public Vector3 Position { get { return _position; } set { _position = value; } }
        public Vector3 Rotation { get { return _rotation; } set { _rotation = value; } }
        public Vector3 Scale { get { return _sale; } set { _sale = value; } }

        public Transform(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }
        public Transform()
        {
            Position = new Vector3(0.0f, 0.0f, 0.0f);
            Rotation = new Vector3(0.0f, 0.0f, 0.0f);
            Scale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
}
