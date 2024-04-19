using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ContestProject
{
#if Level4

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

            //3
            //7 6
            //.......
            //.......
            //.......
            //.......
            //.X.....
            //.......
            //7 7
            //.......
            //.......
            //.......
            //.X.....
            //.......
            //.......
            //.......
            //6 6
            //......
            //....X.
            //......
            //......
            //......
            //......

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
                        if (lawn.Field[k, j])
                            lawn.Tree = (k, j);
                    }
                }

                lawns.Add(lawn);

                i += lawn.Height + 1;
            }


            foreach (var lawn in lawns)
            {
                int x = 0;
                int y = lawn.Height - 1;

                // Startposition ist unten links  
                MowLawn(lawn, x, y);
                lawn.Visited = new List<(int x, int y)>();

                x = lawn.Width - 1;
                y = lawn.Height - 1;

                // Startposition ist unten links  
                MowLawn(lawn, x, y);
                lawn.Visited = new List<(int x, int y)>();

                x = lawn.Width - 1;
                y = 0;

                // Startposition ist unten links  
                MowLawn(lawn, x, y);
                lawn.Visited = new List<(int x, int y)>();

                x = 0;
                y = 0;

                // Startposition ist unten links  
                MowLawn(lawn, x, y);
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

        static bool IsValidMove(Lawn lawn, int x, int y)
        {
            return x >= 0 && x < lawn.Width && y >= 0 && y < lawn.Height && !lawn.Visited.Contains((x, y)) && (x, y) != lawn.Tree;
        }

        static void MowLawn(Lawn lawn, int x, int y)
        {
            // Prüfen, ob der aktuelle Schritt gültig ist  
            if (!IsValidMove(lawn, x, y)) return;

            // Aktuelle Position als besucht markieren  
            lawn.Visited.Add((x, y));

            

            // Versuche, in alle Richtungen zu mähen  
            if (IsValidMove(lawn, x, y - 1)) // Norden  
            {
                lawn.Path.Add(Direction.North);
                MowLawn(lawn, x, y - 1);
            }
            if (IsValidMove(lawn, x + 1, y)) // Osten  
            {
                lawn.Path.Add(Direction.East);
                MowLawn(lawn, x + 1, y);
            }
            if (IsValidMove(lawn, x, y + 1)) // Süden  
            {
                lawn.Path.Add(Direction.South);
                MowLawn(lawn, x, y + 1);
            }
            if (IsValidMove(lawn, x - 1, y)) // Westen  
            {
                lawn.Path.Add(Direction.North);
                MowLawn(lawn, x - 1, y);
            }

            if (lawn.Visited.Count == (lawn.Width * lawn.Height - 1))
            {
            }

            string path = string.Join("", lawn.Path.Select(d => (char)d));

            // Rückgängig machen (Backtracking)  
            //lawn.Visited.Remove((x, y));
            //if (lawn.Path.Count > 0) 
            //    lawn.Path.RemoveAt(lawn.Path.Count - 1);

        }

    }

    public class Lawn
    {
        public int Width { get; set; }
        public int Height { get; set; }

        // true = tree, false = grass
        public bool[,] Field { get; set; }
        public (int x, int y) Tree { get; set; }

        public List<Direction> Path { get; set; } = new List<Direction>();
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
