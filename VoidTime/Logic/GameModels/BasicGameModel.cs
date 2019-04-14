using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Box2DSharp.Dynamics;

namespace VoidTime
{
    public abstract class BasicGameModel :IGameModel
    {
        public abstract World Physics { get; set; }
        public abstract Controls Controls { get; set; }
        public abstract TimeData Time { get; set; }
        public abstract void Run();

        public abstract void Pause();

        public abstract event Action<List<GameObject>, BasicCamera> Tick;
        public abstract event Action<IGameModel> GameModelChanged;

        public virtual void OnKeyPress(object sender, KeyEventArgs args)
        {
            if (!Controls.KeysHandler.Contains(args.KeyCode))
                Controls.KeysHandler.Add(args.KeyCode);
        }

        public virtual void OnKeyRelease(object sender, KeyEventArgs args)
        {
            if (Controls.KeysHandler.Contains(args.KeyCode))
                Controls.KeysHandler.Remove(args.KeyCode);
        }

        public virtual void OnMouseWheel(object sender, MouseEventArgs args)
        {
            Controls.MouseHandler.MouseWheelDelta = args.Delta;
        }

        public virtual void OnMouseMove(object sender, MouseEventArgs args)
        {
            Controls.MouseHandler.MousePositionDelta =
                new Vector2D(args.X, args.Y) - Controls.MouseHandler.MousePositionDelta;
            Controls.MouseHandler.MousePosition = new Vector2D(args.X, args.Y);
        }

        public virtual void OnMouseDoubleClick(object sender, MouseEventArgs args)
        {
            Controls.MouseHandler.DoubleClicked = args.Button;
        }

        public abstract void OnSizeChanged(object sender, EventArgs args);

        public virtual void OnMousePressed(object sender, MouseEventArgs args)
        {
            Controls.MouseHandler.PressedButtons.Add(args.Button);
            Controls.MouseHandler.MouseDown = args.Button;
        }

        public virtual void OnMouseReleased(object sender, MouseEventArgs args)
        {
            Controls.MouseHandler.PressedButtons.Remove(args.Button);
            Controls.MouseHandler.MouseUp = args.Button;
        }
    }
}