using System;
using System.Collections.Generic;
using System.Linq;
using Box2DSharp.Collision.Shapes;
using Box2DSharp.Dynamics;
using Box2DSharp.Dynamics.Contacts;

namespace VoidTime
{
    public class Blast : PhysicalGameObject
    {
        private const float speed = 3000;
        private readonly HashSet<Type> damagableTypes;

        private readonly float damage;

        private readonly Ship owner;
        private readonly Vector2D velocity;
        private float remainingRange;
        public double Angle { get; }

        /// <param name="damagableTypes">Only Ship class allowed</param>
        public Blast(Vector2D possition, double angle, float damage, Ship owner, float range,
                     params Type[] damagableTypes)
        {
            Position = possition;
            remainingRange = range;
            Angle = angle + Math.PI / 2;
            velocity = new Vector2D(speed, 0).Rotate(angle);
            this.owner = owner;
            this.damagableTypes = new HashSet<Type>(damagableTypes.Select(x =>
            {
                if (!x.IsSubclassOf(typeof(Ship)) && x != typeof(Ship))
                    throw new ArgumentException("Only Ship allowed in blast damagable types");
                return x;
            }));
            this.damage = damage;
        }

        public override void Update()
        {
            SetLinearVelocity(velocity);
            if (remainingRange <= 0)
            {
                Instantiate(new Explosion(Position));
                Destoy();
            }

            remainingRange -= velocity.Magnitude * 0.018f;
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
            if (owner is Player && other.Body.UserData is MapEnemy ||
                owner is MapEnemy && other.Body.UserData is Player)
            {
                var player = (Player) (owner is Player ? owner : other.Body.UserData);
                var enemy = (MapEnemy) (owner is MapEnemy ? owner : other.Body.UserData);
                player.StartBattle(enemy);
            }

            if (!damagableTypes.Contains(other.Body.UserData.GetType()) || other.Body.UserData == owner)
                return;

            (other.Body.UserData as Ship)?.Data.ShipStats.GetDamage(damage);
            Destoy();
        }
    }
}