using System.Drawing;

namespace VoidTime
{
    public class SmoothCamera : Camera
    {
        public float LerpСoefficient = 0.05f;

        public SmoothCamera(Size size, GameObject gameObject) : base(size, gameObject)
        {
        }

        public override void Update()
        {
            Position = Lerp(Position, FollowTo.Position, LerpСoefficient);
        }

        public Vector2D Lerp(Vector2D a, Vector2D b, float t)
        {
            return a + (b - a) * t;
        }
    }
}