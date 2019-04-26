using System;

namespace VoidTime
{
    public class Chip : IItem
    {
        public virtual int CurrentLevel { get; set; }
        public virtual float[] Values { get; set; }
        public virtual int[] Costs { get; set; }
        public virtual string Description { get; set; }
        public virtual ChipType Type { get; set; }
        public virtual int MaxLevel { get; set; }
        public int CurrentCost => Costs[CurrentLevel - 1];
        public float CurrentValue => Values[CurrentLevel - 1];
        public virtual string Name { get; set; }
        public int Price { get; set; }

        public string GetInfo()
        {
            return $"{Name}{(CurrentLevel != 1 ? $" +{CurrentLevel}" : "")}:\n" +
                   $"{Description}\n" +
                   $"Value: {CurrentValue}\n";
        }

        public virtual void SetChip(Ship ship) { }
        public virtual void RemoveChip(Ship ship) { }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (!typeof(object).IsSubclassOf(typeof(Chip))) return false;

            return GetType() == obj.GetType() && CurrentLevel == ((Chip) obj).CurrentLevel;
        }

        public Chip Copy()
        {
            var result = (Chip)Activator.CreateInstance(GetType());
            result.CurrentLevel = CurrentLevel;
            result.Costs = Costs;
            result.Description = Description;
            result.MaxLevel = MaxLevel;
            result.Name = Name;
            result.Price = Price;
            result.Type = Type;
            result.Values = Values;
            return result;
        }
    }
}