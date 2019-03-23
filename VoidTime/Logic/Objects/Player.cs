using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace VoidTime
{
    public class Player : GameObject
    {
        #region Public Properties

        public double Angle = Math.PI / 2;
        private Vector2D velocity = new Vector2D();
        private float maxSpeed = 20;
        private Rectangle AllowedCoordinates;

        #endregion

        #region Constructor

        public Player(Rectangle allowedCoordinates, Vector2D position)
        {
            AllowedCoordinates = allowedCoordinates;
            Position = position;
        }


        #endregion

        #region Public Methods

        public override void Update()
        {

            Move();
            CheckCoordinate();
        }

        private void Move()
        {
            var rotationVector = new Vector2D(ReadonlyKeys.GetAxis("horizontal"), ReadonlyKeys.GetAxis("vertical"));
            if (ReadonlyKeys.IsAnyKeyPressed(Keys.D, Keys.W, Keys.A, Keys.S))
            {
                velocity += rotationVector;
                if (velocity.Magnitude > maxSpeed)
                    velocity = velocity.Normilized * maxSpeed;
                Angle = rotationVector.GetAngle();
            }
            else
                velocity *= 0.95f;
            Position += velocity;
        }

        private void CheckCoordinate()
        {
            if (Position.X < AllowedCoordinates.Left)
                Position.X = AllowedCoordinates.Left;

            if (Position.X > AllowedCoordinates.Right)
                Position.X = AllowedCoordinates.Right;

            if (Position.Y > AllowedCoordinates.Bottom)
                Position.Y = AllowedCoordinates.Bottom;

            if (Position.Y < AllowedCoordinates.Top)
                Position.Y = AllowedCoordinates.Top;
        }

        #endregion

    }
}