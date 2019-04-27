using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class InventoryPanel : BasicGameWindow
    {
        private readonly Button exitButton;
        private readonly ListBox installedChips;
        private readonly ListBox installedGuns;
        private readonly ListBox items;
        private readonly Button setRemoveButton;
        private readonly Player ship;

        public InventoryPanel(MainForm form, Window owner, Player ship)
        {
            this.form = form;
            this.owner = owner;
            this.ship = ship;
            Action setRemoveClick = () => { };

            var inventoryPanel = new TableLayoutPanel
            {
                BackColor = Color.FromArgb(25, 34, 57),
                Size = new Size(250, 300),
                Location = new Point(100, 100)
            };
            inventoryPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            inventoryPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            inventoryPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 90));
            inventoryPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            var tabs = new TabControl
            {
                BackColor = Color.FromArgb(40, 49, 72),
                Dock = DockStyle.Fill
            };
            tabs.TabPages.Add("Installed Chips");
            tabs.TabPages.Add("Installed Guns");

            for (var i = 0; i < tabs.TabCount; i++)
            {
                tabs.TabPages[i].BackColor = Color.FromArgb(40, 49, 72);
                tabs.TabPages[i].BorderStyle = BorderStyle.None;
            }

            var tabInventory = new TabControl
            {
                BackColor = Color.FromArgb(40, 49, 72),
                Dock = DockStyle.Fill
            };
            tabInventory.TabPages.Add("Inventory");
            tabInventory.TabPages[0].BackColor = Color.FromArgb(40, 49, 72);
            tabInventory.TabPages[0].BorderStyle = BorderStyle.None;

            items = new ListBox
            {
                BackColor = Color.FromArgb(40, 49, 72),
                BorderStyle = BorderStyle.None,
                ForeColor = Color.FromArgb(233, 238, 201),
                Dock = DockStyle.Fill
            };

            installedChips = new ListBox
            {
                BackColor = Color.FromArgb(40, 49, 72),
                BorderStyle = BorderStyle.None,
                ForeColor = Color.FromArgb(233, 238, 201),
                Dock = DockStyle.Fill
            };
            installedGuns = new ListBox
            {
                BackColor = Color.FromArgb(40, 49, 72),
                BorderStyle = BorderStyle.None,
                ForeColor = Color.FromArgb(233, 238, 201),
                Dock = DockStyle.Fill
            };

            tabs.TabPages[0].Controls.Add(installedChips);
            tabs.TabPages[1].Controls.Add(installedGuns);
            tabInventory.TabPages[0].Controls.Add(items);

            setRemoveButton = new Button
            {
                Visible = false,
                BackColor = Color.FromArgb(40, 49, 72),
                ForeColor = Color.FromArgb(233, 238, 201),
                Dock = DockStyle.Fill
            };
            exitButton = new Button
            {
                BackColor = Color.FromArgb(40, 49, 72),
                ForeColor = Color.FromArgb(233, 238, 201),
                Text = "Exit",
                Dock = DockStyle.Fill
            };

            items.SelectedIndexChanged += (s, a) =>
            {
                var item = items.SelectedItem as IItem;
                if (item == null)
                    return;
                setRemoveButton.Visible = true;
                setRemoveButton.Text = "Install";
                switch (item)
                {
                    case Chip _:
                        setRemoveClick = () =>
                        {
                            ship.Inventory.InstallChip(item as Chip);
                            Update();
                        };
                        break;
                    case GunData gun:
                        setRemoveClick = () =>
                        {
                            var slots = ship.Data.GetSlots().Where(x => x.MaxTier >= gun.Tier);
                            if (!slots.Any())
                                return;
                            var result = SetGunPrompt.ShowDialog(slots.Select(x => x.SlotId).ToArray());
                            if (result == -1)
                                return;
                            ship.Data.SetGun(gun, result);
                            ship.Inventory.Remove(gun);
                            Update();
                        };
                        break;
                }
            };

            items.MouseDoubleClick += (s, a) =>
            {
                if (items.SelectedItem == null)
                    return;
                setRemoveButton.PerformClick();
            };

            installedChips.SelectedIndexChanged += (s, a) =>
            {
                if (!(installedChips.SelectedItem is Chip item))
                    return;
                setRemoveButton.Visible = true;
                setRemoveButton.Text = "Uninstall";
                setRemoveClick = () =>
                {
                    ship.Chips.Remove(item);
                    Update();
                };
            };

            installedChips.MouseDoubleClick += (s, a) =>
            {
                if (installedChips.SelectedItem == null)
                    return;
                setRemoveButton.PerformClick();
            };

            installedGuns.SelectedIndexChanged += (s, a) =>
            {
                if (!(installedGuns.SelectedItem is GunData item))
                    return;
                setRemoveButton.Visible = true;
                setRemoveButton.Text = "Uninstall";
                setRemoveClick = () =>
                {
                    ship.Data.RemoveGun(item.Slot);
                    Update();
                };
            };

            installedGuns.MouseDoubleClick += (s, a) =>
            {
                if (installedGuns.SelectedItem == null)
                    return;
                setRemoveButton.PerformClick();
            };

            setRemoveButton.Click += (s, a) => setRemoveClick();
            exitButton.Click += (s, a) => Switch();

            inventoryPanel.Controls.Add(tabs, 0, 0);
            inventoryPanel.Controls.Add(tabInventory, 1, 0);
            inventoryPanel.Controls.Add(setRemoveButton, 0, 1);
            inventoryPanel.Controls.Add(exitButton, 1, 1);

            window = inventoryPanel;
        }

        protected override void Update()
        {
            setRemoveButton.Visible = false;
            items.Items.Clear();
            foreach (var item in ship.Inventory.GetItems) items.Items.Add(item);
            installedChips.Items.Clear();
            foreach (var item in ship.Chips.GetChips) installedChips.Items.Add(item);
            installedGuns.Items.Clear();
            foreach (var item in ship.Data.ShipStats.Guns) installedGuns.Items.Add(item);
        }
    }
}