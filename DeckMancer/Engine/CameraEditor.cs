using OpenTK.Mathematics;
using System;

namespace DeckMancer.Engine
{
    public class CameraEditor
    {
        public const float SIZE = 10f;
        private const float NEAR = -10f;
        private const float FAR = 10f;
        private float _scale = 1.0f;
        private float _width = 597f;
        private float _height = 335f;
        private Vector2 _position = new Vector2(0f, 0f);
        public float Width { get { return _width; } set { if (_width != value) { _width = value; CameraResize(); OnSizeChanged?.Invoke(this, EventArgs.Empty); } } }
        public float Height { get { return _height; } set { if (_height != value) { _height = value; CameraResize(); OnSizeChanged?.Invoke(this, EventArgs.Empty); } } }
        public float AspectRatio => Width / Height;
        public float Scale { get { return _scale; } set { if (_scale != value) { _scale = value; CameraScale(); OnScaleChanged?.Invoke(this, EventArgs.Empty); } } }
        public Vector2 Position { get { return _position; } set { if (_position.X != value.X || _position.Y != value.Y) { _position = value; CameraTranslation(); OnPositionChanged?.Invoke(this, EventArgs.Empty); } } }

        private ProjectionView _projectionView;

        private Vector3 CameraPosition = new Vector3(0.0f, 0.0f, 0.0f);
        private Vector3 CameraFront = new Vector3(0.0f, 0.0f, -1.0f);
        private Vector3 CameraUp = new Vector3(0.0f, 1.0f, 0.0f);

        public struct ProjectionView 
        {
            public Matrix4 ProjectionMatrix;
            public Matrix4 ViewMatrix;

            public ProjectionView(Matrix4 P, Matrix4 V) 
            {
                ProjectionMatrix = P;
                ViewMatrix = V;
            }
        }
        public ProjectionView Camera { get { return _projectionView; } }

        public event EventHandler OnScaleChanged;
        public event EventHandler OnSizeChanged;
        public event EventHandler OnPositionChanged;

        public CameraEditor()
        {
            var View = Matrix4.LookAt(CameraPosition, CameraPosition + CameraFront, CameraUp);
            _projectionView = new ProjectionView(UpdateOrthographic(), View);
        
        }

        private void CameraScale()
        {
            _projectionView.ProjectionMatrix = UpdateOrthographic();
            CameraTranslation();
        }


        private void CameraResize()
        {
            _projectionView.ProjectionMatrix = UpdateOrthographic();
        }
        private Matrix4 UpdateOrthographic() 
        {
            return Matrix4.CreateOrthographic(SIZE * (1.0f / Scale)*AspectRatio, (SIZE * (1.0f / Scale)), NEAR, FAR);
        }

        private void CameraTranslation() 
        {

            var PositionNormalize = new Vector3(Position.X / Width * (SIZE * (1.0f / Scale)) * AspectRatio, Position.Y / Height * ((SIZE * (1.0f / Scale))), 0.0f);
            CameraPosition.X = PositionNormalize.X;
            CameraPosition.Y = PositionNormalize.Y;


            var View = Matrix4.LookAt(CameraPosition, CameraPosition + CameraFront, CameraUp);
            _projectionView.ViewMatrix = View;

        }

        public void OnTranslationMove(Vector2 currentMousePosition, Vector2 lastMousePosition)
        {
            Vector2 Delta = new Vector2(currentMousePosition.X - lastMousePosition.X, currentMousePosition.Y - lastMousePosition.Y);
            Position = new Vector2(Position.X - Delta.X, Position.Y + Delta.Y);
        }
    }
}
