using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoidTime
{
    public class GameObject
    {
        #region Public Properties

        public string NameObject { get; }
        public Vector2D Position { get; set; }
        public byte DrawingPriority { get; }

        #endregion
        
        #region Constructor

        public GameObject(string nameObject)
        {
            NameObject = nameObject;
            DrawingPriority = 0;
            Position = new Vector2D();
        }

        public GameObject(string nameObject, Vector2D position)
        {
            NameObject = nameObject;
            DrawingPriority = 0;
            Position = position;
        }

        public GameObject(string nameObject, Vector2D position, byte drawingPriority)
        {
            NameObject = nameObject;
            DrawingPriority = drawingPriority;
            Position = position;
        }

        #endregion

        #region Public Methods

        public virtual void Update() { }

        public void Destoy() => OnDestroy?.Invoke(this);

        #endregion

        public event Action<GameObject> OnDestroy;
    }
}
