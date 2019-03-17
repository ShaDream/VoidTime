using System.Collections.Generic;
using System.Drawing;

namespace VoidTime.GUI
{
    public class FontAtlas
    {
        private Dictionary<char, Character> Characters = new Dictionary<char, Character>();
        public Bitmap Atlas { get; }
        public int FontSize { get; }
        public float LineSpacing { get; }
        public float Spacing { get; }


        public FontAtlas(Dictionary<char, Character> characters,
            Bitmap atlas,
            int fontSize,
            float lineSpacing,
            float spacing)
        {
            Atlas = atlas;
            Characters = characters;
            FontSize = fontSize;
            LineSpacing = lineSpacing;
            Spacing = spacing;
        }

        public Character GetCharacter(char ch)
        {
            return Characters?[ch];
        }
    }
}