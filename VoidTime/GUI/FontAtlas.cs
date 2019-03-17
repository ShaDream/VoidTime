using System.Collections.Generic;
using System.Drawing;

namespace VoidTime.GUI
{
    public class FontAtlas
    {
        private Dictionary<char, Character> Characters = new Dictionary<char, Character>();
        private Bitmap Atlas;

        public FontAtlas(Dictionary<char,Character> characters, Bitmap atlas)
        {
            Atlas = atlas;
            Characters = characters;
        }

        public Character GetCharacter(char ch)
        {
            return Characters?[ch];
        }
    }
}