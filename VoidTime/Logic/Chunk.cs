using System;
using System.Collections.Generic;
using System.Drawing;

namespace VoidTime
{
    public class Chunk
    {
        public Point Coordinates { get; private set; }
        public Size Size { get; private set; }

        private List<GameObject> GameObjects = new List<GameObject>();

        public void AddGameObject()
        {

        }

        public void AddGameObjects()
        {

        }

        public event Action<GameObject> GameObjectOutOfChunk;

    }
}