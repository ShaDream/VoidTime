using System;
using System.Drawing;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using VoidTime.Resources;

namespace VoidTime
{
    public class PlanetDrawHelper : IDrawable
    {
        private readonly Texture planet = new Texture();
        public Type GameObjectType { get; } = typeof(Planet);

        public void DrawObject(ObjectOnDisplay obj, OpenGL gl)
        {
            planet.Bind(gl);
            var size = new Size(1000, 1000);
            DrawHelper.Draw(obj, gl, size);
        }

        public void Init(OpenGL gl)
        {
            planet.Create(gl, Textures.upiter);
        }
    }
}