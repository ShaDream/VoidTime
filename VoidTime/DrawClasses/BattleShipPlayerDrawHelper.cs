using System;
using System.Drawing;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using VoidTime.Resources;

namespace VoidTime
{
    public class BattleShipPlayerDrawHelper : IDrawable
    {
        private readonly Texture player = new Texture();
        public Type GameObjectType { get; } = typeof(BattleShipPlayer);

        public void DrawObject(ObjectOnDisplay obj, OpenGL gl)
        {
            player.Bind(gl);
            var size = new Size(92, 76);
            DrawHelper.Draw(obj, gl, size, ((BattleShipPlayer)obj.GameObject).Angle);
        }

        public void Init(OpenGL gl)
        {
            player.Create(gl, Textures.RD2);
        }
    }
}