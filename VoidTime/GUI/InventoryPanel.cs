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
                Size = new Size(500, 500),
                Location = new Point(700, 200),
            };
            tabs.TabPages.Add("Installed Chips");
            tabs.TabPages.Add("Installed Guns");

            for (var i = 0; i < tabs.TabCount; i++)
            {
                tabs.TabPages[i].BackColor = Color.Black;
                tabs.TabPages[i].BorderStyle = BorderStyle.None;
            }

            items = new ListBox
            {
                BackColor = Color.Black,
                BorderStyle = BorderStyle.Fixed3D,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 23, 3, 0)
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

            setRemoveButton = new Button()
            {
                Visible = false,
                BackColor = Color.Black,
                ForeColor = Color.White,
                Size = new Size(150, 50)
            };
            exitButton = new Button
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Text = "Exit",
                Size = new Size(150, 50)
            };

            items.SelectedIndexChanged += (s, a) =>
            {
                setRemoveButton.Visible = true;
                var item = items.SelectedItem as IItem;
                setRemoveButton.Text = "Install";
                if (item is Chip)
                    setRemoveClick = () =>
                    {
                        ship.Inventory.InstallChip(item as Chip);
                        Update();
                    };
            };
            installedChips.SelectedIndexChanged += (s, a) =>
            {
                setRemoveButton.Visible = true;
                var item = items.SelectedItem as IItem;
                setRemoveButton.Text = "Uninstall";
                if (item is Chip)
                    setRemoveClick = () =>
                    {
                        ship.Chips.Remove(item as Chip);
                        Update();
                    };
            };
            installedGuns.SelectedIndexChanged += (s, a) =>
            {
                var item = items.SelectedItem as IItem;
                setRemoveButton.Text = "Uninstall";
            };

            setRemoveButton.Click += (s, a) => setRemoveClick();
            exitButton.Click += (s, a) => Switch();

            inventoryPanel.Controls.Add(tabs, 0, 0);
            inventoryPanel.Controls.Add(items, 1, 0);
            inventoryPanel.Controls.Add(setRemoveButton, 0, 1);
            inventoryPanel.Controls.Add(exitButton, 1, 1);

            window = inventoryPanel;
        }

        private void Update()
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