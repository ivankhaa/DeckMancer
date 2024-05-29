using DeckMancer.API.OpenGL;
using DeckMancer.Core;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.Engine
{
    public class QuadRenderer
    {
        private readonly ShaderProgram _shaderProgram;
        private readonly int _vbo;
        private readonly int _vao;

        public QuadRenderer()
        {
            // Шейдеры
            string vertexShaderSource = @"
            #version 330 core

            layout(location = 0) in vec3 Position;

            uniform mat4 ModelMatrix; 
            uniform mat4 ViewMatrix;  
            uniform mat4 ProjectionMatrix; 

            void main()
            {
                mat4 ModelViewProjectionMatrix = ProjectionMatrix * ViewMatrix * ModelMatrix;
                gl_Position = ModelViewProjectionMatrix * vec4(Position, 1.0);
            }";

            string fragmentShaderSource = @"
            #version 330 core
            out vec4 FragColor;
            uniform vec4 Color;
            void main()
            {
                FragColor = Color;
            }";

            _shaderProgram = new ShaderProgram(vertexShaderSource, fragmentShaderSource);

            _vbo = GL.GenBuffer();
            // VAO
            _vao = GL.GenVertexArray();
            GL.BindVertexArray(_vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void RenderQuad(float width, float height, Color4 color4, Matrix4 projection, Matrix4 view, Transform transform)
        {
            _shaderProgram.Use();
            Matrix4 ModelMatrix = Matrix4.CreateScale(transform.Scale) *
                Matrix4.CreateRotationX(MathHelper.DegreesToRadians(transform.Rotation.X)) *
                Matrix4.CreateRotationY(MathHelper.DegreesToRadians(transform.Rotation.Y)) *
                Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(transform.Rotation.Z)) *
                Matrix4.CreateTranslation(transform.Position);
            _shaderProgram.SetUniform("ProjectionMatrix", projection);
            _shaderProgram.SetUniform("ViewMatrix", view);
            _shaderProgram.SetUniform("ModelMatrix", ModelMatrix);
            _shaderProgram.SetUniform("Color", color4);

            GL.BindVertexArray(_vao);

            float[] vertices =
            {
               -width / 2, -height / 2, 0.0f,
                width / 2, -height / 2, 0.0f,
                width / 2,  height / 2, 0.0f,
               -width / 2,  height / 2, 0.0f
            };


            GL.Disable(EnableCap.DepthTest);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * vertices.Length, vertices, BufferUsageHint.StaticDraw);

            GL.DrawArrays(PrimitiveType.LineLoop, 0, 4);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
        }
    }
}
