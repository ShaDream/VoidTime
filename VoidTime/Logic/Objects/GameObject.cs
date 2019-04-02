﻿using System;

namespace VoidTime
{
    public class GameObject
    {
        public string NameObject { get; set; }
        public virtual Vector2D Position { get; set; }
        public virtual Vector2D Size { get; set; }
        public byte DrawingPriority { get; set; }


        public virtual void Update() { }

        public virtual void Destoy()
        {
            OnDestroy?.Invoke(this);
        }


        public event Action<GameObject> OnDestroy;
    }
}