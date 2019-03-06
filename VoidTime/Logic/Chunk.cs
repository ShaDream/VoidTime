using System;
using System.Collections.Generic;
using System.Drawing;

namespace VoidTime
{
    public class Chunk
    {
        public Point Coordinates { get; private set; }
        public Size Size { get; private set; }

        private List<GameObject> gameObjects = new List<GameObject>();

        #region Constructor

        public Chunk()
        {

        }

        public Chunk(Point coordinates, Size size)
        {
            Coordinates = coordinates;
            Size = size;
        }

        #endregion

        public void AddGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        public void AddGameObjects(IEnumerable<GameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                this.gameObjects.Add(gameObject);
            }
        }

        public List<GameObject> GetGameObjects()
        {
            return gameObjects;
        }

        public event Action<GameObject> GameObjectOutOfChunk;

    }
}