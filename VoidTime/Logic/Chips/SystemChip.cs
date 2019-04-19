using System;
using System.Drawing;

namespace VoidTime
{
    public class SystemChip : Chip
    {
        public override void SetChip(Player player)
        {
            player.data.ShipBuffs.Destroy = false;
        }

        public override void RemoveChip(Player player)
        {
            player.data.ShipBuffs.Destroy = true;
        }
    }
}