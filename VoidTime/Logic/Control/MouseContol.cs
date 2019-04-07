using System.Collections.Generic;

namespace VoidTime
{
    public class MouseContol
    {
        public HashSet<MouseButton> PressedPuttons = new HashSet<MouseButton>();

        public bool IsDoubleClicked;
        public Vector2D MousePosition;
        public Vector2D MousePositionDelta;

        public float MouseWheelDelta;
    }
}