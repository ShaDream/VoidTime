using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VoidTime
{
    public struct InstalledChips
    {
        private readonly List<Chip> chips;
        private readonly int maxWeight;
        private int curWeight;
        private readonly Ship owner;

        public InstalledChips(Ship owner, int maxWeight)
        {
            chips = new List<Chip>();
            this.owner = owner;
            this.maxWeight = maxWeight;
            curWeight = 0;
        }

        public void Add(Chip chip)
        {
            if (!IsCanAdd(chip)) return;
            chips.Add(chip);
            curWeight += chip.CurrentCost;
            chip.SetChip(owner);
            owner.Data.UpdateFields();
        }

        public void Remove(Chip chip)
        {
            chips.Remove(chip);
            chip.RemoveChip(owner);
            owner.Data.UpdateFields();
            owner.Inventory.Add(chip);
            curWeight -= chip.CurrentCost;
        }

        public IReadOnlyList<Chip> GetChips => new ReadOnlyCollection<Chip>(chips);

        public bool IsCanAdd(Chip chip)
        {
            return maxWeight - curWeight >= chip.CurrentCost;
        }
    }
}