using System.Collections.Generic;
using System.Windows.Forms;

namespace VoidTime
{
    public class MouseContol
    {
        public HashSet<MouseButtons> PressedPuttons = new HashSet<MouseButtons>();

        public bool IsDoubleClicked;
        public Vector2D MousePosition;
        public Vector2D MousePositionDelta;

        public float MouseWheelDelta;
    }
}