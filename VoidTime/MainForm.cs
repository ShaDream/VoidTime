using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SharpGL;
using SharpGL.Enumerations;

namespace VoidTime
{
    public class MainForm : Form
    {
        #region Constructor

        public MainForm(GameModel model)
        {
            openGL = new OpenGLControl
            {
                FrameRate = 60,
                RenderContextType = RenderContextType.NativeWindow,
                RenderTrigger = RenderTrigger.TimerBased,
                Dock = DockStyle.Fill,
                DrawFPS = false
            };

            HelperInitialization();

            ((ISupportInitialize) openGL).BeginInit();
            openGL.OpenGLDraw += OpenGLDraw;
            openGL.OpenGLInitialized += OpenGL_OpenGLInitialized;

            Controls.Add(openGL);
            ((ISupportInitialize) openGL).EndInit();

            WindowState = FormWindowState.Maximized;
            ShowIcon = false;
            model.Tick += FrameTick;

            openGL.KeyUp += model.OnKeyRelease;
            openGL.KeyDown += model.OnKeyPress;
            SizeChanged += (s, a) => model.GameBasicCamera.Size = Size;
            model.GameBasicCamera.Size = Size;

            model.Run();
        }

        #endregion

        #region Public Properties

        private readonly OpenGLControl openGL;

        private readonly Dictionary<Type, Action<ObjectOnDisplay, OpenGL>> drawHelpers =
            new Dictionary<Type, Action<ObjectOnDisplay, OpenGL>>();

        private List<ObjectOnDisplay> DrawObjects = new List<ObjectOnDisplay>();

        #endregion

        #region Private Methods

        private void OpenGL_OpenGLInitialized(object sender, EventArgs e)
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

            foreach (var objectOnDisplay in DrawObjects)
                drawHelpers[objectOnDisplay.GameObject.GetType()].Invoke(objectOnDisplay, gl);

            gl.Disable(OpenGL.GL_BLEND);

            gl.Flush();
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
                var typeGameObject = (Type) drawClass.GetProperty("GameObjectType").GetValue(instance, null);
                var drawingMethod = (Action<ObjectOnDisplay, OpenGL>) drawClass.GetMethod("DrawObject")
                                                                               .CreateDelegate(
                                                                                   typeof(Action<ObjectOnDisplay,
                                                                                       OpenGL>), instance);

                drawHelpers.Add(typeGameObject, drawingMethod);
            }
        }

        private void FrameTick(List<GameObject> objectsToDraw, BasicCamera gameBasicCamera)
        {
            DrawObjects = (from objectToDraw in objectsToDraw.OrderByDescending(x => x.DrawingPriority)
                           let positionOnDisplay = gameBasicCamera.GamePositionToWindow(objectToDraw.Position)
                           select new ObjectOnDisplay(objectToDraw, positionOnDisplay)).ToList();
        }

        #endregion
    }
}