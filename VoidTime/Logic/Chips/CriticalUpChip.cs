using System;
using System.Drawing;

namespace VoidTime
{
    public abstract class CriticalUpChip : Chip
    {
        public override void SetChip(Ship ship)
        {
            ship.data.ShipBuffs.CriticalChanceIncrease += Values[CurrentLevel - 1];
        }

        public override void RemoveChip(Ship ship)
        {
            ship.data.ShipBuffs.CriticalChanceIncrease -= Values[CurrentLevel - 1];
        }
    }
}