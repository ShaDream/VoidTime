using System;
using System.Drawing;
using Box2DSharp.Dynamics;
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
            var map = new GameMap(new Size(2, 2), new Size(100, 100), new World());
        }

        [Test]
        public void WithSomeRightObjects()
        {
            var objects = new[] { new GameObject(), new GameObject { Position = new Vector2D(5, 5) }, };
            var map = new GameMap(new Size(2, 2), new Size(100, 100), new World(), objects);
        }

        [Test]
        public void WithSomeNotRightObjects()
        {
            var objects = new[] { new GameObject(), new GameObject { Position = new Vector2D(10005, 10005) }, };
            try
            {
                new GameMap(new Size(2, 2), new Size(100, 100), new World(), objects);
                Assert.Fail("Should Arguments out of range");
            }
            catch { }
        }

        [Test]
        public void DestroyObject()
        {
            var objects = new[]
            {
                new GameObject() { Position = new Vector2D(1, 1) },
                new GameObject(){ Position = new Vector2D(2, 2) },
                new GameObject(){ Position = new Vector2D(15, 15) }
            };
            var camera = new Camera(new Size(2, 2), objects[2]);
            var map = new GameMap(new Size(2, 2), new Size(10, 10), new World(), objects);
            var objectsFromMap = map.GetGameObjects(camera);
            objectsFromMap[1].Destoy();
            objectsFromMap = map.GetGameObjects(camera, camera.Size);
            Assert.AreEqual(1, objectsFromMap.Count);
        }

        [Test]
        public void MoveObject()
        {
            var objects = new[]
            {
                new GameObject {Position =  new Vector2D(1, 1)},
                new GameObject { Position =  new Vector2D(2, 2)},
                new GameObject() {Position =  new Vector2D(15, 15)}
            };
            var camera = new Camera(new Size(2, 2), objects[2]);
            var map = new GameMap(new Size(2, 2), new Size(10, 10), new World(), objects);
            var objectsFromMap = map.GetGameObjects(camera, camera.Size);
            objectsFromMap[0].Position = new Vector2D(3, 3);
            objectsFromMap = map.GetGameObjects(camera);
            Assert.AreEqual(2, objectsFromMap.Count);
            camera.FollowTo = objects[0];
            camera.Update();
            objectsFromMap = map.GetGameObjects(camera);
            Assert.AreEqual(3, objectsFromMap.Count);
        }

        [Test]
        public void FailCameraSize()
        {
            var player = new[] { new GameObject { Position = new Vector2D(1, 1) } };
            var camera = new Camera(new Size(20, 20), player[0]);
            var map = new GameMap(new Size(2, 2), new Size(10, 10), new World(), player);
            try
            {
                var objectsFromMap = map.GetGameObjects(camera, camera.Size);
                Assert.Fail("Should Arguments out of range");
            }
            catch { }
        }
    }
}
