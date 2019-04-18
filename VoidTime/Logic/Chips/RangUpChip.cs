using System;
using System.Drawing;

namespace VoidTime
{
    public class RangUpChip : Chip
    {
        public override void SetChip(Player player)
        {
            player.stats.RangUp += Values[CurrentLevel - 1];
        }

        public override void RemoveChip(Player player)
        {
            player.stats.RangUp -= Values[CurrentLevel - 1];
        }
    }
}