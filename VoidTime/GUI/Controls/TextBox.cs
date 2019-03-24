using System.Collections.Generic;
using System.Drawing;

namespace VoidTime.GUI
{
    public class TextBox : GUIControl
    {
        public string Text { get; set; }
        public Color Background { get; set; }
        public FontSettings Font { get; set; }

        public override List<IDrawData> Draw()
        {
            List<IDrawData> data = new List<IDrawData>();
            if (Background != Color.Transparent)
            {
                data.Add(new RectangleDrawData
                {
                    Color = Background,
                    DrawPriority = DrawIndex,
                    Mask = Mask,
                    Points = new[]
                    {
                        new Vector2D(Location.X,Location.Y),
                        new Vector2D(Location.X+Size.Width,Location.Y),
                        new Vector2D(Location.X+Size.Width,Location.Y + Size.Height),
                        new Vector2D(Location.X,Location.Y+ Size.Height),
                    }
                });
            }

            return data;
        }
    }
}