using System;

namespace VoidTime
{
    public class Enemy : Ship
    {
        public double Angle = Math.PI / 2;
        private readonly PhysicalGameObject target;
        private Vector2D velocity;


        public Enemy(Vector2D position, PhysicalGameObject target)
        {
            Position = position;
            this.target = target;
            Data.SetShip(ShipParser.GetShip("Base ship"));
        }

        public override void Update()
        {
            Move();
        }

        private void Move()
        {
            var rotationVector = (target.Position - Position).Normilized * Time.DeltaTime;
            velocity += rotationVector;
            if (velocity.Magnitude > Data.ShipStats.Speed)
                velocity = velocity.Normilized * Data.ShipStats.Speed;
            Angle = Angle + (rotationVector.Angle - Angle) * 0.2;
            SetLinearVelocity(velocity);
        }
    }
}