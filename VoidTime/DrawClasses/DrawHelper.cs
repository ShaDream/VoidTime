using System.Drawing;
using SharpGL;
using SharpGL.Enumerations;

namespace VoidTime
{
    public static class DrawHelper
    {
        private static readonly Vector2D[] ArrayTexCoords =
        {
            new Vector2D(0.0f, 0.0f),
            new Vector2D(0.0f, 1.0f),
            new Vector2D(1.0f, 1.0f),
            new Vector2D(1.0f, 0.0f)
        };

        private static Vector2D[] GetObjectCorners(Vector2D vector, Size size)
        {
            return new[]
            {
                vector + new Vector2D(-size.Width / 2, -size.Height / 2),
                vector + new Vector2D(-size.Width / 2, size.Height / 2),
                vector + new Vector2D(size.Width / 2, size.Height / 2),
                vector + new Vector2D(size.Width / 2, -size.Height / 2)
            };
        }

        public static void Draw(ObjectOnDisplay obj, OpenGL gl, Size size, double angle)
        {
            var arrayVectors = GetObjectCorners(obj.PositionOnDisplay, size);
            var rotatedVectors = new Vector2D[4];
            for (var i = 0; i < 4; i++)
                rotatedVectors[i] = (arrayVectors[i] - obj.PositionOnDisplay).Rotate(-angle) + obj.PositionOnDisplay;
            var priority = obj.GameObject.DrawingPriority;

            gl.Begin(BeginMode.Quads);
            for (var i = 0; i < 4; i++)
            {
                gl.TexCoord(ArrayTexCoords[i].X, ArrayTexCoords[i].Y);
                gl.Vertex(rotatedVectors[i].X, rotatedVectors[i].Y, priority);
            }

            gl.End();
        }

        public static void Draw(ObjectOnDisplay obj, OpenGL gl, Size size)
        {
            var arrayVectors = GetObjectCorners(obj.PositionOnDisplay, size);
            var priority = obj.GameObject.DrawingPriority;

            gl.Begin(BeginMode.Quads);
            for (var i = 0; i < 4; i++)
            {
                gl.TexCoord(ArrayTexCoords[i].X, ArrayTexCoords[i].Y);
                gl.Vertex(arrayVectors[i].X, arrayVectors[i].Y, priority);
            }

            gl.End();
        }
    }
}