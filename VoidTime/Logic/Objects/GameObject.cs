using System;

namespace VoidTime
{
    public class GameObject
    {
        #region Public Properties

        public string NameObject { get; }
        public Vector2D Position { get; set; }
        public Vector2D Size { get; set; }
        public byte DrawingPriority { get; }

        #endregion
        
        #region Constructor

        public GameObject(string nameObject)
        {
            NameObject = nameObject;
            Size = new Vector2D(0,0);
            DrawingPriority = 0;
            Position = new Vector2D();
        }

        public GameObject(string nameObject, Vector2D position)
        {
            NameObject = nameObject;
            Size = new Vector2D(0,0);
            DrawingPriority = 0;
            Position = position;
        }

        public GameObject(string nameObject, Vector2D position, byte drawingPriority)
        {
            NameObject = nameObject;
            Size = new Vector2D(0,0);
            DrawingPriority = drawingPriority;
            Position = position;
        }

        #endregion

        #region Public Methods

        public virtual void Update() { }

        public void Destoy() => OnDestroy?.Invoke(this);

        #endregion
        
        #region Public Events

        public event Action<GameObject> OnDestroy;

        #endregion

    }
}
