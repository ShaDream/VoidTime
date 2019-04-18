using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class Window
    {
        private MainForm owner;
        private Player player;
        private PlanetPanel TradeMenu;
        private PausePanel PauseMenu;
        private HashSet<Keys> windowKeys = new HashSet<Keys>{ Keys.F, Keys.Escape };
        private Dictionary<Keys, ISwitcheble> windows;
        public Keys lastKey = Keys.None;
        private Label EnterLabel;
        public Window(MainForm form, Player player)
        {
            owner = form;
            this.player = player;
            player.EnterChanged += UpdateEnterLabel;

            EnterLabel = new Label
            {
                Text = "Press F",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(new FontFamily("Arial"), 24),
                ForeColor = Color.White,
                Size = new Size(150, 50),
                Location = new Point(900, 800),

            };

            TradeMenu = new PlanetPanel(owner, this, player);
            PauseMenu = new PausePanel(owner, this);
            windows = new Dictionary<Keys, ISwitcheble>
            {
                { Keys.F, TradeMenu },
                { Keys.Escape, PauseMenu }
            };
            owner.SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            var form = sender as MainForm;
            EnterLabel.Location = new Point(
                (form.Size.Width - EnterLabel.Size.Width) / 2,
                (form.Size.Height - EnterLabel.Size.Height) * 9 / 10);
            TradeMenu.Size = new Size(form.Size.Width * 3 / 6, form.Size.Height * 4 / 6);
            TradeMenu.Location = new Point(form.Size.Width * 1 / 4, form.Size.Height * 1 / 6);
            PauseMenu.Location = new Point((form.Size.Width - PauseMenu.Size.Width) / 2,
                                           (form.Size.Height - PauseMenu.Size.Height) / 2);
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

        public void OnKeyPress(object sender, KeyEventArgs args)
        {
            if (lastKey != Keys.None)
            {
                if (args.KeyCode != lastKey) return;
                windows[args.KeyCode].Switch();

            }
            else
            {
                if (!windowKeys.Contains(args.KeyCode)) return;
                if (args.KeyCode == Keys.F && player.EnterObject == null)
                    return;
                windows[args.KeyCode].Switch();
                lastKey = args.KeyCode;
            }
        }
    }
}