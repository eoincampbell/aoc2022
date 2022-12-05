//AOC2022 - Day 05
//Result 1: CVCWCRTVQ
//Result 2: CNSCZWLVT

namespace aoc2022.Puzzles
{
    internal class Day05 : Puzzle
    {
        public override int Day => 5;
        public override string Name => "Supply Stacks";
        protected override object RunPart1() => RunCrane(true);
        protected override object RunPart2() => RunCrane(false);
        public Day05() : base("Inputs/Day05.txt") { }

        private object RunCrane( bool part1)
        {
            var stacks = CreateYard().ToList();
            LoadStacks(PuzzleInput.Take(8).ToList(), stacks);
            if (part1)
            {
                RunCrateMover9000(PuzzleInput.Skip(10).ToList(), stacks);
            }
            else
            {
                RunCrateMover9001(PuzzleInput.Skip(10).ToList(), stacks);
            }
            return GetResult(stacks);
        }

        private string GetResult(List<Stack<char>> stacks)
        {
            var r = string.Empty;
            for (var i = 0; i < stacks.Count; i++)
            {
                r += stacks[i].Pop();
            }

            return r;
        }

        private void RunCrateMover9000(List<string> instructions, List<Stack<char>> stacks)
        {
            foreach (var instruction in instructions)
            {
                var (count, src, dest) = Parse(instruction);
                for (int i = 0; i < count; i++)
                {
                    var crate = stacks[src].Pop();
                    stacks[dest].Push(crate);
                }
            }
        }

        private void RunCrateMover9001(List<string> instructions, List<Stack<char>> stacks)
        {
            foreach (var instruction in instructions)
            {
                var (count, src, dest) = Parse(instruction);
                var crates = new List<char>();
                for (var i = 0; i < count; i++)
                {
                    crates.Add(stacks[src].Pop());
                }
                for (var i = count - 1; i >= 0; i--)
                {
                    stacks[dest].Push(crates[i]);
                }
            }
        }

        private (int count, int src, int dest) Parse(string instruction)
        {
            var x = instruction
                .Replace("move ", "")
                .Replace(" from ", ",")
                .Replace(" to ", ",")
                .Split(',')
                .Select(int.Parse)
                .ToArray();

            return (x[0], --x[1], --x[2]);
        }

        private static IEnumerable<Stack<char>> CreateYard()
        {
            for (var i = 1; i <= 9; i++)
            {
                yield return new Stack<char>();
            }
        }

        private static void LoadStacks(List<string> rows, List<Stack<char>> stacks)
        {
            for (var y = 7; y >= 0; y--) //bottom to top through rows
            {
                //stacks go from 0..8
                //crate letters go from index 1..N in 4's
                for (int x = 1, s = 0;x <= rows[y].Length; x += 4, s++)
                {
                    if (rows[y][x] != ' ')
                    {
                        stacks[s].Push(rows[y][x]);
                    }
                }
            }
        }
    }
}