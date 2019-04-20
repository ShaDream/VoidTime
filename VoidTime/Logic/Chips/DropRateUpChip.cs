namespace VoidTime
{
    public class DropRateUpChip : Chip
    {
        public override void SetChip(Ship ship)
        {
            ship.Data.ShipBuffs.DropRateChanceIncrease += Values[CurrentLevel - 1];
        }

        public override void RemoveChip(Ship ship)
        {
            ship.Data.ShipBuffs.DropRateChanceIncrease -= Values[CurrentLevel - 1];
        }
    }
}