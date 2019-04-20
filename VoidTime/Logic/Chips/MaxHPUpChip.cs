using System;
using System.Drawing;

namespace VoidTime
{
    public class MaxHPUpChip : Chip
    {
        public override void SetChip(Ship ship)
        {
            ship.Data.ShipBuffs.HPUp += Values[CurrentLevel - 1];
        }

        public override void RemoveChip(Ship ship)
        {
            ship.Data.ShipBuffs.HPUp -= Values[CurrentLevel - 1];
        }
    }
}