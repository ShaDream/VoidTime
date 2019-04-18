namespace VoidTime
{
    public class DropRateUpChip : Chip
    {
        public override void SetChip(Player player)
        {
            player.stats.DropRateChanceIncrease += Values[CurrentLevel - 1];
        }

        public override void RemoveChip(Player player)
        {
            player.stats.DropRateChanceIncrease -= Values[CurrentLevel - 1];
        }
    }
}