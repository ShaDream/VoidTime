using System;
using System.Drawing;

namespace VoidTime
{
    public class Chip
    {
        public virtual string Name { get; set; }
        public virtual int CurrentLevel { get; set; }
        public virtual float[] Values { get; set; }
        public virtual int[] Costs { get; set; }
        public virtual string Description { get; set; }
        public virtual ChipType Type { get; set; }
        public virtual int MaxLevel { get; set; }

        public virtual void SetChip(Player player) { }
        public virtual void RemoveChip(Player player) { }
    }
}