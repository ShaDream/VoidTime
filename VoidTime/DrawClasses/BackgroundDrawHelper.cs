using System;
using System.Drawing;

namespace VoidTime
{
    public class BackgroundDrawHelper : IDrawable
    {
        public Type GameObjectType { get; }
        public Image BackgroundImage =
            new Bitmap(@"C:\Users\Иван\source\repos\ShaDream\VoidTime\VoidTime\Resources\Textures\back.png");

        public void DrawObject(ObjectOnDisplay obj, Graphics graphics)
        {
            var x = obj.GameObject.Position.X;
            var y = obj.GameObject.Position.Y;
            graphics.DrawImage(BackgroundImage, x, y);
        }
    }
}