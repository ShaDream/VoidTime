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
    }
}
