using System;
using System.Drawing;
using SharpGL;
using SharpGL.SceneGraph.Assets;

namespace VoidTime
{
    public class BackgroundDrawHelper : IDrawable
    {
        public Type GameObjectType { get; } = typeof(Background);
        public Bitmap BackgroundImage =
            new Bitmap(@"C:\Users\ShaDream\source\repos\VoidTime\VoidTime\Resources\Textures\back.png");

        public void DrawObject(ObjectOnDisplay obj, OpenGL gl)
        {
            var x = obj.PositionOnDisplay.X;
            var y = obj.PositionOnDisplay.Y;

            MainForm.background.Bind(gl);

            gl.Begin(OpenGL.GL_QUADS);
            gl.TexCoord(0.0f, 0.0f); gl.Vertex(x, y, 1.0f); // Bottom Left Of The Texture and Quad
            gl.TexCoord(1.0f, 0.0f); gl.Vertex(x + 1000, y, 1.0f);  // Bottom Right Of The Texture and Quad
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(x + 1000f, y + 1000f, 1.0f);   // Top Right Of The Texture and Quad
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(x, y + 1000f, 1.0f);
            gl.End();

        }
    }
}