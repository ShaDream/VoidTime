using System.Collections.Generic;
using System.Drawing;

namespace VoidTime.GUI
{
    public static class TextRenderer
    {
        public static List<QuadData> GetTextQuads(string text, FontAtlas atlas, Vector2D start)
        {
            var data = new List<QuadData>();
            var xOffset = 0f;

            foreach (var symbol in text)
            {
                if (symbol == ' ')
                {
                    xOffset += 40;
                    continue;
                }


                var character = atlas.GetCharacter(symbol);

                var yFirst = start.Y + character.OriginOffset;
                var pointQuadData = new Vector2D[4];
                var pointTextureData = new Vector2D[4];
                pointQuadData[0] = new Vector2D(xOffset, yFirst);
                pointTextureData[0] = character.AtlasOrigin;
                pointQuadData[3] = new Vector2D(xOffset + character.Size.Width, yFirst);
                pointTextureData[3] = new Vector2D(character.AtlasOrigin.X + character.AtlasSize.Width,
                    character.AtlasOrigin.Y);
                pointQuadData[2] = new Vector2D(xOffset + character.Size.Width, yFirst - character.Size.Height);
                pointTextureData[2] = new Vector2D(character.AtlasOrigin.X + character.AtlasSize.Width,
                    character.AtlasOrigin.Y - character.AtlasSize.Height);
                pointQuadData[1] = new Vector2D(xOffset, yFirst - character.Size.Height);
                pointTextureData[1] = new Vector2D(character.AtlasOrigin.X,
                    character.AtlasOrigin.Y - character.AtlasSize.Height);

                xOffset += character.Size.Width + character.OffsetX;

                data.Add(new QuadData {Points = pointQuadData, Textures = pointTextureData});
            }

            return data;
        }
    }
}