using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoidTime
{
    class GameObject
    {
        #region Public Properties

        public string NameObject { get; }
        public PointF Position { get; set; }
        public byte DrawingPriority { get; }

        #endregion
        
        #region Constructor

        public GameObject(string nameObject)
        {
            NameObject = nameObject;
            DrawingPriority = 0;
            Position = new PointF();
        }

        public GameObject(string nameObject, PointF position)
        {
            NameObject = nameObject;
            DrawingPriority = 0;
            Position = position;
        }

        public GameObject(string nameObject, PointF position, byte drawingPriority)
        {
            NameObject = nameObject;
            DrawingPriority = drawingPriority;
            Position = position;
        }

        #endregion

    }
}
