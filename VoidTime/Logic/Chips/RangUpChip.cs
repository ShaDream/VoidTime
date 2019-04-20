﻿using System;
using System.Drawing;

namespace VoidTime
{
    public class RangUpChip : Chip
    {
        public override void SetChip(Ship ship)
        {
            ship.Data.ShipBuffs.RangUp += Values[CurrentLevel - 1];
        }

        public override void RemoveChip(Ship ship)
        {
            ship.Data.ShipBuffs.RangUp -= Values[CurrentLevel - 1];
        }
    }
}