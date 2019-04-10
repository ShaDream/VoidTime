using System;
using System.Drawing;
using System.Windows.Forms;
using Box2DSharp.Collision;
using Box2DSharp.Collision.Shapes;
using Box2DSharp.Dynamics;

namespace VoidTime
{
    public class Player : PhysicalGameObject
    {
        private readonly Rectangle AllowedCoordinates;
        private readonly bool canMove;
        private readonly float maxSpeed = 1000;

        public double Angle = Math.PI / 2;
        private Vector2D velocity;


        public Player(Rectangle allowedCoordinates, Vector2D position, bool canMove = true)
        {
            AllowedCoordinates = allowedCoordinates;
            Position = position;
            this.canMove = canMove;
        }


        public override void Update()
        {
            if (canMove)
                Move();

            CheckCoordinate();
        }

        private void Move()
        {
            var rotationVector =
                new Vector2D(ReadonlyKeys.GetAxis("horizontal"), ReadonlyKeys.GetAxis("vertical")) * 10;
            if (ReadonlyKeys.IsAnyKeyPressed(Keys.D, Keys.W, Keys.A, Keys.S))
            {
                velocity += rotationVector;
                if (velocity.Magnitude > maxSpeed)
                    velocity = velocity.Normilized * maxSpeed;
                Angle = rotationVector.GetAngle();
            }
            else
                velocity *= 0.95f;

            SetLinearVelocity(velocity);
        }

        private void CheckCoordinate()
        {
            if (Position.X < AllowedCoordinates.Left)
                Position = new Vector2D(AllowedCoordinates.Left, Position.Y);

            if (Position.X > AllowedCoordinates.Right)
                Position = new Vector2D(AllowedCoordinates.Right, Position.Y);

            if (Position.Y > AllowedCoordinates.Bottom)
                Position = new Vector2D(Position.X, AllowedCoordinates.Bottom);

            if (Position.Y < AllowedCoordinates.Top)
                Position = new Vector2D(Position.X, AllowedCoordinates.Top);
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
            Fixtures.Add(canMove ? Body.CreateFixture(shape, 1) : Body.CreateFixture(shape, 100));
        }
    }
}