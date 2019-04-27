using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace VoidTime
{
    public class Inventory
    {
        private readonly List<IItem> items;
        private readonly Ship owner;
        private int money;
        public int Money
        {
            get => money;
            set
            {
                money = value;
                OnChangeMoney?.Invoke();
            }
        }

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

        public IReadOnlyCollection<IItem> GetItems => new ReadOnlyCollection<IItem>(items);

        public IReadOnlyCollection<IItem> GetItemsByType(Type type)
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

        public event Action OnChangeMoney;
    }
}