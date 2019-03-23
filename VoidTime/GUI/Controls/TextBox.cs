using System.Drawing;

namespace VoidTime.GUI
{
    public class TextBox : GUIControl
    {
        public string Text;
        public Color Background;
        public FontSettings Font { get; set; }
    }
}