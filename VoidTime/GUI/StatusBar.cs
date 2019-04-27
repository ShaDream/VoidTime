using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class StatusBar : BasicGameWindow
    {
        private readonly Player ship;
        private readonly ProgressBar hp;
        private readonly Label money;
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
                Value = 50,
            };

            panel.Controls.Add(hp, 0, 0);
            money = new Label
            {
                ForeColor = Color.FromArgb(233, 238, 201),
                Text = "Money : ",
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

        public void Resize()
        {
            var offset = window.Height * 15 / 100;
            var width = window.Size.Width / 4;
            var height = window.Size.Height;

            hp.Margin = new Padding(offset, offset, 0, 0);
            hp.Size = new Size(width - 2 * offset, height - 2 * offset);

            money.Margin = new Padding(offset, 0, 0, 0);
            money.Font = new Font("Calibri", money.Size.Height * 0.5f);
        }
    }
}