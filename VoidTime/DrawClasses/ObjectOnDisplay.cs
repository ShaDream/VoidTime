namespace VoidTime
{
    public class ObjectOnDisplay
    {
        public GameObject GameObject { get; }
        public Vector2D PositionOnDisplay { get; }


        public ObjectOnDisplay(GameObject gameObject, Vector2D positionOnDisplay)
        {
            GameObject = gameObject;
            PositionOnDisplay = positionOnDisplay;
        }
    }
}