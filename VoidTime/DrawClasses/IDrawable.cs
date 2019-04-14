using System;
using SharpGL;

namespace VoidTime
{
    public interface IDrawable
    {
        Type GameObjectType { get; }

        void DrawObject(ObjectOnDisplay obj, OpenGL gl);

        void Init(OpenGL gl);
    }
}