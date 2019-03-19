using System;
using System.Drawing;
using SharpGL;
using SharpGL.SceneGraph.Assets;

namespace VoidTime
{
    public class BackgroundDrawHelper : IDrawable
    {
        public Type GameObjectType { get; } = typeof(Background);

        private Texture background = new Texture();

        public void DrawObject(ObjectOnDisplay obj, OpenGL gl)
        {
            background.Bind(gl);
            var size = new Size(1000, 1000);
            DrawHelper.Draw(obj, gl, size);
        }

        public void Init(OpenGL gl)
        {
            throw new NotImplementedException();
        }
    }
}