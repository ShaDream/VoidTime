using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class Window
    {
        public Keys lastKey = Keys.None;
        private readonly MainForm owner;
        private readonly Player ship;
        private readonly PausePanel PauseMenu;
        private readonly PlanetPanel TradeMenu;
        private readonly StatsPanel StatsMenu;
        private readonly InventoryPanel InventoryMenu;
        private readonly EnterPanel EnterMenu;
        private readonly HashSet<Keys> windowKeys = new HashSet<Keys> { Keys.F, Keys.Escape, Keys.E, Keys.Q };
        private readonly Dictionary<Keys, ISwitcheble> windows;

        public Window(MainForm form, Player ship)
        {
            owner = form;
            this.ship = ship;
            ship.EnterChanged += UpdateEnterLabel;

            EnterMenu = new EnterPanel(owner, this);
            TradeMenu = new PlanetPanel(owner, this, ship);
            PauseMenu = new PausePanel(owner, this);
            InventoryMenu = new InventoryPanel(owner, this, ship);
            StatsMenu = new StatsPanel(owner, this, ship);
            windows = new Dictionary<Keys, ISwitcheble>
            {
                {Keys.F, TradeMenu},
                {Keys.Escape, PauseMenu},
                {Keys.E, InventoryMenu},
                {Keys.Q, StatsMenu }
            };
            owner.SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            var form = sender as MainForm;
            ChangeSize(PauseMenu, form, new SizeF(), new PointF(0.5f, 0.5f), false);
            ChangeSize(TradeMenu, form, new SizeF(0.6f, 0.6f), new PointF(0.5f, 0.5f));
            ChangeSize(InventoryMenu, form, new SizeF(0.6f, 0.6f), new PointF(0.5f, 0.5f));
            ChangeSize(StatsMenu, form, new SizeF(0.2f, 0.6f), new PointF(0.5f, 0.5f));
            ChangeSize(EnterMenu, form, new SizeF(), new PointF(0.5f, 0.9f), false);
        }

        private void ChangeSize(BasicGameWindow window, MainForm form, SizeF size, PointF center, bool isChange = true)
        {
            if (isChange)
            {
                window.Size = new Size((int)(form.Size.Width * size.Width),
                                       (int)(form.Size.Height * size.Height));
            }
            window.Location = new Point((int)((form.Size.Width - window.Size.Width) * center.X),
                                        (int)((form.Size.Height - window.Size.Height) * center.Y));
        }

        private void UpdateEnterLabel(bool isShow)
        {
            EnterMenu.Switch();
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