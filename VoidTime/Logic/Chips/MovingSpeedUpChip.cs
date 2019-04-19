using System;
using System.Drawing;

namespace VoidTime
{
    public class MovingSpeedUpChip : Chip
    {
        public override void SetChip(Ship ship)
        {
            ship.data.ShipBuffs.MoveSpeedIncrease += Values[CurrentLevel - 1];
        }

        public override void RemoveChip(Ship ship)
        {
            ship.data.ShipBuffs.MoveSpeedIncrease -= Values[CurrentLevel - 1];
        }
    }
}