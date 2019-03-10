using System;
using System.Drawing;

namespace VoidTime
{
    public interface IDrawable
    {
        Type GameObjectType { get; }

        void DrawObject(ObjectOnDisplay obj, Graphics graphics);
    }
}