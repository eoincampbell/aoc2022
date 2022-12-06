//AOC2022 - Day 06
//Result 1: 1707
//Result 2: 3697
using MoreLinq;
namespace aoc2022.Puzzles
{
    internal class Day06 : Puzzle
    {
        public override int Day => 6;
        public override string Name => "Tuning Trouble";
        protected override object RunPart1() => Calc(4);
        protected override object RunPart2() => Calc(14);
        public Day06() : base("Inputs/Day06.txt") { }

        private object Calc(int len)
        {
            var i = 0;
            foreach (var yy in PuzzleInput.First().Window(len).Select(s => s.Distinct().Count()))
            {
                if (yy == len) break;
                i++;
            }

            return i+len;
        }
    }
}