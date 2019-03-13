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

        public void DrawObject(ObjectOnDisplay obj, OpenGL gl)
        {
            var x = obj.PositionOnDisplay.X;
            var y = obj.PositionOnDisplay.Y;

            MainForm.player.Bind(gl);

            gl.Begin(OpenGL.GL_QUADS);
            gl.TexCoord(0.0f, 0.0f); gl.Vertex(x, y, 1.0f); // Bottom Left Of The Texture and Quad
            gl.TexCoord(1.0f, 0.0f); gl.Vertex(x+200, y, 1.0f);  // Bottom Right Of The Texture and Quad
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(x+200f, y+200f, 1.0f);   // Top Right Of The Texture and Quad
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(x, y+200f, 1.0f);
            gl.End();

        }
    }
}