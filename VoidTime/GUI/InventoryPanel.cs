using System;
using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class InventoryPanel : BasicGameWindow
    {
        private Player ship;
        private ListBox items;
        private ListBox installedChips;
        private ListBox installedGuns;
        private Button setRemoveButton;
        private Button exitButton;

        public InventoryPanel(MainForm form, Window owner, Player ship)
        {
            this.form = form;
            this.owner = owner;
            this.ship = ship;
            Action setRemoveClick = () => { };

            var inventoryPanel = new TableLayoutPanel
            {
                Size = new Size(250, 300),
                Location = new Point(100, 100)
            };
            inventoryPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            inventoryPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            inventoryPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 90));
            inventoryPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            var tabs = new TabControl
            {
                BackColor = Color.Black,
                Dock = DockStyle.Fill
            };
            tabs.TabPages.Add("Installed Chips");
            tabs.TabPages.Add("Installed Guns");

            for (var i = 0; i < tabs.TabCount; i++)
            {
                tabs.TabPages[i].BackColor = Color.Black;
                tabs.TabPages[i].BorderStyle = BorderStyle.None;
            }
            var tabInventory = new TabControl
            {
                BackColor = Color.Black,
                Dock = DockStyle.Fill
            };
            tabInventory.TabPages.Add("Inventory");
            tabInventory.TabPages[0].BackColor = Color.Black;
            tabInventory.TabPages[0].BorderStyle = BorderStyle.None;

            items = new ListBox
            {
                BackColor = Color.Black,
                BorderStyle = BorderStyle.None,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
            };

            installedChips = new ListBox
            {
                BackColor = Color.Black,
                BorderStyle = BorderStyle.None,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
            };
            installedGuns = new ListBox
            {
                BackColor = Color.Black,
                BorderStyle = BorderStyle.None,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
            };

            tabs.TabPages[0].Controls.Add(installedChips);
            tabs.TabPages[1].Controls.Add(installedGuns);
            tabInventory.TabPages[0].Controls.Add(items);

            setRemoveButton = new Button()
            {
                Visible = false,
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Fill
            };
            exitButton = new Button
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
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
                if (item is Chip)
                    setRemoveClick = () =>
                    {
                        ship.Inventory.InstallChip(item as Chip);
                        Update();
                    };
            };
            items.MouseDoubleClick += (s, a) =>
            {
                if (!(items.SelectedItem is Chip item))
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
                if (!(installedChips.SelectedItem is Chip item))
                    return;
                setRemoveButton.PerformClick();
            };

            installedGuns.SelectedIndexChanged += (s, a) =>
            {
                var item = items.SelectedItem as IItem;
                if (item == null)
                    return;
                setRemoveButton.Visible = true;
                setRemoveButton.Text = "Uninstall";
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
            foreach (var item in ship.Inventory.GetItems)
            {
                items.Items.Add(item);
            }
            installedChips.Items.Clear();
            foreach (var item in ship.Chips.GetChips)
            {
                installedChips.Items.Add(item);
            }
        }
    }
}