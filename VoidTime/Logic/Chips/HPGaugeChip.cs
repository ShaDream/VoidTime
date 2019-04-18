using System;
using System.Drawing;

namespace VoidTime
{
    public class HPGaugeChip : Chip
    {
        public override void SetChip(Player player)
        {
            player.stats.DisplayHP = true;
        }

        public override void RemoveChip(Player player)
        {
            player.stats.DisplayHP = false;
        }
    }
}