using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using VoidTime.Resources;

namespace VoidTime.GUI
{
    public class PlanetPanel : BasicGameWindow
    {
        private Player ship;
        private TableLayoutPanel BuySellPanel;
        private ListBox itemBox;
        private TextBox description;

        private Button exitButton;
        private Button buyButton;
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
                Location = new Point(700, 200),
            };
            tabs.TabPages.Add("Buy/Sell");
            tabs.TabPages.Add("Upgrade");
            tabs.TabPages.Add("Set Weapons");

            BuySellPanelInitialization();

            tabs.TabPages[0].Controls.Add(BuySellPanel);
            window = tabs;
        }

        private void UpgradePanelInitialization()
        {

        }

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

            itemBox = new ListBox
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None
            };
            ListPanel.Controls.Add(itemBox, 0, 0);

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
            };
            ButtonPanel.Controls.Add(buyButton, 1, 0);

            repairButton = new Button
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
            };
            ButtonPanel.Controls.Add(repairButton, 2, 0);


            BuySellPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 90));
            BuySellPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            BuySellPanel.Controls.Add(ButtonPanel, 0, 1);
            BuySellPanel.Controls.Add(ListPanel, 0, 0);


            itemBox.SelectedIndexChanged += (s, a) =>
            {
                var name = (itemBox.SelectedItem as string);
                name = name.Substring(0, name.LastIndexOf(' '));
            };
        }
    }
}