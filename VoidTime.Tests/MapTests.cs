using System;
using System.Drawing;
using NUnit.Framework;
using VoidTime;

namespace VoidTime.Tests
{
    [TestFixture]
    public class MapTests
    {
        [Test]
        public void With2x2ChunksWithoutGameObjects()
        {
            var map = new GameMap(new Size(2, 2), new Size(100, 100));
        }

        [Test]
        public void WithSomeRightObjects()
        {
            var objects = new[] { new GameObject("Player"), new GameObject("Planet", new Vector2D(5, 5)), };
            var map = new GameMap(new Size(2, 2), new Size(100, 100), objects);
        }

        [Test]
        public void WithSomeNotRightObjects()
        {
            var objects = new[] { new GameObject("Player"), new GameObject("Planet", new Vector2D(10005, 10005)), };
            try
            {
                new GameMap(new Size(2, 2), new Size(100, 100), objects);
                Assert.Fail("Should Arguments out of range");
            }
            catch { }
        }

        [Test]
        public void DestroyObject()
        {
            var objects = new[]
            {
                new GameObject("Player", new Vector2D(1, 1)),
                new GameObject("Planet", new Vector2D(2, 2)),
                new GameObject("Star", new Vector2D(15, 15))
            };
            var camera = new Camera(new Size(2, 2), objects[2]);
            var map = new GameMap(new Size(2, 2), new Size(10, 10), objects);
            var objectsFromMap = map.GetGameObjects(camera);
            objectsFromMap[0].Destoy();
            objectsFromMap = map.GetGameObjects(camera);
            Assert.AreEqual(0, objectsFromMap.Count);
        }

        [Test]
        public void MoveObject()
        {
            var objects = new[]
            {
                new GameObject("Player", new Vector2D(1, 1)),
                new GameObject("Planet", new Vector2D(2, 2)),
                new GameObject("Star", new Vector2D(15, 15))
            };
            var camera = new Camera(new Size(2, 2), objects[2]);
            var map = new GameMap(new Size(2, 2), new Size(10, 10), objects);
            var objectsFromMap = map.GetGameObjects(camera);
            objectsFromMap[0].Position = new Vector2D(3, 3);
            objectsFromMap = map.GetGameObjects(camera);
            Assert.AreEqual(0, objectsFromMap.Count);
            camera.FollowTo = objects[0];
            camera.Update();
            objectsFromMap = map.GetGameObjects(camera);
            Assert.AreEqual(3, objectsFromMap.Count);
        }

        [Test]
        public void FailCameraSize()
        {
            var player =  new[] { new GameObject("Player", new Vector2D(1, 1)) } ;
            var camera = new Camera(new Size(20, 20), player[0]);
            var map = new GameMap(new Size(2, 2), new Size(10, 10), player);
            try
            {
                var objectsFromMap = map.GetGameObjects(camera);
                Assert.Fail("Should Arguments out of range");
            }
            catch { }
        }
    }
}
