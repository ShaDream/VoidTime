using System;
using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class PlanetPanel : BasicGameWindow
    {
        private readonly Player ship;
        private Button buyButton;
        private TableLayoutPanel BuySellPanel;
        private TextBox description;

        private Button exitButton;
        private ListBox planetInventory;
        private Button repairButton;

        public PlanetPanel(MainForm form, Window owner, Player ship)
        {
            this.form = form;
            this.ship = ship;
            this.owner = owner;

            var tabs = new TabControl
            {
                BackColor = Color.Black,
                Size = new Size(500, 500),
                Location = new Point(700, 200)
            };
            tabs.TabPages.Add("Buy/Sell");
            tabs.TabPages.Add("Upgrade");

            for (var i = 0; i < tabs.TabCount; i++)
            {
                tabs.TabPages[i].BackColor = Color.Black;
                tabs.TabPages[i].BorderStyle = BorderStyle.None;
            }

            BuySellPanelInitialization();

            tabs.TabPages[0].Controls.Add(BuySellPanel);
            window = tabs;
        }

        private void UpgradePanelInitialization() { }

        private void BuySellPanelInitialization()
        {
            BuySellPanel = new TableLayoutPanel
            {
                Size = new Size(500, 500),
                Location = new Point(700, 200),
                Dock = DockStyle.Fill
            };

            var ButtonPanel = new TableLayoutPanel {Dock = DockStyle.Fill};
            var ListPanel = new TableLayoutPanel {Dock = DockStyle.Fill};

            ListPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            ListPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            ListPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            planetInventory = new ListBox
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None
            };
            ListPanel.Controls.Add(planetInventory, 0, 0);

            description = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BackColor = Color.Black,
                ForeColor = Color.White
            };
            ListPanel.Controls.Add(description, 1, 0);

            ButtonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            ButtonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            ButtonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            ButtonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            ButtonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 0));

            Action buyClick = () => { };

            exitButton = new Button
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                Text = "Exit"
            };
            ButtonPanel.Controls.Add(exitButton, 0, 0);
            exitButton.Click += (s, a) => Switch();

            buyButton = new Button
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                Visible = false
            };
            ButtonPanel.Controls.Add(buyButton, 1, 0);

            repairButton = new Button
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Fill
            };
            ButtonPanel.Controls.Add(repairButton, 2, 0);


            BuySellPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 90));
            BuySellPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            BuySellPanel.Controls.Add(ButtonPanel, 0, 1);
            BuySellPanel.Controls.Add(ListPanel, 0, 0);

            buyButton.Click += (s, a) => buyClick();

            planetInventory.SelectedIndexChanged += (s, a) =>
            {
                var item = planetInventory.SelectedItem as IItem;
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
        }


        protected override void Update()
        {
            planetInventory.Items.Clear();
            planetInventory.Items.Add(ChipParser.GetChip("Attack Up"));
            planetInventory.Items.Add(ChipParser.GetChip("Defence Up"));
            buyButton.Visible = false;
        }
    }
}