using System.Collections.Generic;
using System.Drawing;
using SharpGL;
using SharpGL.SceneGraph.Assets;

namespace VoidTime.GUI
{
    public class FontAtlas
    {
        private readonly Dictionary<char, Character> Characters = new Dictionary<char, Character>();
        private bool first = true;
        public Bitmap Atlas { get; }
        private Texture texture { get; } = new Texture();
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

        public Texture GetTexture(OpenGL gl)
        {
            if (first)
            {
                texture.Create(gl, Atlas);
                first = false;
            }

            return texture;
        }

        public Character GetCharacter(char ch)
        {
            return Characters?[ch];
        }
    }
}