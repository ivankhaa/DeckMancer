using ColorPicker.Models;
using DeckMancer.Core;
using OpenTK.Mathematics;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DeckMancer.WPF
{
    public class MeshViewModel : ComponentViewModel
    {
        private Mesh _mesh;
        public WarpMode WarpMode
        {
            get { return _mesh.Material.Texture.WarpMode_; }
            set
            {
                if (_mesh.Material.Texture.WarpMode_ != value)
                {
                    _mesh.Material.SetTexture(new Texture(_mesh.Material.Texture.Width, _mesh.Material.Texture.Height, _mesh.Material.Texture.Data, value, _mesh.Material.Texture.FilterMode_, _mesh.Material.Texture.AnisotropicMode_));
                    OnPropertyChanged(nameof(WarpMode)); 
                }
            }
        }
        public FilterMode FilterMode
        {
            get { return _mesh.Material.Texture.FilterMode_; }
            set
            {
                if (_mesh.Material.Texture.FilterMode_ != value)
                {
                    _mesh.Material.SetTexture(new Texture(_mesh.Material.Texture.Width, _mesh.Material.Texture.Height, _mesh.Material.Texture.Data, _mesh.Material.Texture.WarpMode_, value, _mesh.Material.Texture.AnisotropicMode_));
                    OnPropertyChanged(nameof(FilterMode));
                }
            }
        }
        public int AnisotropicMode
        {
            get 
            {
                var _anisotropicMode = (int)_mesh.Material.Texture.AnisotropicMode_;
                return _anisotropicMode; 
            }
            set
            {
                _mesh.Material.SetTexture(new Texture(_mesh.Material.Texture.Width, _mesh.Material.Texture.Height, _mesh.Material.Texture.Data, _mesh.Material.Texture.WarpMode_, _mesh.Material.Texture.FilterMode_, (AnisotropicMode)value));
                OnPropertyChanged(nameof(AnisotropicMode));
            }
        }
        public string Vertices { get { return "Vertices: " + _mesh.Vertices.Length.ToString()+$", float32";} }
        public string UVs { get { return "UVs: " + _mesh.UVs.Length.ToString() + $", float32"; } }
        public string Indices { get { return "Indices: " + _mesh.Indices.Length.ToString() + $", Uint32"; } }
        public BitmapSource Texture 
        {
            get
            {
                var b = FromArray(_mesh.Material.Texture.Data, _mesh.Material.Texture.Width, _mesh.Material.Texture.Height);
                
                return b; 
            }
        }
        public static BitmapSource FromArray(byte[] data, int w, int h)
        {
            PixelFormat format = PixelFormats.Bgra32;


            WriteableBitmap wbm = new WriteableBitmap(w, h, 96, 96, format, null);
            wbm.WritePixels(new Int32Rect(0, 0, w, h), data, 4 * w, 0);

            return wbm;
        }
        public void SetTexture(SKBitmap bitmap) 
        {
  
            _mesh.Material.SetTexture(new Texture(bitmap.Width, bitmap.Height, bitmap.Bytes,
                _mesh.Material.Texture.WarpMode_, _mesh.Material.Texture.FilterMode_, _mesh.Material.Texture.AnisotropicMode_));
            OnPropertyChanged(nameof(Texture));

        }
        public ColorState Color 
        { 
            get 
            { 
                return Color4ToColorState(_mesh.Material.Color); 
            } 
            set 
            {
                _mesh.Material.SetColor(ColorStateToColor4(value));
                OnPropertyChanged(nameof(Color));
            } 
        }

        private ColorState Color4ToColorState(Color4 color4) 
        {
            var cs = new ColorState();
            cs.SetARGB(color4.A, color4.R, color4.G, color4.B);

            return cs;
        }
        private Color4 ColorStateToColor4(ColorState colorState)
        {
            float R = (float)colorState.RGB_R;
            float G = (float)colorState.RGB_G;
            float B = (float)colorState.RGB_B;
            float A = (float)colorState.A;

            return new Color4(R, G, B, A);
        }

        public MeshViewModel(Mesh mesh) 
        {
            _mesh = mesh;
        }

    }
}
