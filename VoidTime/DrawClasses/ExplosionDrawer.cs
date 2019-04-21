using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using System.Xml.Resolvers;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph.Assets;

namespace VoidTime
{
    public class ExplosionDrawer : IDrawable
    {
        public Texture exp;
        public Dictionary<int, Tuple<PointF, SizeF>> data;


        private static Vector2D[] ArrayTexCoords(PointF location, SizeF size)
        {
            return new[]
            {
                new Vector2D(location.X, location.Y-size.Height),
                new Vector2D(location.X, location.Y),
                new Vector2D(location.X+size.Width, location.Y),
                new Vector2D(location.X+size.Width, location.Y-size.Height)
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

        public Type GameObjectType { get; } = typeof(Explosion);
        public void DrawObject(ObjectOnDisplay obj, OpenGL gl)
        {
            exp.Bind(gl);
            var o = (Explosion)obj.GameObject;
            var size = new Size(1000, 1000);
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
            exp.Create(gl, Resources.Textures.explosion);

            var doc = new XmlDocument();
            doc.LoadXml(Resources.Data.Explosion);

            var nodes = doc.SelectNodes("//sprite");

            foreach (XmlNode node in nodes)
            {
                var loc = new PointF(float.Parse(node["posX"].InnerText), float.Parse(node["posY"].InnerText));
                var id = int.Parse(node["id"].InnerText);
                var size = new SizeF(float.Parse(node["sizeX"].InnerText), float.Parse(node["sizeY"].InnerText));
                data[id] = Tuple.Create(loc, size);
            }
        }
    }
}