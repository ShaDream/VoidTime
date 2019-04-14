using System;

namespace VoidTime
{
    public class GameObject
    {
        public string NameObject { get; set; }
        public virtual Vector2D Position { get; set; }
        public virtual Vector2D Size { get; set; }
        public byte DrawingPriority { get; set; }

        public bool Destroyed { get; protected set; }

        public virtual void Update() { }

        public virtual void Destoy()
        {
            if (Destroyed)
                return;

            OnDestroy?.Invoke(this);
            Destroyed = true;
        }

        protected void Instantiate(GameObject obj)
        {
            OnCreate?.Invoke(obj);
        }

        public event Action<GameObject> OnDestroy;
        public event Action<GameObject> OnCreate;
    }
}