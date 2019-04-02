using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private readonly Dictionary<Type, Action<IDrawData, OpenGL>> drawHelpersUI =
            new Dictionary<Type, Action<IDrawData, OpenGL>>();

        private IGameModel currentModel;
        private UIWindow currentWindow;

        private List<ObjectOnDisplay> drawObjects = new List<ObjectOnDisplay>();
        private List<IDrawData> drawUI = new List<IDrawData>();

        private OpenGLControl openGL;


        public MainForm(IGameModel model)
        {
            currentModel = model;

            OpenGLCreate();

            HelperInitialization();

            WindowState = FormWindowState.Maximized;
            ShowIcon = false;
            currentModel.Tick += FrameTick;

            currentWindow = GUIFactory.Create(currentModel);

            openGL.KeyUp += currentWindow.OnKeyRelease;
            openGL.KeyDown += currentWindow.OnKeyPress;
            SizeChanged += currentWindow.OnGameSizeChanged;
            currentWindow.UIChanged += UIChanged;

            currentModel.GameModelChanged += ChangeGameModel;


            model.Run();
        }

        private void UIChanged(GUIControl control, List<IDrawData> obj)
        {
            drawUI = obj.OrderByDescending(x => x.DrawPriority).ToList();
        }


        private void OpenGLCreate()
        {
            openGL = new OpenGLControl
            {
                FrameRate = 60,
                RenderContextType = RenderContextType.NativeWindow,
                RenderTrigger = RenderTrigger.TimerBased,
                Dock = DockStyle.Fill,
                DrawFPS = true
            };

            ((ISupportInitialize) openGL).BeginInit();
            openGL.OpenGLDraw += OpenGLDraw;
            openGL.OpenGLInitialized += OpenGLInitialized;

            Controls.Add(openGL);
            ((ISupportInitialize) openGL).EndInit();
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

            foreach (var drawData in drawUI)
                drawHelpersUI[drawData.GetType()].Invoke(drawData, gl);

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
                ((IDrawable) instance).Init(openGL.OpenGL);
                var typeGameObject = (Type) drawClass.GetProperty("GameObjectType")?.GetValue(instance, null);
                var drawingMethod = (Action<ObjectOnDisplay, OpenGL>) drawClass
                    .GetMethod("DrawObject")
                    ?.CreateDelegate(typeof(Action<ObjectOnDisplay, OpenGL>), instance);

                drawHelpers.Add(typeGameObject, drawingMethod);
            }

            foreach (var drawClass in AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IUIDrawable).IsAssignableFrom(x) &&
                            !x.IsInterface &&
                            !x.IsAbstract))
            {
                var instance = Activator.CreateInstance(drawClass);
                var typeGameObject = (Type) drawClass.GetProperty("DrawDataType")?.GetValue(instance, null);
                var drawingMethod = (Action<IDrawData, OpenGL>) drawClass
                    .GetMethod("DrawUi")
                    ?.CreateDelegate(typeof(Action<IDrawData, OpenGL>), instance);

                drawHelpersUI.Add(typeGameObject, drawingMethod);
            }
        }

        private void FrameTick(List<GameObject> objectsToDraw, BasicCamera gameBasicCamera)
        {
            drawObjects = (from objectToDraw in objectsToDraw.OrderByDescending(x => x.DrawingPriority)
                let positionOnDisplay = gameBasicCamera.GamePositionToWindow(objectToDraw.Position)
                select new ObjectOnDisplay(objectToDraw, positionOnDisplay)).ToList();
        }

        private void ChangeGameModel(IGameModel obj)
        {
            currentWindow.Unsubscribe();
            currentModel = obj;
            currentWindow = GUIFactory.Create(obj);
        }
    }
}