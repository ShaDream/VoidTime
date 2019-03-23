﻿using System;
using SharpGL;
using SharpGL.Enumerations;

namespace VoidTime.GUI
{
    public class TextDrawHelper : IUIDrawable
    {
        public Type DrawDataType { get; } = typeof(TextDrawData);
        public void DrawUi(IDrawData obj, OpenGL gl)
        {
            var o = (TextDrawData)obj;

            if (o.Mask != null)
                MaskHelper.Begin(o.Mask, gl);

            o.Atlas.GetTexture(gl).Bind(gl);

            gl.Begin(BeginMode.Quads);

            for (var i = 0; i < o.Points.Length; i++)
            {
                gl.TexCoord(o.Textures[i].X, o.Textures[i].Y, o.DrawPriority);
                gl.Vertex(o.Points[i].X, o.Points[i].Y, o.DrawPriority);
            }

            gl.End();


            if (o.Mask != null)
                MaskHelper.End(o.Mask, gl);
        }
    }
}