using System;
using System.Drawing;
using System.Windows.Forms;
using Box2DSharp.Collision.Shapes;
using Box2DSharp.Dynamics;

namespace VoidTime
{
    public class Player : Ship
    {
        private readonly Rectangle AllowedCoordinates;
        public double Angle = Math.PI / 2;
        private GameObject enterObject;
        public Vector2D velocity;

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

        public Player(Rectangle allowedCoordinates, Vector2D position, bool canMove = true)
        {
            Inventory = new Inventory(this);
            Chips = new InstalledChips(this, 60);
            AllowedCoordinates = allowedCoordinates;
            Position = position;
            Size = new Size(100, 111);
            Data.SetShip(ShipParser.GetShip("Base ship"));
            //Data.SetGun("Base gun", 0);
            Data.SetGun(GunParser.GetGun("Base gun"), 1);
            Data.SetGun(GunParser.GetGun("Base gun"), 2);
        }

        public event Action<bool> EnterChanged;


        public override void Update()
        {
            Move();
            var blasts = Data.ShipStats.Shoot(this);
            foreach (var blast in blasts)
                Instantiate(blast);
            Data.ShipStats.UpdateStats(this);
            CheckCoordinate();
        }


        private void Move()
        {
            Angle = (Input.GetWorldMousePosition() - Position).Angle;
            var rotationVector =
                new Vector2D(Input.GetAxis("horizontal"), Input.GetAxis("vertical")) * Time.DeltaTime;
            if (Input.IsAnyKeyPressed(Keys.D, Keys.W, Keys.A, Keys.S))
            {
                velocity += rotationVector;
                if (velocity.Magnitude > Data.ShipStats.Speed)
                    velocity = velocity.Normilized * Data.ShipStats.Speed;
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
            Fixtures.Add(Body.CreateFixture(shape, 1));
        }
    }
}