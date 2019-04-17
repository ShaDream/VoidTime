using System;
using System.Drawing;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using VoidTime.Resources;

namespace VoidTime
{
    public class EnemyDrawHelper : IDrawable
    {
        private readonly Texture enemy = new Texture();
        public Type GameObjectType { get; } = typeof(Enemy);

        public void DrawObject(ObjectOnDisplay obj, OpenGL gl)
        {
            enemy.Bind(gl);
            var size = new Size(80, 80);
            DrawHelper.Draw(obj, gl, size, ((Enemy)obj.GameObject).Angle);
        }

        public void Init(OpenGL gl)
        {
            enemy.Create(gl, Textures.RD2);
        }
    }
}