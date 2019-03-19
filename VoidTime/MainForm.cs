using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph.Assets;
using VoidTime.GUI;
using VoidTime.Resources;
using TextRenderer = VoidTime.GUI.TextRenderer;

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
        public static Texture mars = new Texture();
        public static Texture FontTexture = new Texture();

        public static FontAtlas Font = EuropeFontAtlas.GetAtlas();
        public static List<QuadData> Text;
        public static int fontSize = 1;

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
                DrawFPS = true,
            };

            ((System.ComponentModel.ISupportInitialize)(openGL)).BeginInit();
            openGL.OpenGLDraw += OpenGLDraw;
            openGL.OpenGLInitialized += OpenGL_OpenGLInitialized;

            Controls.Add(openGL);
            ((System.ComponentModel.ISupportInitialize)(openGL)).EndInit();

            WindowState = FormWindowState.Maximized;
            ShowIcon = false;
            model.Tick += FrameTick;

            openGL.KeyUp += model.OnKeyRelease;
            openGL.KeyDown += model.OnKeyPress;
            SizeChanged += (s, a) => model.GameBasicCamera.Size = Size;
            MouseWheel += (sender, args) =>
            {
                fontSize += Math.Sign(args.Delta);
                Text = TextRenderer.GetTextQuads("Hello, World!", Font, new Vector2D(0, 500), fontSize);
            };

            model.GameBasicCamera.Size = Size;

            model.Run();
        }

        private void OpenGL_OpenGLInitialized(object sender, EventArgs e)
        {
            var gl = openGL.OpenGL;
            gl.Enable(OpenGL.GL_TEXTURE_2D);

            player.Create(openGL.OpenGL,
                Textures.player);
            background.Create(openGL.OpenGL,
                Textures.back);
            mars.Create(openGL.OpenGL,
                Textures.time);
            FontTexture.Create(openGL.OpenGL,
                Textures.EuropeFont);

            Text = TextRenderer.GetTextQuadsTextBox("Hello, World!", Font, 72, new RectangleF(new PointF(0, 500), new Size(100, 500)));
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

            foreach (var objectOnDisplay in DrawObjects)
                drawHelpers[objectOnDisplay.GameObject.GetType()].Invoke(objectOnDisplay, gl);

            FontTexture.Bind(gl);

            gl.Begin(OpenGL.GL_QUADS);

            foreach (var letter in Text)
            {
                gl.TexCoord(letter.Textures[0].X, letter.Textures[0].Y);
                gl.Vertex(letter.Points[0].X, letter.Points[0].Y, 0);
                gl.TexCoord(letter.Textures[1].X, letter.Textures[1].Y);
                gl.Vertex(letter.Points[1].X, letter.Points[1].Y, 0);
                gl.TexCoord(letter.Textures[2].X, letter.Textures[2].Y);
                gl.Vertex(letter.Points[2].X, letter.Points[2].Y, 0);
                gl.TexCoord(letter.Textures[3].X, letter.Textures[3].Y);
                gl.Vertex(letter.Points[3].X, letter.Points[3].Y, 0);
            }

            gl.End();

            gl.Disable(OpenGL.GL_BLEND);
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