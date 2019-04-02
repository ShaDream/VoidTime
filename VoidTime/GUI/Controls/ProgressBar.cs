using System;
using System.Collections.Generic;

namespace VoidTime.GUI
{
    public class ProgressBar : GUIControl
    {
        private float value;

        public float Value
        {
            get => value;
            set
            {
                if (Math.Abs(value - this.value) < float.Epsilon)
                    return;

                this.value = Math.Max(Math.Min(1f, value), 0);
                OnUIChanged(this, Draw());
            }
        }


        public override List<IDrawData> Draw()
        {
            var data = new List<IDrawData>
            {
                new RectangleDrawData
                {
                    Color = Background,
                    DrawPriority = DrawIndex,
                    Mask = Mask,
                    Points = LocationSizeConverter.ToVector2DPoints(Location, Size)
                },
                new RectangleDrawData
                {
                    Color = ForeGround,
                    DrawPriority = DrawIndex,
                    Mask = Mask,
                    Points = new[]
                    {
                        new Vector2D(Location.X, Location.Y),
                        new Vector2D(Location.X + Size.Width * Value, Location.Y),
                        new Vector2D(Location.X + Size.Width * Value, Location.Y + Size.Height),
                        new Vector2D(Location.X, Location.Y + Size.Height)
                    }
                }
            };

            return data;
        }
    }
}