using System;

namespace VoidTime
{
    public class MapEnemy : Ship
    {
        public double Angle = Math.PI / 2;
        private readonly PhysicalGameObject target;
        private Vector2D velocity;

        private int EnemiesCount;
        private EnemyDifficult difficult;

        public MapEnemy(Vector2D position, PhysicalGameObject target, int enemiesCount, EnemyDifficult difficult)
        {
            Position = position;
            this.target = target;
        }

        public override void Update()
        {
            Move();
            var blasts = Data.ShipStats.Shoot(this);
            foreach (var blast in blasts)
                Instantiate(blast);

            Data.ShipStats.UpdateStats(this);
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