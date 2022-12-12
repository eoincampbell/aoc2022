//AOC2022 - Day 12
//Result 1: 330
//Result 2: 321
namespace aoc2022.Puzzles
{
    internal class Day12 : Puzzle
    {
        public override int Day => 12;
        public override string Name => "";
        protected override object RunPart1() => Hike('`');
        protected override object RunPart2() => Hike('a');
        public Day12() : base("Inputs/Day12.txt")
        {
            _map = PuzzleInput.To2DArray();
            _shortest = new int[_map.GetUpperBound(0)+1, _map.GetUpperBound(1)+1];
            SetupMap();
        }

        private readonly char[,] _map;
        private readonly int[,] _shortest;
        private int MapWidth => _map.GetUpperBound(1);
        private int MapHeight => _map.GetUpperBound(0);

        private void SetupMap()
        {
            //hack to do char-math by putting start and end 1 char before
            //and after 'a' and 'z' respectively.
            for (int y = 0; y <= MapHeight; y++)
                for (int x = 0; x <= MapWidth; x++)
                    if (_map[y, x] == 'S')
                        _map[y, x] = '`'; 
                    else if (_map[y, x] == 'E')
                        _map[y, x] = '{'; 
        }

        private void ResetShortest()
        {
            for (int i = 0; i <= _map.GetUpperBound(0); i++)
                for (int j = 0; j <= _map.GetUpperBound(1); j++)
                    _shortest[i, j] = int.MaxValue;
        }

        private object Hike(char start)
        {
            ResetShortest();
            for (int y = 0; y <= MapHeight; y++)
                for (int x = 0; x <= MapWidth; x++)
                    if (_map[y, x] == start)
                        Traverse(x, y, 0);

            for (int y = 0; y <= MapHeight; y++)
                for (int x = 0; x <= MapWidth; x++)
                    if (_map[y, x] == '{')
                        return _shortest[y, x];

            return -1;
        }

        private bool NextStepInbounds(int x, int y) =>
            x >= 0 && y >= 0 && x <= MapWidth && y <= MapHeight;

        private bool NextStepValid(int cx, int cy, int tx, int ty, int count) =>
            NextStepInbounds(tx, ty)                    //target must be inbounds 
            && _map[ty, tx] - _map[cy, cx] <= 1 //target must be at most 1 higher than current
            && _shortest[ty, tx] > count;       //target isn't further than a prev. visit


        private void Traverse(int x, int y, int count)
        {
            _shortest[y, x] = (_shortest[y, x] > count) ? count : _shortest[y, x];
            count++;
            if (_map[y, x] == '{') return;

            if (NextStepValid(x, y, x, y + 1, count)) //down
                Traverse(x, y + 1, count);

            if (NextStepValid(x, y, x - 1, y, count)) //left
                Traverse(x - 1, y, count);

            if (NextStepValid(x, y, x + 1, y, count)) //right
                Traverse(x + 1, y, count);

            if (NextStepValid(x, y, x, y - 1, count)) //up
                Traverse(x, y - 1, count);
        }
    }
}