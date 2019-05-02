using System;
using System.Numerics;
using Box2DSharp.Collision.Shapes;
using Box2DSharp.Dynamics;
using NUnit.Framework;

namespace VoidTime.Tests
{
    [TestFixture]
    public class PhysicsTests
    {
        [TestCase(100)]
        [TestCase(500)]
        [TestCase(1000)]
        [TestCase(2500)]
        [TestCase(5000)]
        public void PhysicsStressTest(int count)
        {
            var world = new World();
            for (var i = 0; i < count; i++)
            {
                var def = new BodyDef();
                var body = world.CreateBody(def);
                body.IsBullet = true;
                body.SetLinearVelocity(new Vector2(1, 0));
                body.CreateFixture(new CircleShape(), 1);
            }

            for (var i = 0; i < 1000; i++)
            {
                world.Step(0.16f, 3, 8);
            }
        }

        [Test]
        public void CreatePhysics()
        {
            var w = new World();
            var obj = new BattleEnemy();
            obj.CreatePhysics(w);
            Assert.AreEqual(1, w.BodyList.Count);
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(500)]
        public void DestroyPhysics(int count)
        {
            var w = new World();
            for (var i = 0; i < count; i++)
            {
                var obj = new BattleEnemy();
                obj.CreatePhysics(w);
                PhysicsBodyRemover.Add(obj.Body, obj.Fixtures);
            }
            w.StepWithDelete(0.1f, 1, 1);
            Assert.AreEqual(0, w.BodyList.Count);
        }

    }
}
