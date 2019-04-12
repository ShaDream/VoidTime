using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VoidTime
{
    public class Input
    {
        private static Controls controlses;
        private static bool IsCreated;

        public static void Create(Controls controlses)
        {
            if (IsCreated)
                return;
            IsCreated = true;
            Input.controlses = controlses;
        }

        public static bool IsKeyPressed(Keys key)
        {
            return controlses.KeysHandler.Contains(key);
        }

        public static bool IsAnyKeyPressed(params Keys[] keys)
        {
            return keys.Any(x => Input.controlses.KeysHandler.Contains(x));
        }

        public static bool IsMouseButtonPressed(MouseButtons button)
        {
            return controlses.MouseHandler.PressedButtons.Contains(button);
        }

        public static float GetAxis(string name)
        {
            if (!controlses.AxesHandler.ContainsKey(name)) return 0;

            var local = controlses.AxesHandler[name];
            return local.GetValue(controlses.KeysHandler.Contains(local.PositiveKey),
                controlses.KeysHandler.Contains(local.NegativeKey));
        }

        public static Vector2D GetMouseDelta()
        {
            return controlses.MouseHandler.MousePositionDelta;
        }

        public static Vector2D GetMousePosition()
        {
            return controlses.MouseHandler.MousePosition;
        }

        public static Vector2D GetWorldMousePosition()
        {
            return controlses.MouseHandler.ScreenToWorld();
        }

        public static bool GetMouseButton(MouseButtons button)
        {
            return controlses.MouseHandler.PressedButtons.Contains(button);
        }

        public static bool GetMouseButtonUp(MouseButtons button)
        {
            return controlses.MouseHandler.MouseUp == button;
        }

        public static bool GetMouseButtonDown(MouseButtons button)
        {
            return controlses.MouseHandler.MouseDown == button;
        }
    }
}