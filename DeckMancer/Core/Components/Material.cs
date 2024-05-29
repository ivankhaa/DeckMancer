using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.Core
{
    [Serializable]
    public class Material
    {
        [field: NonSerialized]
        public event EventHandler MaterialChanged;

        private Color4 _color;
        public Color4 Color
        {
            get { return _color; }
            private set
            {
                if (_color != value)
                {
                    _color = value;
                    OnMaterialChanged("Color");
                }
            }
        }

        private Texture _texture;
        public Texture Texture
        {
            get { return _texture; }
            private set
            {
                if (_texture != value)
                {
                    _texture = value;
                    OnMaterialChanged("Texture");
                }
            }
        }
        public Material()
        {
            Color = Color4.White;
            Texture = new Texture();
        }
        public Material(Color4 color4, Texture texture)
        {
            Color = color4;
            Texture = texture;
        }
        public void SetColor(Color4 color) 
        {
            Color = color;
        }
        public void SetTexture(Texture texture) 
        {
            Texture = texture;
        }
        protected virtual void OnMaterialChanged(string propertyName)
        {
            MaterialChanged?.Invoke(propertyName, EventArgs.Empty);
        }
    }
}
