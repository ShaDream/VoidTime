using System;
using System.Drawing;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using VoidTime.Resources;

namespace VoidTime
{
    public class BackgroundDrawHelper : IDrawable
    {
        private readonly Texture background = new Texture();
        public Type GameObjectType { get; } = typeof(Background);

        public void DrawObject(ObjectOnDisplay obj, OpenGL gl)
        {
            background.Bind(gl);
            var size = new Size(1000, 1000);
            DrawHelper.Draw(obj, gl, size);
        }

        public void Init(OpenGL gl)
        {
            background.Create(gl, Textures.back);
        }
    }
}