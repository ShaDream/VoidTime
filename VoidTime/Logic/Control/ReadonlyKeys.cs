using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VoidTime
{
    public class ReadonlyKeys
    {
        #region Private Fields

        private static PressedKeys keys;
        private static Dictionary<string, Axis> axes;
        private static bool IsCreated = false;

        #endregion

        #region Constructor

        public ReadonlyKeys(PressedKeys keys, HashSet<Axis> axes)
        {
            if (IsCreated)
                return;
            IsCreated = true;
            ReadonlyKeys.keys = keys;
            ReadonlyKeys.axes = new Dictionary<string, Axis>();

            foreach (var axis in axes)
            {
                ReadonlyKeys.axes.Add(axis.Name, axis);
            }
        }

        #endregion

        #region Public Methods

        public static bool GetKey(Keys key)
        {
            return keys.keys.Contains(key);
        }

        public static bool GetKeys(params Keys[] keys)
        {
            return keys.All(x => ReadonlyKeys.keys.keys.Contains(x));
        }

        public static float GetAxis(string name)
        {
            if (axes.ContainsKey(name))
                return axes[name].GetValue(keys.keys.Contains(axes[name].PositiveKey),
                    keys.keys.Contains(axes[name].PositiveKey));
            return 0;
        }

        #endregion

    }
}