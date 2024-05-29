using DeckMancer.Core;
using DeckMancer.Engine;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.WPF
{
    public class TransformViewModel : ComponentViewModel
    {
        private Transform _transform;

        public float PositionX
        {
            get { return _transform.Position.X; }
            set { _transform.Position = new Vector3(value, _transform.Position.Y, _transform.Position.Z); }
        }
        public float PositionY
        {
            get { return _transform.Position.Y; }
            set { _transform.Position = new Vector3(_transform.Position.X, value, _transform.Position.Z); }
        }
        public float PositionZ
        {
            get { return _transform.Position.Z; }
            set { _transform.Position = new Vector3(_transform.Position.X, _transform.Position.Y, value); }
        }
        public float RotationX
        {
            get { return _transform.Rotation.X; }
            set { _transform.Rotation = new Vector3(value, _transform.Rotation.Y, _transform.Rotation.Z); }
        }
        public float RotationY
        {
            get { return _transform.Rotation.Y; }
            set { _transform.Rotation = new Vector3(_transform.Rotation.X, value, _transform.Rotation.Z); }
        }
        public float RotationZ
        {
            get { return _transform.Rotation.Z; }
            set { _transform.Rotation = new Vector3(_transform.Rotation.X, _transform.Rotation.Y, value); }
        }
        public float ScaleX
        {
            get { return _transform.Scale.X; }
            set { _transform.Scale = new Vector3(value, _transform.Scale.Y, _transform.Scale.Z); }
        }
        public float ScaleY
        {
            get { return _transform.Scale.Y; }
            set { _transform.Scale = new Vector3(_transform.Scale.X, value, _transform.Scale.Z); }
        }
        public float ScaleZ
        {
            get { return _transform.Scale.Z; }
            set { _transform.Scale = new Vector3(_transform.Scale.X, _transform.Scale.Y, value); }
        }
        public void Update(object sender, EventArgs e) 
        {
            base.OnPropertyChanged(nameof(PositionX));
            base.OnPropertyChanged(nameof(PositionY));
            base.OnPropertyChanged(nameof(PositionZ));
        }
        public TransformViewModel(Transform transform)
        {
            _transform = transform;
            RenderEditor.TransformGameObjectChange += Update;
        }
    }
}
