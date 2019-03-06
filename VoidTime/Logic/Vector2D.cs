namespace VoidTime
{
    public class Vector2D
    {
        public float X { get; set; }
        public float Y { get; set; }

        #region Constructors

        public Vector2D()
        {
            X = Y = 0;
        }

        public Vector2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        #endregion


    }
}