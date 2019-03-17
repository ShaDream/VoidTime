using System.Drawing;

namespace VoidTime.GUI
{
    public class Character
    {
        public char Symbol { get; set; }
        public SizeF Size { get; set; }
        public int OriginOffset { get; set; }
        public float OffsetX { get; set; }

        public Vector2D AtlasOrigin { get; set; }
        public SizeF AtlasSize { get; set; }

        public Character(char symbol, SizeF size, int originOffset, float offsetX, Vector2D atlasOrigin,
            SizeF atlasSize)
        {
            Symbol = symbol;
            Size = size;
            OriginOffset = originOffset;
            OffsetX = offsetX;
            AtlasOrigin = atlasOrigin;
            AtlasSize = atlasSize;
        }
    }
}