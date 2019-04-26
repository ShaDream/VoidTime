using System.Collections.Generic;
using System.Windows.Forms;

namespace VoidTime
{
    public class MouseContol
    {
        private BasicCamera currentCamera;
        public MouseButtons DoubleClicked = MouseButtons.None;
        public MouseButtons MouseDown = MouseButtons.None;

        public Vector2D MousePosition = Vector2D.Zero;
        public Vector2D MousePositionDelta = Vector2D.Zero;

        public MouseButtons MouseUp = MouseButtons.None;

        public float MouseWheelDelta;
        public HashSet<MouseButtons> PressedButtons = new HashSet<MouseButtons>();

        public MouseContol(BasicCamera camera)
        {
            currentCamera = camera;
        }

        public void ChangeCamera(BasicCamera camera)
        {
            currentCamera = camera;
        }

        public Vector2D ScreenToWorld()
        {
            return currentCamera.TopLeft + new Vector2D(MousePosition.X, -MousePosition.Y);
        }
    }
}