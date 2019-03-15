using System.Drawing;

namespace VoidTime.GUI
{
    public class Character
    {
        public string Symbol { get; set; }
        public SizeF Size { get; set; }
        public PointF Origin { get; set; }
        public float OffsetX { get; set; }

        public PointF AtlasOrigin { get; set; }
        public SizeF AtlasSize { get; set; }
    }
}