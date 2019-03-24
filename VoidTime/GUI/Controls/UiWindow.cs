﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public abstract class UIWindow : GUIControl
    {
        public EventList<GUIControl> Controls = new EventList<GUIControl>();

        public Dictionary<GUIControl,List<IDrawData>> drawData = new Dictionary<GUIControl, List<IDrawData>>();

        public abstract void Unsubscribe();
    }
}