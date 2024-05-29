using DeckMancer.Core;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.API.OpenGL
{
    public class TextureBuffer
    {
        private int _textureBufferId;
        private Texture _texture;
        public TextureBuffer()
        {
            _textureBufferId = GL.GenTexture();
        }
        public int ArrayCount { get; private set; }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, _textureBufferId);
        }
        public void SetTexture(Texture texture) 
        {
            _texture = texture;
            ArrayCount = _texture.Data.Length;

            Bind();
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, _texture.Width, _texture.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, _texture.Data);

            var warp = TextureWrapMode.Repeat;
            var filterMin = TextureMinFilter.Nearest;
            var filterMag = TextureMinFilter.Nearest;
            float AnisoLvl = (float)_texture.AnisotropicMode_;

            switch (_texture.WarpMode_) 
            {
                case WarpMode.Clamp:
                    warp = TextureWrapMode.ClampToEdge;
                    break;
                case WarpMode.Mirror:
                    warp = TextureWrapMode.MirroredRepeat;
                    break;
            }
            switch (_texture.FilterMode_)
            {
                case FilterMode.Bilinear:
                    GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
                    filterMin = TextureMinFilter.LinearMipmapNearest;
                    filterMag = TextureMinFilter.Linear;
                    break;
                case FilterMode.Trilinear:
                    GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
                    filterMin = TextureMinFilter.LinearMipmapLinear;
                    filterMag = TextureMinFilter.Linear;
                    break;
            }
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)warp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)warp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)filterMin);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)filterMag);
            
            if(AnisoLvl > 0.0f)
                GL.TexParameter(TextureTarget.Texture2D, (TextureParameterName)All.TextureMaxAnisotropy, AnisoLvl);
        }
    }
}
