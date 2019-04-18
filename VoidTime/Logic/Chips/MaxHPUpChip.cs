using System;
using System.Drawing;

namespace VoidTime
{
    public class MaxHPUpChip : Chip
    {
        public override void SetChip(Player player)
        {
            player.stats.HPUp += Values[CurrentLevel - 1];
        }

        public override void RemoveChip(Player player)
        {
            player.stats.HPUp -= Values[CurrentLevel - 1];
        }
    }
}