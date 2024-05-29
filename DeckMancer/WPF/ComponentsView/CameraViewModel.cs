using DeckMancer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.WPF
{
    public class CameraViewModel:ComponentViewModel
    {
        private Camera _camera;
        public float SIZE { get { return _camera.SIZE; } set { _camera.SIZE = value; } }
        public float NEAR { get { return _camera.NEAR; } set { _camera.NEAR = value; } }
        public float FAR { get { return _camera.FAR; } set { _camera.FAR = value; } }
        public float Width { get { return _camera.Width; } set { _camera.Width = value; } }
        public float Height { get { return _camera.Height; } set { _camera.Height = value; } }
        public CameraViewModel(Camera camera)
        {
            _camera = camera;
        }
    }
}
