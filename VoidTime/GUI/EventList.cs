using System;
using System.Collections.Generic;

namespace VoidTime.GUI
{
    public class EventList<T>
    {
        private readonly List<T> objects = new List<T>();

        public void Add(T item)
        {
            objects.Add(item);
            ItemAdded?.Invoke(item);
        }

        public void Remove(T item)
        {
            objects.Remove(item);
            ItemRemoved?.Invoke(item);
        }

        public event Action<T> ItemAdded;
        public event Action<T> ItemRemoved;
    }
}