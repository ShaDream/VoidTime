using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box2DSharp.Collision.Shapes;
using Box2DSharp.Dynamics;

namespace VoidTime.Logic.Objects
{
    class Blast : PhysicalGameObject
    {
        private Vector2D velocity;
        private const float speed = 200;

        public Blast(Vector2D possition, double angle)
        {
            Position = possition;
            velocity = new Vector2D(speed, 0).Rotate(angle);
        }

        public override void Update()
        {
            SetLinearVelocity(velocity);
        }

        public override void CreatePhysics(World world)
        {
            var bodyDef = CreateBodyDef();
            bodyDef.FixedRotation = true;
            bodyDef.Position = ConvertToPhysicsVector(Position);
            bodyDef.BodyType = BodyType.DynamicBody;

            var shape = new PolygonShape();
            shape.SetAsBox(30 * ScaleFactor, 40 * ScaleFactor);
            Body = world.CreateBody(bodyDef);
            var fixture = Body.CreateFixture(shape, 1);
            Fixtures.Add(fixture);
            fixture.IsSensor = true;
            Body.IsBullet = true;
        }
    }
}
