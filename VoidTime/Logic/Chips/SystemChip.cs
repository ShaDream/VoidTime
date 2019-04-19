using System;
using System.Drawing;

namespace VoidTime
{
    public class SystemChip : Chip
    {
        public override void SetChip(Ship ship)
        {
            ship.data.ShipBuffs.Destroy = false;
        }

        public override void RemoveChip(Ship ship)
        {
            ship.data.ShipBuffs.Destroy = true;
        }
    }
}