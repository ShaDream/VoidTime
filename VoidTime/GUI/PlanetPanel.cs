using System;
using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class PlanetPanel : BasicGameWindow
    {
        private readonly Player ship;
        
        private Label description;
        
        private Button exitButton;
        private Button menuButton;

        private ListBox items;
        private ListBox planetInventory;
        private ListBox quests;

        public PlanetPanel(MainForm form, Window owner, Player ship)
        {
            this.form = form;
            this.ship = ship;
            this.owner = owner;

            var planetPanel = InitPanelControl();
            var tabs = InitTabControl();
            var tabsInfo = InitTabInfoControl();

            planetPanel.Controls.Add(tabs, 0, 0);
            planetPanel.Controls.Add(tabsInfo, 1, 0);

            planetInventory = InitPlanetInventory();
            quests = InitQuests();
            description = InitDescription();
            items = InitInventory();

            tabs.TabPages[0].Controls.Add(planetInventory);
            tabs.TabPages[2].Controls.Add(quests);
            tabsInfo.TabPages[0].Controls.Add(description);
            tabsInfo.TabPages[1].Controls.Add(items);

            Action menuButtonClick = () => { };

            exitButton = new Button
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                Text = "Exit"
            };

            planetPanel.Controls.Add(exitButton, 1, 1);
            exitButton.Click += (s, a) => Switch();

            menuButton = new Button
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                Visible = false
            };
            planetPanel.Controls.Add(menuButton, 0, 1);

            menuButton.Click += (s, a) => menuButtonClick();

            planetInventory.SelectedIndexChanged += (s, a) =>
            {
                if (!(planetInventory.SelectedItem is IItem item))
                    return;
                description.Text = item.GetInfo();
                menuButton.Text = "Buy";
                menuButton.Visible = true;
                if (item.Price > ship.Inventory.Money)
                    menuButton.Enabled = false;
                menuButtonClick = () =>
                {
                    ship.Inventory.Money -= item.Price;
                    ship.Inventory.Add(item);
                    Update();
                };
            };

            planetInventory.MouseDoubleClick += (s, a) =>
            {
                if (planetInventory.SelectedItem == null)
                    return;
                menuButton.PerformClick();
            };

            quests.SelectedIndexChanged += (s, a) =>
            {
                if (!(quests.SelectedItem is Quest item))
                    return;
                description.Text = item.GetInfo();
                if (item.Status == QuestStatus.NotStarted)
                {
                    menuButton.Text = "Accept";
                    menuButton.Visible = true;
                    menuButtonClick = () =>
                    {
                        item.AcceptQuest();
                        Update();
                    };
                }

                if (item.Status == QuestStatus.Сompleted)
                {
                    menuButton.Text = "Get reward";
                    menuButton.Visible = true;
                    menuButtonClick = () =>
                    {
                        ship.Inventory.Money += item.Reward;
                        Update();
                    };
                }
            };
            quests.MouseDoubleClick += (s, a) =>
            {
                if (quests.SelectedItem == null)
                    return;
                menuButton.PerformClick();
            };

            var testQuest = new Quest("Noob Challenge", 10, EnemyCount.Few, EnemyDifficult.Easy, () =>
            {
                var mainForm = form.currentModel as MainGameModel;
                var enemy = new Enemy(ship.Position + new Vector2D(200, 200), ship);
                mainForm.AddObjectOnMap(enemy);
                return enemy;
            });

            quests.Items.Add(testQuest);
            window = planetPanel;
        }

        protected override void Update()
        {
            description.Text = string.Empty;;
            planetInventory.Items.Clear();
            planetInventory.Items.AddRange(ChipParser.GetAllChips());
            items.Items.Clear();
            foreach (var item in ship.Inventory.GetItems)
            {
                items.Items.Add(item);
            }
            menuButton.Visible = false;
        }

        private TableLayoutPanel InitPanelControl()
        {
            var planetPanel = new TableLayoutPanel
            {
                Size = new Size(250, 300),
                Location = new Point(100, 100)
            };

            planetPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            planetPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            planetPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 90));
            planetPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            return planetPanel;
        }

        private TabControl InitTabControl()
        {
            var tabs = new TabControl
            {
                BackColor = Color.Black,
                Size = new Size(500, 500),
                Location = new Point(700, 200),
                Dock = DockStyle.Fill
            };

            tabs.TabPages.Add("Buy/Sell");
            tabs.TabPages.Add("Upgrade");
            tabs.TabPages.Add("Quests");

            for (var i = 0; i < tabs.TabCount; i++)
            {
                tabs.TabPages[i].BackColor = Color.Black;
                tabs.TabPages[i].BorderStyle = BorderStyle.None;
            }

            return tabs;
        }

        private TabControl InitTabInfoControl()
        {
            var tabsInfo = new TabControl
            {
                BackColor = Color.Black,
                Size = new Size(500, 500),
                Location = new Point(700, 200),
                Dock = DockStyle.Fill
            };

            tabsInfo.TabPages.Add("Information");
            tabsInfo.TabPages.Add("Inventory");

            for (var i = 0; i < tabsInfo.TabCount; i++)
            {
                tabsInfo.TabPages[i].BackColor = Color.Black;
                tabsInfo.TabPages[i].BorderStyle = BorderStyle.None;
            }

            return tabsInfo;
        }

        private ListBox InitPlanetInventory()
        {
            return new ListBox
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None
            };
        }

        private ListBox InitQuests()
        {
            return new ListBox
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None
            };
        }

        private Label InitDescription()
        {
            return new Label
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Black,
                ForeColor = Color.White,
            };
        }

        private ListBox InitInventory()
        {
            return new ListBox
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None
            };
        }
    }
}