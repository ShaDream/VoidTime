﻿using System;
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

        private readonly float damage;
        private readonly HashSet<Type> damagableTypes;

        private readonly GameObject owner;
        private readonly Vector2D velocity;
        public double Angle { get; }
        public float remainingRange;

        /// <param name="damagableTypes">Only Ship class allowed</param>
        public Blast(Vector2D possition, double angle, float damage, GameObject owner, float range,
            params Type[] damagableTypes)
        {
            Position = possition;
            remainingRange = range;
            Angle = angle + Math.PI / 2;
            velocity = new Vector2D(speed, 0).Rotate(angle);
            this.owner = owner;
            this.damagableTypes = new HashSet<Type>(damagableTypes.Select(x =>
            {
                if (!x.IsSubclassOf(typeof(Ship)))
                    throw new ArgumentException("Only BattleShipObject allowed in blast damagable types");
                return x;
            }));
            this.damage = damage;
        }

        public override void Update()
        {
            SetLinearVelocity(velocity);
            if (remainingRange <= 0)
                Destoy();
            remainingRange -= velocity.Magnitude*0.018f;
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
                (other.Body.UserData as Ship)?.Data.ShipStats.GetDamage(damage);
                Destoy();
            }
        }
    }
}