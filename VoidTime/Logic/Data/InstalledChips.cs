using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VoidTime
{
    public struct InstalledChips
    {
        private List<Chip> chips;
        private int maxWeight;
        private int curWeight;
        private Ship owner;

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
            owner.data.UpdateFields();
        }

        public void Remove(Chip chip)
        {
            chips.Remove(chip);
            chip.RemoveChip(owner);
            owner.data.UpdateFields();
            owner.inventory.Add(chip);
        }

        public IReadOnlyList<Chip> GetChips => new ReadOnlyCollection<Chip>(chips);
        public bool IsCanAdd(Chip chip)
        {
            return maxWeight - curWeight > chip.CurrentCost;
        }
    }
}