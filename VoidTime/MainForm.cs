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

        private Dictionary<Type, Action<ObjectOnDisplay, Graphics>> drawHelpers = new Dictionary<Type, Action<ObjectOnDisplay, Graphics>>();

        public List<ObjectOnDisplay> DrawObjects = new List<ObjectOnDisplay>();

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

            model.Run();
        }

        #endregion

        #region Private Methods

        private void HelperInitialization()
        {
            foreach (var drawClass in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IDrawable).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract))
            {
                drawHelpers.Add(
                    (Type)drawClass.GetProperty("GameObjectType", BindingFlags.Static).GetValue(null, null),
                    (Action<ObjectOnDisplay, Graphics>)drawClass.GetMethod("DrawObject", BindingFlags.Static)
                        .CreateDelegate(typeof(Action<ObjectOnDisplay, Graphics>)));
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
            {
                drawHelpers[objectOnDisplay.GetType()]?.Invoke(objectOnDisplay, e.Graphics);
            }
        }

        #endregion

    }
}