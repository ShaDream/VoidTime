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
        public Type GameObjectType { get; } = typeof(MapEnemy);

        public void DrawObject(ObjectOnDisplay obj, OpenGL gl)
        {
            enemy.Bind(gl);
            var size = new Size(80, 80);
            DrawHelper.Draw(obj, gl, size, ((MapEnemy)obj.GameObject).Angle);
        }

        public void Init(OpenGL gl)
        {
            enemy.Create(gl, Textures.enemy_basic_ship);
        }
    }
}