namespace VoidTime
{
    public class ObjectOnDisplay
    {
        #region Public Properties

        public GameObject GameObject { get; }
        public Vector2D PositionOnDisplay { get; }

        #endregion

        #region Constructor

        public ObjectOnDisplay(GameObject gameObject, Vector2D positionOnDisplay)
        {
            GameObject = gameObject;
            PositionOnDisplay = positionOnDisplay;
        }

        #endregion

    }
}