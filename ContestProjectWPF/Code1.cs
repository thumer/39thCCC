using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LINQtoCSV;

namespace ContestProjectWPF
{
#if Level1
    public class Code : CodeBase
    {
        public override string Execute(string input)
        {
            var lines = input.Split('\n', '\r').Where(i => !string.IsNullOrEmpty(i));

            var firstline = lines.First().Split();
            var start = int.Parse(firstline[0]);
            var end = int.Parse(firstline[1]);
            var imageCount = int.Parse(firstline[2]);

            var data = lines.Skip(1).ToArray();

            var results = processNextImage(data)
                .Where(i => i != 0)
                .OrderBy(i => i)
                .Select(i => i.ToString())
                .Aggregate((agg, v) => $"{agg}{Environment.NewLine}{v}");

            return results;
        }

        int[] processNextImage(string[] data)
        {
            var head = data.First().Split();
            var timestamp = head[0].ToInt();
            var rows = head[1].ToInt();
            var columns = head[2].ToInt();

            var imageLines = data.Skip(1).Take(rows);

            var image = new Image(timestamp)

            var pixels = imageLines.SelectMany(l => l.Split().Select(p => p.ToInt()));
            timestamp = pixels.Any(i => i != 0) ? timestamp : 0;
            var rest = data.Skip(1+rows).ToArray();

            return rest.Any()
                ? processNextImage(rest).Concat(new[] { timestamp }).ToArray()
                : new[] { timestamp };
        }

        private class Image
        {
            public Image(int timestamp, int[][] data)
            {
                Timestamp = timestamp;
                Data = data;
            }

            public int Timestamp { get; }

            public int[][] Data { get; }
            public bool HasData => Data.SelectMany(i => i).Any(i => i != 0);
        }
    }

    public static partial class Extensions
    {
        public static int ToInt(this string @this)
            => int.Parse(@this);
    }
#endif
}
