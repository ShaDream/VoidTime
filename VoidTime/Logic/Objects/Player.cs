using System;
using System.Drawing;
using System.Windows.Forms;
using Box2DSharp.Collision.Shapes;
using Box2DSharp.Dynamics;

namespace VoidTime
{
    public class Player : PhysicalGameObject
    {
        private readonly Rectangle AllowedCoordinates;
        private readonly bool canMove;
        private readonly float maxSpeed = 1000;
        private PlayerStats stats;

        public double Angle = Math.PI / 2;
        private GameObject enterObject;
        private Vector2D velocity;

        public Player(Rectangle allowedCoordinates, Vector2D position, bool canMove = true)
        {
            AllowedCoordinates = allowedCoordinates;
            Position = position;
            this.canMove = canMove;
            
        }

        public GameObject EnterObject
        {
            get => enterObject;
            set
            {
                if (enterObject == value) return;

                enterObject = value;
                EnterChanged?.Invoke(enterObject != null);
            }
        }

        public event Action<bool> EnterChanged;
        public event Action Entering;


        public override void Update()
        {
            if (canMove)
                Move();

            if (Input.IsKeyPressed(Keys.E) && EnterObject != null) Entering?.Invoke();

            CheckCoordinate();
        }

        private void Move()
        {
            var rotationVector =
                new Vector2D(Input.GetAxis("horizontal"), Input.GetAxis("vertical")) * Time.DeltaTime;
            if (Input.IsAnyKeyPressed(Keys.D, Keys.W, Keys.A, Keys.S))
            {
                velocity += rotationVector;
                if (velocity.Magnitude > maxSpeed)
                    velocity = velocity.Normilized * maxSpeed;
                Angle = Angle + (rotationVector.Angle - Angle) * 0.2;
            }
            else
            {
                velocity *= 0.99f;
            }

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
            shape.SetAsBox(30 * ScaleFactor, 30 * ScaleFactor);
            Body = world.CreateBody(bodyDef);
            Fixtures.Add(canMove ? Body.CreateFixture(shape, 1) : Body.CreateFixture(shape, 100));
        }
    }
}