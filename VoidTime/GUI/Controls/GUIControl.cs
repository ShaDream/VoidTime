using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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

        public virtual void OnKeyPress(object sender, KeyEventArgs args) { }
        public virtual void OnKeyRelease(object sender, KeyEventArgs args) { }
        public virtual void OnMouseWheel(object sender, MouseEventArgs args) { }
        public virtual void OnMouseMove(object sender, MouseEventArgs args) { }
        public virtual void OnMouseClick(object sender, MouseEventArgs args) { }
        public virtual void OnMouseDoubleClick(object sender, MouseEventArgs args) { }
        public virtual void OnSizeChanged(object sender, EventArgs args) { }

        public event Action<GUIControl, List<IDrawData>> UIChanged;

        public void OnUIChanged(GUIControl control, List<IDrawData> data)
        {
            UIChanged?.Invoke(control, data);
        }

        public virtual List<IDrawData> Draw()
        {
            return new List<IDrawData>();
        }
    }
}