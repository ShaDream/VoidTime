using System;
using Box2DSharp.Collision.Shapes;
using Box2DSharp.Dynamics;

namespace VoidTime
{
    public class Enemy : PhysicalGameObject
    {
        private readonly bool canMove;
        private readonly float maxSpeed = 1000;
        private Vector2D velocity;
        private PhysicalGameObject target;
        public double Angle = Math.PI / 2;
        

        public Enemy(Vector2D position, PhysicalGameObject target)
        {
            Position = position;
            maxSpeed = 1000;
            this.target = target;
        }

        public override void Update()
        {
            Move();
        }

        private void Move()
        {
            var rotationVector = (target.Position - Position).Normilized * Time.DeltaTime;
            velocity += rotationVector;
            if (velocity.Magnitude > maxSpeed)
                velocity = velocity.Normilized * maxSpeed;
            Angle = Angle + (rotationVector.Angle - Angle) * 0.2;
            SetLinearVelocity(velocity);
        }
        public override void CreatePhysics(World world)
        {
            var bodyDef = CreateBodyDef();
            bodyDef.FixedRotation = true;
            bodyDef.Position = ConvertToPhysicsVector(Position);
            bodyDef.BodyType = BodyType.DynamicBody;

            var shape = new PolygonShape();
            shape.SetAsBox(30 * ScaleFactor, 30 * ScaleFactor);
            Body = world.CreateBody(bodyDef);
            Fixtures.Add(canMove ? Body.CreateFixture(shape, 1) : Body.CreateFixture(shape, 100));
        }
    }
}