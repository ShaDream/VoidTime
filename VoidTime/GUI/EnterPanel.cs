using System;
using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class EnterPanel : BasicGameWindow
    {
        public EnterPanel(MainForm form, Window owner)
        {
            this.form = form;
            this.owner = owner;
            window = new Label
            {
                Text = "Press F",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(new FontFamily("Arial"), 24),
                ForeColor = Color.FromArgb(233, 238, 201),
                BackColor = Color.Black,
                Size = new Size(150, 50),
                Location = new Point(900, 800)
            };
        }

        protected override void Show()
        {
            form.BeginInvoke(new Action(() =>
            {
                form.Controls.Add(window);
                window.BringToFront();
            }));
        }

        protected override void Hide()
        {
            form.BeginInvoke(new Action(() => { form.Controls.Remove(window); }));
        }

        public override void Switch()
        {
            if (isShow)
                Hide();
            else
                Show();
            isShow = !isShow;
        }
    }
}