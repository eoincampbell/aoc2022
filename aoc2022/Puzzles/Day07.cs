//AOC2022 - Day 07
//Result 1: 1243729
//Result 2: 4443914
namespace aoc2022.Puzzles
{
    internal class Day07 : Puzzle
    {
        public override int Day => 7;
        public override string Name => "No Space Left On Device";
        protected override object RunPart1() => GetSize(_root);
        protected override object RunPart2() => Part2();
        private readonly Dir _root;
        public Day07() : base("Inputs/Day07.txt") =>
            _root = LoadDirectoryStructure();

        private object Part2()
        {
            const long max = 70000000, need = 30000000;
            var shortfall = need - (max - _root.Size());
            var bestDirectory = Find(_root, _root, shortfall);
            return bestDirectory.Size();
        }

        private Dir Find(Dir src, Dir bestmatch, long shortfall)
        {
            var srcSize = src.Size();
            //if this is smaller than current best, but bigger than short fall
            if (srcSize < bestmatch.Size() && srcSize > shortfall)
                bestmatch = src;

            foreach (var d in src.Dirs) //recurse sub directories
                bestmatch = Find(d, bestmatch, shortfall);

            return bestmatch;
        }

        private long GetSize(Dir dir) =>
            ((dir.Size() < 100000) ? dir.Size() : 0) //current if over 100,000
                + dir.Dirs.Select(d => GetSize(d)).Sum(); //+ all subs

        private Dir LoadDirectoryStructure()
        {
            Dir root = new("/", null!), cur = root;
            foreach (var l in PuzzleInput.Skip(1))
            {
                switch (l)
                {
                    case "$ cd ..": 
                        cur = cur.Parent;
                        break;
                    case "$ ls": break;
                    case string x when x.StartsWith("dir "):
                        cur.Dirs.Add(new Dir(l[4..], cur));
                        break;
                    case string x when x.StartsWith("$ cd "):
                        cur = cur.Dirs.Single(d => d.Name == l[5..]);
                        break;
                    default:
                        var f = l.Split(' ');
                        cur.Files.Add(new File(f[1], long.Parse(f[0])));
                        break;
                }
            }
            return root;
        }

        private record File(string Name, long Size);
        private record Dir(string Name, Dir Parent)
        {
            public IList<Dir> Dirs { get; set; } = new List<Dir>();
            public IList<File> Files { get; set; } = new List<File>();
            public long Size() => Files.Select(f => f.Size).Sum() +
                Dirs.Select(d => d.Size()).Sum();
        }
    }
}