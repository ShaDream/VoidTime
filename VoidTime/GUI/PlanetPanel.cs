using System;
using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class PlanetPanel : BasicGameWindow
    {
        private readonly Player ship;
        private Button buyButton;
        private Label description;
        private ListBox items;
        private Button exitButton;
        private ListBox planetInventory;

        public PlanetPanel(MainForm form, Window owner, Player ship)
        {
            this.form = form;
            this.ship = ship;
            this.owner = owner;

            var planetPanel = new TableLayoutPanel
            {
                Size = new Size(250, 300),
                Location = new Point(100, 100)
            };
            planetPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            planetPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            planetPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 90));
            planetPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            var tabs = new TabControl
            {
                BackColor = Color.Black,
                Size = new Size(500, 500),
                Location = new Point(700, 200),
                Dock = DockStyle.Fill
            };
            tabs.TabPages.Add("Buy/Sell");
            tabs.TabPages.Add("Upgrade");

            for (var i = 0; i < tabs.TabCount; i++)
            {
                tabs.TabPages[i].BackColor = Color.Black;
                tabs.TabPages[i].BorderStyle = BorderStyle.None;
            }
            planetPanel.Controls.Add(tabs, 0, 0);

            var tabsInfo = new TabControl
            {
                BackColor = Color.Black,
                Size = new Size(500, 500),
                Location = new Point(700, 200),
                Dock = DockStyle.Fill
            };
            tabsInfo.TabPages.Add("Information");
            tabsInfo.TabPages.Add("Inventory");
            tabsInfo.TabPages[0].BackColor = Color.Black;
            tabsInfo.TabPages[0].BorderStyle = BorderStyle.None;
            planetPanel.Controls.Add(tabsInfo, 1, 0);

            planetInventory = new ListBox
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None
            };

            tabs.TabPages[0].Controls.Add(planetInventory);

            description = new Label
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Black,
                ForeColor = Color.White,
            };

            items = new ListBox
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None
            };

            tabsInfo.TabPages[0].Controls.Add(description);
            tabsInfo.TabPages[1].Controls.Add(items);

            Action buyClick = () => { };

            exitButton = new Button
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                Text = "Exit"
            };

            planetPanel.Controls.Add(exitButton, 1, 1);
            exitButton.Click += (s, a) => Switch();

            buyButton = new Button
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                Visible = false
            };
            planetPanel.Controls.Add(buyButton, 0, 1);

            buyButton.Click += (s, a) => buyClick();

            planetInventory.SelectedIndexChanged += (s, a) =>
            {
                if (!(planetInventory.SelectedItem is IItem item))
                    return;
                var t = item.GetInfo();
                description.Text = item.GetInfo();
                buyButton.Text = "Buy";
                buyButton.Visible = true;
                if (item.Price > ship.Inventory.Money)
                    buyButton.Enabled = false;
                buyClick = () =>
                {
                    ship.Inventory.Money -= item.Price;
                    ship.Inventory.Add(item);
                    Update();
                };
            };

            window = planetPanel;
        }

        protected override void Update()
        {
            planetInventory.Items.Clear();
            planetInventory.Items.AddRange(ChipParser.GetAllChips());
            items.Items.Clear();
            foreach (var item in ship.Inventory.GetItems)
            {
                items.Items.Add(item);
            }
            buyButton.Visible = false;
        }
    }
}