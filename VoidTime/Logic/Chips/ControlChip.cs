using System.Drawing;

namespace VoidTime
{
    public class ControlChip : Chip
    {
        public override void SetChip(Player player)
        {
            player.stats.CanMove = true;
        }

        public override void RemoveChip(Player player)
        {
            player.stats.CanMove = false;
        }
    }
}