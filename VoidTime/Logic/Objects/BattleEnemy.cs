namespace VoidTime
{
    public class BattleEnemy : Ship
    {
        public EnemyBehaviorType behavior;
        public Player player;
        private Vector2D velocity;

        public BattleEnemy()
        {

        }

        public override void Update()
        {
            Move();
            if ((player.Position - Position).Magnitude < 1000)
            {
                var blasts = Data.ShipStats.Shoot(this,typeof(Player));
                foreach (var blast in blasts)
                    Instantiate(blast);
            }

            Data.ShipStats.UpdateStats(this);
        }

        private void Move()
        {
            var rotationVector = (player.Position - Position).Normilized * Time.DeltaTime;
            velocity += rotationVector;
            if (velocity.Magnitude > Data.ShipStats.Speed)
                velocity = velocity.Normilized * Data.ShipStats.Speed;
            Angle = Angle + (rotationVector.Angle - Angle) * 0.2;
            SetLinearVelocity(velocity);
        }
    }
}