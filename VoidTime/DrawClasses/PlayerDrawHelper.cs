using System;
using System.Drawing;

namespace VoidTime
{
    public class PlayerDrawHelper : IDrawable
    {
        public Type GameObjectType { get; } = typeof(Player);

        public Image PlayerImage =
            new Bitmap(@"C:\Users\Иван\source\repos\ShaDream\VoidTime\VoidTime\Resources\Textures\player.png");

        public void DrawObject(ObjectOnDisplay obj, Graphics graphics)
        {
            var x = obj.GameObject.Position.X;
            var y = obj.GameObject.Position.Y;
            graphics.DrawImage(PlayerImage, x, y);
        }
    }
}