using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box2DSharp.Collision.Shapes;
using Box2DSharp.Dynamics;
using Box2DSharp.Dynamics.Contacts;

namespace VoidTime.Logic.Objects
{
    class Blast : PhysicalGameObject
    {
        private Vector2D velocity;
        private const float speed = 1000;
        public double Angle { get; }
        private int currentLiveTime;
        private const int LiveTime = 120;

        public Blast(Vector2D possition, double angle)
        {
            Position = possition;
            Angle = angle + Math.PI / 2;
            velocity = new Vector2D(speed, 0).Rotate(angle);
        }

        public override void Update()
        {
            SetLinearVelocity(velocity);
            if (currentLiveTime >= LiveTime)
                Destoy();
            currentLiveTime++;
        }

        public override void CreatePhysics(World world)
        {
            var bodyDef = CreateBodyDef();
            bodyDef.FixedRotation = true;
            bodyDef.Position = ConvertToPhysicsVector(Position);
            bodyDef.BodyType = BodyType.KinematicBody;

            var shape = new PolygonShape();
            shape.SetAsBox(30 * ScaleFactor, 40 * ScaleFactor);
            Body = world.CreateBody(bodyDef);
            var fixture = Body.CreateFixture(shape, 1);
            Fixtures.Add(fixture);
            fixture.IsSensor = true;
            Body.IsBullet = true;
        }

        public override void BeginContact(Contact contact)
        {
            var other = contact.FixtureA.Body == Body ? contact.FixtureB : contact.FixtureA;
            if (other.Body.UserData is Blast blast)
            {
                blast.Destoy();
            }
        }
    }
}
