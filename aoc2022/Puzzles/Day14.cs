//AOC2022 - Day 14
//Result 1: 1298
//Result 2: 25585
namespace aoc2022.Puzzles
{
    using MoreLinq;
    using System.Drawing;

    internal class Day14 : Puzzle
    {
        public override int Day => 14;
        public override string Name => "Regolith Reservoir";
        protected override object RunPart1() => PourSand(true);
        protected override object RunPart2() => PourSand(false);
        private readonly Dictionary<Point, char> _map;
        private readonly int _bottomFloor;
        private const char SAND = '.';
        private const char ROCK = '▓';
        public Day14() : base("Inputs/Day14.txt")
        {
            _map = new Dictionary<Point, char>();
            LoadMap();
            _bottomFloor = _map.Max(kvp => kvp.Key.Y) + 2;
        }

        private void PrintMap()
        {
            int minx = _map.Min(kvp => kvp.Key.X),
                maxx = _map.Max(kvp => kvp.Key.X),
                miny = _map.Min(kvp => kvp.Key.Y),
                maxy = _map.Max(kvp => kvp.Key.Y);

            for(int y = MN(0, miny); y <= maxy; y++)
            {
                for (int x = minx; x <= maxx; x++)
                    Console.Write(_map.ContainsKey(new Point(x, y)) ? _map[new Point(x, y)] : ' ');

                Console.WriteLine();
            }
        }

        private static int MN(int a, int b) => Math.Min(a,b);
        private static int MX(int a, int b) => Math.Max(a, b);
        private void LoadMap() =>
            PuzzleInput
                .ToList()
                .ForEach(l =>
                {
                    l.Split(" -> ")
                    .Select(s => s.Split(','))
                    .Select(p => new Point(int.Parse(p[0]), int.Parse(p[1])))
                    .Pairwise((a, b) => new { S = a, T = b })
                    .ToList()
                    .ForEach(p =>
                    {
                        if (p.S.X == p.T.X)
                            for (var y = MN(p.S.Y, p.T.Y); y <= MX(p.S.Y, p.T.Y); y++)
                                _map.TryAdd(new Point(p.S.X, y), ROCK);

                        if (p.S.Y == p.T.Y)
                            for (var x = MN(p.S.X, p.T.X); x <= MX(p.S.X, p.T.X); x++)
                                _map.TryAdd(new Point(x, p.S.Y), ROCK);
                    });
                });

        private object PourSand(bool part1)
        {
            var bottomY = _map.Max(kvp => kvp.Key.Y);
            var stop = false;
            while (!stop)
            {
                var grain = new Point(500, 0);
                if (_map.ContainsKey(grain)) break; //source blocked
                while (true)
                {
                    var temp = MoveGrain(grain, part1); 
                    if (temp.Y >= bottomY && part1) stop = true;
                    if (temp == grain || stop) break; //sand at rest
                    grain = temp;
                }
            }
            PrintMap();
            return _map.Count(kvp => kvp.Value == SAND);
        }

        private Point MoveGrain(Point curr, bool part1)
        {
            Point dd = new(curr.X, curr.Y + 1),
                dl = new(curr.X - 1, curr.Y + 1),
                dr = new(curr.X + 1, curr.Y + 1);

            if (part1 && !_map.ContainsKey(dd)) return dd;
            if (part1 && !_map.ContainsKey(dl)) return dl;
            if (part1 && !_map.ContainsKey(dr)) return dr;
            if (!_map.ContainsKey(dd) && dd.Y < _bottomFloor) return dd;
            if (!_map.ContainsKey(dl) && dl.Y < _bottomFloor) return dl;
            if (!_map.ContainsKey(dr) && dr.Y < _bottomFloor) return dr;

            _map.TryAdd(curr, SAND);
            return curr;
        }
    }
}