using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class StatusBar : BasicGameWindow
    {
        private readonly Player ship;

        public StatusBar(MainForm form, Window owner, Player ship)
        {
            this.form = form;
            this.ship = ship;
            this.owner = owner;
            window = InitPanel();
            form.Controls.Add(window);
            window.BringToFront();
        }

        private TableLayoutPanel InitPanel()
        {
            return new TableLayoutPanel()
            {
                BackColor = Color.LightSlateGray
            };
        }
    }
}