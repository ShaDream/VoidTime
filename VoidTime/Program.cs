using System;
using System.Drawing;
using System.Windows.Forms;

namespace VoidTime
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(new MainGameModel()));
            //Application.Run(new MainForm(new BattleGameModel(new BattleGameModelData {MapSize = new Size(100000,100000)})));

        }
    }
}