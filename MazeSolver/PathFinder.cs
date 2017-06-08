using System;
using System.Collections.Generic;
using System.Linq;
using MazeSolver.Domain;
using MazeSolver.Lib;

namespace MazeSolver
{
    public class PathFinder
    {
        public const int MaxLives = 3;

        public int[] Run(Maze maze, int start, int end)
        {
            var cameFrom = FindPath(maze, start, end);

            // Now that we found the path, let's trace our steps back to find the path.
            List<int> finalPath = new List<int>();
            var currNode = end;
            while (cameFrom[currNode] >= 0 && cameFrom[currNode] < Int32.MaxValue)
            {
                finalPath.Add(currNode);
                currNode = cameFrom[currNode];
            }
            finalPath.Add(start);

            //get correct order - from start to end
            finalPath.Reverse();

            return finalPath.ToArray();
        }

        //This abstracts away the implementation detail of the algorithm used.
        protected int[] FindPath(Maze maze, int start, int end, bool useAStar = true)
        {
            if (useAStar)
            {
                return AStar(maze, start, end);
            }
            
            return BFS(maze, start, end);
            
        }

        #region Sample Implementations

        private int[] AStar(Maze maze, int start, int end)
        {
            var frontier = new PriorityQueue<int>();
            frontier.Enqueue(start, 0);

            var cameFrom = new int[maze.Nodes.Count];
            var costSoFar = new Dictionary<int, double>();

            for (int i = 0; i < cameFrom.Length; i++)
            {
                cameFrom[i] = Int32.MaxValue;
            }

            cameFrom[start] = -1;
            costSoFar[start] = maze.Nodes[start].Cost;

            int lives_remaining = MaxLives;

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                // found the destination
                if (current == end)
                    break;

                if (maze.Nodes[current].HasMine())
                {
                    maze.Nodes[current].Cost *= 10;
                }

                foreach (var next in maze.CellMatrix[current])
                {
                    double newCost = costSoFar[current] + maze.Nodes[next].Cost;
                    if (!costSoFar.ContainsKey(next)
                        || newCost < costSoFar[next])
                    {
                        costSoFar[next] = newCost;
                        double priority = newCost + Heuristic(maze.Nodes[next].Location, maze.Nodes[end].Location);
                        frontier.Enqueue(next, priority);
                        cameFrom[next] = current;
                    }
                }
            }

            return cameFrom;
        }

        private double Heuristic(CellLocation a, CellLocation b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }

        private int[] BFS(Maze maze, int start, int end)
        {
            var frontier = new Queue<int>();
            frontier.Enqueue(start);

            var cameFrom = new int[maze.Nodes.Count];
            for (int i = 0; i < cameFrom.Length; i++)
            {
                cameFrom[i] = Int32.MaxValue;
            }

            cameFrom[start] = -1;
            int lives_remaining = MaxLives;

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                // found the destination
                if (current == end)
                    break;

                foreach (var next in maze.CellMatrix[current])
                {
                    if (cameFrom[next] == Int32.MaxValue)
                    {
                        if (lives_remaining > 1)
                        {
                            frontier.Enqueue(next);
                            cameFrom[next] = current;                            
                        }
                        else
                        {
                            if (!maze.Nodes[next].HasMine())
                            {
                                frontier.Enqueue(next);
                                cameFrom[next] = current;                                                            
                            }
                        }
                    }
                }
            }

            return cameFrom;
        }

        #endregion

    }
}