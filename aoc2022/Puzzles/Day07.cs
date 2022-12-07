//AOC2022 - Day 07
//Result 1: 1243729
//Result 2: 4443914
namespace aoc2022.Puzzles
{
    internal class Day07 : Puzzle
    {
        public override int Day => 7;
        public override string Name => "No Space Left On Device";
        protected override object RunPart1() => Part1();
        protected override object RunPart2() => Part2();
        private readonly Dir _root;
        public Day07() : base("Inputs/Day07.txt") =>
            _root = LoadDirectoryStructure();

        private object Part1() => GetSize(_root);

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

            //recurse sub directories
            foreach (var d in src.Directories)
                bestmatch = Find(d, bestmatch, shortfall);

            return bestmatch;
        }

        private long GetSize(Dir dir) =>
            ((dir.Size() < 100000) ? dir.Size() : 0) //current if over 100,000
                + dir.Directories.Select(d => GetSize(d)).Sum(); //+ all subs

        private Dir LoadDirectoryStructure()
        {
            var root = new Dir("/", null);
            var cur = root;

            foreach (var l in PuzzleInput.Skip(1))
            {
                if (l == "$ ls") { }//NO OP
                else if (l == "$ cd ..")
                {
                    cur = cur.Parent;
                }
                else if (l.StartsWith("$ cd "))
                {
                    cur = cur.Directories.Single(d => d.Name == l[5..]);
                }
                else if (l.StartsWith("dir "))
                {
                    cur.Directories.Add(new Dir(l[4..], cur));
                }
                else //must be a file
                {
                    var f = l.Split(' ');
                    cur.Files.Add(new File(f[1], long.Parse(f[0])));
                }
            }
            return root;
        }

        private record File(string Name, long Size);

        private class Dir
        {
            public string Name { get; set; }
            public Dir Parent { get; set; }
            public IList<Dir> Directories { get; set; }
            public IList<File> Files { get; set; }

            public Dir(string name, Dir parent)
            {
                Name = name;
                Parent = parent;
                Directories = new List<Dir>();
                Files = new List<File>();
            }

            public long Size() =>
                Files.Select(f => f.Size).Sum() +
                Directories.Select(d => d.Size()).Sum();
        }
    }
}