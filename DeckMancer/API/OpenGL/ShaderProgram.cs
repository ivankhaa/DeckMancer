using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.API.OpenGL
{
    public class ShaderProgram
    {
        public int ProgramID;
        public string vertexShader;
        public string fragmentShader;

        public ShaderProgram(string vertexShader, string fragmentShader)
        {
            this.vertexShader = vertexShader;
            this.fragmentShader = fragmentShader;
            Init(vertexShader, fragmentShader);
        }
        private void Init(string vertexShader, string fragmentShader)
        {
            int vertex, fragment;
            int success;
            string infoLog;

            vertex = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertex, vertexShader);
            GL.CompileShader(vertex);

            GL.GetShader(vertex, ShaderParameter.CompileStatus, out success);
            if (success != 0)
            {
                GL.GetShaderInfoLog(vertex, out infoLog);
                Console.WriteLine(infoLog);
            };

            fragment = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragment, fragmentShader);
            GL.CompileShader(fragment);

            GL.GetShader(fragment, ShaderParameter.CompileStatus, out success);
            if (success != 0)
            {
                GL.GetShaderInfoLog(fragment, out infoLog);
                Console.WriteLine(infoLog);
            };

            ProgramID = GL.CreateProgram();
            GL.AttachShader(ProgramID, vertex);
            GL.AttachShader(ProgramID, fragment);
            GL.LinkProgram(ProgramID);
            GL.GetProgram(ProgramID, GetProgramParameterName.LinkStatus, out success);
            if (success != 0)
            {
                GL.GetProgramInfoLog(ProgramID, out infoLog);
                Console.WriteLine(infoLog);
            }
            GL.DeleteShader(vertex);
            GL.DeleteShader(fragment);
        }
        public ShaderProgram GetClone() 
        {
            return new ShaderProgram(vertexShader, fragmentShader);
        }
        public void Use()
        {
            GL.UseProgram(ProgramID);
        }
        public bool[] SetTextures(string[] name) 
        {
            bool[] suspect = new bool[name.Length];
            for(int i = 0; i < name.Length; i++) 
            {
                suspect[i] = SetUniform(name[i], i);
            }

            return suspect;
        }
        public bool SetUniform(string name, object value)
        {
            var location = GL.GetUniformLocation(ProgramID, name);
            if (value is int)
            {
                GL.Uniform1(location, (int)value);
            }
            if (value is uint)
            {
                GL.Uniform1(location, (uint)value);
            }
            else if (value is float)
            {
                GL.Uniform1(location, (float)value);
            }
            else if (value is double)
            {
                GL.Uniform1(location, (double)value);
            }
            else if (value is Vector2)
            {
                GL.Uniform2(location, (Vector2)value);
            }
            else if (value is Vector3)
            {
                GL.Uniform3(location, (Vector3)value);
            }
            else if (value is Vector4)
            {
                GL.Uniform4(location, (Vector4)value);
            }
            else if (value is Color4)
            {
                Color4 colorValue = (Color4)value;
                GL.Uniform4(location, new Vector4(colorValue.R, colorValue.G, colorValue.B, colorValue.A));
            }
            else if (value is Matrix4 mat4) 
            {
                GL.UniformMatrix4(location, false, ref mat4);
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
