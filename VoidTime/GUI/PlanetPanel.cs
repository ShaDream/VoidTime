using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using VoidTime.Resources;

namespace VoidTime.GUI
{
    public class PlanetPanel
    {
        private MainForm owner;
        private Player player;

        private TableLayoutPanel planetPanel;
        private ListBox itemBox;
        private TextBox description;

        private Button exitButton;
        private Button buyButton;
        private Button repairButton;

        private XmlNodeList list;

        public PlanetPanel(MainForm owner, Player player)
        {
            this.owner = owner;
            this.player = player;

            planetPanel = new TableLayoutPanel
            {
                Size = new Size(500, 500),
                Location = new Point(700, 200),
            };
            var ButtonPanel = new TableLayoutPanel { Dock = DockStyle.Fill };
            var ListPanel = new TableLayoutPanel { Dock = DockStyle.Fill };

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
            ButtonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            ButtonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            ButtonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));

            exitButton = new Button
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                Text = "Exit"
            };
            ButtonPanel.Controls.Add(exitButton, 0, 0);
            exitButton.Click += (s, a) => Hide();

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


            planetPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 90));
            planetPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            planetPanel.Controls.Add(ButtonPanel, 0, 1);
            planetPanel.Controls.Add(ListPanel, 0, 0);


            itemBox.SelectedIndexChanged += (s, a) =>
            {
                var name = (itemBox.SelectedItem as string);
                name = name.Substring(0, name.LastIndexOf(' '));

            };

        }

        public void Show()
        {
            owner.BeginInvoke(new Action(() =>
            {
                owner.Controls.Add(planetPanel);
                planetPanel.BringToFront();
                owner.currentModel.Pause();
            }));
        }

        public void Hide()
        {
            owner.BeginInvoke(new Action(() =>
            {
                owner.Controls.Remove(planetPanel);
                owner.currentModel.Run();
            }));
        }

        public void UpdateFields()
        {
            var names = new List<string>();
            foreach (XmlNode node in list)
                names.Add($"{node.Attributes["Name"].Value} {node.Attributes["price"].Value}$");

            itemBox.Items.AddRange(names.ToArray());
        }

    }
}