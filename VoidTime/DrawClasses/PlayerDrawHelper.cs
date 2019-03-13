using System;
using System.Drawing;
using SharpGL;
using SharpGL.SceneGraph.Assets;

namespace VoidTime
{
    public class PlayerDrawHelper : IDrawable
    {
        public Type GameObjectType { get; } = typeof(Player);

        public Bitmap PlayerImage =
            new Bitmap(@"D:\Downloads/UFO.png");

        public void DrawObject(ObjectOnDisplay obj, OpenGL graphics)
        {
            graphics.Enable(OpenGL.GL_TEXTURE_2D);
            MainForm.player.Bind(graphics);
            graphics.TexParameter(SharpGL.OpenGL.GL_TEXTURE_2D, SharpGL.OpenGL.GL_TEXTURE_WRAP_S, SharpGL.OpenGL.GL_REPEAT);
            graphics.TexParameter(SharpGL.OpenGL.GL_TEXTURE_2D, SharpGL.OpenGL.GL_TEXTURE_WRAP_T, SharpGL.OpenGL.GL_REPEAT);
            graphics.TexParameter(SharpGL.OpenGL.GL_TEXTURE_2D, SharpGL.OpenGL.GL_TEXTURE_MAG_FILTER, SharpGL.OpenGL.GL_LINEAR);
            graphics.TexParameter(SharpGL.OpenGL.GL_TEXTURE_2D, SharpGL.OpenGL.GL_TEXTURE_MIN_FILTER, SharpGL.OpenGL.GL_LINEAR);



            var x = obj.PositionOnDisplay.X;
            var y = obj.PositionOnDisplay.Y;
            //graphics.DrawImage(PlayerImage, x, y, PlayerImage.Width / 3, PlayerImage.Height / 3);

            graphics.Color(1.0f, 1.0f, 1.0f,1f);//задает цвет 

            graphics.Begin(OpenGL.GL_QUADS);
            graphics.TexCoord(0, 0);
            graphics.Vertex(0, 0);
            graphics.TexCoord(0, 1);
            graphics.Vertex(0, 0.5);
            graphics.TexCoord(1, 1);
            graphics.Vertex(0.5, 0.5);
            graphics.TexCoord(1, 0);
            graphics.Vertex(0.5, 0);
            graphics.End();
            graphics.Disable(OpenGL.GL_TEXTURE_2D);

        }
    }
}