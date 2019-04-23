namespace VoidTime
{
    public class ControlChip : Chip
    {
        public override void SetChip(Ship ship)
        {
            ship.Data.ShipBuffs.CanMove = true;
        }

        public override void RemoveChip(Ship ship)
        {
            ship.Data.ShipBuffs.CanMove = false;
        }
    }
}