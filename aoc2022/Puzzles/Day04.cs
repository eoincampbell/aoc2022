namespace aoc2022.Puzzles
{
    internal class Day04 : Puzzle
    {
        public override int Day => 4;
        public override string Name => "Camp Cleanup";
        protected override object RunPart1() => _countA;
        protected override object RunPart2() => _countB;
        private int _countA;
        private int _countB;
        public Day04() : base("Inputs/Day04.txt") => Calc();
        
        private void Calc()
        {
            foreach (var row in PuzzleInput)
            {
                var s = row.Split(',');
                var a = s[0].Split('-').Select(int.Parse).ToArray();
                var b = s[1].Split('-').Select(int.Parse).ToArray();

                if ((a[0] <= b[0] && a[1] >= b[1]) || //a surrounds b
                    (a[0] >= b[0] && a[1] <= b[1]))   //b surrounds a
                    _countA++;

                if(!(a[1] < b[0] || b[1] < a[0] )) //not ( a completely precedes b
                    _countB++;                     //    ||b completely precedes a)
            }
        }
    }
}