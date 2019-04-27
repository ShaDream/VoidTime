using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class StatsPanel : BasicGameWindow
    {
        private readonly Label label;
        private readonly Player ship;
        private readonly string stats;

        public StatsPanel(MainForm form, Window owner, Player ship)
        {
            this.form = form;
            this.owner = owner;
            this.ship = ship;
            var tabs = new TabControl
            {
                BackColor = Color.FromArgb(25, 34, 57)
            };
            tabs.TabPages.Add("Player Statistics");
            tabs.TabPages[0].BackColor = Color.FromArgb(40, 49, 72);
            tabs.TabPages[0].BorderStyle = BorderStyle.None;
            stats = ship.Data.ShipStats.GetInfo();
            label = new Label
            {
                Text = stats,
                ForeColor = Color.FromArgb(233, 238, 201),
                Dock = DockStyle.Fill
            };
            tabs.TabPages[0].Controls.Add(label);
            window = tabs;
        }

        protected override void Update()
        {
            label.Text = ship.Data.ShipStats.GetInfo();
        }
    }
}