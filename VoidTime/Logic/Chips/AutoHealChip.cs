using System;
using System.Drawing;

namespace VoidTime
{
    public class AutoHealChip : Chip
    {
        public override void SetChip(Ship ship)
        {
            ship.Data.ShipBuffs.AutoHeal += Values[CurrentLevel - 1];
        }

        public override void RemoveChip(Ship ship)
        {
            ship.Data.ShipBuffs.AutoHeal -= Values[CurrentLevel - 1];
        }
    }
}