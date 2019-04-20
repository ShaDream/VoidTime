using System;
using System.Drawing;

namespace VoidTime
{
    public class HPGaugeChip : Chip
    {
        public override void SetChip(Ship ship)
        {
            ship.Data.ShipBuffs.DisplayHP = true;
        }

        public override void RemoveChip(Ship ship)
        {
            ship.Data.ShipBuffs.DisplayHP = false;
        }
    }
}