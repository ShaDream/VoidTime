using System.Windows.Forms;

namespace VoidTime.Logic.Objects
{
    public class BattleShipEnemy : BattleShipObject
    {
        private BattleShipPlayer player;

        public double Angle;
        private float maxSpeed = 500;
        private float speed = 10;
        private Vector2D velocity;

        private float recoveryTime = 5;
        private float currentRecovery = 0;
        private bool canShoot => currentRecovery-- <= 0;
        private float shootRange = 100;
        private float damage = 20;


        public BattleShipEnemy(Vector2D position, BattleShipPlayer player)
        {
            Position = position;
            this.player = player;
        }

        public override void Update()
        {
            if (IsNeedToShoot() && canShoot)
                Shoot();

            Move();
        }

        private void Move()
        {
            if (player == null)
                return;

            var rotationVector = (player.Position - Position).Normilized * 10;
            velocity += rotationVector;
            if (velocity.Magnitude > maxSpeed)
                velocity = velocity.Normilized * maxSpeed;
            Angle = rotationVector.GetAngle();

            SetLinearVelocity(velocity);
        }

        private void Shoot()
        {
            var blast = new Blast(Position, Angle, damage, this, typeof(BattleShipPlayer));
            Instantiate(blast);
            currentRecovery = recoveryTime;
        }

        private bool IsNeedToShoot()
        {
            return player != null && (player.Position - Position).Magnitude < shootRange;
        }
    }
}