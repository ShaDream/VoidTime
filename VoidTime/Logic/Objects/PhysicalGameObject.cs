using System.Collections.Generic;
using System.Numerics;
using Box2DSharp.Collision.Collider;
using Box2DSharp.Dynamics;
using Box2DSharp.Dynamics.Contacts;

namespace VoidTime
{
    public abstract class PhysicalGameObject : GameObject
    {
        public const float ScaleFactor = 0.001F;

        private bool deleted;

        private Vector2D position;

        public Body Body { get; set; }
        public List<Fixture> Fixtures { get; set; } = new List<Fixture>();

        public override Vector2D Position
        {
            get
            {
                if (Body == null)
                    return position;

                var pos = Body.GetPosition();
                return new Vector2D(pos.X / ScaleFactor, pos.Y / ScaleFactor);
            }
            set
            {
                if (Body == null)
                    position = value;
                else
                    PhysicsPosition = new Vector2(value.X * ScaleFactor, value.Y * ScaleFactor);
            }
        }

        private Vector2 PhysicsPosition
        {
            get => Body.GetPosition();
            set => Body.SetTransform(value, Body.GetAngle());
        }

        public virtual void BeginContact(Contact contact) { }

        public virtual void EndContact(Contact contact) { }

        public virtual void PreSolve(Contact contact, in Manifold oldManifold) { }

        public virtual void PostSolve(Contact contact, in ContactImpulse impulse) { }

        public static Vector2 ConvertToPhysicsVector(Vector2D vector)
        {
            return new Vector2(vector.X * ScaleFactor, vector.Y * ScaleFactor);
        }

        public void SetLinearVelocity(Vector2D velocity)
        {
            if (!deleted)
                Body.SetLinearVelocity(new Vector2(velocity.X * ScaleFactor, velocity.Y * ScaleFactor));
        }

        public void ApplyLinearImpulseToCenter(Vector2D velocity)
        {
            Body.ApplyLinearImpulseToCenter(new Vector2(velocity.X * ScaleFactor, velocity.Y * ScaleFactor), true);
        }

        public abstract void CreatePhysics(World world);

        protected BodyDef CreateBodyDef()
        {
            deleted = false;
            return new BodyDef {UserData = this};
        }

        public void DeletePhysics()
        {
            PhysicsBodyRemover.Add(Body, Fixtures);

            Fixtures.Clear();
            position = Position;

            Body = null;
            deleted = true;
        }

        public override void Destoy()
        {
            if (Destroyed)
                return;
            DeletePhysics();
            base.Destoy();
        }
    }
}