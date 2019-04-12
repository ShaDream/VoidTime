using System;
using Box2DSharp.Collision.Shapes;
using Box2DSharp.Dynamics;
using Box2DSharp.Dynamics.Contacts;

namespace VoidTime
{
    public class Planet : PhysicalGameObject {


        public override void CreatePhysics(World world)
        {
            var bodyDef = CreateBodyDef();
            bodyDef.FixedRotation = true;
            bodyDef.Position = ConvertToPhysicsVector(Position);
            bodyDef.BodyType = BodyType.StaticBody;

            var shape = new CircleShape {Radius = 5};
            Body = world.CreateBody(bodyDef);
            var fixture = Body.CreateFixture(shape, 1);
            Fixtures.Add(fixture);
            fixture.IsSensor = true;
        }

        public override void BeginContact(Contact contact)
        {
            var other = contact.FixtureA.Body == Body ? contact.FixtureB : contact.FixtureA;
            if (other.Body.UserData is Player)
            {

            }
        }
        public override void EndContact(Contact contact)
        {
            var other = contact.FixtureA.Body == Body ? contact.FixtureB : contact.FixtureA;
            if (other.Body.UserData is Player)
            {

            }
        }
    }
}