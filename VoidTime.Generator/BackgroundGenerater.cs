using System;
using System.Collections.Generic;
using System.Drawing;

namespace VoidTime.Generator
{
    public class BackgroundGenerater
    {
        public Color BackColor;
        public List<Color> ColorPatterns = new List<Color>();
        public List<string> PathPatterns = new List<string>();
        public List<float> PatternsSpawnRate = new List<float>();

        public Size Resolution = new Size(1000, 1000);

        private readonly Random rng = new Random();
        public int StarCount = 100;

        private IEnumerable<Tuple<Bitmap, int>> ChooseByChance(List<string> objects, List<float> chance)
        {
            var sum = 0f;
            chance.ForEach(x => sum += x);

            for (var i = 0; i < objects.Count; i++)
                yield return Tuple.Create(new Bitmap(objects[i]), (int)(chance[i] / sum * StarCount));
        }

        public Image GetImage()
        {
            var result = new Bitmap(Resolution.Width, Resolution.Height);

            var patterns = new List<Bitmap>();

            foreach (var pathPattern in PathPatterns)
                patterns.Add(new Bitmap(pathPattern));

            for (var x = 0; x < Resolution.Width; x++)
                for (var y = 0; y < Resolution.Height; y++)
                    result.SetPixel(x, y, BackColor);

            foreach (var tuple in ChooseByChance(PathPatterns, PatternsSpawnRate))
                for (var i = 0; i < tuple.Item2; i++)
                {
                    var Position = new Point(rng.Next(Resolution.Width - tuple.Item1.Width),
                        rng.Next(Resolution.Height - tuple.Item1.Height));
                    var Color = ColorPatterns[rng.Next(ColorPatterns.Count)];

                    for (var x = 0; x < tuple.Item1.Width; x++)
                        for (var y = 0; y < tuple.Item1.Height; y++)
                            if (tuple.Item1.GetPixel(x, y).A != 0)
                                result.SetPixel(Position.X + x, Position.Y + y,
                                    Multiply(tuple.Item1.GetPixel(x, y), Color));
                }

            return result;
        }

        private Color Multiply(Color a, Color b)
        {
            return Color.FromArgb(a.R * b.R / 255, a.G * b.G / 255, a.B * b.B / 255);
        }
    }
}