using System.Drawing;

namespace VoidTime
{
    public class SmoothCamera : Camera
    {
        private readonly float lerpСoefficient = 0.09f;


        public SmoothCamera(Size size, GameObject gameObject) : base(size, gameObject) { }


        public override void Update()
        {
            Position = Lerp(Position, FollowTo.Position, lerpСoefficient);
        }


        private static Vector2D Lerp(Vector2D a, Vector2D b, float t)
        {
            return a + (b - a) * t;
        }
    }
}