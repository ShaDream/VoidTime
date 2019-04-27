using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class StatusBar : BasicGameWindow
    {
        private readonly Player ship;
        private readonly ProgressBar HP;

        public StatusBar(MainForm form, Window owner, Player ship)
        {
            this.form = form;
            this.ship = ship;
            this.owner = owner;

            var panel = InitPanel();

            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));

            HP = new HPBar
            {
                Dock = DockStyle.Fill,
                Maximum = (int)ship.Data.ShipStats.MaxHP,
                Value = 50,
            };

            panel.Controls.Add(HP, 0, 0);
            var money = new Label
            {
                ForeColor = Color.FromArgb(233, 238, 201),
                Text = ship.Inventory.Money.ToString(),
                Margin = new Padding(0, 3, 0, 0)
            };

            panel.Controls.Add(money, 1, 0);
            panel.Controls.Add(new Label(), 2, 0);
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
    }
}