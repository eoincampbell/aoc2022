//AOC2022 - Day 14
//Result 1: 1298
//Result 2: 25585
namespace aoc2022.Puzzles
{
    using MoreLinq;
    internal class Day14 : Puzzle
    {
        public override int Day => 14;
        public override string Name => "Regolith Reservoir";
        protected override object RunPart1() => PourSand(true);
        protected override object RunPart2() => PourSand(false);
        private readonly Dictionary<P, char> _map;
        private readonly int _bottomFloor;
        private readonly int _bottomRock;
        private const char SAND = '.';
        private const char ROCK = '▓';
        private static int MN(int a, int b) => Math.Min(a, b);
        private static int MX(int a, int b) => Math.Max(a, b);
        public record P (int X, int Y);
        public Day14() : base("Inputs/Day14.txt")
        {
            _map = new Dictionary<P, char>();
            LoadMap();
            _bottomRock = _map.Max(k => k.Key.Y);
            _bottomFloor = _bottomRock + 2;
        }

        private void PrintMap()
        {
            int minx = _map.Min(k => k.Key.X),
                maxx = _map.Max(k => k.Key.X),
                miny = _map.Min(k => k.Key.Y);
            for (int y = MN(0, miny); y <= _bottomFloor; y++)
            {
                for (int x = minx; x <= maxx; x++)
                    Console.Write(_map.ContainsKey(new P(x, y)) ? _map[new P(x, y)] : ' ');

                Console.WriteLine();
            }
        }

        private void LoadMap() =>
            PuzzleInput
                .ToList().ForEach(l =>
                {
                    l.Split(" -> ")
                    .Select(s => s.Split(',').Select(int.Parse).ToArray())
                    .Pairwise((a, b) => new { S = new P(a[0], a[1]), T = new P(b[0], b[1]) })
                    .ToList().ForEach(f =>
                    {
                        if (f.S.X == f.T.X) //vertical rocks
                        {
                            for (var y = MN(f.S.Y, f.T.Y); y <= MX(f.S.Y, f.T.Y); y++)
                                _map.TryAdd(new P(f.S.X, y), ROCK);
                        }

                        if (f.S.Y == f.T.Y) //horizontal rocks
                        {
                            for (var x = MN(f.S.X, f.T.X); x <= MX(f.S.X, f.T.X); x++)
                                _map.TryAdd(new P(x, f.S.Y), ROCK);
                        }
                    });
                });

        private object PourSand(bool part1, bool stop = false)
        {
            while (!stop)
            {
                var grain = new P(500, 0);
                if (_map.ContainsKey(grain)) break; //source blocked
                while (true)
                {
                    var temp = MoveGrain(grain, part1); //try move down
                    if (temp.Y >= _bottomRock && part1) stop = true;
                    if (temp == grain || stop) break; //sand at rest on rock or bottom
                    grain = temp;
                }
            }
            //PrintMap();
            return _map.Count(kvp => kvp.Value == SAND);
        }

        private P MoveGrain(P curr, bool part1)
        {
            P dd = new(curr.X, curr.Y + 1),
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