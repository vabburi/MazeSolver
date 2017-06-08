using System;
using System.Collections.Generic;

namespace MazeSolver.Domain
{
    public class Maze
    {
        public List<Cell> Nodes;

        public Dictionary<int, List<int>> CellMatrix;

        public Maze(int height, int width)
        {
            Nodes = new List<Cell>();

            CellMatrix = new Dictionary<int, List<int>>();

            int k = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Nodes.Add(new Cell() { RawIndex = k, Location = new CellLocation(i,j)});
                    CellMatrix.Add(k, new List<int>());
                    k++;
                }
            }
        }

        public int FindCell(int x,int y)
        {
            foreach (var node in Nodes)
            {
                if (node.Location.x == x && node.Location.y == y)
                    return node.RawIndex;
            }

            return -1;
        }

        public void Display()
        {
            foreach (var node in Nodes)
            {
                Console.WriteLine("Node["+node.RawIndex+"]="+node.Location);
                Console.WriteLine("Neighbors:");
                var neighbors = CellMatrix[node.RawIndex];

                foreach (var neighbor in neighbors)
                {
                    Console.WriteLine(Nodes[neighbor].Location);
                }

                Console.WriteLine();
            }
        }

    }
}