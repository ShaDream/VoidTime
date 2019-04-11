using System;
using System.Windows.Forms;

namespace VoidTime
{
    public class BattleShipPlayer : BattleShipObject
    {
        private readonly float maxSpeed = 1000;
        private float speed = 40;
        private const int ShootRecover = 10;
        private int currentShootRecover = 0;

        public double Angle = Math.PI / 2;
        private Vector2D velocity;
        private float damage = 20;

        public BattleShipPlayer(Vector2D position, float HP, float maxHP)
        {
            Position = position;
            Data.MaxHealth = maxHP;
            Data.CurrentHealth = HP;
        }

        public override void Update()
        {
            Move();
            if (Input.GetMouseButton(MouseButtons.Left) && currentShootRecover <= 0)
            {
                var blast = new Blast(Position, Angle, damage, this, typeof(BattleShipEnemy));
                Instantiate(blast);
                currentShootRecover = ShootRecover;
            }

            if (currentShootRecover > 0)
                currentShootRecover--;
        }

        private void Move()
        {
            Angle = (Input.GetWorldMousePosition() - Position).Angle;

            var rotationVector =
                new Vector2D(Input.GetAxis("horizontal"), Input.GetAxis("vertical")) * speed;
            if (Math.Abs(Input.GetAxis("horizontal")) + Math.Abs(Input.GetAxis("vertical")) > float.Epsilon)
            {
                if (velocity.Magnitude > maxSpeed)
                    velocity = velocity.Normilized * maxSpeed;
            }
            else
            {
                velocity *= 0.95f;
            }

            SetLinearVelocity(velocity);
        }
    }
}