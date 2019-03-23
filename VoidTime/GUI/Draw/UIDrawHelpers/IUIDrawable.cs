using System;
using SharpGL;

namespace VoidTime.GUI
{
    public interface IUIDrawable
    {
        Type DrawDataType { get; }

        void DrawUi(IDrawData obj, OpenGL gl);
    }
}