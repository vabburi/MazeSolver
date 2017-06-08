using System;
using System.Linq;
using MazeSolver.Domain;

namespace MazeSolver
{
    public class BuildMaze
    {
        public Maze Run(string line)
        {
            var sb = line.Split('-');
            var height = Convert.ToInt32(sb[0].TrimStart('(').Split(',')[0]);
            var width = Convert.ToInt32(sb[0].TrimEnd(')').Split(',')[1]);

            int[] mazeStructure = sb[1].TrimStart('[').TrimEnd(']').Split(',').Select(n => Convert.ToInt32((string)n)).ToArray();
            int msIndex = 0;

            //var featureResult = new int[7];

            var maze = new Maze(height , width);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    //var frCount = 0;
                    var cell = new Cell()
                    {
                        Location = new CellLocation(i, j),
                        RawData = mazeStructure[msIndex],
                        RawIndex = msIndex
                    };

                    foreach (var enumValue in Enum.GetValues(typeof(FeatureCodesEnum)))
                    {
                        var featureResult = mazeStructure[msIndex] & Convert.ToInt32(enumValue);

                        if (featureResult != 0)
                        {
                            switch ((FeatureCodesEnum) featureResult)
                            {
                                case FeatureCodesEnum.START:
                                    cell.Data = 'S';
                                    cell.Cost = 1;
                                    break;
                                case FeatureCodesEnum.END:
                                    cell.Data = 'E';
                                    cell.Cost = 1;
                                    break;
                                case FeatureCodesEnum.MINE:
                                    cell.Data = '*';
                                    cell.Cost = 1;
                                    break;
                                case FeatureCodesEnum.LEFT:
                                    if (cell.Location.y != 0)
                                    {
                                        maze.CellMatrix[msIndex].Add(maze.FindCell(cell.Location.x,cell.Location.y-1));
                                    }
                                    break;
                                case FeatureCodesEnum.RIGHT:
                                    if (cell.Location.y <width-1)
                                    {
                                        maze.CellMatrix[msIndex].Add(maze.FindCell(cell.Location.x, cell.Location.y+1));
                                    }
                                    break;
                                case FeatureCodesEnum.DOWN:
                                    if (maze.Nodes[msIndex].Location.x <height-1)
                                    {
                                        maze.CellMatrix[msIndex].Add(maze.FindCell(cell.Location.x+1, cell.Location.y));
                                    }
                                    break;
                                case FeatureCodesEnum.UP:
                                    if (maze.Nodes[msIndex].Location.x !=0)
                                    {
                                        maze.CellMatrix[msIndex].Add(maze.FindCell(cell.Location.x-1, cell.Location.y));
                                    }
                                    break;
                            }
                        }

                        //frCount++;
                    }

                    maze.Nodes[msIndex] = cell;
                    msIndex++;

                }
            }

            return maze;
        }
    }
}