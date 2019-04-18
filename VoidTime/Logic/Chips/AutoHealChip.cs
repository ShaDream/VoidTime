using System;
using System.Drawing;

namespace VoidTime
{
    public class AutoHealChip : Chip
    {
        public override void SetChip(Player player)
        {
            player.stats.AutoHeal += Values[CurrentLevel - 1];
        }

        public override void RemoveChip(Player player)
        {
            player.stats.AutoHeal -= Values[CurrentLevel - 1];
        }
    }
}