using System.Drawing;

namespace VoidTime
{
    public static class LocationSizeConverter
    {
        public static Vector2D[] ToVector2DPoints(Point location, Size size)
        {
            return ToVector2DPoints((PointF)location,(SizeF)size);
        }

        public static Vector2D[] ToVector2DPoints(PointF location, SizeF size)
        {
            return new[]
            {
                new Vector2D(location.X, location.Y),
                new Vector2D(location.X + size.Width, location.Y),
                new Vector2D(location.X + size.Width, location.Y + size.Height),
                new Vector2D(location.X, location.Y + size.Height),
            };
        }
    }
}