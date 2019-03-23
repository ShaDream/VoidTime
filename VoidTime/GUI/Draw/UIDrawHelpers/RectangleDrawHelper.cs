using System;
using SharpGL;
using SharpGL.Enumerations;

namespace VoidTime.GUI
{
    public class RectangleDrawHelper : IUIDrawable
    {
        public Type DrawDataType { get; } = typeof(RectangleDrawData);

        public void DrawUi(IDrawData obj, OpenGL gl)
        {
            var o = (RectangleDrawData)obj;
            gl.Disable(OpenGL.GL_TEXTURE_2D);

            if (o.Mask != null)
                MaskHelper.Begin(o.Mask, gl);


            gl.Begin(BeginMode.Quads);
            gl.Color(o.Color.R, o.Color.G, o.Color.B);

            gl.Vertex(o.Points[0].X, o.Points[0].Y,o.DrawPriority);
            gl.Vertex(o.Points[1].X, o.Points[1].Y, o.DrawPriority);
            gl.Vertex(o.Points[2].X, o.Points[2].Y, o.DrawPriority);
            gl.Vertex(o.Points[3].X, o.Points[3].Y, o.DrawPriority);

            gl.Color(1, 1, 1);
            gl.End();


            if (o.Mask != null)
                MaskHelper.End(o.Mask, gl);

            gl.Enable(OpenGL.GL_TEXTURE_2D);
        }
    }
}