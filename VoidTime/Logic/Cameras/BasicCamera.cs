using System;
using System.Collections.Generic;
using System.Drawing;

namespace VoidTime
{
    public abstract class BasicCamera
    {
        #region Public Properties

        public Vector2D Position { get; protected set; }
        public Size Size { get; set; }

        public Vector2D TopLeft => new Vector2D(Position.X - Size.Width / 2, Position.Y + Size.Height / 2);
        public Vector2D TopRight => new Vector2D(Position.X + Size.Width / 2, Position.Y + Size.Height / 2);
        public Vector2D BottomLeft => new Vector2D(Position.X - Size.Width / 2, Position.Y - Size.Height / 2);
        public Vector2D BottomRight => new Vector2D(Position.X + Size.Width / 2, Position.Y - Size.Height / 2);

        #endregion

        #region Public Methods

        public List<Vector2D> ToVectors()
        {
            return new List<Vector2D> { TopLeft, TopRight, BottomLeft, BottomRight };
        }

        public virtual void Update() { }

        public Vector2D GamePositionToWindow(Vector2D position)
        {
            var topLeftOffset = position - TopLeft;
            return new Vector2D(topLeftOffset.X, Math.Abs(topLeftOffset.Y));
        }

        #endregion
    }
}