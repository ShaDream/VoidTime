using System;

namespace VoidTime
{
    public class GameObject
    {
        #region Public Properties

        public string NameObject { get; set; }
        public Vector2D Position { get; set; }
        public Vector2D Size { get; set; }
        public byte DrawingPriority { get; set; }

        #endregion
        
        #region Constructor

      

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
