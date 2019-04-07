using System.Collections.Generic;
using System.Windows.Forms;

namespace VoidTime
{
    public class Controls
    {
        public HashSet<Keys> KeysHandler { get; }
        public Dictionary<string, Axis> AxesHandler { get; set; } = new Dictionary<string, Axis>();
        public MouseContol MouseHandler;

        public Controls()
        {
            KeysHandler = new HashSet<Keys>();
        }
    }
}