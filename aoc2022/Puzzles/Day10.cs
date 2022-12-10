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
        private const int Row = 6;
        private const int Col = 40;
        private object Part1()
        {
            int cyc = 0, reg = 1;
            var totals = new List<int>();
            var arr = new char[Row * Col];

            foreach (var i in PuzzleInput.Select(s => s.Split(' ')))
            {
                Cycle(ref cyc, ref reg, ref arr, ref totals); //NOOP is 1 cycle
                if (i[0] != "addx") continue;
                Cycle(ref cyc, ref reg, ref arr, ref totals); //ADDX is 2nd cycle + reg-add
                reg += int.Parse(i[1]);
            }

            for(int i = 0; i < Row * Col; i++)
                Console.Write(arr[i] + (((i + 1) % Col == 0) ? Environment.NewLine : ""));

            return totals.Sum();
        }

        private static void Cycle (ref int cyc, ref int reg, ref char [] arr, ref List<int> tot)
        {
            Draw(cyc++, reg, ref arr); //Draw & then increment cycle
            Check(cyc, reg, tot);
        }

        private static void Draw(int cyc, int reg, ref char[] arr)
        {
            var mod = cyc % Col;
            arr[cyc] = (mod == reg || mod == reg - 1 || mod == reg + 1) ? '#' : ' ';
        }

        private static void Check(int cyc, int reg, List<int> tot)
        {
            if (cyc >= 20 && cyc <= (Row * Col) - 20 && (cyc - 20) % Col == 0)
                tot.Add(cyc * reg);
        }
    }
}