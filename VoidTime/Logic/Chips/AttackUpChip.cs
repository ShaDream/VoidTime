using System;
using System.Drawing;

namespace VoidTime.Logic.Chips
{
    public class AttackUpChip : IChip
    {
        public string Name { get; set; }
        public int CurrentLevel { get; set; }
        public float[] Values { get; set; }
        public Size[] Sizes { get; set; }
        public string Description { get; set; }
        public ChipType Type { get; set; }
        public int MaxLevel { get; set; }

        public void SetChip(Player player)
        {
            player.stats.AttackUp += Values[CurrentLevel - 1];
        }

        public void RemoveChip(Player player)
        {
            player.stats.AttackUp -= Values[CurrentLevel - 1];
        }
    }
}