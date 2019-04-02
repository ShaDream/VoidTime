using System;
using System.Collections.Generic;

namespace VoidTime.GUI
{
    public abstract class UIWindow : GUIControl
    {
        public EventList<GUIControl> Controls = new EventList<GUIControl>();

        public Dictionary<GUIControl, List<IDrawData>> drawData = new Dictionary<GUIControl, List<IDrawData>>();

        public abstract void OnGameSizeChanged(object sender, EventArgs args);

        public abstract void Unsubscribe();
    }
}