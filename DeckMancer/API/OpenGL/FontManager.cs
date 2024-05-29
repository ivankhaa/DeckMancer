using FreeTypeSharp;
using FreeTypeSharp.Native;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.API.OpenGL
{
    public class FontManager
    {
        public Dictionary<char, Character> characters = new Dictionary<char, Character>();

        private FreeTypeLibrary FTLibrary = new FreeTypeLibrary();
        private FreeTypeFaceFacade faceFacade;
        private IntPtr nativeFTLibrary;
        private IntPtr nativeFace;

        public FontManager(string fontPath, uint size)
        {
            InitializeFreeType(fontPath, size);
        }

        private void InitializeFreeType(string fontPath, uint size)
        {
            var errorInitFreeType = FT.FT_Init_FreeType(out nativeFTLibrary);
            var errorNewFace = FT.FT_New_Face(nativeFTLibrary, fontPath, 0, out nativeFace);
            var errorSetPixelSize = FT.FT_Set_Pixel_Sizes(nativeFace, 0, size);
        }

        public void LoadGlyphs(string text)
        {
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);

            foreach (char c in text.ToCharArray())
            {
                if (characters.ContainsKey(c))
                    continue;

                var glyphIndex = FT.FT_Get_Char_Index(nativeFace, c);

                if (glyphIndex == 0 || FT.FT_Load_Char(nativeFace, c, FT.FT_LOAD_RENDER) != 0)
                    continue;
                
                faceFacade = new FreeTypeFaceFacade(FTLibrary, nativeFace);
                var texture = LoadGlyphTexture();

                var glyph = new Character
                {
                    TextureID = texture,
                    Size = new Vector2i((int)faceFacade.GlyphBitmap.width, (int)faceFacade.GlyphBitmap.rows),
                    Bearing = new Vector2i(faceFacade.GlyphBitmapLeft, faceFacade.GlyphBitmapTop),
                    Advance = (uint)faceFacade.GlyphMetricHorizontalAdvance
                };

                characters.Add(c, glyph);
            }
        }

        private uint LoadGlyphTexture()
        {
            uint texture;
            GL.GenTextures(1, out texture);
            GL.BindTexture(TextureTarget.Texture2D, texture);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.R8, (int)faceFacade.GlyphBitmap.width, (int)faceFacade.GlyphBitmap.rows, 0, PixelFormat.Red, PixelType.UnsignedByte, faceFacade.GlyphBitmap.buffer);

            // Texture parameters
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            return texture;
        }
    }

}
