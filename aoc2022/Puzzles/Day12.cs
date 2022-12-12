//AOC2022 - Day 12
//Result 1: 330
//Result 2: 321
namespace aoc2022.Puzzles
{
    internal class Day12 : Puzzle
    {
        public override int Day => 12;
        public override string Name => "";
        protected override object RunPart1() => Hike();
        protected override object RunPart2() => Hike('a');
        public Day12() : base("Inputs/Day12.txt")
        {
            _map = PuzzleInput.To2DArray();
            _pth = new int[MapHeight+1, MapWidth+1];
            SetupMap();
        }
        private readonly char[,] _map;
        private readonly int[,] _pth;
        private int MapWidth => _map.GetUpperBound(1);
        private int MapHeight => _map.GetUpperBound(0);
        private int EndX;
        private int EndY;
        private const char Start = '`';
        private const char End = '{';
        private void SetupMap()
        {
            //hack to do char-math by putting start and end 1 char before
            //and after 'a' and 'z' respectively.
            for (var y = 0; y <= MapHeight; y++)
                for (var x = 0; x <= MapWidth; x++)
                    if (_map[y, x] == 'S')
                        _map[y, x] = Start;
                    else if (_map[y, x] == 'E')
                    {
                        _map[y, x] = End;
                        EndY = y; EndX = x;
                    }
        }

        private void ResetShortestPath()
        {
            for (var y = 0; y <= _map.GetUpperBound(0); y++)
                for (var x = 0; x <= _map.GetUpperBound(1); x++)
                    _pth[y, x] = int.MaxValue;
        }

        private object Hike(char start = Start)
        {
            ResetShortestPath();
            for (var y = 0; y <= MapHeight; y++)
                for (var x = 0; x <= MapWidth; x++)
                    if (_map[y, x] == start)
                        Traverse(x, y, 0);

            return _pth[EndY, EndX];
        }

        private bool Inbounds(int x, int y) =>
            x >= 0 && y >= 0 && x <= MapWidth && y <= MapHeight;

        private bool Valid(int cx, int cy, int tx, int ty, int count) =>
            Inbounds(tx, ty)                    //target must be inbounds 
            && _map[ty, tx] - _map[cy, cx] <= 1 //target must be at most 1 higher than current
            && _pth[ty, tx] > count;       //target isn't further than a prev. visit

        private void Traverse(int x, int y, int count)
        {
            _pth[y, x] = (_pth[y, x] > count) ? count : _pth[y, x];
            count++;
            if (_map[y, x] == End) return;
            if (Valid(x, y, x, y - 1, count)) Traverse(x, y - 1, count);//north
            if (Valid(x, y, x, y + 1, count)) Traverse(x, y + 1, count);//south
            if (Valid(x, y, x - 1, y, count)) Traverse(x - 1, y, count);//west
            if (Valid(x, y, x + 1, y, count)) Traverse(x + 1, y, count);//east
        }
    }
}