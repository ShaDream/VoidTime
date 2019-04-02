using SharpGL;
using SharpGL.Enumerations;

namespace VoidTime.GUI
{
    public static class MaskHelper
    {
        public static void Begin(UIMask mask, OpenGL GL)
        {
            GL.Enable(OpenGL.GL_STENCIL_TEST);

            GL.ColorMask(0, 0, 0, 0);
            GL.DepthMask(0);
            GL.StencilFunc(StencilFunction.Always, 1, 0xFFFFFFFF);
            GL.StencilOp(StencilOperation.Increase, StencilOperation.Increase, StencilOperation.Increase);

            // Draw rectangle
            GL.Color(0, 0, 0);
            GL.Begin(BeginMode.Quads);
            GL.Vertex(mask.Rect.Left, mask.Rect.Top);
            GL.Vertex(mask.Rect.Right, mask.Rect.Top);
            GL.Vertex(mask.Rect.Right, mask.Rect.Bottom);
            GL.Vertex(mask.Rect.Left, mask.Rect.Bottom);
            GL.End();

            GL.ColorMask(1, 1, 1, 1);
            GL.DepthMask(1);
            GL.StencilFunc(StencilFunction.Equal, 1, 0xFFFFFFF);
            GL.StencilOp(StencilOperation.Keep, StencilOperation.Keep, StencilOperation.Keep);
        }


        public static void End(UIMask mask, OpenGL GL)
        {
            GL.ColorMask(0, 0, 0, 0);
            GL.DepthMask(0);
            GL.StencilFunc(StencilFunction.Always, 0, 0xFFFFFFF);
            GL.StencilOp(StencilOperation.Decrease, StencilOperation.Decrease, StencilOperation.Decrease);
            // Draw rectangle
            GL.Color(0, 0, 0);
            GL.Begin(BeginMode.Quads);
            GL.Vertex(mask.Rect.Left, mask.Rect.Top);
            GL.Vertex(mask.Rect.Right, mask.Rect.Top);
            GL.Vertex(mask.Rect.Right, mask.Rect.Bottom);
            GL.Vertex(mask.Rect.Left, mask.Rect.Bottom);
            GL.End();

            GL.ColorMask(1, 1, 1, 1);
            GL.DepthMask(1);

            GL.Disable(OpenGL.GL_STENCIL_TEST);
        }
    }
}