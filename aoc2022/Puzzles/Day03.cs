//AOC2022 - Day 03
//Result 1: 7980
//Result 2: 2881

namespace aoc2022.Puzzles
{
    internal class Day03 : Puzzle
    {
        public override int Day => 3;
        public override string Name => "Rucksack Reorganization";
        protected override object RunPart1() => Part1();
        protected override object RunPart2() => Part2();

        public Day03() : base("Inputs/Day03.txt") { }

        private object Part1()
        {
            return PuzzleInput
                .Select(r => r[0..(r.Length / 2)]           //Range [0..half]
                    .Intersect(r[(r.Length / 2)..r.Length]) //Range[half..end]
                    .First()
                    .PriorityCode())
                .Sum();
        }

        private object Part2()
        {
            return PuzzleInput.Chunk(3)
                .Select(r => r[0]
                    .Intersect(r[1])
                    .Intersect(r[2])
                    .First()
                    .PriorityCode())
                .Sum();
        }
    }

    internal static class Day03Extensions
    {
        //A = char(65) => 27 so subtract 38
        //a = char(97) => 1 so subtract 96
        public static int PriorityCode(this char x) =>
            (x >= 'a' && x <= 'z') ? x - 96 : x - 38;
    }
}