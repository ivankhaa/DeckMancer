using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.Core
{
    [Serializable]
    public class MainCamera : GameObject
    {
        public Matrix4 ViewModel { 
            get
            {
                var RotateXYZ = Matrix4.CreateRotationX(AngleToRadians(CameraRotation.X)) * Matrix4.CreateRotationY(AngleToRadians(CameraRotation.Y)) * Matrix4.CreateRotationZ(AngleToRadians(CameraRotation.Z)); 
                return Matrix4.LookAt(CameraPosition, CameraPosition + CameraFront, CameraUp) * RotateXYZ; 
            }
        }
        
        private Vector3 CameraFront = new Vector3(0.0f, 0.0f, -1.0f);
        private Vector3 CameraUp = new Vector3(0.0f, 1.0f, 0.0f);
        private Vector3 CameraPosition => base.Transform.Position;
        private Vector3 CameraRotation => base.Transform.Rotation;
        public MainCamera(string name):base(name)
        {
            AddComponent<Camera>();
        }
        private float AngleToRadians(float angle)
        {
            return (angle * MathHelper.Pi) / 180.0f;

        }
    }
}
