using System;
using System.Drawing;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using VoidTime.Resources;

namespace VoidTime
{
    public class MapEnemyDrawHelper : IDrawable
    {
        public Type GameObjectType { get; } = typeof(MapEnemy);

        public void DrawObject(ObjectOnDisplay obj, OpenGL gl)
        {
            ShipTexturesHelper.GetShipTexture(gl, ((MapEnemy)obj.GameObject).Data.ShipName).Bind(gl);
            var size = new Size(80, 80);
            DrawHelper.Draw(obj, gl, size, ((MapEnemy)obj.GameObject).Angle);
        }

        public void Init(OpenGL gl)
        {
        }
    }
}