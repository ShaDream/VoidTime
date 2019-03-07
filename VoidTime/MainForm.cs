using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace VoidTime
{
    public class MainForm : Form
    {
        public List<Vector2D> DrawObjects = new List<Vector2D>();
        public Image plane = new Bitmap(@"D:\Downloads\UFO.png");

        public MainForm(GameModel model)
        {
            DoubleBuffered = true;
            Size = new Size(800, 800);

            model.Tick += FrameTick;

            KeyUp += model.OnKeyRelease;
            KeyDown += model.OnKeyPress;


            model.Run();
        }


        private void FrameTick(List<GameObject> objectsToDraw, List<Vector2D> position)
        {
            DrawObjects = position;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var pen = new Pen(Color.Black);
            foreach (var drawObject in DrawObjects)
            {
                e.Graphics.DrawImage(plane, drawObject.X, drawObject.Y, 100, 50);
            }
        }
    }
}