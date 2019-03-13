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

        Texture texture = new Texture();

        public static Texture player = new Texture();
        public static Texture background = new Texture();

        #endregion

        #region Constructor

        public MainForm(GameModel model)
        {

            openGL = new OpenGLControl
            {
                DrawFPS = true,
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

            KeyUp += model.OnKeyRelease;
            KeyDown += model.OnKeyPress;
            SizeChanged += (s, a) => model.GameBasicCamera.Size = Size;
            model.GameBasicCamera.Size = Size;
            HelperInitialization();
            
            //model.Run();
        }

        private void OpenGL_Resized(object sender, EventArgs e)
        {
            var gl = openGL.OpenGL;
            //gImage1 = new Bitmap(@"C:\Users\ShaDream\source\repos\VoidTime\VoidTime\Resources\Textures\back.png");// Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)

            //Rectangle rect = new Rectangle(0, 0, gImage1.Width, gImage1.Height);
            //gbitmapdata = gImage1.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //gImage1.UnlockBits(gbitmapdata);
            //gl.GenTextures(1, gtexture);
            //gl.BindTexture(OpenGL.GL_TEXTURE_2D, gtexture[0]);
            //gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, (int)OpenGL.GL_RGB8, gImage1.Width, gImage1.Height, 0, OpenGL.GL_BGR_EXT, OpenGL.GL_UNSIGNED_BYTE, gbitmapdata.Scan0);
            //uint[] array = new uint[] { OpenGL.GL_NEAREST };


            //  Create an orthographic projection.
            //gl.MatrixMode(MatrixMode.Projection);
            //gl.LoadIdentity();

            // NOTE: Basically no matter what I do, the only points I see are those at
            // the "near" surface (with z = -zNear)--in this case, I only see green points
            //gl.Ortho(0, openGL.Width, openGL.Height, 0, 0,1);

            //  Back to the modelview.
            //gl.MatrixMode(MatrixMode.Modelview);

        }

        private void OpenGL_OpenGLInitialized(object sender, EventArgs e)
        {
            var gl = openGL.OpenGL;

            gl.Enable(OpenGL.GL_TEXTURE_2D);
            //gl.Disable(OpenGL.GL_DEPTH_TEST);

            //player.Create(openGL.OpenGL,
            //    new Bitmap(@"C:\Users\ShaDream\source\repos\VoidTime\VoidTime\Resources\Textures\player.png"));
            //background.Create(openGL.OpenGL,
            //    new Bitmap(@"C:\Users\ShaDream\source\repos\VoidTime\VoidTime\Resources\Textures\back.png"));
            texture.Create(gl, @"C:\Users\ShaDream\source\repos\VoidTime\VoidTime\Resources\Textures\player.png");

        }

        private void OpenGLDraw(object sender, RenderEventArgs args)
        {
            var gl = openGL.OpenGL;

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.MatrixMode(MatrixMode.Projection);
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -6.0f);


            texture.Bind(gl);
            //  Draw the sprite
            gl.Begin(OpenGL.GL_QUADS);
            gl.TexCoord(0.0f, 0.0f); gl.Vertex(-1.0f, -1.0f, 1.0f); // Bottom Left Of The Texture and Quad
            gl.TexCoord(1.0f, 0.0f); gl.Vertex(1.0f, -1.0f, 1.0f);  // Bottom Right Of The Texture and Quad
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(1.0f, 1.0f, 1.0f);   // Top Right Of The Texture and Quad
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(-1.0f, 1.0f, 1.0f);
            gl.End();

            gl.Flush();
            //foreach (var objectOnDisplay in DrawObjects)
            //    drawHelpers[objectOnDisplay.GameObject.GetType()](objectOnDisplay, gl);
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