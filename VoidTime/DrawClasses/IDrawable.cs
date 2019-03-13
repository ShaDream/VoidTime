﻿using System;
using System.Drawing;
using SharpGL;

namespace VoidTime
{
    public interface IDrawable
    {
        Type GameObjectType { get; }

        void DrawObject(ObjectOnDisplay obj, OpenGL graphics);
    }
}