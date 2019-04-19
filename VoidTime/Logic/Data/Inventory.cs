using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace VoidTime
{
    public struct Inventory
    {
        private List<IItem> items;
        private Ship owner;

        public Inventory(Ship owner)
        {
            this.owner = owner;
            items = new List<IItem>();
        }

        public void Add(IItem item)
        {
            items.Add(item);
        }

        public void Remove(IItem item)
        {
            items.Remove(item);
        }

        public IReadOnlyCollection<IItem> GetItems()
        {
            return new ReadOnlyCollection<IItem>(items);
        }

        public IReadOnlyCollection<IItem> GetItems(Type type)
        {
            return new ReadOnlyCollection<IItem>(items
                .Where(x => x.GetType() == type)
                .ToList());
        }

        public void InstallChip(Chip chip)
        {
            if (items.Contains(chip) && owner.chips.IsCanAdd(chip))
            {
                Remove(chip);
                owner.chips.Add(chip);
            }
        }
    }
}