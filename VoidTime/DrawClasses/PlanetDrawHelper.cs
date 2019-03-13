using System;
using System.Drawing;
using SharpGL;

namespace VoidTime
{
    public class PlanetDrawHelper : IDrawable
    {
        public Type GameObjectType { get; } = typeof(Planet);
        public void DrawObject(ObjectOnDisplay obj, OpenGL gl)
        {
            MainForm.mars.Bind(gl);
            var size = new Size(1000, 1000);
            DrawHelper.Draw(obj, gl, size);
        }
    }
}