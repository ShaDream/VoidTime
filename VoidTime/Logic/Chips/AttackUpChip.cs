using System;
using System.Drawing;

namespace VoidTime
{
    public class AttackUpChip : Chip
    {
        public override void SetChip(Ship ship)
        {
            ship.Data.ShipBuffs.AttackUp += Values[CurrentLevel - 1];
        }

        public override void RemoveChip(Ship ship)
        {
            ship.Data.ShipBuffs.AttackUp -= Values[CurrentLevel - 1];
        }
    }
}