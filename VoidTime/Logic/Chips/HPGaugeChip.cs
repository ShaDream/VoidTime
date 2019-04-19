using System;
using System.Drawing;

namespace VoidTime
{
    public class HPGaugeChip : Chip
    {
        public override void SetChip(Player player)
        {
            player.data.ShipBuffs.DisplayHP = true;
        }

        public override void RemoveChip(Player player)
        {
            player.data.ShipBuffs.DisplayHP = false;
        }
    }
}