using System;
using System.Drawing;

namespace VoidTime
{
    public class DefenceUpChip : Chip
    {
        public override void SetChip(Player player)
        {
            player.stats.DefenceUp += Values[CurrentLevel - 1];
        }

        public override void RemoveChip(Player player)
        {
            player.stats.DefenceUp -= Values[CurrentLevel - 1];
        }
    }
}