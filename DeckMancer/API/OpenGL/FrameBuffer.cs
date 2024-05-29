using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.API.OpenGL
{
    public class FrameBuffer
    {
        private int _frameBufferId;
        private int _renderBufferId;
        private int _pickingTextureBufferId;

        public FrameBuffer() 
        {
            _frameBufferId = GL.GenFramebuffer();
            _pickingTextureBufferId = GL.GenTexture();
            _renderBufferId = GL.GenRenderbuffer();
        }
        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _frameBufferId);
        }
        public void SetFrame(int width, int height) 
        {
            Bind();

            GL.BindTexture(TextureTarget.Texture2D, _pickingTextureBufferId);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.R32ui, width, height, 0, PixelFormat.RedInteger, PixelType.UnsignedInt, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, _pickingTextureBufferId, 0);
           
            GL.DrawBuffer(DrawBufferMode.ColorAttachment0);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, _renderBufferId);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.DepthComponent, width, height);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, _renderBufferId);


            if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
            {
                throw new Exception("Framebuffer is not complete.");
            }
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

        }
    }
}
