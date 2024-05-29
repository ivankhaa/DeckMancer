using DeckMancer.API.OpenGL;
using DeckMancer.Core;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

public class GameObjectRender
{
    private ShaderProgram _shaderProgram;
    private ShaderProgram _shaderProgramSelect;
    public readonly GameObject _gameObject;
    private Transform _transform;
    private Mesh _mesh;
    private Material _material;
    private VertexArray _vertexArray;
    private ArrayBuffer _vertexBuffer;
    private ArrayBuffer _texCoordBuffer;
    private ArrayBuffer _indicesBuffer;
    private TextureBuffer _textureBuffer;
    private MainCamera _mainCamera;
    private Camera _camera;
    private Matrix4 ModelMatrix;


    public GameObjectRender(MainCamera mainCamera, GameObject gameObject)
    {
        _mainCamera = mainCamera;
        _camera = mainCamera.GetComponent<Camera>();

        _gameObject = gameObject;
        _transform = gameObject.Transform;
        _mesh = gameObject.GetComponent<Mesh>();
        _material = _mesh.Material;
        _material.MaterialChanged += OnMaterialChanged;

        var _vertexShader = new VertexShader();
        var _fragmentShader = new FragmentShader();
        var _vertexShaderSelect = new VertexShader(ReadFile.ReadAllText(@"Resource\OpenGL\Shader\VertexShaderSelect.glsl"));
        var _fragmentShaderSelect = new FragmentShader(ReadFile.ReadAllText(@"Resource\OpenGL\Shader\FragmentShaderSelect.glsl"));

        _shaderProgram = new ShaderProgram(_vertexShader.Shader, _fragmentShader.Shader);
        _shaderProgramSelect = new ShaderProgram(_vertexShaderSelect.Shader, _fragmentShaderSelect.Shader);

        _vertexArray = new VertexArray();
        _vertexBuffer = new ArrayBuffer(VertexLayout.Standard, VertexAttributeType.Vertices);
        _texCoordBuffer = new ArrayBuffer(VertexLayout.Standard, VertexAttributeType.Texture);
        _indicesBuffer = new ArrayBuffer(VertexLayout.Standard, VertexAttributeType.Indices);
        _textureBuffer = new TextureBuffer();

        _vertexArray.Bind();

        _vertexBuffer.SetArray(_mesh.Vertices);
        _texCoordBuffer.SetArray(_mesh.UVs);
        _indicesBuffer.SetArray(_mesh.Indices);
        _textureBuffer.SetTexture(_material.Texture);


        _shaderProgramSelect.Use();
        _shaderProgramSelect.SetUniform("ID", _gameObject.ID);
        _shaderProgramSelect.SetUniform("ProjectionMatrix", _camera.Projection);
        _shaderProgramSelect.SetUniform("ViewMatrix", _mainCamera.ViewModel);


        _shaderProgram.Use();
        _shaderProgram.SetUniform("ProjectionMatrix", _camera.Projection);
        _shaderProgram.SetUniform("ViewMatrix", _mainCamera.ViewModel);

        _shaderProgram.SetUniform("Color", _material.Color);
        _shaderProgram.SetTextures(new string[] { "Texture0" });

        GL.UseProgram(0);
    }
    private void OnMaterialChanged(object sender, EventArgs e)
    {
        var propertyName = (string)sender;
        _shaderProgram.Use();
        if (propertyName == "Color")
        {
            _shaderProgram.SetUniform("Color", _material.Color);
        }
        else if (propertyName == "Texture")
        {
            _vertexArray.Bind();
            _textureBuffer.SetTexture(_material.Texture);
        }
    }
    public void Draw()
    {


        ModelMatrix = Matrix4.CreateScale(_transform.Scale) *
            Matrix4.CreateRotationX(MathHelper.DegreesToRadians(_transform.Rotation.X)) *
            Matrix4.CreateRotationY(MathHelper.DegreesToRadians(_transform.Rotation.Y)) *
            Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(_transform.Rotation.Z)) *
            Matrix4.CreateTranslation(_transform.Position);



        _shaderProgram.Use();
        _shaderProgram.SetUniform("ProjectionMatrix", _camera.Projection);
        _shaderProgram.SetUniform("ViewMatrix", _mainCamera.ViewModel);
        _shaderProgram.SetUniform("ModelMatrix", ModelMatrix);

        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

        GL.ActiveTexture(TextureUnit.Texture0);
        _textureBuffer.Bind();

        _vertexArray.Bind();
        GL.DrawElements(PrimitiveType.Triangles, _indicesBuffer.ArrayCount, DrawElementsType.UnsignedInt, 0);
        GL.BindVertexArray(0);
        GL.BindTexture(TextureTarget.Texture2D, 0);
        GL.UseProgram(0);

        GL.Disable(EnableCap.Blend);


    }
    public void DrawFrame()
    {
        ModelMatrix = Matrix4.CreateScale(_transform.Scale) *
            Matrix4.CreateRotationX(MathHelper.DegreesToRadians(_transform.Rotation.X)) *
            Matrix4.CreateRotationY(MathHelper.DegreesToRadians(_transform.Rotation.Y)) *
            Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(_transform.Rotation.Z)) *
            Matrix4.CreateTranslation(_transform.Position);

        _shaderProgramSelect.Use();
        _shaderProgramSelect.SetUniform("ProjectionMatrix", _camera.Projection);
        _shaderProgramSelect.SetUniform("ViewMatrix", _mainCamera.ViewModel);
        _shaderProgramSelect.SetUniform("ModelMatrix", ModelMatrix);

        GL.Enable(EnableCap.DepthTest);

        _vertexArray.Bind();
        GL.DrawElements(PrimitiveType.Triangles, _indicesBuffer.ArrayCount, DrawElementsType.UnsignedInt, 0);
        GL.BindVertexArray(0);

        GL.UseProgram(0);

    }

}
