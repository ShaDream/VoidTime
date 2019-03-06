using System;
using System.Collections.Generic;
using System.Drawing;

namespace VoidTime
{
    public class GameMap
    {
        #region Private Fields

        private readonly Chunk[,] chunks;
        private readonly Size chunkSize;

        #endregion

        #region Public Properties

        public Size MapSize { get; }

        #endregion

        #region Сonstructor

        public GameMap(Size mapSize, Size chunkSize) : this(mapSize, chunkSize, new List<GameObject>()) { }

        public GameMap(Size mapSize, Size chunkSize, IEnumerable<GameObject> gameObjects)
        {
            MapSize = mapSize;
            this.chunkSize = chunkSize;
            chunks = new Chunk[MapSize.Width, MapSize.Height];
            Initialization();
            AddGameObjectsToChunks(gameObjects);
        }

        #endregion

        #region Public Methods

        public void AddGameObjectsToChunks(IEnumerable<GameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                var x = (int)gameObject.Position.X / chunkSize.Width;
                var y = (int)gameObject.Position.Y / chunkSize.Height;
                if (x >= MapSize.Width || y >= MapSize.Height || x < 0 || y < 0)
                    throw new IndexOutOfRangeException(
                        $"Index was out of range when {typeof(GameObject)}, was initialize in {typeof(GameMap)}");
                chunks[x, y].AddGameObject(gameObject);
            }
        }

        #endregion

        #region Private Methods

        private void Initialization()
        {
            for (var i = 0; i < MapSize.Width; i++)
            for (var j = 0; j < MapSize.Height; j++)
            {
                var coordinates = new Point(i, j);
                chunks[i, j] = new Chunk(coordinates, chunkSize);
            }
        }

        #endregion
        
    }
}
