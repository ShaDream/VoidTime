using System.Collections.Generic;
using System.Drawing;

namespace VoidTime
{
    public class Chunk
    {
        #region Public Properties

        public Point Coordinates { get; }
        public Size Size { get; }

        #endregion

        #region Private Fields

        private readonly List<GameObject> gameObjects = new List<GameObject>();

        #endregion

        #region Constructor

        public Chunk()
        {

        }

        public Chunk(Point coordinates, Size size)
        {
            Coordinates = coordinates;
            Size = size;
            gameObjects.Add(new Background
            {
                Position = new Vector2D(Coordinates.X * Size.Width, Coordinates.Y * Size.Height + Size.Height - 1),
                DrawingPriority = 2
            });
        }

        #endregion

        #region Public Methods

        public void AddGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
            gameObject.OnDestroy += RemoveGameObject;
        }

        public void AddGameObjects(IEnumerable<GameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                this.gameObjects.Add(gameObject);
                gameObject.OnDestroy += RemoveGameObject;
            }
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            gameObjects.Remove(gameObject);
            gameObject.OnDestroy -= RemoveGameObject;
        }

        public List<GameObject> GetGameObjects() => gameObjects;

        #endregion
    }
}