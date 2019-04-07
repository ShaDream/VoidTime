using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VoidTime
{
    public class ReadonlyKeys
    {
        private static Controls _controlses;
        private static bool IsCreated;

        public ReadonlyKeys(Controls controlses)
        {
            if (IsCreated)
                return;
            IsCreated = true;
            ReadonlyKeys._controlses = controlses;
        }

        public static bool IsKeyPressed(Keys key)
        {
            return _controlses.KeysHandler.Contains(key);
        }

        public static bool IsAnyKeyPressed(params Keys[] keys)
        {
            return keys.Any(x => ReadonlyKeys._controlses.KeysHandler.Contains(x));
        }

        public static bool IsMouseButtonPressed(MouseButtons button)
        {
            return _controlses.MouseHandler.PressedPuttons.Contains(button);
        }

        public static float GetAxis(string name)
        {
            if (!_controlses.AxesHandler.ContainsKey(name)) return 0;

            var local = _controlses.AxesHandler[name];
            return local.GetValue(_controlses.KeysHandler.Contains(local.PositiveKey),
                _controlses.KeysHandler.Contains(local.NegativeKey));
        }
    }
}