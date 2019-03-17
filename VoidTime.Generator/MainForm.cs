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
            var createFontSpriteGenerator = new ToolStripMenuItem("Create Font Sprite");
            generatorMenuItem.DropDownItems.Add(createBackgroundGenerator);
            generatorMenuItem.DropDownItems.Add(createFontSpriteGenerator);

            menu.Items.Add(generatorMenuItem);

            createBackgroundGenerator.Click += CreateBackgroundGeneratorPage;
            createFontSpriteGenerator.Click += CreateFontSpriteGeneratorPage;

            grid.Controls.Add(menu, 0, 0);
            grid.Controls.Add(panel, 0, 1);

            var s = new FontSpriteGenerator();
            s.CellSize = 83;
            s.ImagePath = @"C:\Users\ShaDream\source\repos\VoidTime\VoidTime.Resources\Resources\EuropeFont.png";
            s.OriginLine = 65;
            s.WidthCount = 24;
            s.characters =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.,;:?!-_~#\"\'&()[]|`\\/@°+=*$£€<>%АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            s.GenerateDataSettings();
        }

        private void CreateFontSpriteGeneratorPage(object sender, System.EventArgs e)
        {
            ClearPanel();


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