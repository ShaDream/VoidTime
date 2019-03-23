using System.Drawing;

namespace VoidTime.GUI
{
    public class RectangleDrawData : IDrawData
    {
        public Vector2D[] Points { get; set; }
        public UIMask Mask { get; set; }
        public float DrawPriority { get; set; }
        public Color Color { get; set; }
    }
}