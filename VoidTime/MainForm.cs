using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SharpGL;
using SharpGL.Enumerations;
using TextRenderer = VoidTime.GUI.TextRenderer;

namespace VoidTime
{
    public class MainForm : Form
    {
        #region Public Properties

        private readonly OpenGLControl openGL;

        private readonly Dictionary<Type, Action<ObjectOnDisplay, OpenGL>> drawHelpers =
            new Dictionary<Type, Action<ObjectOnDisplay, OpenGL>>();

        private List<ObjectOnDisplay> DrawObjects = new List<ObjectOnDisplay>();

        #endregion

        #region Constructor

        public MainForm(GameModel model)
        {
            openGL = new OpenGLControl
            {
                FrameRate = 60,
                RenderContextType = RenderContextType.NativeWindow,
                RenderTrigger = RenderTrigger.TimerBased,
                Dock = DockStyle.Fill,
                DrawFPS = true
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
            MouseWheel += (sender, args) =>
            {
                fontSize += Math.Sign(args.Delta);
                Text = TextRenderer.GetTextQuads("Hello, World!", Font, new Vector2D(0, 500), fontSize);
            };

            model.GameBasicCamera.Size = Size;

            model.Run();
        }

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