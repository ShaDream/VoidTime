using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace VoidTime.Generator
{
    public class FontSpriteGenerator
    {
        public string ImagePath;
        public int CellSize;
        public int WidthCount;
        public int OriginLine;
        public string characters;

        public void GenerateDataSettings()
        {
            var bitmap = new Bitmap(ImagePath);

            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";

            var builder = new StringBuilder();
            builder.Append(
                "private static Dictionary<char, Character> characters = new Dictionary<char, Character>\r\n        {");

            for (var i = 0; i < characters.Length; i++)
            {
                var character = characters[i];
                var xCount = i % WidthCount;
                var yCount = i / WidthCount;
                var xMin = CellSize;
                var xMax = 0;
                var yMin = CellSize;
                var yMax = 0;

                for (int y = 0; y < CellSize; y++)
                    for (int x = 0; x < CellSize; x++)
                    {
                        var color = bitmap.GetPixel(xCount * CellSize + x, yCount * CellSize + y);
                        if (color.A != 0)
                        {
                            xMin = Math.Min(xMin, x);
                            xMax = Math.Max(xMax, x);
                            yMin = Math.Min(yMin, y);
                            yMax = Math.Max(yMax, y);
                        }
                    }

                var originOffset =  yMax - OriginLine;
                originOffset = originOffset > 0 ? originOffset : 0;
                SizeF size = new SizeF(xMax - xMin, yMax - yMin);
                PointF atlasOrigin = new PointF((float)(xCount * CellSize + xMin) / bitmap.Size.Width,
                    (float)(yCount * CellSize + yMax) / bitmap.Size.Height);

                SizeF atlasSize = new SizeF((float)(xCount * CellSize + xMax) / bitmap.Size.Width - atlasOrigin.X,
                    atlasOrigin.Y - (float)(yCount * CellSize + yMin) / bitmap.Size.Height);

                builder.Append(
                    $"{{'{character}',new Character('{character}', new SizeF({size.Width}, {size.Height}), {originOffset}, 2, new Vector2D({atlasOrigin.X.ToString(nfi)}f, {atlasOrigin.Y.ToString(nfi)}f), new SizeF({atlasSize.Width.ToString(nfi)}f, {atlasSize.Height.ToString(nfi)}f)) }},\n");
            }

            builder.Append("        };");
            using (var output = new StreamWriter("D:\\text.txt"))
            {
                output.Write(builder.ToString());
            }
        }

    }
}