//AOC2022 - Day 06
//Result 1: 1707
//Result 2: 3697

namespace aoc2022.Puzzles
{
    internal class Day06 : Puzzle
    {
        public override int Day => 6;
        public override string Name => "";
        protected override object RunPart1() => Calc(4);
        protected override object RunPart2() => Calc(14);
        public Day06() : base("Inputs/Day06.txt") { }

        private object Calc(int len)
        {
            var x = PuzzleInput.First();
            string f(char _, int i) //chunker function
                => x.Substring(i, (i + len > x.Length) ? x.Length - i : len);

            var i = 0;
            foreach (var yy in x.Select(f))
            {
                if (yy.ToCharArray().Distinct().Count() == len)
                    return i + len;
                i++;
            }

            return -1;
        }
    }
}