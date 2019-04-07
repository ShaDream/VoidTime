using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace VoidTime.Generator
{
    public class AtlasSpriteGenerator
    {
        public List<string> ImagePaths;

        public Bitmap CreateAtlasFromImages(int width)
        {
            var images = ImagePaths.Select(imagePath => Tuple.Create(new Bitmap(imagePath),
                    Path.GetFileName(imagePath).Replace(Path.GetExtension(imagePath), "")))
                .ToList()
                .OrderByDescending(x => x.Item1.Height);

            if (images.First().Item1.Width > width)
                throw new InvalidDataException($"Picture {images.First().Item2} was Bigger then atlas");

            var globalHeight = images.First().Item1.Height;
            var levels = new List<Level> { new Level(globalHeight, width, globalHeight) };

            foreach (var bitmap in images)
                for (var i = 0; i < levels.Count; i++)
                {
                    var level = levels[i];
                    if (level.TryFill(bitmap.Item1))
                        break;

                    if (i + 1 != levels.Count)
                        continue;

                    globalHeight += bitmap.Item1.Height + 1;
                    levels.Add(new Level(bitmap.Item1.Height, width, globalHeight));
                }

            var result = new Bitmap(width, globalHeight);
            var g = Graphics.FromImage(result);
            foreach (var level in levels)
                foreach (var drawData in level.GetDrawData(globalHeight))
                    g.DrawImage(drawData.Item1, drawData.Item2);
            g.Dispose();
            foreach (var image in images)
                image.Item1.Dispose();

            return result;
        }
    }

    public class Level
    {
        public readonly int Height;
        public readonly int Width;
        private readonly List<Tuple<Rectangle, Bitmap>> Ceiling = new List<Tuple<Rectangle, Bitmap>>();
        private readonly List<Tuple<Rectangle, Bitmap>> Floor = new List<Tuple<Rectangle, Bitmap>>();
        //The left top point in global image
        private readonly int GlobalHeight;

        public Level(int height, int width, int globalHeight)
        {
            Height = height;
            Width = width;
            GlobalHeight = globalHeight;
        }

        public bool TryFill(Bitmap bitmap)
        {
            var position = Floor.Count == 0
                ? new Point(0, bitmap.Height)
                : new Point(Floor[Floor.Count - 1].Item1.Right, bitmap.Height);
            var rect = new Rectangle(position, bitmap.Size);
            if (rect.Right <= Width && !IsIntersectWithCeil(rect))
            {
                Floor.Add(Tuple.Create(rect, bitmap));
                return true;
            }

            position = Ceiling.Count == 0
                ? new Point(Width - bitmap.Width, Height)
                : new Point(Ceiling[Ceiling.Count - 1].Item1.X - bitmap.Width, Height);
            rect = new Rectangle(position, bitmap.Size);
            if (rect.X < 0 || IsIntersectWithFloor(rect))
                return false;

            Ceiling.Add(Tuple.Create(rect, bitmap));
            return true;
        }

        private bool IsIntersectWithCeil(Rectangle rect)
        {
            foreach (var tuple in Ceiling)
                if (tuple.Item1.IntersectsWith(rect))
                    return true;

            return false;
        }

        private bool IsIntersectWithFloor(Rectangle rect)
        {
            foreach (var tuple in Floor)
                if (tuple.Item1.IntersectsWith(rect))
                    return true;

            return false;
        }

        public IEnumerable<Tuple<Bitmap, Point>> GetDrawData(int imageHeight)
        {
            foreach (var tuple in Floor)
            {
                yield return Tuple.Create(tuple.Item2,
                    new Point(tuple.Item1.X, imageHeight - GlobalHeight + (Height - tuple.Item1.Y)));
            }

            foreach (var tuple in Ceiling)
            {
                yield return Tuple.Create(tuple.Item2,
                    new Point(tuple.Item1.X, imageHeight - GlobalHeight + (Height - tuple.Item1.Y)));
            }
        }
    }
}