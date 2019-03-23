using System;

namespace VoidTime
{
    public class Vector2D
    {
        #region Public Properties

        public static Vector2D Zero => new Vector2D(0,0);

        public float X { get; set; }
        public float Y { get; set; }

        public float Magnitude => (float)Math.Sqrt(X * X + Y * Y);
        public float SqrMagnitude => X * X + Y * Y;
        public Vector2D Normilized => new Vector2D(X / Magnitude, Y / Magnitude);

        #endregion

        #region Constructors

        public Vector2D() : this(0, 0) { }

        public Vector2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Operators

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

        #endregion

        #region Methods

        public override bool Equals(object obj)
        {
            if (obj is Vector2D d)
            {
                return Math.Abs(d.X - X) < float.Epsilon && Math.Abs(d.Y - Y) < float.Epsilon;
            }
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

        public double GetAngle()
        {
            return Math.Atan2(Y, X);
        }

        /// <summary>
        /// Calculate the product of two vectors.
        /// 0 if perpendicular.
        /// If more than 90 degrees give negative value
        /// </summary>
        public static float Dot(Vector2D a, Vector2D b)
        {
            return (a.X * b.X) + (a.Y * b.Y);
        }

        #endregion
    }
}