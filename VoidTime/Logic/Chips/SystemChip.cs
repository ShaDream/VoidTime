namespace VoidTime
{
    public class SystemChip : Chip
    {
        public override void SetChip(Ship ship)
        {
            ship.Data.ShipBuffs.Destroy = false;
        }

        public override void RemoveChip(Ship ship)
        {
            ship.Data.ShipBuffs.Destroy = true;
        }
    }
}