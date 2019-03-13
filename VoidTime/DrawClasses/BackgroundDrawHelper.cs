﻿using System;
using System.Drawing;
using SharpGL;

namespace VoidTime
{
    public class BackgroundDrawHelper : IDrawable
    {
        public Type GameObjectType { get; } = typeof(Background);

        public void DrawObject(ObjectOnDisplay obj, OpenGL gl)
        {
            MainForm.background.Bind(gl);
            var size = new Size(1000, 1000);
            DrawHelper.Draw(obj, gl, size);
        }
    }
}