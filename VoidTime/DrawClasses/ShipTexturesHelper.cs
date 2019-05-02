using System.Collections.Generic;
using System.Drawing;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using VoidTime.Resources;

namespace VoidTime
{
    public static class ShipTexturesHelper
    {
        private static bool initialized = false;
        private static Dictionary<string, Texture> textures;

        public static Texture GetShipTexture(OpenGL gl, string shipName)
        {
            if (!initialized)
                Init(gl);

            return textures[shipName];
        }

        private static void Init(OpenGL gl)
        {
            textures = new Dictionary<string, Texture>
            {
                {"Base ship", CreateTexture(gl, Textures.BaseShip)},
                {"Base enemy ship", CreateTexture(gl, Textures.BaseEnemyShip)},
                {"Fight ship", CreateTexture(gl, Textures.BaseShip)},
                {"Fast ship", CreateTexture(gl, Textures.BaseShip)}
            };
            initialized = true;
        }

        private static Texture CreateTexture(OpenGL gl, Bitmap picture)
        {
            var texture = new Texture();
            texture.Create(gl, picture);
            return texture;
        }
    }
}