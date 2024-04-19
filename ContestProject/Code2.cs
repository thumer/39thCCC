using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ContestProject
{
#if Level2
    public class Code : CodeBase
    {
        public override string Execute(string input)
        {
            input = input.Replace("\r\n", "\n");
            var inputLines = input.Split('\n');
            int lineCount = int.Parse(inputLines[0]);

            // Direction:
            // North = 'W',
            // East = 'D',
            // South = 'S',
            // West = 'A'

            // Example input:
            //3
            //WASAWWDDDSS
            //DDSAASDDSAA
            //DSASSDWDSDWWAWDDDSASSDWDSDWWAWD


            IList<string> lines = new List<string>();
            //read the other input lines
            for (int i = 1; i <= lineCount; i++)
            {
                lines.Add(inputLines[i]);
            }

            IList<IList<Direction>> directions = new List<IList<Direction>>(); 
            foreach (var line in lines)
            {
                IList<Direction> lineDirections = new List<Direction>();
                foreach (var c in line)
                {
                    lineDirections.Add((Direction)c);
                }
                directions.Add(lineDirections);
            }

            // Example output:
            //3 3 3 2
            //0 4 3 4
            //8 11 8 4

            // You are given a list of paths for a lawn mower.
            // For each path, calculate the size of the
            // smallest rectangular lawn, that it can fit into.

            StringBuilder sb = new StringBuilder();

            foreach (var line in directions)
            {

                int maxY = 0, minY = 0, maxX = 0, minX = 0;
                int maxWidth = 0, maxHeight = 0;
                int x = 0;
                int y = 0;
                foreach (var d in line)
                {
                    switch (d)
                    {
                        case Direction.North:
                            y++;
                            break;
                        case Direction.East:
                            x++;
                            break;
                        case Direction.South:
                            y--;
                            break;
                        case Direction.West:
                            x--;
                            break;
                    }

                    if (maxX < x)
                        maxX = x;
                    if (minX > x)
                        minX = x;
                    if (maxY < y)
                        maxY = y;
                    if (minY > y)
                        minY = y;

                    maxWidth = maxX - minX + 1;
                    maxHeight = maxY - minY + 1;
                }

                sb.Append(maxWidth  + " " + maxHeight + "\n");
            }

            return sb.ToString();
        }
    }

    public enum Direction
    {
        North = 'W',
        East = 'D',
        South = 'S',
        West = 'A'
    }
#endif
}
