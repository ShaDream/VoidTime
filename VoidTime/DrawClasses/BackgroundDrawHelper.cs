using System;
using System.Drawing;

namespace VoidTime
{
    public class BackgroundDrawHelper : IDrawable
    {
        public Type GameObjectType { get; } = typeof(Background);
        public Image BackgroundImage =
            new Bitmap(@"C:\Users\Иван\source\repos\ShaDream\VoidTime\VoidTime\Resources\Textures\back.png");

        public void DrawObject(ObjectOnDisplay obj, Graphics graphics)
        {
            var x = obj.PositionOnDisplay.X;
            var y = obj.PositionOnDisplay.Y;
            graphics.DrawImage(BackgroundImage, x, y, 1001, 1001);
            graphics.DrawString(obj.GameObject.NameObject, new Font("Arial", 40, FontStyle.Bold),
                new SolidBrush(Color.Yellow), x, y);
        }
    }
}