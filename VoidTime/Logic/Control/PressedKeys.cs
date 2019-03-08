﻿using System.Collections.Generic;
using System.Windows.Forms;

namespace VoidTime
{
    public class PressedKeys
    {
        #region Public Properties

        public HashSet<Keys> keys { get; }

        #endregion

        #region Public Methods

        public PressedKeys()
        {
            keys = new HashSet<Keys>();
        }

        #endregion

    }
}