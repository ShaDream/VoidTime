using System;
using System.Collections.Generic;

namespace VoidTime
{
    public class FrameListComparator<T>
    {
        private HashSet<T> lastFrame = new HashSet<T>();

        public void SetNewFrameData(List<T> newData)
        {
            foreach (var item in newData)
            {
                if (!lastFrame.Contains(item))
                    ItemAdded?.Invoke(item);
                lastFrame.Remove(item);
            }

            foreach (var removedItem in lastFrame) ItemRemoved?.Invoke(removedItem);

            lastFrame = new HashSet<T>(newData);
        }

        public bool Contains(T item)
        {
            return lastFrame.Contains(item);
        }

        public event Action<T> ItemAdded;
        public event Action<T> ItemRemoved;
    }
}