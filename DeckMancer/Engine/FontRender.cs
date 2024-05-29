using DeckMancer.API.OpenGL;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeckMancer.Engine
{
    public class FontRender
    {
        public readonly FontManager FontManager_;
        public static readonly ShaderProgram SP_RenderText = new ShaderProgram(VertexShaderRenderText, FragmentShaderRenderText);
        private static string VertexShaderRenderText => ReadFile(System.IO.Path.GetFullPath(@"Resource\OpenGL\Shader\VertexShaderRenderText.glsl"));
        private static string FragmentShaderRenderText => ReadFile(System.IO.Path.GetFullPath(@"Resource\OpenGL\Shader\FragmentShaderRenderText.glsl"));
        private Dictionary<char, Character> Characters => FontManager_.characters;

        private Matrix4 orthoProjection = Matrix4.Identity;

        private uint VAO_RenderText, VBO_RenderText;

        public FontRender()
        {
            FontManager_ = new FontManager(System.IO.Path.GetFullPath("Resource\\Font\\Calibri-Light.ttf"), 48);
            orthoProjection = Matrix4.CreateOrthographicOffCenter(0f, 1600f,0f, 900, 1.0f, -1.0f);
            InitRenderText();
        }
        public void Resize(float width, float height) 
        {
            orthoProjection = Matrix4.CreateOrthographicOffCenter(0f, width, 0f, height, 1.0f, -1.0f);
            SP_RenderText.Use();
            SP_RenderText.SetUniform("OrthoProjection", orthoProjection);
            GL.UseProgram(0);
        }
        private void InitRenderText()
        {
            GL.GenVertexArrays(1, out VAO_RenderText);
            GL.GenBuffers(1, out VBO_RenderText);
            GL.BindVertexArray(VAO_RenderText);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO_RenderText);
            GL.BufferData(BufferTarget.ArrayBuffer, 6 * 4 * sizeof(float), (IntPtr)0, BufferUsageHint.DynamicDraw);
            GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }
        public void RenderText(string text, float x, float y, float scale, Vector3 color, bool isLastCharFixed = false)
        {
            float X = x;
            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            FontManager_.LoadGlyphs(text);

            SP_RenderText.Use();

            GL.Uniform3(GL.GetUniformLocation(SP_RenderText.ProgramID, "textColor"), color.X, color.Y, color.Z);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindVertexArray(VAO_RenderText);

            float xMaxString = 0f;

            if (isLastCharFixed)
                xMaxString = text.ToCharArray().Sum(c => Characters.ContainsKey(c) ? Characters[c].Advance * scale : 0);

            foreach (char c in text.ToCharArray())
            {
                if (c == '\n')
                {
                    X = x;
                    y -= scale * 30 * 1.5f;
                    continue;
                }

                Character ch = Characters[c];

                float xpos = (X - xMaxString) + ch.Bearing.X * scale;
                float ypos = y - (ch.Size.Y - ch.Bearing.Y) * scale;

                float w = ch.Size.X * scale;
                float h = ch.Size.Y * scale;

                float[] vertices = new float[]
            {
                      xpos, ypos + h, 0.0f, 0.0f,
                      xpos, ypos, 0.0f, 1.0f,
                      xpos + w, ypos, 1.0f, 1.0f,
                      xpos, ypos + h, 0.0f, 0.0f,
                      xpos + w, ypos, 1.0f, 1.0f,
                      xpos + w, ypos + h, 1.0f, 0.0f
            };

                GL.BindTexture(TextureTarget.Texture2D, ch.TextureID);

                GL.BindBuffer(BufferTarget.ArrayBuffer, VBO_RenderText);
                GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, vertices.Length * sizeof(float), vertices);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

                GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

                X += ch.Advance * scale;
            }
            GL.UseProgram(0);
            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.Disable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
        }
        private static string ReadFile(string filePath)
        {
            try
            {

                return System.IO.File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File read error: {ex.Message}");
                return null;
            }
        }
    }
}
