using System;
using System.Collections.Generic;
using System.Drawing;

namespace VoidTime
{
    public class Camera
    {
        #region Public Properties

        public Vector2D Position { get; private set; }
        public Size Size { get; set; }
        public GameObject FollowTo { get; set; }

        public Vector2D TopLeft => new Vector2D(Position.X - Size.Width / 2, Position.Y + Size.Height / 2);
        public Vector2D TopRight => new Vector2D(Position.X + Size.Width / 2, Position.Y + Size.Height / 2);
        public Vector2D BottomLeft => new Vector2D(Position.X - Size.Width / 2, Position.Y - Size.Height / 2);
        public Vector2D BottomRight => new Vector2D(Position.X + Size.Width / 2, Position.Y - Size.Height / 2);

        #endregion

        #region Conctructor

        public Camera(GameObject followTo, Size size)
        {
            Position = followTo.Position;
            Size = size;
            FollowTo = followTo;
        }

        #endregion

        #region Public Methods

        public List<Vector2D> ToVectors()
        {
            return new List<Vector2D> { TopLeft, TopRight, BottomLeft, BottomRight };
        }

        public void Update()
        {
            Position = Lerp(Position, FollowTo.Position, 0.02f);
        }

        public Vector2D GamePositionToWindow(Vector2D position)
        {
            var topLeftOffset = position - TopLeft;
            return new Vector2D(topLeftOffset.X, Math.Abs(topLeftOffset.Y));
        }

        private Vector2D Lerp(Vector2D a, Vector2D b, float t)
        {
            return new Vector2D(Lerp(a.X,b.X,t), Lerp(a.Y, b.Y, t));
        }

        private float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        #endregion
    }
}