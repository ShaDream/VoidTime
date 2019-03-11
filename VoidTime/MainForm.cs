using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VoidTime
{
    public class MainForm : Form
    {
        #region Public Properties

        private DateTime lastTime;
        private int framesRendered;
        private int fps;
        private Dictionary<Type, Action<ObjectOnDisplay, Graphics>> drawHelpers =
            new Dictionary<Type, Action<ObjectOnDisplay, Graphics>>();

        private List<ObjectOnDisplay> DrawObjects = new List<ObjectOnDisplay>();

        #endregion

        #region Constructor

        public MainForm(GameModel model)
        {
            DoubleBuffered = true;
            WindowState = FormWindowState.Maximized;
            ShowIcon = false;
            model.Tick += FrameTick;

            KeyUp += model.OnKeyRelease;
            KeyDown += model.OnKeyPress;
            SizeChanged += (s, a) => model.GameBasicCamera.Size = Size;
            model.GameBasicCamera.Size = Size;
            HelperInitialization();
            model.Run();
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
                var drawingMethod = (Action<ObjectOnDisplay, Graphics>)drawClass.GetMethod("DrawObject")
                    .CreateDelegate(typeof(Action<ObjectOnDisplay, Graphics>), instance);

                drawHelpers.Add(typeGameObject, drawingMethod);
            }

        }

        private void FrameTick(List<GameObject> objectsToDraw, BasicCamera gameBasicCamera)
        {
            DrawObjects = (from objectToDraw in objectsToDraw.OrderByDescending(x => x.DrawingPriority)
                           let positionOnDisplay = gameBasicCamera.GamePositionToWindow(objectToDraw.Position)
                           select new ObjectOnDisplay(objectToDraw, positionOnDisplay)).ToList();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (var objectOnDisplay in DrawObjects)
                drawHelpers[objectOnDisplay.GameObject.GetType()](objectOnDisplay, e.Graphics);
            e.Graphics.DrawString($"fps {fps.ToString()}", new Font("Arial", 40, FontStyle.Bold),
                new SolidBrush(Color.Yellow), 0, 0);
            Update();
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