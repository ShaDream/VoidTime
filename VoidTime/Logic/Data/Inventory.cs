using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace VoidTime
{
    public struct Inventory
    {
        private readonly List<IItem> items;
        private readonly Ship owner;
        public int Money;

        public Inventory(Ship owner)
        {
            this.owner = owner;
            items = new List<IItem>();
            Money = 0;
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
            if (items.Contains(chip) && owner.Chips.IsCanAdd(chip))
            {
                Remove(chip);
                owner.Chips.Add(chip);
            }
        }
    }
}