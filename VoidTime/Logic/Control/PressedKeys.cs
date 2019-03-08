using System.Collections.Generic;
using System.Windows.Forms;

namespace VoidTime
{
    public class PressedKeys
    {
        public HashSet<Keys> keys { get; }

        public PressedKeys()
        {
            keys = new HashSet<Keys>();
        }
    }
}