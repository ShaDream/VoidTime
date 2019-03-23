using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices.ComTypes;

namespace VoidTime.GUI
{
    public static class TextRenderer
    {
        public static TextDrawData GetTextQuads(string text, FontAtlas atlas, Vector2D start, float fontSize, float drawPriority)
        {
            var data = new List<TextDrawData>();
            var xOffset = start.X;
            var yRow = 0;
            var fSize = fontSize / atlas.FontSize;
            var spacing = atlas.Spacing * fSize;
            var lineSpacing = atlas.LineSpacing * fSize;

            var pointQuadData = new List<Vector2D>();
            var pointTextureData = new List<Vector2D>();

            foreach (var symbol in text)
            {
                switch (symbol)
                {
                    case ' ':
                        xOffset += spacing;
                        continue;
                    case '\n':
                        xOffset = start.X;
                        yRow++;
                        continue;
                }

                var character = atlas.GetCharacter(symbol);

                var yFirst = start.Y + yRow * lineSpacing + character.OriginOffset * fSize;
                var characterSize = new SizeF(character.Size.Width * fSize, character.Size.Height * fSize);

                

                pointQuadData.Add(new Vector2D(xOffset, yFirst));
                pointTextureData.Add(character.AtlasOrigin);

                pointQuadData.Add(new Vector2D(xOffset, yFirst - characterSize.Height));
                pointTextureData.Add(new Vector2D(character.AtlasOrigin.X,
                    character.AtlasOrigin.Y - character.AtlasSize.Height));

                pointQuadData.Add( new Vector2D(xOffset + characterSize.Width, yFirst - characterSize.Height));
                pointTextureData.Add( new Vector2D(character.AtlasOrigin.X + character.AtlasSize.Width,
                    character.AtlasOrigin.Y - character.AtlasSize.Height));

                pointQuadData.Add( new Vector2D(xOffset + characterSize.Width, yFirst));
                pointTextureData.Add( new Vector2D(character.AtlasOrigin.X + character.AtlasSize.Width,
                    character.AtlasOrigin.Y));

                xOffset += characterSize.Width + character.OffsetX;

            }

            return new TextDrawData {Points = pointQuadData.ToArray(), Atlas = atlas, DrawPriority = drawPriority};
        }

        public static TextDrawData GetTextQuadsTextBox(string text, FontAtlas atlas, float fontSize, RectangleF size, float drawPriority)
        {
            var data = new List<TextDrawData>();
            var xOffset = size.X;
            var yRow = 1;
            var fSize = fontSize / atlas.FontSize;
            var spacing = atlas.Spacing * fSize;
            var lineSpacing = atlas.LineSpacing * fSize;

            var pointQuadData = new List<Vector2D>();
            var pointTextureData = new List<Vector2D>();

            foreach (var symbol in text)
            {

                switch (symbol)
                {
                    case ' ':
                        xOffset += spacing;
                        continue;
                    case '\n':
                        xOffset = size.X;
                        yRow++;
                        continue;
                }

                var character = atlas.GetCharacter(symbol);
                var characterSize = new SizeF(character.Size.Width * fSize, character.Size.Height * fSize);

                if (xOffset + characterSize.Width + character.OffsetX > size.Right)
                {
                    xOffset = size.X;
                    yRow++;
                }

                var yFirst = size.Y + yRow * lineSpacing + character.OriginOffset * fSize;

                pointQuadData.Add(new Vector2D(xOffset, yFirst));
                pointTextureData.Add(character.AtlasOrigin);

                pointQuadData.Add(new Vector2D(xOffset, yFirst - characterSize.Height));
                pointTextureData.Add(new Vector2D(character.AtlasOrigin.X,
                    character.AtlasOrigin.Y - character.AtlasSize.Height));

                pointQuadData.Add(new Vector2D(xOffset + characterSize.Width, yFirst - characterSize.Height));
                pointTextureData.Add(new Vector2D(character.AtlasOrigin.X + character.AtlasSize.Width,
                    character.AtlasOrigin.Y - character.AtlasSize.Height));

                pointQuadData.Add(new Vector2D(xOffset + characterSize.Width, yFirst));
                pointTextureData.Add(new Vector2D(character.AtlasOrigin.X + character.AtlasSize.Width,
                    character.AtlasOrigin.Y));

                xOffset += characterSize.Width + character.OffsetX;

            }

            return new TextDrawData { Points = pointQuadData.ToArray(), Atlas = atlas, DrawPriority = drawPriority };
        }

        public static bool CanFitInTextBox(string text, FontAtlas atlas, float fontSize, RectangleF size)
        {
            var xOffset = size.X;
            var yRow = 1;
            var fSize = fontSize / atlas.FontSize;
            var spacing = atlas.Spacing * fSize;
            var lineSpacing = atlas.LineSpacing * fSize;

            foreach (var symbol in text)
            {

                switch (symbol)
                {
                    case ' ':
                        xOffset += spacing;
                        continue;
                    case '\n':
                        xOffset = size.X;
                        yRow++;
                        continue;
                }

                var character = atlas.GetCharacter(symbol);
                var characterSize = new SizeF(character.Size.Width * fSize, character.Size.Height * fSize);

                if (xOffset + characterSize.Width + character.OffsetX > size.Right)
                {
                    xOffset = size.X;
                    yRow++;
                }

                var yFirst = size.Y + yRow * lineSpacing + character.OriginOffset * fSize;

                xOffset += characterSize.Width + character.OffsetX;

                if (yFirst > size.Bottom)
                    return false;
            }

            return true;
        }
    }
}