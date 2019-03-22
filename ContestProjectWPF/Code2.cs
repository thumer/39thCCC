using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LINQtoCSV;

// -------------------------------------------------
// Wer hots vabrochn? Da Stoascheißer Koarl!
// -------------------------------------------------
namespace ContestProjectWPF
{
#if Level2
    public class Code : CodeBase
    {
        public override string Execute(string input)
        {
            var lines = input.Split('\n', '\r').Where(i => !string.IsNullOrEmpty(i)).ToArray();

            var firstline = lines.First().Split();
            var start = int.Parse(firstline[0]);
            var end = int.Parse(firstline[1]);
            var imageCount = int.Parse(firstline[2]);

            var data = lines.Skip(1).ToArray();

            var images = processImages(data)
                .Where(i => i.HasData)
                .OrderBy(i => i.Timestamp)
                .ToArray();

            var equalImages = images.GroupBy(i => i.PointDivs.Skip(1).Aggregate($"{i.PointDivs.First().X}/{i.PointDivs.First().Y}", (agg, p) => $"{agg},{p.X}/{p.Y}"))
                .Select(g => (first: g.First().Timestamp, last: g.Last().Timestamp, count: g.Count()));

            var results = equalImages
                .OrderBy(i => i.first)
                .Select(i => $"{i.first} {i.last} {i.count}")
                .Aggregate((agg, v) => $"{agg}{Environment.NewLine}{v}");

            return results;
        }

        Image[] processImages(string[] data)
        {
            var head = data.First().Split();
            var timestamp = head[0].ToInt();
            var rows = head[1].ToInt();
            var columns = head[2].ToInt();

            var imageLines = data.Skip(1).Take(rows);

            var imageData = imageLines
                .Select(l => l.Split()
                .Select(i => i.ToInt()).ToArray())
                .ToArray();
            var image = new Image(timestamp, imageData);

            var rest = data.Skip(1 + rows).ToArray();

            return rest.Any()
                ? processImages(rest).Concat(new[] { image }).ToArray()
                : new[] { image };
        }
    }
    public struct Image
    {
        public Image(int timestamp, int[][] data)
        {
            Timestamp = timestamp;
            Data = data;
            DataOffset = calculateOffsetData();
            BlackWhiteOffset = DataOffset.Select(i => i.Select(x => x != 0).ToArray()).ToArray();

            HasData = Data.SelectMany(i => i).Any(i => i != 0);

            var points = new List<Point>();
            for (int x = 0; x < data.Length; x++)
                for (int y = 0; y < data[x].Length; y++)
                    if (data[x][y] != 0)
                        points.Add(new Point(x, y));
            Points = points.ToArray();

            if (Points.Any())
            {
                var firstPoint = Points.First();
                PointDivs = Points.Skip(1).Select(p => firstPoint.Subtract(p)).ToArray();
            }
            else
                PointDivs = new Point[0];

            int[][] calculateOffsetData()
            {
                var newData = data.Where(x => x.Any(a => a != 0)).ToArray();
                for (int x = 0; x < newData.Length; x++)
                    for (int y = 0, lastY = 0; y < newData[x].Length; y++)
                        if (newData[x][y] != 0 && y >= lastY)
                        {
                            newData[x][lastY] = newData[x][lastY];
                            lastY++;
                        }

                return newData;
            }
        }

        public int Timestamp { get; }

        public int[][] Data { get; }
        public int[][] DataOffset { get; }
        public bool[][] BlackWhiteOffset { get; set; }
        public Point[] Points { get; }
        public Point[] PointDivs { get; }
        public bool HasData { get; }
    }

    public struct Point : IEqualityComparer<Point>
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public bool Equals(Point x, Point y)
            => x.GetHashCode() == y.GetHashCode();

        public int GetHashCode(Point obj)
            => obj.X.GetHashCode() + obj.Y.GetHashCode();
    }
    public static partial class Extensions
    {
        public static int ToInt(this string @this)
            => int.Parse(@this);

        public static Point Subtract(this Point @this, Point p)
            => new Point(@this.X - p.X, @this.Y - p.Y);

        public static Point Add(this Point @this, Point p)
           => new Point(@this.X + p.X, @this.Y + p.Y);

        public static bool CompareWith(this Image @this, IEnumerable<Image> images)
        {
            foreach (var img in images)
                for (int x = 0; x < img.BlackWhiteOffset.Length; x++)
                    for (int y = 0; y < img.BlackWhiteOffset[x].Length; y++)
                        if (@this.BlackWhiteOffset[x][y] != img.BlackWhiteOffset[x][y])
                            return false;
            return true;
        }
    }
#endif
}
