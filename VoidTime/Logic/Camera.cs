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
            var topLeftPoint = new Vector2D(Position.X - Size.Width / 2, Position.Y + Size.Height / 2);
            var topRightPoint = new Vector2D(Position.X + Size.Width / 2, Position.Y + Size.Height / 2);
            var bottomLeftPoint = new Vector2D(Position.X - Size.Width / 2, Position.Y - Size.Height / 2);
            var bottomRightPoint = new Vector2D(Position.X + Size.Width / 2, Position.Y - Size.Height / 2);
            return new List<Vector2D> { topLeftPoint, topRightPoint, bottomLeftPoint, bottomRightPoint };
        }

        public void Update()
        {
            Position = FollowTo.Position;
        }

        #endregion
    }
}