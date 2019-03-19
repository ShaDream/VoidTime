using System.Drawing;

namespace VoidTime.GUI
{
    public class GUIControl
    {
        public string Name { get; set; }
        public Point Location { get; set; }
        public Size Size { get; set; }

        public int DrawIndex { get; set; }
    }
}