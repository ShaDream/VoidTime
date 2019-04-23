namespace VoidTime
{
    public class CriticalUpChip : Chip
    {
        public override void SetChip(Ship ship)
        {
            ship.Data.ShipBuffs.CriticalChanceIncrease += Values[CurrentLevel - 1];
        }

        public override void RemoveChip(Ship ship)
        {
            ship.Data.ShipBuffs.CriticalChanceIncrease -= Values[CurrentLevel - 1];
        }
    }
}