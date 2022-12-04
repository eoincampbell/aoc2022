namespace aoc2022.Puzzles
{
    internal class Day04 : Puzzle
    {
        public override int Day => 4;
        public override string Name => "Camp Cleanup";
        protected override object RunPart1() => Calc(true);
        protected override object RunPart2() => Calc(false);

        public Day04() : base("Inputs/Day04.txt") { }

        private object Calc(bool part1)
        {
            int count = 0;
            foreach (var row in PuzzleInput)
            {
                var s = row.Split(',');

                var a = s[0].Split('-').Select(int.Parse).ToArray();
                var b = s[1].Split('-').Select(int.Parse).ToArray();

                if (part1 && (
                    (a[0] <= b[0] && a[1] >= b[1]) || //a surrounds b
                    (a[0] >= b[0] && a[1] <= b[1])    //b surrounds a
                    ))
                {
                    count++;
                }
                else if (!part1 && (
                    (a[0] <= b[0] && a[1] >= b[0]) || //a surrounds b[0]
                    (a[0] <= b[1] && a[1] >= b[1]) || //a surrounds b[1]
                    (b[0] <= a[0] && b[1] >= a[0]) || //b surrounds a[0]
                    (b[0] <= a[1] && b[1] >= a[1])    //b surrounds a[1]
                    ))
                {
                    count++;
                }
            }
            return count;
        }
    }
}