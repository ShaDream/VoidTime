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
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(187, 187, 187)), 0, 0, Size.Width, Size.Height);
            var value = (int)(Size.Width * ((double)Value / Maximum));
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(145, 53, 53)),  0, 0, value, Size.Height);
        }
    }
}