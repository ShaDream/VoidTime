using System;
using System.Drawing;

namespace VoidTime
{
    public class MovingSpeedUpChip : Chip
    {
        public override void SetChip(Player player)
        {
            player.data.ShipBuffs.MoveSpeedIncrease += Values[CurrentLevel - 1];
        }

        public override void RemoveChip(Player player)
        {
            player.data.ShipBuffs.MoveSpeedIncrease -= Values[CurrentLevel - 1];
        }
    }
}