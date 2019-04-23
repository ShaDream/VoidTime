namespace VoidTime
{
    public class MovingSpeedUpChip : Chip
    {
        public override void SetChip(Ship ship)
        {
            ship.Data.ShipBuffs.MoveSpeedIncrease += Values[CurrentLevel - 1];
        }

        public override void RemoveChip(Ship ship)
        {
            ship.Data.ShipBuffs.MoveSpeedIncrease -= Values[CurrentLevel - 1];
        }
    }
}