using System;
using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class PausePanel : ISwitcheble
    {
        private MainForm form;
        private TableLayoutPanel pausePanel;
        private Window owner;
        public bool isShow { get; private set; }
        public Point Location
        {
            get => pausePanel.Location;
            set => pausePanel.Location = value;
        }
        public Size Size
        {
            get => pausePanel.Size;
            set => pausePanel.Size = value;
        }

        public PausePanel(MainForm form, Window owner)
        {
            this.form = form;
            this.owner = owner;
            pausePanel = new TableLayoutPanel
            {
                Size = new Size(250, 250),
                Location = new Point(100, 100)
            };
            pausePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            pausePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            pausePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            var exitButton = new Button
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Text = "Exit",
                Size = new Size(150, 50),
                Font = new Font(new FontFamily("Arial"), 24),
                Margin = new Padding(50, 25, 0, 0)
            };
            var returnButton = new Button
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Text = "Resume",
                Size = new Size(150, 50),
                Font = new Font(new FontFamily("Arial"), 24),
                Margin = new Padding(50, 50, 0, 0)
            };
            exitButton.Click += (s, a) => form.Close();
            returnButton.Click += (s, a) => Switch();
            pausePanel.Controls.Add(returnButton, 0, 0);
            pausePanel.Controls.Add(exitButton, 0, 1);
        }
        private void Hide()
        {
            form.BeginInvoke(new Action(() =>
            {
                form.Controls.Remove(pausePanel);
                form.currentModel.Run();
            }));
            owner.lastKey = Keys.None;
        }

        private void Show()
        {
            form.BeginInvoke(new Action(() =>
            {
                form.Controls.Add(pausePanel);
                pausePanel.BringToFront();
                form.currentModel.Pause();
            }));
        }

        public void Switch()
        {
            if (isShow)
                Hide();
            else
                Show();
            isShow = !isShow;
        }
    }
}