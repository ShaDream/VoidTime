namespace VoidTime
{
    public class Background : GameObject
    {
        #region Constructor

        public Background(string nameObject) : base(nameObject)
        {
        }

        public Background(string nameObject, Vector2D position) : base(nameObject, position)
        {
        }

        public Background(string nameObject, Vector2D position, byte drawingPriority) : base(nameObject, position, drawingPriority)
        {
        }

        #endregion
    }
}