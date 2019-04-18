using System;
using System.Drawing;

namespace VoidTime
{
    public class AttackUpChip : Chip
    {
        public override void SetChip(Player player)
        {
            player.stats.AttackUp += Values[CurrentLevel - 1];
        }

        public override void RemoveChip(Player player)
        {
            player.stats.AttackUp -= Values[CurrentLevel - 1];
        }
    }
}