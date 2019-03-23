using System.Drawing;

namespace VoidTime.GUI
{
    public abstract class GUIControl
    {
        public string Name { get; set; }
        public Point Location { get; set; }
        public Size Size { get; set; }

        public int DrawIndex { get; set; }

        public bool Enabled { get; set; }
        public bool Visible { get; set; }

        public UIMask Mask { get; set; }
    }
}