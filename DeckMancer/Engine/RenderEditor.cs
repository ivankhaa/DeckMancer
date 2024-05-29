using DeckMancer.API.OpenGL;
using DeckMancer.Core;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Linq;


namespace DeckMancer.Engine
{
    public class RenderEditor
    {
        private static GLWpfControl OpenTKControl;
        public static float WidthEditor => (float)OpenTKControl.FrameBufferWidth;
        public static float HeightEditor => (float)OpenTKControl.FrameBufferHeight;

        public static event EventHandler TransformGameObjectChange;

        private GridRenderer _gridRenderer;
        private QuadRenderer _quadRenderer;
        private FrameBuffer _frameBuffer;
        private uint id = 0;

        public RenderEditor(GLWpfControl Control)
        {
            OpenTKControl = Control;
            Start();
        }
        private void Start()
        {
            _gridRenderer = new GridRenderer(RenderEditorManager.CameraEditor);
            _frameBuffer = new FrameBuffer();
            _quadRenderer = new QuadRenderer();

            GL.ClearColor(Color4.DarkSlateGray);
            GL.Enable(EnableCap.DepthTest);
            InputSubscribe();
        }
        private void _OnRender(TimeSpan delta)
        {
            Render();
        }
        private void Render()
        {
            List<GameObjectRenderEditor> sortedObjects = RenderEditorManager.GameObjectsRender.OrderBy(obj => obj._gameObject.Transform.Position.Z).ToList();

            _frameBuffer.Bind();
            GL.Viewport(0, 0, (int)OpenTKControl.ActualWidth, (int)OpenTKControl.ActualHeight);
            GL.ClearColor(Color4.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            foreach (var renderGM in sortedObjects)
            {
                renderGM.DrawFrame();
            }

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, OpenTKControl.Framebuffer);

            GL.Viewport(0, 0, (int)WidthEditor, (int)HeightEditor);
            GL.ClearColor(Color4.DarkSlateGray);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            _gridRenderer.Draw();
            foreach (var renderGM in sortedObjects)
            {
                renderGM.Draw();
            }
            if (RenderEditorManager.Camera != null) 
            {
                Camera cameraComponent = RenderEditorManager.Camera.GetComponent<Camera>();
                if(cameraComponent != null)
                    _quadRenderer.RenderQuad(cameraComponent.SIZE * cameraComponent.AspectRatio, cameraComponent.SIZE, Color4.Black, RenderEditorManager.CameraEditor.Camera.ProjectionMatrix, RenderEditorManager.CameraEditor.Camera.ViewMatrix, RenderEditorManager.Camera.Transform); 
            }
        }
        public uint[] ReadPixel(int x, int y)
        {
            _frameBuffer.Bind();
            GL.ReadBuffer(ReadBufferMode.ColorAttachment0);
            uint[] pixel = new uint[3];
            GL.ReadPixels(x, (int)OpenTKControl.ActualHeight - y, 1, 1, PixelFormat.RedInteger, PixelType.UnsignedInt, pixel);

            GL.ReadBuffer(ReadBufferMode.None);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, OpenTKControl.Framebuffer);
            return pixel;
        }
        private void InputSubscribe() 
        {
            OpenTKControl.Render += new Action<TimeSpan>(_OnRender);
            OpenTKControl.MouseMove += OpenTKControl_MouseMove;
            OpenTKControl.MouseWheel += OpenTKControl_MouseWheel;
            OpenTKControl.MouseDown += OpenTKControl_MouseDown;
            OpenTKControl.MouseUp += OpenTKControl_MouseUp;
            OpenTKControl.SizeChanged += GLWpfControl_SizeChanged;
        }
        private void GLWpfControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RenderEditorManager.CameraEditor.Width = (float)OpenTKControl.ActualWidth;
            RenderEditorManager.CameraEditor.Height = (float)OpenTKControl.ActualHeight;
            _frameBuffer.SetFrame((int)WidthEditor, (int)HeightEditor);
        }
        
        private Vector2 currentMousePosition;
        private Vector2 lastMousePosition;
        private bool mouseRightButtonClick = false;
        private void OpenTKControl_MouseMove(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition(OpenTKControl);
            currentMousePosition = new Vector2((float)point.X, (float)point.Y);

            if (e.RightButton == MouseButtonState.Pressed)
            {
                if (!mouseRightButtonClick)
                {
                    mouseRightButtonClick = true;
                    point = e.GetPosition(OpenTKControl);
                    lastMousePosition = new Vector2((float)point.X, (float)point.Y);
                }

                RenderEditorManager.CameraEditor.OnTranslationMove(currentMousePosition, lastMousePosition);
                lastMousePosition = currentMousePosition;
            }
            else if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (id != 0)
                {
                    float Width = (float)OpenTKControl.ActualWidth;
                    float Height = (float)OpenTKControl.ActualHeight;
                    var SIZE = CameraEditor.SIZE;
                    var Scale = RenderEditorManager.CameraEditor.Scale;
                    var AspectRatio = Width / Height;
                    Vector2 Delta = new Vector2(currentMousePosition.X - lastMousePosition.X, currentMousePosition.Y - lastMousePosition.Y);

                    var GOPos = SceneManager.CurrentGameObjectSelected.Transform.Position;

                    SceneManager.CurrentGameObjectSelected.Transform.Position = new Vector3(GOPos.X + (Delta.X / Width * (SIZE * (1.0f / Scale)) * AspectRatio), GOPos.Y - (Delta.Y / Height * (SIZE * (1.0f / Scale))), GOPos.Z);
                    lastMousePosition = currentMousePosition;
                    TransformGameObjectChange?.Invoke(null, EventArgs.Empty);
                }
            }

        }
        private void OpenTKControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var point = e.GetPosition(OpenTKControl);
                lastMousePosition = new Vector2((float)point.X, (float)point.Y);

                id = ReadPixel((int)point.X, (int)point.Y)[0];
                if (id != 0)
                {
                    GameObjectRenderEditor foundObjectRender = RenderEditorManager.GameObjectsRender.FirstOrDefault(objRender => objRender._gameObject.ID == id);
                    if (foundObjectRender != null)
                    {
                        SceneManager.CurrentGameObjectSelected = foundObjectRender._gameObject;
                    }

                }
            }
        }
        private void OpenTKControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseRightButtonClick = false;
        }
        private void OpenTKControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            RenderEditorManager.CameraEditor.Scale += (0.0001f * RenderEditorManager.CameraEditor.Scale) * e.Delta;
        }
    }
}
