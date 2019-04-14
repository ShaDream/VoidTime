using System.Collections.Generic;
using System.Windows.Forms;

namespace VoidTime
{
    public class Controls
    {
        public HashSet<Keys> KeysHandler { get; }
        public Dictionary<string, Axis> AxesHandler { get; set; } = new Dictionary<string, Axis>();
        public MouseContol MouseHandler;

        public Controls(BasicCamera camera)
        {
            KeysHandler = new HashSet<Keys>();
            MouseHandler = new MouseContol(camera);
        }

        public void ClearOneFrameValues()
        {
            MouseHandler.DoubleClicked = MouseButtons.None;
            MouseHandler.MousePositionDelta = Vector2D.Zero;
            MouseHandler.MouseWheelDelta = 0;
            MouseHandler.MouseUp = MouseButtons.None;
            MouseHandler.MouseDown = MouseButtons.None;

        }
    }
}