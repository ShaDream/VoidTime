using System;

namespace VoidTime
{
    public struct Vector2D
    {
        public static Vector2D Zero { get; } = new Vector2D(0, 0);

        public static Vector2D One { get; } = new Vector2D(1, 1);

        public float X { get; }
        public float Y { get; }

        public float Magnitude => (float) Math.Sqrt(X * X + Y * Y);
        public float SqrMagnitude => X * X + Y * Y;
        public Vector2D Normilized => new Vector2D(X / Magnitude, Y / Magnitude);
        public double Angle => Math.Atan2(Y, X);

        public Vector2D(float x = 0, float y = 0)
        {
            X = x;
            Y = y;
        }


        public static Vector2D operator +(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2D operator -(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2D operator *(Vector2D a, int b)
        {
            return new Vector2D(a.X * b, a.Y * b);
        }

        public static Vector2D operator *(Vector2D a, float b)
        {
            return new Vector2D(a.X * b, a.Y * b);
        }

        public static Vector2D operator /(Vector2D a, float b)
        {
            return new Vector2D(a.X / b, a.Y / b);
        }

        public static Vector2D operator /(Vector2D a, int b)
        {
            return new Vector2D(a.X / b, a.Y / b);
        }


        public override bool Equals(object obj)
        {
            if (obj is Vector2D d) return Math.Abs(d.X - X) < float.Epsilon && Math.Abs(d.Y - Y) < float.Epsilon;
            throw new ArgumentException($"{obj.GetType()} is not equals to {typeof(Vector2D)}");
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public Vector2D Rotate(double angle)
        {
            return new Vector2D((float) (X * Math.Cos(angle) - Y * Math.Sin(angle)),
                (float) (X * Math.Sin(angle) + Y * Math.Cos(angle)));
        }


        /// <summary>
        ///     Calculate the product of two vectors.
        ///     0 if perpendicular.
        ///     If more than 90 degrees give negative value
        /// </summary>
        public static float Dot(Vector2D a, Vector2D b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public override string ToString()
        {
            return $"{{{X}, {Y}}}";
        }
    }
}