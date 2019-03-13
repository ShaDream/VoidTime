using System;
using System.Drawing;
using SharpGL;
using SharpGL.SceneGraph.Assets;

namespace VoidTime
{
    public class PlayerDrawHelper : IDrawable
    {
        public Type GameObjectType { get; } = typeof(Player);

        public void DrawObject(ObjectOnDisplay obj, OpenGL gl)
        {
            MainForm.player.Bind(gl);
            var size = new Size(201, 100);
            DrawHelper.Draw(obj, gl, size, ((Player) obj.GameObject).Angle);
        }
    }
}