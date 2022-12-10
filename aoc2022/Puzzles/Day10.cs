//AOC2022 - Day 10
//Result 1: 11820
//Result 2: EPJBRKAH
namespace aoc2022.Puzzles
{
    internal class Day10 : Puzzle
    {
        public override int Day => 10;
        public override string Name => "Cathode-Ray Tube";
        protected override object RunPart1() => Part1();
        protected override object RunPart2() => "NO OP";
        public Day10() : base("Inputs/Day10.txt") { }
        private const int Col = 40;
        private object Part1()
        {
            int cyc = 0, reg = 1;
            var totals = new List<int>();
            foreach (var i in PuzzleInput.Select(s => s.Split(' ')))
            {
                Cycle(ref cyc, reg, totals); //NOOP is 1 cycle
                if (i[0] != "addx") continue;
                Cycle(ref cyc, reg, totals); //ADDX is 2nd cycle + reg-add
                reg += int.Parse(i[1]);
            }
            return totals.Sum();
        }

        private static void Cycle(ref int cyc, int reg, List<int> tot)
        {
            Console.Write((cyc % Col == reg - 1 || cyc % Col == reg || cyc % Col == reg + 1) ? '#' : ' ');
            if (cyc % Col == Col - 1) Console.WriteLine();
            cyc++;
            if (cyc >= 20 && cyc <= 220 && (cyc - 20) % Col == 0)
                tot.Add(cyc * reg);
        }
    }
}