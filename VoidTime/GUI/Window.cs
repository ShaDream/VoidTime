using System;
using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class Window
    {
        private MainForm owner;
        private Player player;

        private Label EnterLabel;
        private PlanetPanel panel;

        public Window(MainForm form, Player player)
        {
            owner = form;
            this.player = player;
            player.EnterChanged += UpdateEnterLabel;
            player.Entering += PlayerEnter;

            EnterLabel = new Label
            {
                Text = "[E]nter",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(new FontFamily("Arial"), 24),
                ForeColor = Color.White,
                Size = new Size(110, 50),
                Location = new Point(900, 800)
            };

            panel = new PlanetPanel(owner, player);
        }

        private void PlayerEnter()
        {
            panel.Show();
        }

        private void UpdateEnterLabel(bool isShow)
        {
            if (isShow)
            {
                owner.BeginInvoke(new Action(() =>
                {
                    owner.Controls.Add(EnterLabel);
                    EnterLabel.BringToFront();
                }));
            }
            else
                owner.BeginInvoke(new Action(() =>
                {
                    owner.Controls.Remove(EnterLabel);
                }));
        }
    }
}