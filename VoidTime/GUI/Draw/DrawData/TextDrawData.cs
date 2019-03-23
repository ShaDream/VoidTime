namespace VoidTime.GUI
{
    public class TextDrawData : IDrawData
    {
        public Vector2D[] Points { get; set; }
        public Vector2D[] Textures { get; set; }
        public FontAtlas Atlas { get; set; }
        public UIMask Mask { get; set; }
        public float DrawPriority { get; set; }
    }
}