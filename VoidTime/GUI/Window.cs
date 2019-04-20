using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class Window
    {
        private readonly Label EnterLabel;
        private readonly InventoryPanel InventoryMenu;
        public Keys lastKey = Keys.None;
        private readonly MainForm owner;
        private readonly PausePanel PauseMenu;
        private readonly Player ship;
        private readonly PlanetPanel TradeMenu;
        private readonly HashSet<Keys> windowKeys = new HashSet<Keys> {Keys.F, Keys.Escape, Keys.E};
        private readonly Dictionary<Keys, ISwitcheble> windows;

        public Window(MainForm form, Player ship)
        {
            owner = form;
            this.ship = ship;
            ship.EnterChanged += UpdateEnterLabel;

            EnterLabel = new Label
            {
                Text = "Press F",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(new FontFamily("Arial"), 24),
                ForeColor = Color.White,
                Size = new Size(150, 50),
                Location = new Point(900, 800)
            };

            TradeMenu = new PlanetPanel(owner, this, ship);
            PauseMenu = new PausePanel(owner, this);
            InventoryMenu = new InventoryPanel(owner, this, ship);
            windows = new Dictionary<Keys, ISwitcheble>
            {
                {Keys.F, TradeMenu},
                {Keys.Escape, PauseMenu},
                {Keys.E, InventoryMenu}
            };
            owner.SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            var form = sender as MainForm;
            ChangeSize(PauseMenu, form, new SizeF(0.3f, 0.3f), false);
            ChangeSize(TradeMenu, form, new SizeF(0.6f, 0.6f));
            ChangeSize(InventoryMenu, form, new SizeF(0.6f, 0.6f));
        }

        private void ChangeSize(BasicGameWindow window, MainForm form, SizeF size, bool isChange = true)
        {
            if (isChange)
            {
                window.Size = new Size((int)(form.Size.Width * size.Width),
                                       (int)(form.Size.Height * size.Height));
            }
            window.Location = new Point((form.Size.Width - window.Size.Width) / 2,
                                        (form.Size.Height - window.Size.Height) / 2);
        }

        private void UpdateEnterLabel(bool isShow)
        {
            if (isShow)
                owner.BeginInvoke(new Action(() =>
                {
                    owner.Controls.Add(EnterLabel);
                    EnterLabel.BringToFront();
                }));
            else
                owner.BeginInvoke(new Action(() => { owner.Controls.Remove(EnterLabel); }));
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
                if (args.KeyCode == Keys.F && ship.EnterObject == null)
                    return;
                windows[args.KeyCode].Switch();
                lastKey = args.KeyCode;
            }
        }
    }
}