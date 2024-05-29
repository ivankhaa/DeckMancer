using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using DeckMancer.Core;
using DeckMancer.API.OpenGL;
using DeckMancer.Serialization;
using System.Threading;

class Program
{
    public class Game : GameWindow
    {
        private readonly object locker = new object();
        private static Thread mainThread;
        private static List<Action> nonContextEvents;
        private Vector2 currentMousePosition;
        private uint id = 0;

        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            Console.WriteLine(GL.GetString(StringName.Version));
            Console.WriteLine(GL.GetString(StringName.Vendor));
            Console.WriteLine(GL.GetString(StringName.Renderer));
            Console.WriteLine(GL.GetString(StringName.ShadingLanguageVersion));

            VSync = VSyncMode.On;
            CursorState = CursorState.Normal;
            UpdateFrequency = 360;
       
        }
        protected override void OnLoad()
        {
            mainThread = Thread.CurrentThread;

            nonContextEvents = new List<Action>();
            this.WindowBorder = WindowBorder.Fixed;
            frameBuffer = new FrameBuffer();
            GlobalGameManager manager = BinarySerialization.Deserialize<GlobalGameManager>(Path.GetFullPath("Resource\\Globalgamemanager"));
            GlobalGameManager.SetInstance(manager);
            SceneManager.SceneLoaded += _SceneLoaded;
            SceneManager.LoadScene(manager.SceneNames[0]);
            MainCamera mainCamera = ComponentMapping.GetGameObjectsForComponent<Camera>()[0] as MainCamera;
            var Camera = mainCamera.GetComponent<Camera>();
            ClientSize = new Vector2i((int)Camera.Width, (int)Camera.Height);
            InitializGameObjectsScript();
            base.OnLoad();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e) 
        {
            foreach (var script in scripts)
            {
                script.BehaviourScript.OnMouseDown(FindGameObjectByID(id), e);
            }
        }
        protected override void OnMouseUp(MouseButtonEventArgs e) 
        {
            foreach (var script in scripts)
            {
                script.BehaviourScript.OnMouseUp(FindGameObjectByID(id), e);
            }
        }
        protected override void OnMouseMove(MouseMoveEventArgs e) 
        {
            currentMousePosition = e.Position;
            foreach (var script in scripts)
            {
                script.BehaviourScript.OnMouseMove(FindGameObjectByID(id), e);
            }
        }
        protected override void OnMouseWheel(MouseWheelEventArgs e) 
        {
            foreach (var script in scripts)
            {
                script.BehaviourScript.OnMouseWheel(e);
            }
        }
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            if (e.Alt && e.Key == Keys.Enter)
            {
                if (this.WindowState == WindowState.Fullscreen)
                {
                    this.WindowState = WindowState.Normal;
                    WindowBorder = WindowBorder.Fixed;
                }
                else
                {
                    WindowBorder = WindowBorder.Hidden;
                    this.WindowState = WindowState.Fullscreen;

                }
            }
            foreach (var script in scripts)
            {
                script.BehaviourScript.OnKeyDown(e);
            }
        }
        protected override void OnKeyUp(KeyboardKeyEventArgs e) 
        {
            foreach (var script in scripts)
            {
                script.BehaviourScript.OnKeyUp(e);
            }
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            if(ClientSize.X != 0 && ClientSize.Y !=0)
                frameBuffer.SetFrame(ClientSize.X, ClientSize.Y);
            base.OnResize(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            lock (locker)
            {
                if (nonContextEvents.Count > 0)
                {
                    for (int i = 0; i < nonContextEvents.Count; i++)
                    {
                        if (i == 0)
                        {
                            nonContextEvents[i]();
                        }
                        else if (nonContextEvents[i] != nonContextEvents[i - 1])
                        {
                            nonContextEvents[i]();
                        }
                    }
                    nonContextEvents = new List<Action>();
                }
            }
            id = ReadPixel((int)currentMousePosition.X, (int)currentMousePosition.Y)[0];

            foreach (var script in scripts) 
            {
                script.BehaviourScript.Update();
            }

            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            Render();
            SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
        }
        
        public static List<GameObjectRender> GameObjectsRender;
        public static List<Script> scripts;
        private static FrameBuffer frameBuffer;
        void InitializGameObjectsRender()
        {
            lock (locker)
            {
                GameObjectsRender = new List<GameObjectRender>();
                List<GameObject> gameObjects = ComponentMapping.GetGameObjectsForComponent<Mesh>();
                MainCamera Camera = ComponentMapping.GetGameObjectsForComponent<Camera>()[0] as MainCamera;
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    GameObjectsRender.Add(new GameObjectRender(Camera, gameObjects[i]));
                }
            }
        }
        void InitializGameObjectsScript()
        {
            scripts = new List<Script>();
            var gameObjects = ComponentMapping.GetGameObjectsForComponent<Script>();
            foreach (var gameObject in gameObjects)
            {
                var script = gameObject.GetComponent<Script>();
                if (script != null)
                {
                    if (script.Name != null)
                    {
                        var assembly = Assembly.LoadFrom(Path.GetFullPath(GameName));
                        Type type = assembly.GetType(script.Name);

                        Type behaviourType = typeof(Behaviour);
                        if (behaviourType.IsAssignableFrom(type))
                        {
                            var behaviour = (Behaviour)Activator.CreateInstance(type);
                            behaviour.Start();

                            script.BehaviourScript = behaviour;
                            scripts.Add(script);
                        }
                    }
                }
            }
        }
        void Render()
        {

            List<GameObjectRender> sortedObjects = GameObjectsRender.OrderBy(obj => obj._gameObject.Transform.Position.Z).ToList();
            frameBuffer.Bind();
            
            GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
            GL.ClearColor(Color4.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            foreach (var renderGM in sortedObjects)
            {
                renderGM.DrawFrame();
            }

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            
            GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
            GL.ClearColor(Color4.DarkSlateGray);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            foreach (var renderGM in sortedObjects)
            {
                renderGM.Draw();
            }
           
        }
        public GameObject FindGameObjectByID(uint id) 
        {

            if (id != 0)
            {
                lock (locker)
                {
                    var go = SceneManager.CurrentScene.GameObjects.FirstOrDefault(obj => obj.ID == id);
             
                    return go;
                }
            }
            else
            {
                return null;
            }
            
        }
        public uint[] ReadPixel(int x, int y)
        {
            frameBuffer.Bind();
            GL.ReadBuffer(ReadBufferMode.ColorAttachment0);
            uint[] pixel = new uint[3];
            GL.ReadPixels(x, ClientSize.Y - y, 1, 1, PixelFormat.RedInteger, PixelType.UnsignedInt, pixel);
            GL.ReadBuffer(ReadBufferMode.None);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            return pixel;
        }
        public void _SceneLoaded(object sender, EventArgs e)
        {
            SceneManager.CurrentScene.GameObjectsChanged += _GameObjectsChanged;
            if (Thread.CurrentThread == mainThread)
            {
                InitializGameObjectsRender();
            }
            else
            {
                lock (locker)
                {
                    nonContextEvents.Add(InitializGameObjectsRender);
                }
            }

        }
        public void _GameObjectsChanged(object sender, EventArgs e) 
        {
            SceneManager.CurrentScene.GameObjectsChanged -= _GameObjectsChanged;
            SceneManager.LoadScene(SceneManager.CurrentScene);
        }

    }

    public static string GameName;
    static void Main(string[] args)
    {
        GameName = Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().MainModule.ModuleName);

        var nativeWinSettings = new NativeWindowSettings()
        {
            ClientSize = new Vector2i(640, 360),
            Location = new Vector2i(0, 0),
            WindowBorder = WindowBorder.Resizable,
            WindowState = WindowState.Normal,
            Title = GameName,

            Flags = ContextFlags.ForwardCompatible,
            APIVersion = new Version(4, 6),
            Profile = ContextProfile.Core,
            API = ContextAPI.OpenGL,

            NumberOfSamples = 0
        };

        using (Game game = new Game(GameWindowSettings.Default, nativeWinSettings))
        {
            game.Run();
        }
    }
}

