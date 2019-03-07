using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
            AddGameObjects(gameObjects);
        }

        #endregion

        #region Public Methods

        public void AddGameObjects(IEnumerable<GameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                var chunkCoordinate = ChunkCoordinateFromVector(gameObject.Position);
                chunks[chunkCoordinate.X, chunkCoordinate.Y].AddGameObject(gameObject);
            }
        }

        public void AddGameObject(GameObject gameObject) =>
            AddGameObjects(new List<GameObject> { gameObject });

        public List<GameObject> GetGameObjects(Camera camera)
        {
            var points = camera.ToVectors();
            return points
                .Select(ChunkCoordinateFromVector)
                .Distinct()
                .Select(coordinate =>
                    {
                        CheckChunk(coordinate);
                        return chunks[coordinate.X, coordinate.Y].GetGameObjects();
                    })
                .SelectMany(x => x).ToList();
        }

        private void CheckChunk(Point chunkCoordinate)
        {
            var gameObjects = chunks[chunkCoordinate.X, chunkCoordinate.Y].GetGameObjects();
            for (var i = 0; i < gameObjects.Count; i++)
            {
                var gameObject = gameObjects[i];
                var chunkCoordinateObject = ChunkCoordinateFromVector(gameObject.Position);
                if (chunkCoordinateObject == chunkCoordinate) continue;
                gameObject.Destoy();
                chunks[chunkCoordinateObject.X, chunkCoordinateObject.Y].AddGameObject(gameObject);
                i--;
            }
        }

        private Point ChunkCoordinateFromVector(Vector2D vector)
        {
            var x = (int)Math.Floor(vector.X / chunkSize.Width);
            var y = (int)Math.Floor(vector.Y / chunkSize.Height);
            if (x >= MapSize.Width || y >= MapSize.Height || x < 0 || y < 0)
                throw new IndexOutOfRangeException(
                    $"Index was out of range when {typeof(Vector2D)}, was convert to {typeof(Chunk)} coordinate");
            return new Point(x, y);
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
