using System;
using SharpGL;

namespace VoidTime
{
    public class PlayerDrawHelper : IDrawable
    {
        public Type GameObjectType { get; } = typeof(Player);

        public void DrawObject(ObjectOnDisplay obj, OpenGL gl)
        {
            ShipTexturesHelper.GetShipTexture(gl, ((Player) obj.GameObject).Data.ShipName).Bind(gl);
            DrawHelper.Draw(obj, gl, obj.GameObject.Size, ((Player) obj.GameObject).Angle);
        }

        public void Init(OpenGL gl)
        {
        }
    }
}