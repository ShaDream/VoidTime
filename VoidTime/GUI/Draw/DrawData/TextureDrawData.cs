using SharpGL.SceneGraph.Assets;

namespace VoidTime.GUI
{
    public class TextureDrawData
    {
        public Vector2D[] Points { get; set; }
        public Vector2D[] Textures { get; set; }
        public UIMask Mask { get; set; }
        public Texture Texture { get; set; }
        public float DrawPriority { get; set; }
    }
}