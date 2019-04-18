using System;
using System.Drawing;

namespace VoidTime
{
    public class SystemChip : Chip
    {
        public override void SetChip(Player player)
        {
            player.stats.Destroy = false;
        }

        public override void RemoveChip(Player player)
        {
            player.stats.Destroy = true;
        }
    }
}