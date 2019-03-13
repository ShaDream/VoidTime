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

        public void DrawObject(ObjectOnDisplay obj, OpenGL graphics)
        {

            //graphics.PushMatrix();

            //MainForm.background.Bind(graphics);
            //var x = obj.PositionOnDisplay.X;
            //var y = obj.PositionOnDisplay.Y;
            ////graphics.DrawImage(PlayerImage, x, y, PlayerImage.Width / 3, PlayerImage.Height / 3);

            //graphics.Begin(SharpGL.Enumerations.BeginMode.Quads);
            //graphics.Color(new[] { 1f, 1f, 1f });
            //graphics.TexCoord(0, 0);
            //graphics.Vertex(0, 50);
            //graphics.TexCoord(0, 1);
            //graphics.Vertex(0, 0);
            //graphics.TexCoord(1, 1);
            //graphics.Vertex(50, 50);
            //graphics.TexCoord(1, 0);
            //graphics.Vertex(50, 0);
            //graphics.End();

            //graphics.PopMatrix();


        }
    }
}