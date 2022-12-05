//AOC2022 - Day 05
//Result 1: CVCWCRTVQ
//Result 2: CNSCZWLVT

namespace aoc2022.Puzzles
{
    internal class Day05 : Puzzle
    {
        public override int Day => 5;
        public override string Name => "Supply Stacks";
        protected override object RunPart1() => RunCrane(false);
        protected override object RunPart2() => RunCrane(true);
        public Day05() : base("Inputs/Day05.txt") { }

        private object RunCrane(bool isCrateMover9001)
        {
            var stacks = Enumerable.Range(0,9).Select(_ => new Stack<char>()).ToList();
            LoadStacks(PuzzleInput.Take(8).ToList(), stacks);
            RunCrateMover(PuzzleInput.Skip(10), stacks, isCrateMover9001);
            return GetResult(stacks);
        }

        private void RunCrateMover(IEnumerable<string> instructions, List<Stack<char>> stacks, bool isCrateMover9001)
        {
            foreach (var (count, src, dest) in instructions.Select(Parse))
            {
                if (isCrateMover9001)
                {
                    var crates = new List<char>();
                    for (var i = 0; i < count; i++)      //pop all crates first
                        crates.Add(stacks[src].Pop());
                    for (var i = count - 1; i >= 0; i--) //push them in reverse order
                        stacks[dest].Push(crates[i]);
                }
                else
                {
                    for (int i = 0; i < count; i++)     //pop & push crates 1 at a time
                        stacks[dest].Push(stacks[src].Pop());
                }
            }
        }
        private static string GetResult(List<Stack<char>> stacks) =>
            string.Join("", stacks.Select(i => i.Peek()));

        private static (int count, int src, int dest) Parse(string instruction)
        {
            var x = instruction
                .Replace("move ", "").Replace(" from ", ",")
                .Replace(" to ", ",").Split(',')
                .Select(int.Parse).ToArray();
            return (x[0], --x[1], --x[2]);
        }

        private static void LoadStacks(List<string> rows, List<Stack<char>> stacks)
        {
            for (var y = 7; y >= 0; y--) //row 7 up to 0                
                for (int x = 1, s = 0;x <= rows[y].Length; x += 4, s++) //stacks 0..8, crates n+4
                    if (rows[y][x] != ' ')
                        stacks[s].Push(rows[y][x]);
        }
    }
}