using System;
using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class PlanetPanel : BasicGameWindow
    {
        private readonly Player ship;

        private readonly Label description;
        private Tabs currentTab;

        private readonly Button exitButton;
        private readonly Button menuButton;

        private readonly ListBox inventory;
        private readonly ListBox shop;
        private readonly ListBox upgrades;
        private readonly ListBox quests;

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

            shop = InitListBox();
            quests = InitListBox();
            inventory = InitListBox();
            upgrades = InitListBox();
            description = InitDescription();

            tabs.TabPages[0].Controls.Add(shop);
            tabs.TabPages[1].Controls.Add(upgrades);
            tabs.TabPages[2].Controls.Add(quests);
            tabsInfo.TabPages[0].Controls.Add(description);
            tabsInfo.TabPages[1].Controls.Add(inventory);

            Action menuButtonClick = () => { };

            exitButton = InitButton();
            menuButton = InitButton();

            exitButton.Text = "Exit";
            exitButton.Visible = true;

            planetPanel.Controls.Add(exitButton, 1, 1);
            exitButton.Click += (s, a) => Switch();

            planetPanel.Controls.Add(menuButton, 0, 1);

            menuButton.Click += (s, a) => menuButtonClick();

            tabs.SelectedIndexChanged += (s, a) =>
            {
                currentTab = (Tabs)tabs.SelectedIndex;
            };

            inventory.SelectedIndexChanged += (s, a) =>
            {
                if (!(inventory.SelectedItem is Chip item) || currentTab != Tabs.Upgrades)
                    return;
                if (upgrades.Items.Count > 0 && !((Chip)upgrades.Items[0]).Equals(item))
                    return;
                if (upgrades.Items.Count == 3 || item.CurrentLevel == item.MaxLevel)
                    return;

                menuButton.Text = "To upgrade";
                menuButton.Visible = true;
                menuButtonClick = () =>
                {
                    ship.Inventory.Remove(item);
                    upgrades.Items.Add(item);

                    if (upgrades.Items.Count == 3)
                    {
                        if (UpgradePrompt.ShowDialog())
                        {
                            item.CurrentLevel++;
                            ship.Inventory.Add(item);
                        }
                        else
                        {
                            foreach (var it in upgrades.Items)
                                ship.Inventory.Add((Chip)it);
                        }
                        upgrades.Items.Clear();
                    }
                        

                    Update();
                };
            };

            inventory.MouseDoubleClick += (s, a) =>
            {
                if (inventory.SelectedItem == null)
                    return;
                menuButton.PerformClick();
            };

            upgrades.SelectedIndexChanged += (s, a) =>
            {
                if (!(upgrades.SelectedItem is Chip item))
                    return;

                menuButton.Text = "To inventory";
                menuButton.Visible = true;
                menuButtonClick = () =>
                {
                    ship.Inventory.Add(item);
                    upgrades.Items.Remove(item);
                    Update();
                };
            };

            upgrades.MouseDoubleClick += (s, a) =>
            {
                if (upgrades.SelectedItem == null)
                    return;
                menuButton.PerformClick();
            };

            shop.SelectedIndexChanged += (s, a) =>
            {
                if (!(shop.SelectedItem is IItem item))
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

            shop.MouseDoubleClick += (s, a) =>
            {
                if (shop.SelectedItem == null)
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
                var enemy = new MapEnemy(ship.Position + new Vector2D(200, 200), ship, 100, EnemyDifficult.Easy);
                mainForm.AddObjectOnMap(enemy);
                return enemy;
            });

            quests.Items.Add(testQuest);
            window = planetPanel;
        }

        protected override void Update()
        {
            description.Text = string.Empty;
            shop.Items.Clear();
            shop.Items.AddRange(ChipParser.GetAllChips());
            inventory.Items.Clear();
            foreach (var item in ship.Inventory.GetItems)
                inventory.Items.Add(item);
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

        private ListBox InitListBox()
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
                ForeColor = Color.White
            };
        }

        private Button InitButton()
        {
            return new Button
            {
                BackColor = Color.Black,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                Visible = false
            };
        }

        protected override void Hide()
        {
            while (upgrades.Items.Count > 0)
            {
                var item = (IItem)upgrades.Items[0];
                ship.Inventory.Add(item);
                upgrades.Items.Remove(item);
            }
            base.Hide();
        }
    }
}