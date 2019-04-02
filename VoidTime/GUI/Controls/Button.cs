using System.Collections.Generic;
using System.Drawing;

namespace VoidTime.GUI
{
    public class Button : GUIControl
    {
        public string Text { get; set; }
        public FontAtlas Font { get; set; }
        public float FontSize { get; set; }

        public override List<IDrawData> Draw()
        {
            var data = new List<IDrawData>();

            if (!visible)
                return data;

            if (Background != Color.Empty)
                data.Add(new RectangleDrawData
                {
                    Color = Background,
                    DrawPriority = DrawIndex,
                    Mask = Mask,
                    Points = LocationSizeConverter.ToVector2DPoints(Location, Size)
                });
            if (!string.IsNullOrWhiteSpace(Text) || Font == null)
                data.Add(TextRenderer.GetTextData(Text, Font, FontSize, new RectangleF(Location, Size),
                    DrawIndex));
            return data;
        }
    }
}