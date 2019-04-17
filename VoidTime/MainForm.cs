using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SharpGL;
using SharpGL.Enumerations;
using VoidTime.GUI;

namespace VoidTime
{
    public class MainForm : Form
    {
        private readonly Dictionary<Type, Action<ObjectOnDisplay, OpenGL>> drawHelpers =
            new Dictionary<Type, Action<ObjectOnDisplay, OpenGL>>();

        public IGameModel currentModel;

        private List<ObjectOnDisplay> drawObjects = new List<ObjectOnDisplay>();

        private OpenGLControl openGL;


        public MainForm(IGameModel model)
        {
            BackColor = Color.Black;
            currentModel = model;
            OpenGLCreate();

            Window window = new Window(this, (currentModel as MainGameModel).player);

            HelperInitialization();


            WindowState = FormWindowState.Maximized;
            ShowIcon = false;
            currentModel.Tick += FrameTick;


            openGL.KeyUp += currentModel.OnKeyRelease;
            openGL.KeyDown += currentModel.OnKeyPress;
            openGL.MouseMove += currentModel.OnMouseMove;
            openGL.MouseDown += currentModel.OnMousePressed;
            openGL.MouseUp += currentModel.OnMouseReleased;
            openGL.MouseDoubleClick += currentModel.OnMouseDoubleClick;
            openGL.MouseWheel += currentModel.OnMouseWheel;
            SizeChanged += currentModel.OnSizeChanged;

            model.Run();
        }


        private void OpenGLCreate()
        {
            openGL = new OpenGLControl
            {
                FrameRate = 1000,
                RenderContextType = RenderContextType.NativeWindow,
                RenderTrigger = RenderTrigger.TimerBased,
                Dock = DockStyle.Fill,
                DrawFPS = true,
            };

            ((ISupportInitialize)openGL).BeginInit();
            openGL.OpenGLDraw += OpenGLDraw;
            openGL.OpenGLInitialized += OpenGLInitialized;

            Controls.Add(openGL);
            ((ISupportInitialize)openGL).EndInit();
        }

        private void OpenGLInitialized(object sender, EventArgs e)
        {
            var gl = openGL.OpenGL;
            gl.Enable(OpenGL.GL_TEXTURE_2D);
        }

        private void OpenGLDraw(object sender, RenderEventArgs args)
        {
            var gl = openGL.OpenGL;

            gl.Enable(OpenGL.GL_BLEND);
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.MatrixMode(MatrixMode.Projection);
            gl.LoadIdentity();
            gl.Ortho(0, openGL.Width, openGL.Height, 0, 0.01, -100);

            gl.MatrixMode(MatrixMode.Modelview);

            gl.Color(1.0, 1.0, 1.0);

            foreach (var objectOnDisplay in drawObjects)
                drawHelpers[objectOnDisplay.GameObject.GetType()].Invoke(objectOnDisplay, gl);

            gl.Disable(OpenGL.GL_BLEND);
        }

        private void HelperInitialization()
        {
            foreach (var drawClass in AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IDrawable).IsAssignableFrom(x) &&
                            !x.IsInterface &&
                            !x.IsAbstract))
            {
                var instance = Activator.CreateInstance(drawClass);
                ((IDrawable)instance).Init(openGL.OpenGL);
                var typeGameObject = (Type)drawClass.GetProperty("GameObjectType")?.GetValue(instance, null);
                var drawingMethod = (Action<ObjectOnDisplay, OpenGL>)drawClass
                    .GetMethod("DrawObject")
                    ?.CreateDelegate(typeof(Action<ObjectOnDisplay, OpenGL>), instance);

                drawHelpers.Add(typeGameObject, drawingMethod);
            }
        }

        private void FrameTick(List<GameObject> objectsToDraw, BasicCamera gameBasicCamera)
        {
            drawObjects = (from objectToDraw in objectsToDraw.OrderByDescending(x => x.DrawingPriority)
                           let positionOnDisplay = gameBasicCamera.GamePositionToWindow(objectToDraw.Position)
                           select new ObjectOnDisplay(objectToDraw, positionOnDisplay)).ToList();
        }
    }
}