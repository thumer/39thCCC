using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ContestProject
{
#if Level3
    public class Code : CodeBase
    {
        public override string Execute(string input)
        {
            input = input.Replace("\r\n", "\n");
            var inputLines = input.Split('\n');
            int lawnCount = int.Parse(inputLines[0]);

            // Direction:
            // North = 'W',
            // East = 'D',
            // South = 'S',
            // West = 'A'

            // Example input:

            //1. Line: Number of lawns
            //1. Line  of the lawn: The width followed by the height of the lawn, separated by a space
            //Next Lines: Field (. represents grass, X represents a tree)

            //4
            //5 3
            //..X..
            //.....
            //.....
            //SSDWDSDWWDSS
            //5 3
            //..X..
            //.....
            //.....
            //DSSAWDDSDWWDSS
            //5 3
            //..X..
            //.....
            //.....
            //SSDWWDSSDWWDSS
            //5 3
            //..X..
            //.....
            //.....
            //ASSDWDSDWWDSS

            // Write a Inputroutine that reads the input and returns the output

            IList<Lawn> lawns = new List<Lawn>();
            for (int i = 1; lawns.Count < lawnCount;)
            {
                var lawn = new Lawn();
                var lawnSize = inputLines[i].Split(' ');
                lawn.Width = int.Parse(lawnSize[0]);
                lawn.Height = int.Parse(lawnSize[1]);
                lawn.Field = new bool[lawn.Width, lawn.Height];

                for (int j = 0; j < lawn.Height; j++)
                {
                    var line = inputLines[i + j + 1];
                    for (int k = 0; k < lawn.Width; k++)
                    {
                        lawn.Field[k, j] = line[k] == 'X';
                    }
                }

                int indexOfPath = i + lawn.Height + 1;

                foreach (var c in inputLines[indexOfPath])
                    lawn.Path.Add((Direction)c);

                lawns.Add(lawn);

                i = indexOfPath + 1;
            }

            //It visits each free cell of the given lawn
            //It does not visit any cell twice
            //It does not visit the cell with the tree on it
            //It does not leave the lawn

            //Validate all lawns

            foreach (var lawn in lawns)
            {
                int x = 0;
                int y = 0;

                //calculate the start position, so that the path is in width and heigt
                int maxY = 0, minY = 0, maxX = 0, minX = 0;
                int maxWidth = 0, maxHeight = 0;

                foreach (var d in lawn.Path)
                {
                    switch (d)
                    {
                        case Direction.North:
                            y--;
                            break;
                        case Direction.East:
                            x++;
                            break;
                        case Direction.South:
                            y++;
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

                x = Math.Abs(minX);
                y = Math.Abs(minY);

                if (maxHeight > lawn.Height || maxWidth > lawn.Width)
                    lawn.IsValid = false;

                lawn.Visited.Add((x, y));
                foreach (var d in lawn.Path)
                {
                    //check if leave the width, heigt
                    if (x < 0 || x >= lawn.Width || y < 0 || y >= lawn.Height)
                    {
                        lawn.IsValid = false;
                    }
                    else
                    //check if is on tree
                    if (lawn.Field[x, y])
                    {
                        lawn.IsValid = false;
                    }

                    switch (d)
                    {
                        case Direction.North:
                            y--;
                            break;
                        case Direction.East:
                            x++;
                            break;
                        case Direction.South:
                            y++;
                            break;
                        case Direction.West:
                            x--;
                            break;
                    }

                    //check if already visited
                    if (lawn.Visited.Contains((x, y)))
                    {
                        lawn.IsValid = false;
                    }
                    //check if leave the width, heigt
                    if (x < 0 || x >= lawn.Width || y < 0 || y >= lawn.Height)
                    {
                        lawn.IsValid = false;
                    }
                    else
                    //check if is on tree
                    if (lawn.Field[x, y])
                    {
                        lawn.IsValid = false;
                    }
                    

                    lawn.Visited.Add((x, y));
                }

                int treeCount = lawn.Field.Cast<bool>().Count(b => b);

                //check if all cells are visited except the tree
                if (lawn.Visited.Count < (lawn.Width * lawn.Height - treeCount))
                {
                    lawn.IsValid = false;
                }
            }

            //Create a StringBuilder; each line should write VALID if lawn is valid, INVALID otherwise
            StringBuilder sb = new StringBuilder();
            int i2 = 0;
            foreach (var lawn in lawns)
            {
                sb.Append(lawn.IsValid ? "VALID" : "INVALID");
                //write a matrix of the visited cells lawn.Visited, where the visited cells are marked with the index otherwise write a dot (.) or a tree (X)
                if (i2 == 556)
                {
                    //for (int j = 0; j < lawn.Height; j++)
                    //{
                    //    sb.Append("\n");
                    //    for (int k = 0; k < lawn.Width; k++)
                    //    {
                    //        var index = lawn.Visited.IndexOf((k, j));
                    //        sb.Append(index == -1 ? (lawn.Field[k, j] ? "X\t" : ".\t") : index.ToString() + "\t");
                    //    }
                    //}
                }

                sb.Append("\n");
                i2++;
            }

            return sb.ToString().Trim();
        }
    }

    public class Lawn
    {
        public int Width { get; set; }
        public int Height { get; set; }

        // true = tree, false = grass
        public bool[,] Field { get; set; }

        public IList<Direction> Path { get; set; } = new List<Direction>();
        public IList<(int x, int y)> Visited { get; set; } = new List<(int x, int y)>();

        public bool IsValid { get; set; } = true;
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
