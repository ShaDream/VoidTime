using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace VoidTime
{
    public class MainForm : Form
    {
        #region Public Properties

        public List<Vector2D> DrawObjects = new List<Vector2D>();
        public Image plane = new Bitmap(@"C:\Users\Иван\Documents\Projects\TestFormApp\plane.png");

        #endregion

        #region Constructor

        public MainForm(GameModel model)
        {
            DoubleBuffered = true;
            Size = new Size(800, 800);

            model.Tick += FrameTick;

            KeyUp += model.OnKeyRelease;
            KeyDown += model.OnKeyPress;


            model.Run();
        }

        #endregion

        #region Private Methods

        private void FrameTick(List<GameObject> objectsToDraw, BasicCamera gameBasicCamera)
        {
            var positions = objectsToDraw.Select(x => gameBasicCamera.GamePositionToWindow(x.Position)).ToList();
            DrawObjects = positions;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var pen = new Pen(Color.Black);
            foreach (var drawObject in DrawObjects)
            {
                e.Graphics.DrawImage(plane, drawObject.X, drawObject.Y, 50, 50);
            }
        }

        #endregion

    }
}