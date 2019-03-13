using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph.Assets;

namespace VoidTime
{
    public class MainForm : Form
    {
        #region Public Properties

        private DateTime lastTime;
        private readonly OpenGLControl openGL;
        private int framesRendered;
        private int fps;
        private Dictionary<Type, Action<ObjectOnDisplay, OpenGL>> drawHelpers =
            new Dictionary<Type, Action<ObjectOnDisplay, OpenGL>>();

        private List<ObjectOnDisplay> DrawObjects = new List<ObjectOnDisplay>();

        public static Texture player = new Texture();
        public static Texture background = new Texture();

        #endregion

        #region Constructor

        public MainForm(GameModel model)
        {
            HelperInitialization();

            openGL = new OpenGLControl
            {
                FrameRate = 60,
                RenderContextType = RenderContextType.NativeWindow,
                RenderTrigger = RenderTrigger.TimerBased,
                Dock = DockStyle.Fill,
            };

            ((System.ComponentModel.ISupportInitialize)(openGL)).BeginInit();
            openGL.OpenGLDraw += OpenGLDraw;
            openGL.OpenGLInitialized += OpenGL_OpenGLInitialized;
            //openGL.Resized += OpenGL_Resized;

            Controls.Add(openGL);
            ((System.ComponentModel.ISupportInitialize)(openGL)).EndInit();

            WindowState = FormWindowState.Maximized;
            //ShowIcon = false;
            model.Tick += FrameTick;

            openGL.KeyUp += model.OnKeyRelease;
            openGL.KeyDown += model.OnKeyPress;
            SizeChanged += (s, a) => model.GameBasicCamera.Size = Size;
            model.GameBasicCamera.Size = Size;
            
            model.Run();
        }

        private void OpenGL_OpenGLInitialized(object sender, EventArgs e)
        {
            var gl = openGL.OpenGL;

            gl.Enable(OpenGL.GL_TEXTURE_2D);

            player.Create(openGL.OpenGL,
                new Bitmap(@"C:\Users\ShaDream\source\repos\VoidTime\VoidTime\Resources\Textures\player.png"));
            background.Create(openGL.OpenGL,
                new Bitmap(@"C:\Users\ShaDream\source\repos\VoidTime\VoidTime\Resources\Textures\back.png"));
        }

        private void OpenGLDraw(object sender, RenderEventArgs args)
        {
            var gl = openGL.OpenGL;

            gl.Enable(OpenGL.GL_BLEND);
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);


            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.MatrixMode(MatrixMode.Projection);
            gl.LoadIdentity();
            gl.Ortho(0,openGL.Width,openGL.Height, 0, 0.01, -100);

            gl.MatrixMode(MatrixMode.Modelview);

            foreach (var objectOnDisplay in DrawObjects)
                drawHelpers[objectOnDisplay.GameObject.GetType()].Invoke(objectOnDisplay,gl);

            gl.Disable(OpenGL.GL_BLEND);

            gl.Flush();
        }

        #endregion

        #region Private Methods

        private void HelperInitialization()
        {
            foreach (var drawClass in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IDrawable).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract))
            {
                var instance = Activator.CreateInstance(drawClass);
                var typeGameObject = (Type)drawClass.GetProperty("GameObjectType").GetValue(instance, null);
                var drawingMethod = (Action<ObjectOnDisplay, OpenGL>)drawClass.GetMethod("DrawObject")
                    .CreateDelegate(typeof(Action<ObjectOnDisplay, OpenGL>), instance);

                drawHelpers.Add(typeGameObject, drawingMethod);
            }

        }

        private void FrameTick(List<GameObject> objectsToDraw, BasicCamera gameBasicCamera)
        {
            DrawObjects = (from objectToDraw in objectsToDraw.OrderByDescending(x => x.DrawingPriority)
                           let positionOnDisplay = gameBasicCamera.GamePositionToWindow(objectToDraw.Position)
                           select new ObjectOnDisplay(objectToDraw, positionOnDisplay)).ToList();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //e.Graphics.DrawString($"fps {fps.ToString()}", new Font("Arial", 40, FontStyle.Bold),
            //    new SolidBrush(Color.Yellow), 0, 0);
            //Update();
        }

        private void Update()
        {
            framesRendered++;

            if (!((DateTime.Now - lastTime).TotalSeconds >= 1)) return;
            fps = framesRendered;
            framesRendered = 0;
            lastTime = DateTime.Now;
        }

        #endregion

    }
}