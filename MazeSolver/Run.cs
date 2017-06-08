using System;
using System.Collections.Generic;
using System.IO;
using MazeSolver.Domain;

namespace MazeSolver
{
    public class Run
    {
        public static int MaxLives = 3;

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("[ERROR]: Please provide input file as command line argument!");
                return;
            }

            var lines = File.ReadAllLines(args[0]);

            foreach (var line in lines)
            {
                if (!String.IsNullOrEmpty(line))
                {
                    //Extract+Build Maze
                    var build = new BuildMaze();
                    var maze = build.Run(line);

                    //Find start and end cells
                    int start = -1, end = -1;
                    foreach (var node in maze.Nodes)
                    {
                        if (node.IsStart()) start = node.RawIndex;
                        if (node.IsEnd()) end = node.RawIndex;
                    }

                    //search the maze for a path
                    var pathFinder = new PathFinder();
                    var shortestPath = pathFinder.Run(maze, start, end);

                    //Print Results
                    PrintResults(maze, shortestPath);
                    Console.WriteLine();
                }
            }

        }

        private static void PrintResults(Maze maze, int[] shortestPath)
        {
            var finalPath = new List<string>();

            for (var i = 0; i < shortestPath.Length - 1; i++)
            {
                var curr = maze.Nodes[shortestPath[i]].Location;
                var next = maze.Nodes[shortestPath[i + 1]].Location;

                var xDiff = curr.x - next.x;
                var yDiff = curr.y - next.y;

                if (xDiff == 0 && yDiff > 0)
                {
                    finalPath.Add("'left'");
                }
                if (xDiff == 0 && yDiff < 0)
                {
                    finalPath.Add("'right'");
                }
                if (xDiff > 0 && yDiff == 0)
                {
                    finalPath.Add("'up'");
                }
                if (xDiff < 0 && yDiff == 0)
                {
                    finalPath.Add("'down'");
                }
            }

            Console.WriteLine("[" + String.Join(", ", finalPath) + "]");
        }
    }
}