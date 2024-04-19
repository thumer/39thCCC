using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContestProject
{
#if Level1
    public class Code : CodeBase
    {
        public override string Execute(string input)
        {
            input = input.Replace("\r\n", "\n");
            var inputLines = input.Split('\n');
            int lineCount = int.Parse(inputLines[0]);

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

            //Count the number of times each direction appears in each line

            StringBuilder sb = new StringBuilder();
            foreach (var line in directions)
            {
                int north = line.Count(d => d == Direction.North);
                int east = line.Count(d => d == Direction.East);
                int south = line.Count(d => d == Direction.South);
                int west = line.Count(d => d == Direction.West);

                sb.Append(north + " " + east + " " + south + " " + west + "\n");
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
