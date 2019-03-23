using System;
using System.Drawing;
using SharpGL;
using SharpGL.SceneGraph.Assets;

namespace VoidTime
{
    public class PlayerDrawHelper : IDrawable
    {
        public Type GameObjectType { get; } = typeof(Player);

        private Texture player = new Texture();

        public void DrawObject(ObjectOnDisplay obj, OpenGL gl)
        {
            player.Bind(gl);
            var size = new Size(92, 76);
            DrawHelper.Draw(obj, gl, size, ((Player)obj.GameObject).Angle);
        }

        public void Init(OpenGL gl)
        {
            player.Create(gl, Resources.Textures.player);
        }
    }
}