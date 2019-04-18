using System;
using System.Drawing;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using VoidTime.Resources;

namespace VoidTime
{
    public class PlayerDrawHelper : IDrawable
    {
        private readonly Texture player = new Texture();
        public Type GameObjectType { get; } = typeof(Player);

        public void DrawObject(ObjectOnDisplay obj, OpenGL gl)
        {
            player.Bind(gl);
            DrawHelper.Draw(obj, gl, obj.GameObject.Size, ((Player) obj.GameObject).Angle);
        }

        public void Init(OpenGL gl)
        {
            player.Create(gl, Textures.lightfighter0006);
        }
    }
}