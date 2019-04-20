using System;
using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class PausePanel : BasicGameWindow
    {
        public PausePanel(MainForm form, Window owner)
        {
            this.form = form;
            this.owner = owner;
            var pausePanel = new TableLayoutPanel
            {
                Size = new Size(250, 300),
                Location = new Point(100, 100)
            };
            pausePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
            pausePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            pausePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
            for (var i = 0; i < 5; i++)
                pausePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            
            var exitButton = new Button
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Text = "Exit",
                Size = new Size(150, 50),
                Font = new Font(new FontFamily("Arial"), 24),
            };
            var returnButton = new Button
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Text = "Resume",
                Size = new Size(150, 50),
                Font = new Font(new FontFamily("Arial"), 24),
            };
            exitButton.Click += (s, a) => form.Close();
            returnButton.Click += (s, a) => Switch();
            pausePanel.Controls.Add(returnButton, 1, 1);
            pausePanel.Controls.Add(exitButton, 1, 3);
            window = pausePanel;
        }
    }
}