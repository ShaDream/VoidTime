using System.Drawing;

namespace VoidTime
{
    public class SmoothCamera : Camera
    {
        #region Private Fields

        private readonly float lerpСoefficient = 0.09f;

        #endregion

        #region Constructor

        public SmoothCamera(Size size, GameObject gameObject) : base(size, gameObject) { }

        #endregion

        #region Public Methods

        public override void Update()
        {
            Position = Lerp(Position, FollowTo.Position, lerpСoefficient);
        }

        #endregion

        #region Private Methods

        private static Vector2D Lerp(Vector2D a, Vector2D b, float t)
        {
            return a + (b - a) * t;
        }

        #endregion

    }
}