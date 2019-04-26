namespace VoidTime
{
    public class DefenceUpChip : Chip
    {
        public override void SetChip(Ship ship)
        {
            ship.Data.ShipBuffs.DefenceUp += Values[CurrentLevel - 1];
        }

        public override void RemoveChip(Ship ship)
        {
            ship.Data.ShipBuffs.DefenceUp -= Values[CurrentLevel - 1];
        }
    }
}