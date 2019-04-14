using System;
using System.Numerics;
using Box2DSharp.Collision.Shapes;
using Box2DSharp.Dynamics;
using NUnit.Framework;

namespace VoidTime.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [TestCase(5000)]
        public void PhysicsStressTest(int count)
        {
            var world = new World();
            for (int i = 0; i < count; i++)
            {
                var def = new BodyDef();
                var body = world.CreateBody(def);
                body.IsBullet = true;
                body.SetLinearVelocity(new Vector2(1, 0));
                body.CreateFixture(new CircleShape(), 1);
            }

            for (int i = 0; i < 1000; i++)
            {
                world.Step(0.16f, 3, 8);
            }
        }
    }
}
