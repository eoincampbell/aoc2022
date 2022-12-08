//AOC2022 - Day 08
//Result 1: 1763
//Result 2: 671160
namespace aoc2022.Puzzles
{
    internal class Day08 : Puzzle
    {
        public override int Day => 8;
        public override string Name => "";
        protected override object RunPart1() => _vis.Cast<int>().Sum();
        protected override object RunPart2() => _dist.Cast<int>().Max();

        public Day08() : base("Inputs/Day08.txt")
        {
            _map = PuzzleInput
                .Select(y => y.Select(x => int.Parse(x.ToString())))
                .To2DArray();
            _vis = new int[MapHeight + 1, MapWidth + 1];
            _dist = new int[MapHeight + 1, MapWidth + 1];

            for (int y = 0; y <= MapHeight; y++)
                for (int x = 0; x <= MapWidth; x++)
                    _vis[y, x] = _dist[y,x] = 0;

            Calc();
        }

        private readonly int[,] _vis;
        private readonly int[,] _dist;
        private readonly int[,] _map;
        private int MapWidth => _map.GetUpperBound(0);
        private int MapHeight => _map.GetUpperBound(1);

        private void Calc()
        {
            for(int y = 0; y <= MapHeight; y++)
            {
                for(int x = 0; x <= MapWidth; x++)
                {
                    bool nt = false, et = false, wt = false, st = false;
                    int nc = 0, ec = 0, sc = 0, wc = 0;
                    //north
                    for (int yy = y - 1; yy >= 0; yy--)
                    {
                        nc++;
                        if(_map[yy,x] >= _map[y, x])
                        {
                            nt = true;
                            break;
                        }
                    }
                    //east
                    for (int xx = x - 1; xx >= 0; xx--)
                    {
                        ec++;
                        if (_map[y, xx] >= _map[y, x])
                        {
                            et = true;
                            break;
                        }
                    }
                    //south
                    for (int yy = y + 1; yy <= MapHeight; yy++)
                    {
                        sc++;
                        if (_map[yy, x] >= _map[y, x])
                        {
                            st = true;
                            break;
                        }
                    }
                    //west
                    for (int xx = x + 1; xx <= MapWidth; xx++)
                    {
                        wc++;
                        if (_map[y, xx] >= _map[y, x])
                        {
                            wt = true;
                            break;
                        }
                    }

                    if(!nt || !st || !et || !wt)
                        _vis[y, x] = 1;

                    _dist[y, x] = nc * sc * ec * wc;                }
            }
        }
    }
}