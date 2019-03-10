using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace VoidTime
{
    public class MainForm : Form
    {
        #region Public Properties

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

        private void FrameTick(List<GameObject> objectsToDraw, BasicCamera gameBasicCamera)
        {
            DrawObjects = (from objectToDraw in objectsToDraw.OrderByDescending(x => x.DrawingPriority)
                           let positionOnDisplay = gameBasicCamera.GamePositionToWindow(objectToDraw.Position)
                           select new ObjectOnDisplay(objectToDraw, positionOnDisplay)).ToList();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {

        }

        #endregion

    }
}