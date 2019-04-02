using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public abstract class GUIControl
    {
        protected Color background;
        protected Color foreground;
        protected Point location;
        protected Size size;
        protected bool visible;

        public virtual string Name { get; set; }

        public virtual Point Location
        {
            get => location;
            set
            {
                if (value == location)
                    return;
                location = value;
                LocationChanged?.Invoke(this, location);
                UIChanged?.Invoke(this, Draw());
            }
        }

        public virtual Size Size
        {
            get => size;
            set
            {
                if (value == size)
                    return;
                size = value;
                SizeChanged?.Invoke(this, size);
                UIChanged?.Invoke(this, Draw());
            }
        }

        public virtual int DrawIndex { get; set; }

        public virtual bool Enabled { get; set; }

        public virtual bool Visible
        {
            get => visible;
            set
            {
                if (value == visible)
                    return;
                visible = value;
                VisibleChanged?.Invoke(this, visible);
                UIChanged?.Invoke(this, Draw());
            }
        }

        public virtual UIMask Mask { get; set; }

        public virtual Color Background
        {
            get => background;
            set
            {
                if (value == background)
                    return;
                background = value;
                BackgroundChanged?.Invoke(this, background);
                UIChanged?.Invoke(this, Draw());
            }
        }

        public virtual Color ForeGround
        {
            get => foreground;
            set
            {
                if (value == foreground)
                    return;
                foreground = value;
                ForegroundChanged?.Invoke(this, foreground);
                UIChanged?.Invoke(this, Draw());
            }
        }

        public virtual void OnKeyPress(object sender, KeyEventArgs args) { }
        public virtual void OnKeyRelease(object sender, KeyEventArgs args) { }
        public virtual void OnMouseWheel(object sender, MouseEventArgs args) { }
        public virtual void OnMouseMove(object sender, MouseEventArgs args) { }
        public virtual void OnMouseClick(object sender, MouseEventArgs args) { }
        public virtual void OnMouseDoubleClick(object sender, MouseEventArgs args) { }

        public event Action<GUIControl, List<IDrawData>> UIChanged;
        public event Action<GUIControl, Size> SizeChanged;
        public event Action<GUIControl, Point> LocationChanged;
        public event Action<GUIControl, Color> BackgroundChanged;
        public event Action<GUIControl, Color> ForegroundChanged;
        public event Action<GUIControl, bool> VisibleChanged;

        public void OnUIChanged(GUIControl sender, List<IDrawData> args)
        {
            UIChanged?.Invoke(sender, args);
        }

        public virtual List<IDrawData> Draw()
        {
            return new List<IDrawData>();
        }
    }
}