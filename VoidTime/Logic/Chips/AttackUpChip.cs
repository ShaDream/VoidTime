using System;
using System.Drawing;

namespace VoidTime
{
    public class AttackUpChip : Chip
    {
        public override void SetChip(Player player)
        {
            player.data.ShipBuffs.AttackUp += Values[CurrentLevel - 1];
        }

        public override void RemoveChip(Player player)
        {
            player.data.ShipBuffs.AttackUp -= Values[CurrentLevel - 1];
        }
    }
}