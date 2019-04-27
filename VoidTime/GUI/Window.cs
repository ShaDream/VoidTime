using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class Window
    {
        private readonly EnterPanel enterMenu;
        private readonly InventoryPanel inventoryMenu;
        private readonly MainForm owner;
        private readonly PausePanel pauseMenu;
        private readonly Player ship;
        private readonly StatsPanel statsMenu;
        private readonly PlanetPanel tradeMenu;
        private readonly StatusBar statusBar;
        private readonly HashSet<Keys> windowKeys = new HashSet<Keys> {Keys.F, Keys.Escape, Keys.E, Keys.Q};
        private readonly Dictionary<Keys, ISwitcheble> windows;
        public Keys lastKey = Keys.None;

        public Window(MainForm form, Player ship)
        {
            owner = form;
            this.ship = ship;
            ship.EnterChanged += UpdateEnterLabel;

            enterMenu = new EnterPanel(owner, this);
            tradeMenu = new PlanetPanel(owner, this, ship);
            pauseMenu = new PausePanel(owner, this);
            inventoryMenu = new InventoryPanel(owner, this, ship);
            statsMenu = new StatsPanel(owner, this, ship);
            statusBar = new StatusBar(owner, this, ship);
            windows = new Dictionary<Keys, ISwitcheble>
            {
                {Keys.F, tradeMenu},
                {Keys.Escape, pauseMenu},
                {Keys.E, inventoryMenu},
                {Keys.Q, statsMenu}
            };
            owner.SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            var form = sender as MainForm;
            ChangeSize(pauseMenu, form, new SizeF(), new PointF(0.5f, 0.5f), false);
            ChangeSize(tradeMenu, form, new SizeF(0.6f, 0.6f), new PointF(0.5f, 0.5f));
            ChangeSize(inventoryMenu, form, new SizeF(0.6f, 0.6f), new PointF(0.5f, 0.5f));
            ChangeSize(statsMenu, form, new SizeF(0.2f, 0.6f), new PointF(0.5f, 0.5f));
            ChangeSize(enterMenu, form, new SizeF(), new PointF(0.5f, 0.9f), false);
            ChangeSize(statusBar, form, new SizeF(1f, 0.0261f), new PointF(0.5f, 1f));
            statusBar.Resize();
        }

        private void ChangeSize(BasicGameWindow window, MainForm form, SizeF size, PointF center, bool isChange = true)
        {
            if (isChange)
                window.Size = new Size((int) (form.Size.Width * size.Width),
                                       (int) (form.Size.Height * size.Height));
            window.Location = new Point((int) ((form.Size.Width - window.Size.Width) * center.X),
                                        (int) ((form.Size.Height - window.Size.Height) * center.Y));
        }

        private void UpdateEnterLabel(bool isShow)
        {
            enterMenu.Switch();
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