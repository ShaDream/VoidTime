using System;
using System.Drawing;

namespace VoidTime.Logic.Chips
{
    public interface IChip
    {
        string Name { get; set; }
        int CurrentLevel { get; set; }
        float[] Values { get; set; }
        Size[] Sizes { get; set; }
        string Description { get; set; }
        ChipType Type { get; set; }
        int MaxLevel { get; set; }

        void SetChip(Player player);
        void RemoveChip(Player player);
    }
}