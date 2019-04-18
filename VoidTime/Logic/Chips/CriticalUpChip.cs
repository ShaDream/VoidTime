using System;
using System.Drawing;

namespace VoidTime
{
    public abstract class CriticalUpChip : Chip
    {
        public override void SetChip(Player player)
        {
            player.stats.CriticalChanceIncrease += Values[CurrentLevel - 1];
        }

        public override void RemoveChip(Player player)
        {
            player.stats.CriticalChanceIncrease -= Values[CurrentLevel - 1];
        }
    }
}