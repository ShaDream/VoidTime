using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Xml;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph.Assets;
using VoidTime.Resources;

namespace VoidTime
{
    public class ExplosionDrawer : IDrawable
    {
        public Dictionary<int, Tuple<PointF, SizeF>> data = new Dictionary<int, Tuple<PointF, SizeF>>();
        public Texture exp;

        public Type GameObjectType { get; } = typeof(Explosion);

        public void DrawObject(ObjectOnDisplay obj, OpenGL gl)
        {
            exp.Bind(gl);
            var o = (Explosion) obj.GameObject;
            var size = new Size(1080, 1080);
            var pos = GetObjectCorners(obj.PositionOnDisplay, size);
            var posTex = ArrayTexCoords(data[o.frame].Item1, data[o.frame].Item2);

            gl.Begin(BeginMode.Quads);
            for (var i = 0; i < 4; i++)
            {
                gl.TexCoord(posTex[i].X, posTex[i].Y);
                gl.Vertex(pos[i].X, pos[i].Y, 0);
            }

            gl.End();
        }

        public void Init(OpenGL gl)
        {
            exp = new Texture();
            exp.Create(gl, Textures.explosion);

            var doc = new XmlDocument();
            doc.LoadXml(Data.Explosion);

            var nodes = doc.SelectNodes("//sprite");

            var ci = (CultureInfo) CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
            foreach (XmlNode node in nodes)
            {
                var loc = new PointF(float.Parse(node["posX"].InnerText, NumberStyles.Any, ci),
                                     float.Parse(node["posY"].InnerText, NumberStyles.Any, ci));
                var id = int.Parse(node["id"].InnerText);
                var size = new SizeF(float.Parse(node["sizeX"].InnerText, NumberStyles.Any, ci),
                                     float.Parse(node["sizeY"].InnerText, NumberStyles.Any, ci));
                data[id] = Tuple.Create(loc, size);
            }
        }


        private static Vector2D[] ArrayTexCoords(PointF location, SizeF size)
        {
            return new[]
            {
                new Vector2D(location.X, location.Y),
                new Vector2D(location.X, location.Y + size.Height),
                new Vector2D(location.X + size.Width, location.Y + size.Height),
                new Vector2D(location.X + size.Width, location.Y)
            };
        }

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
    }
}