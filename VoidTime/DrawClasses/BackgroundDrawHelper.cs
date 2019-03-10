using System;
using System.Drawing;

namespace VoidTime
{
    public class BackgroundDrawHelper : IDrawable
    {
        public Type GameObjectType { get; } = typeof(Background);
        public Image BackgroundImage =
            new Bitmap(@"C:\Users\Иван\Downloads\back.jpg");

        public void DrawObject(ObjectOnDisplay obj, Graphics graphics)
        {
            var x = obj.PositionOnDisplay.X;
            var y = obj.PositionOnDisplay.Y;
            graphics.DrawImage(BackgroundImage, x, y, 1001, 1001);
        }
    }
}