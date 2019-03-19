using System.Collections.Generic;
using System.Drawing;

namespace VoidTime.GUI
{
    public static class TextRenderer
    {
        public static List<QuadData> GetTextQuads(string text, FontAtlas atlas, Vector2D start, float fontSize)
        {
            var data = new List<QuadData>();
            var xOffset = start.X;
            var yRow = 0;
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
                        xOffset = start.X;
                        yRow++;
                        continue;
                }

                var character = atlas.GetCharacter(symbol);

                var yFirst = start.Y + yRow * lineSpacing + character.OriginOffset * fSize;
                var characterSize = new SizeF(character.Size.Width * fSize, character.Size.Height * fSize);

                var pointQuadData = new Vector2D[4];
                var pointTextureData = new Vector2D[4];

                pointQuadData[0] = new Vector2D(xOffset, yFirst);
                pointTextureData[0] = character.AtlasOrigin;

                pointQuadData[1] = new Vector2D(xOffset, yFirst - characterSize.Height);
                pointTextureData[1] = new Vector2D(character.AtlasOrigin.X,
                    character.AtlasOrigin.Y - character.AtlasSize.Height);

                pointQuadData[2] = new Vector2D(xOffset + characterSize.Width, yFirst - characterSize.Height);
                pointTextureData[2] = new Vector2D(character.AtlasOrigin.X + character.AtlasSize.Width,
                    character.AtlasOrigin.Y - character.AtlasSize.Height);

                pointQuadData[3] = new Vector2D(xOffset + characterSize.Width, yFirst);
                pointTextureData[3] = new Vector2D(character.AtlasOrigin.X + character.AtlasSize.Width,
                    character.AtlasOrigin.Y);

                xOffset += characterSize.Width + character.OffsetX;

                data.Add(new QuadData { Points = pointQuadData, Textures = pointTextureData });
            }

            return data;
        }

        public static List<QuadData> GetTextQuadsTextBox(string text, FontAtlas atlas, float fontSize, RectangleF size)
        {
            var data = new List<QuadData>();
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

                var pointQuadData = new Vector2D[4];
                var pointTextureData = new Vector2D[4];

                pointQuadData[0] = new Vector2D(xOffset, yFirst);
                pointTextureData[0] = character.AtlasOrigin;

                pointQuadData[1] = new Vector2D(xOffset, yFirst - characterSize.Height);
                pointTextureData[1] = new Vector2D(character.AtlasOrigin.X,
                    character.AtlasOrigin.Y - character.AtlasSize.Height);

                pointQuadData[2] = new Vector2D(xOffset + characterSize.Width, yFirst - characterSize.Height);
                pointTextureData[2] = new Vector2D(character.AtlasOrigin.X + character.AtlasSize.Width,
                    character.AtlasOrigin.Y - character.AtlasSize.Height);

                pointQuadData[3] = new Vector2D(xOffset + characterSize.Width, yFirst);
                pointTextureData[3] = new Vector2D(character.AtlasOrigin.X + character.AtlasSize.Width,
                    character.AtlasOrigin.Y);

                xOffset += characterSize.Width + character.OffsetX;

                data.Add(new QuadData { Points = pointQuadData, Textures = pointTextureData });
            }

            return data;
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