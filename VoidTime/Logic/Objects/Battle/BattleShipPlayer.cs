using System;
using System.Windows.Forms;

namespace VoidTime.Logic.Objects
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

        public override void Update()
        {
            Move();
            if (ReadonlyKeys.IsKeyPressed(Keys.Space) && currentShootRecover <= 0)
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
            var rotationVector =
                new Vector2D(ReadonlyKeys.GetAxis("horizontal"), ReadonlyKeys.GetAxis("vertical")) * speed;
            if (ReadonlyKeys.IsAnyKeyPressed(Keys.D, Keys.W, Keys.A, Keys.S))
            {
                velocity += rotationVector;
                if (velocity.Magnitude > maxSpeed)
                    velocity = velocity.Normilized * maxSpeed;
                Angle = rotationVector.GetAngle();
            }
            else
            {
                velocity *= 0.95f;
            }

            SetLinearVelocity(velocity);
        }
    }
}