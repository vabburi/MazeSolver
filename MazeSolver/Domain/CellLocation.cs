namespace MazeSolver.Domain
{
    public class CellLocation
    {
        public int x;

        public int y;

        public CellLocation(int a, int b)
        {
            this.x = a;
            this.y = b;
        }

        public override string ToString()
        {
            return x + "-" + y;
        }
    }
}