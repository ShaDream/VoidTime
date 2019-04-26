using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public static class UpgradePrompt
    {
        public static bool ShowDialog()
        {
            var result = false;
            var prompt = new Form
            {
                Width = 300,
                Height = 75,
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.CenterScreen,
                BackColor = Color.Black
            };
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.Fixed3D,
                BackColor = Color.Black
            };
            var buttonTrue = new Button
            {
                Text = "Yes",
                ForeColor = Color.White,
                Location = new Point(25, 30),
                Size = new Size(100, 25)
            };

            buttonTrue.Click += (s, a) =>
            {
                result = true;
                prompt.Close();
            };

            var buttonFalse = new Button
            {
                Text = "No",
                ForeColor = Color.White,
                Location = new Point(175, 30),
                Size = new Size(100, 25)
            };

            buttonFalse.Click += (s, a) =>
            {
                prompt.Close();
            };

            var text = new Label()
            {
                ForeColor = Color.White,
                Text = "Upgrade ?",
                Location = new Point(125, 10)
            };

            prompt.Controls.Add(buttonTrue);
            prompt.Controls.Add(buttonFalse);
            prompt.Controls.Add(text);
            prompt.Controls.Add(panel);
            
            prompt.ShowDialog();

            return result;
        }
    }
}