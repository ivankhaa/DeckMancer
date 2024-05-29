using DeckMancer.API.OpenGL;
using DeckMancer.Core;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.Engine
{
    public class GridRenderer
    {
        private ShaderProgram _shaderProgram;
        private GridSettings _gridSettings;
        private ArrayBuffer _vertexBuffer;
        private VertexArray _vertexArray;
        private CameraEditor _cameraEditor;
        private Matrix4 _modelMatrix = Matrix4.Identity;
        private static string vertexShader = @"
#version 330 core

layout (location = 0) in vec3 position;

uniform float gridSize;
uniform float cellSize;
uniform vec4 gridColor;
uniform float Scale;
uniform mat4 ModelMatrix; 
uniform mat4 ViewMatrix;  
uniform mat4 ProjectionMatrix; 

out vec4 fragmentColor;

float checkPeriod(float xCoord, float yCoord, float alpha, float period)
{
    float phasaX = round(xCoord)/cellSize;
    float phasaY = round(yCoord)/cellSize;
    if(xCoord == gridSize)
    {
        if(mod(phasaY, period) != 0)
        {
            alpha = alpha - alpha/1.4;
        }
    } 
    else if(yCoord == gridSize)
    {
        if (mod(phasaX, period) != 0) 
        {
             alpha = alpha - alpha/1.4;
        }    
    } 

    return alpha;
}

void main()
{
    float yCoord = abs(position.y);
    float xCoord = abs(position.x);
    vec3 color = gridColor.zyx; 
    float alpha = gridColor.w;
    float nScale = 1.0 - Scale;
    float period = 2;
    if(Scale<=1)
    {
        period = int(2.0 * sqrt((sqrt(3) -(((nScale*nScale))))/Scale));
    }

    for(int i = 2; period >= i; i++)
    {
        alpha = checkPeriod(xCoord, yCoord, alpha, i);
    }
    
    if(position.x == gridSize && position.y == 0.0)
    {
        color = vec3(1.0, 0.0, 0.0);
    } 
    else if(position.y == gridSize && position.x == 0.0)
    {
        color = vec3(0.0, 1.0, 0.0);
    }
    fragmentColor = vec4(color, alpha);
    
    mat4 ModelViewProjectionMatrix = ProjectionMatrix * ViewMatrix * ModelMatrix;
    gl_Position = ModelViewProjectionMatrix * vec4(position.xy, 1.0, 1.0);
}
";
        private static string fragmentShader = @"#version 330 core

in vec4 fragmentColor;

out vec4 FragColor;

void main()
{
  FragColor = fragmentColor;
}";
        
        public GridRenderer(CameraEditor cameraEditor)
        {
            _modelMatrix = Matrix4.Identity;
            
            _cameraEditor = cameraEditor;
            _cameraEditor.OnScaleChanged += Scale;
            _cameraEditor.OnSizeChanged += Resize;
            _cameraEditor.OnPositionChanged += Position;

            _gridSettings = new GridSettings
            {
                CellSize = 1f,
                GridSize = 50000f,
                Color = new Color4(0.5f, 0.5f, 0.5f, 0.9f),
                Scale = _cameraEditor.Scale
            };
            _vertexArray = new VertexArray();
            _vertexBuffer = new ArrayBuffer(VertexLayout.Default);
            _vertexArray.Bind();
            CreateVertices();
            _modelMatrix = Matrix4.CreateScale(1.0f, 1.0f, 1.0f) *
            Matrix4.CreateRotationX(0.0f) *
            Matrix4.CreateRotationY(0.0f) *
            Matrix4.CreateRotationZ(0.0f) *
            Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);
             _shaderProgram = new ShaderProgram(vertexShader, fragmentShader);
            _shaderProgram.Use();
            _shaderProgram.SetUniform("ProjectionMatrix", _cameraEditor.Camera.ProjectionMatrix);
            _shaderProgram.SetUniform("ViewMatrix", _cameraEditor.Camera.ViewMatrix);
            _shaderProgram.SetUniform("ModelMatrix", _modelMatrix);
            _shaderProgram.SetUniform("gridSize", _gridSettings.GridSize);
            _shaderProgram.SetUniform("cellSize", _gridSettings.CellSize);
            _shaderProgram.SetUniform("gridColor", _gridSettings.Color);
            _shaderProgram.SetUniform("Scale", _gridSettings.Scale);
           
            GL.UseProgram(0);
        }
        private void Scale(object sender, EventArgs e)  
        {
            _gridSettings.Scale = _cameraEditor.Scale;
            _shaderProgram.Use(); 
            _shaderProgram.SetUniform("Scale", _gridSettings.Scale);
            _shaderProgram.SetUniform("ProjectionMatrix", _cameraEditor.Camera.ProjectionMatrix);
            _shaderProgram.SetUniform("ViewMatrix", _cameraEditor.Camera.ViewMatrix);
        }
        private void Resize(object sender, EventArgs e) 
        {
            _shaderProgram.Use();
            _shaderProgram.SetUniform("ProjectionMatrix", _cameraEditor.Camera.ProjectionMatrix);
        }
        private void Position(object sender, EventArgs e)
        {
            _shaderProgram.Use();
            _shaderProgram.SetUniform("ViewMatrix", _cameraEditor.Camera.ViewMatrix);
        }

        public void Draw()
        {
            _shaderProgram.Use();

            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            _vertexBuffer.Bind();
            _vertexArray.Bind();
            
            GL.LineWidth(2f);
            GL.DrawArrays(PrimitiveType.Lines, 0, _vertexBuffer.ArrayCount);
            
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.UseProgram(0);

            GL.Disable(EnableCap.Blend);
        }

        private void CreateVertices()
        {
            var vertices = new List<Vector3>();
            var minmax = _gridSettings.GridSize;
            var shift = _gridSettings.CellSize;

            for (float i = 0; i <= minmax; i+= shift)
            {
                float x = i;
                vertices.Add(new Vector3(new Vector3(x, -minmax, 0f)));
                vertices.Add(new Vector3(new Vector3(x, minmax, 0f)));

            }
            for (float i = 0; i >= -minmax; i -= shift)
            {
                float x = i;
                vertices.Add(new Vector3(new Vector3(x, -minmax, 0f)));
                vertices.Add(new Vector3(new Vector3(x, minmax, 0f)));

            }
            for (float i = 0; i <= minmax; i += shift)
            {
                float y = i;
                vertices.Add(new Vector3(new Vector3(-minmax, y, 0f)));
                vertices.Add(new Vector3(new Vector3(minmax, y, 0f)));
            }
            for (float i = 0; i >= -minmax; i -= shift)
            {
                float y = i;
                vertices.Add(new Vector3(new Vector3(-minmax, y, 0f)));
                vertices.Add(new Vector3(new Vector3(minmax, y, 0f)));
            }
            _vertexBuffer.SetArray(vertices.ToArray());
        }
    }
}
