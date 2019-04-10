using System;
using System.Collections.Generic;
using System.Linq;
using Box2DSharp.Collision.Shapes;
using Box2DSharp.Dynamics;
using Box2DSharp.Dynamics.Contacts;

namespace VoidTime
{
    class Blast : PhysicalGameObject
    {
        private Vector2D velocity;
        private const float speed = 1000;
        public double Angle { get; }
        private int currentLiveTime;
        private const int LiveTime = 120;

        private readonly float damage;

        private GameObject owner;
        private HashSet<Type> damagableTypes;

        /// <param name="damagableTypes">Only BattleShipObject allowed</param>
        public Blast(Vector2D possition, double angle, float damage, GameObject owner,
            params Type[] damagableTypes)
        {
            Position = possition;
            Angle = angle + Math.PI / 2;
            velocity = new Vector2D(speed, 0).Rotate(angle);
            this.owner = owner;
            this.damagableTypes = new HashSet<Type>(damagableTypes.Select(x =>
            {
                if (!(x.IsSubclassOf(typeof(BattleShipObject))))
                    throw new ArgumentException("Only BattleShipObject allowed in blast damagable types");
                return x;
            }));
            this.damage = damage;
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
            if (damagableTypes.Contains(other.Body.UserData.GetType()) && other.Body.UserData != owner)
            {
                (other.Body.UserData as BattleShipObject)?.GetDamage(damage);
                Destoy();
            }
        }
    }
}
