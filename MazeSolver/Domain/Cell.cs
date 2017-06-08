namespace MazeSolver.Domain
{
    public class Cell
    {
        public int RawData;

        public int RawIndex;

        public char Data;

        public int Cost;

        public CellLocation Location;

        public bool IsStart()
        {
            if (Data == 'S') return true;

            return false;
        }

        public bool IsEnd()
        {
            if (Data == 'E') return true;

            return false;
        }

        public bool HasMine()
        {
            if (Data == '*') return true;

            return false;
        }

    }
}