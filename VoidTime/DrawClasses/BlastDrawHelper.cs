using System;
using System.Drawing;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using VoidTime.Resources;

namespace VoidTime
{
    public class BlastDrawHelper : IDrawable
    {
        private readonly Texture blast = new Texture();
        public Type GameObjectType { get; } = typeof(Blast);

        public void DrawObject(ObjectOnDisplay obj, OpenGL gl)
        {
            blast.Bind(gl);
            var size = new Size(10, 50);
            DrawHelper.Draw(obj, gl, size,((Blast) obj.GameObject).Angle);
        }

        public void Init(OpenGL gl)
        {
            blast.Create(gl, Textures.Blast);
        }
    }
}