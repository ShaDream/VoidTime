using System.Drawing;

namespace VoidTime
{
    public static class RectangleConverter
    {
        public static Vector2D[] ToVector2DPoints(Rectangle rect)
        {
            return ToVector2DPoints((RectangleF) rect);
        }

        public static Vector2D[] ToVector2DPoints(RectangleF rect)
        {
            return new[]
            {
                new Vector2D(rect.X, rect.Y),
                new Vector2D(rect.X + rect.Width, rect.Y),
                new Vector2D(rect.X + rect.Width, rect.Y + rect.Height),
                new Vector2D(rect.X, rect.Y + rect.Height)
            };
        }
    }
}