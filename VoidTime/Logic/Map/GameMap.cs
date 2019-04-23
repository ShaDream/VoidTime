using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Box2DSharp.Dynamics;

namespace VoidTime
{
    public class GameMap
    {
        private readonly Chunk[,] chunks;
        private readonly Size chunkSize;
        private readonly FrameListComparator<Point> lastChunks = new FrameListComparator<Point>();
        private readonly World world;

        public Size MapSizeInChunks { get; }
        public Size MapSize { get; }

        public GameMap(Size mapSizeInChunks, Size chunkSize, World world, params GameObject[] gameObjects)
        {
            lastChunks.ItemAdded += ChunkLoad;
            lastChunks.ItemRemoved += ChuckUnload;

            this.world = world;
            MapSizeInChunks = mapSizeInChunks;
            this.chunkSize = chunkSize;
            chunks = new Chunk[MapSizeInChunks.Width, MapSizeInChunks.Height];
            Initialization();
            AddGameObjects(gameObjects);
            MapSize = new Size(MapSizeInChunks.Width * chunkSize.Width, MapSizeInChunks.Height * chunkSize.Height);
        }

        private void ChuckUnload(Point chunkCoordinate)
        {
            chunks[chunkCoordinate.X, chunkCoordinate.Y].ClearPhysicsObjects();
        }

        private void ChunkLoad(Point chunkCoordinate)
        {
            chunks[chunkCoordinate.X, chunkCoordinate.Y].ResumePhysics(world);
        }

        public void AddGameObjects(params GameObject[] gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                var chunkCoordinate = ChunkCoordinateFromVector(gameObject.Position);
                if (lastChunks.Contains(chunkCoordinate) && gameObject is PhysicalGameObject o)
                    o.CreatePhysics(world);
                chunks[chunkCoordinate.X, chunkCoordinate.Y].AddGameObject(gameObject);
            }
        }

        public void AddGameObject(GameObject gameObject)
        {
            AddGameObjects(gameObject);
        }

        public List<GameObject> GetGameObjects(BasicCamera basicCamera, Size overrun = new Size())
        {
            var chunksCoordinate = GetChunksCoordinateFromCamera(basicCamera, overrun);

            lastChunks.SetNewFrameData(chunksCoordinate);

            return chunksCoordinate
                   .Select(coordinate => chunks[coordinate.X, coordinate.Y].GetGameObjects())
                   .SelectMany(x => x).ToList();
        }

        public void UpdateMap(BasicCamera camera, Size overrun = new Size())
        {
            var chunksCoordinate = GetChunksCoordinateFromCamera(camera, overrun);
            chunksCoordinate
                .ForEach(CheckChunk);
        }


        private void Initialization()
        {
            for (var i = 0; i < MapSizeInChunks.Width; i++)
            for (var j = 0; j < MapSizeInChunks.Height; j++)
            {
                var coordinates = new Point(i, j);
                chunks[i, j] = new Chunk(coordinates, chunkSize);
                chunks[i, j].OnObjectCreate += AddGameObject;
            }
        }

        private List<Point> GetChunksCoordinateFromCamera(BasicCamera camera, Size overrun)
        {
            var bottomLeftPoint =
                ChunkCoordinateFromVector(camera.BottomLeft + new Vector2D(-overrun.Width, -overrun.Height));
            var topRightPoint =
                ChunkCoordinateFromVector(camera.TopRight + new Vector2D(overrun.Width, overrun.Height));
            var xMin = Math.Max(bottomLeftPoint.X, 0);
            var xMax = Math.Min(topRightPoint.X, MapSizeInChunks.Width - 1);
            var yMin = Math.Max(bottomLeftPoint.Y, 0);
            var yMax = Math.Min(topRightPoint.Y, MapSizeInChunks.Height - 1);
            var result = new List<Point>();
            for (var x = xMin; x <= xMax; x++)
            for (var y = yMin; y <= yMax; y++)
                result.Add(new Point(x, y));
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
                if (gameObject is PhysicalGameObject o && !lastChunks.Contains(chunkCoordinateObject))
                    o.DeletePhysics();
                chunks[chunkCoordinateObject.X, chunkCoordinateObject.Y].AddGameObject(gameObject);
                i--;
            }
        }

        private Point ChunkCoordinateFromVector(Vector2D vector)
        {
            var x = (int) Math.Floor(vector.X / chunkSize.Width);
            var y = (int) Math.Floor(vector.Y / chunkSize.Height);
            if (x >= MapSizeInChunks.Width || y >= MapSizeInChunks.Height || x < 0 || y < 0)
                throw new IndexOutOfRangeException(
                                                   $"Index was out of range when {typeof(Vector2D)}, was convert to {typeof(Chunk)} coordinate");
            return new Point(x, y);
        }
    }
}