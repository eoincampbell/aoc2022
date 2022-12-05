//AOC2022 - Day 01
//Result 1: 69795
//Result 2: 208437

namespace aoc2022.Puzzles
{
    internal class Day01 : Puzzle
    {
        public override int Day => 1;
        public override string Name => "Calorie Counting";
        protected override object RunPart1() => Part1();
        protected override object RunPart2() => Part2();

        private readonly List<int> _elves;

        public Day01() : base("Inputs/Day01.txt") =>
            _elves = new List<int>() { 0 };

        private object Part1()
        {
            foreach (var row in PuzzleInput)
            {
                if (string.IsNullOrEmpty(row))
                    _elves.Add(0);
                else
                    _elves[^1] += int.Parse(row);
            }

            return _elves.Max();
        }

        private object Part2() =>
            _elves.OrderByDescending(x => x).Take(3).Sum();
    }
}
