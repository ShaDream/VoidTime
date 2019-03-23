using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public abstract class UIWindow : GUIControl
    {
        public List<GUIControl> Controls = new List<GUIControl>();

        public abstract void Unsubscribe();

        public abstract void OnKeyPress(object sender, KeyEventArgs args);
        public abstract void OnKeyRelease(object sender, KeyEventArgs args);
        public abstract void OnMouseWheel(object sender, MouseEventArgs args);
        public abstract void OnMouseMove(object sender, MouseEventArgs args);
        public abstract void OnMouseClick(object sender, MouseEventArgs args);
        public abstract void OnMouseDoubleClick(object sender, MouseEventArgs args);
        public abstract void OnSizeChanged(object sender, EventArgs args);

        public event Action<List<IDrawData>> UIChanged;

        public void OnUIChanged(List<IDrawData> data)
        {
            UIChanged?.Invoke(data);
        }
    }
}