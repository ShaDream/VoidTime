using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.Generator
{
    public class MainForm : Form
    {
        Panel panel = new Panel();


        public MainForm()
        {
            MinimumSize = new Size(1280, 720);

            panel.Dock = DockStyle.Fill;

            var grid = new TableLayoutPanel { Dock = DockStyle.Fill };
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            grid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            Controls.Add(grid);

            var menu = new MenuStrip { Height = 30 };
            var generatorMenuItem = new ToolStripMenuItem("Generator");
            var createBackgroundGenerator = new ToolStripMenuItem("Create background image");
            generatorMenuItem.DropDownItems.Add(createBackgroundGenerator);

            menu.Items.Add(generatorMenuItem);

            createBackgroundGenerator.Click += CreateBackgroundGeneratorPage;

            grid.Controls.Add(menu, 0, 0);
            grid.Controls.Add(panel, 0, 1);
        }

        private void CreateBackgroundGeneratorPage(object sender, System.EventArgs e)
        {
            ClearPanel();
            new BackgroundFormHelper(panel);
        }

        private void ClearPanel()
        {
            panel.Controls.Clear();
        }


    }
}