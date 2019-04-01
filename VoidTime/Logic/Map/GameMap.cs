using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Box2DSharp.Dynamics;

namespace VoidTime
{
    public class GameMap
    {
        #region Private Fields

        private readonly Chunk[,] chunks;
        private readonly Size chunkSize;
        private HashSet<Point> lastChunks = new HashSet<Point>();
        private World world;

        #endregion

        #region Public Properties

        public Size MapSizeInChunks { get; }
        public Size MapSize { get; }

        #endregion

        #region Сonstructor

        public GameMap(Size mapSizeInChunks, Size chunkSize, World world) : this(mapSizeInChunks, chunkSize, world, new List<GameObject>()) { }

        public GameMap(Size mapSizeInChunks, Size chunkSize, World world, IEnumerable<GameObject> gameObjects)
        {
            this.world = world;
            MapSizeInChunks = mapSizeInChunks;
            this.chunkSize = chunkSize;
            chunks = new Chunk[MapSizeInChunks.Width, MapSizeInChunks.Height];
            Initialization();
            AddGameObjects(gameObjects);
            MapSize = new Size(MapSizeInChunks.Width * chunkSize.Width, MapSizeInChunks.Height * chunkSize.Height);
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

        public List<GameObject> GetGameObjects(BasicCamera basicCamera)
        {
            var chunksCoordinate = GetChunksCoordinateFromCamera(basicCamera);

            var isOnce = false;
            foreach (var point in chunksCoordinate)
                if (!lastChunks.Contains(point))
                {
                    isOnce = true;
                    chunks[point.X, point.Y].ResumePhysics(world);
                }
            
            if(isOnce)
                lastChunks = new HashSet<Point>(chunksCoordinate);

            return chunksCoordinate
                .Select(coordinate => chunks[coordinate.X, coordinate.Y].GetGameObjects())
                .SelectMany(x => x).ToList();
        }

        public void UpdateMap(BasicCamera camera)
        {
            var chunksCoordinate = GetChunksCoordinateFromCamera(camera);
            chunksCoordinate
                .ForEach(CheckChunk);
        }

        #endregion

        #region Private Methods

        private void Initialization()
        {
            for (var i = 0; i < MapSizeInChunks.Width; i++)
                for (var j = 0; j < MapSizeInChunks.Height; j++)
                {
                    var coordinates = new Point(i, j);
                    chunks[i, j] = new Chunk(coordinates, chunkSize);
                }
        }

        private List<Point> GetChunksCoordinateFromCamera(BasicCamera camera)
        {
            var bottomLeftPoint = ChunkCoordinateFromVector(camera.BottomLeft);
            var topRightPoint = ChunkCoordinateFromVector(camera.TopRight);
            var xMin = Math.Max(bottomLeftPoint.X - 1, 0);
            var xMax = Math.Min(topRightPoint.X + 1, MapSizeInChunks.Width - 1);
            var yMin = Math.Max(bottomLeftPoint.Y - 1, 0);
            var yMax = Math.Min(topRightPoint.Y + 1, MapSizeInChunks.Height - 1);
            var result = new List<Point>();
            for (var x = xMin; x <= xMax; x++)
            {
                for (var y = yMin; y <= yMax; y++)
                {
                    result.Add(new Point(x, y));
                }
            }
            return result;
        }

        private void CheckChunk(Point chunkCoordinate)
        {
            var gameObjects = chunks[chunkCoordinate.X, chunkCoordinate.Y].GetGameObjects();
            for (var i = 0; i < gameObjects.Count; i++)
            {
                var gameObject = gameObjects[i];
                var chunkCoordinateObject = ChunkCoordinateFromVector(gameObject.Position);
                if (chunkCoordinateObject == chunkCoordinate) continue;
                chunks[chunkCoordinate.X, chunkCoordinate.Y].RemoveGameObject(gameObject);
                chunks[chunkCoordinateObject.X, chunkCoordinateObject.Y].AddGameObject(gameObject);
                i--;
            }
        }

        private Point ChunkCoordinateFromVector(Vector2D vector)
        {
            var x = (int)Math.Floor(vector.X / chunkSize.Width);
            var y = (int)Math.Floor(vector.Y / chunkSize.Height);
            if (x >= MapSizeInChunks.Width || y >= MapSizeInChunks.Height || x < 0 || y < 0)
                throw new IndexOutOfRangeException(
                    $"Index was out of range when {typeof(Vector2D)}, was convert to {typeof(Chunk)} coordinate");
            return new Point(x, y);
        }

        #endregion
    }
}
