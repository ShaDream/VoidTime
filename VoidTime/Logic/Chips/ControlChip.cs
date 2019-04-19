using System.Drawing;

namespace VoidTime
{
    public class ControlChip : Chip
    {
        public override void SetChip(Ship ship)
        {
            ship.data.ShipBuffs.CanMove = true;
        }

        public override void RemoveChip(Ship ship)
        {
            ship.data.ShipBuffs.CanMove = false;
        }
    }
}