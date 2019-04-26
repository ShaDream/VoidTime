using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public static class SetGunPrompt
    {
        public static int ShowDialog(int[] slots)
        {
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
            var inputBox = new ComboBox
            {
                Location = new Point(50, 25),
                Size = new Size(50, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.Black,
                ForeColor = Color.White
            };
            foreach (var slot in slots) inputBox.Items.Add(slot);
            var confirmation = new Button
            {
                Text = "Set slot",
                Location = new Point(150, 25),
                Size = new Size(100, 25),
                ForeColor = Color.White
            };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(panel);
            prompt.ShowDialog();
            return (int) (inputBox.SelectedItem ?? -1);
        }
    }
}