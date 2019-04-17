using System;
using System.Drawing;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using VoidTime.Resources;

namespace VoidTime
{
    public class BattleShipEnemyDrawHelper : IDrawable
    {
        private readonly Texture enemy = new Texture();
        public Type GameObjectType { get; } = typeof(BattleShipEnemy);

        public void DrawObject(ObjectOnDisplay obj, OpenGL gl)
        {
            enemy.Bind(gl);
            var size = new Size(92, 76);
            DrawHelper.Draw(obj, gl, size, ((BattleShipEnemy)obj.GameObject).Angle);
        }

        public void Init(OpenGL gl)
        {
            enemy.Create(gl, Textures.RD2);
        }
    }
}