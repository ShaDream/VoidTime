using System;
using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class StatusBar : BasicGameWindow
    {
        private readonly Player ship;
        private readonly ProgressBar hp;
        private readonly Label money;
        private readonly Label maxHP;
        private readonly TableLayoutPanel panel;

        public StatusBar(MainForm form, Window owner, Player ship)
        {
            this.form = form;
            this.ship = ship;
            this.owner = owner;

            panel = InitPanel();

            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));

            hp = new HPBar
            {
                Maximum = (int)ship.Data.ShipStats.MaxHP,
                Value = (int)ship.Data.ShipStats.CurrentHP,
            };
            ship.Data.ShipStats.OnDamage += ChangeHP;
            ship.Data.ShipStats.OnChangeMaxHP += ChangeMaxHP;
            ship.Inventory.OnChangeMoney += ChangeMoney;

            

            money = new Label
            {
                ForeColor = Color.FromArgb(233, 238, 201),
                Text = $"Money:      {ship.Inventory.Money}",
            };

            maxHP = new Label
            {
                ForeColor = Color.FromArgb(233, 238, 201),
                Text = $"Maximum HP:      {ship.Data.ShipStats.MaxHP}",
            };

            panel.Controls.Add(hp, 0, 0);
            panel.Controls.Add(money, 1, 0);
            panel.Controls.Add(maxHP, 2, 0);
            panel.Controls.Add(new Label(), 3, 0);

            window = panel;
            form.Controls.Add(window);
            window.BringToFront();
        }

        private TableLayoutPanel InitPanel()
        {
            return new TableLayoutPanel()
            {
                BackColor = Color.FromArgb(40, 49, 72)
            };
        }

        public void Resize()
        {
            var offset = window.Height * 15 / 100;
            var width = window.Size.Width / 4;
            var height = window.Size.Height;

            hp.Margin = new Padding(offset, offset, 0, 0);
            hp.Size = new Size(width - 2 * offset, height - 2 * offset);

            money.Margin = new Padding(offset, 0, 0, 0);
            money.Size = new Size(width - 2 * offset, height);
            money.Font = new Font("Calibri", money.Size.Height * 0.6f);

            maxHP.Margin = new Padding(offset, 0, 0, 0);
            maxHP.Size = new Size(width - 2 * offset, height);
            maxHP.Font = new Font("Calibri", maxHP.Size.Height * 0.6f);
        }

        private void ChangeHP()
        {
            window.BeginInvoke(new Action(() =>
            {
                hp.Value = (int)ship.Data.ShipStats.CurrentHP;
            }));
        }

        private void ChangeMoney()
        {
            window.BeginInvoke(new Action(() =>
            {
                money.Text = $"Money:      {ship.Inventory.Money}";
            }));
        }

        private void ChangeMaxHP()
        {
            window.BeginInvoke(new Action(() =>
            {
                maxHP.Text = $"Maximum HP:      {ship.Data.ShipStats.MaxHP}";
            }));
        }
    }
}