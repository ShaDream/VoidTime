using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class StatsPanel : BasicGameWindow
    {
        private Player ship;
        private string stats;
        private Label label;
        public StatsPanel(MainForm form, Window owner, Player ship)
        {
            this.form = form;
            this.owner = owner;
            this.ship = ship;
            var tabs = new TabControl
            {
                BackColor = Color.Black,
            };
            tabs.TabPages.Add("Player Statistics");
            tabs.TabPages[0].BackColor = Color.Black;
            tabs.TabPages[0].BorderStyle = BorderStyle.None;
            stats = ship.Data.ShipStats.GetInfo();
            label = new Label
            {
                Text = stats,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
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