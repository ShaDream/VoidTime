using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class HPBar : ProgressBar
    {
        public HPBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rec = e.ClipRectangle;
            rec.Height = rec.Height - 3;
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(187, 187, 187)), 0, 0, rec.Width, rec.Height);
            rec.Width = (int)(rec.Width * ((double)Value / Maximum));
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(145, 53, 53)),  0, 0, rec.Width, rec.Height);
        }
    }
}