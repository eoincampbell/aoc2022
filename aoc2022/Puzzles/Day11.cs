//AOC2022 - Day 11
//Result 1: 66802
//Result 2: 21800916620
namespace aoc2022.Puzzles
{
    using C = System.Console;
    internal class Day11 : Puzzle
    {
        public override int Day => 11;
        public override string Name => "Monkey in the Middle";
        protected override object RunPart1() => RunPuzzle(20, true);
        protected override object RunPart2() => RunPuzzle(10000, false);
        public Day11() : base("Inputs/Day11.txt") { }

        private object RunPuzzle(int rounds, bool isPart1)
        {
            var monkeys = ParseInput(PuzzleInput.ToArray()).ToList();
            var reducer = monkeys.Product(m => m.Test); //prime product
            for(int rnd = 1; rnd <= rounds; rnd++)
            {
                foreach(var m in monkeys)
                {
                    while(m.Items.Any())
                    {
                        var item = m.Items[0] % reducer;
                        //magic trick because all the tests are the first 8 primes
                        //so you can try MOD by the product of all of those primes
                        m.Items.RemoveAt(0);
                        var tId = (m.Inspect(ref item, isPart1))
                            ? m.TrueTarget 
                            : m.FalseTarget;
                        monkeys.First(m => m.Name == tId).Items.Add(item);
                    }
                }
                //monkeys.PrintRound(rnd);
            }
            //monkeys.PrintEnd(rounds);
            var results = monkeys
                .OrderByDescending(m => m.Inspections)
                .Take(2)
                .ToArray();
            return results[0].Inspections * results[1].Inspections;
        }

        private static IEnumerable<Monkey> ParseInput(string[] pi)
        {
            for (var i = 0; i < pi.Length; i += 7)
            {
                var mid = int.Parse((pi[i].Split()[1]).Replace(":", ""));
                var itm = pi[i + 1].Replace("  Starting items: ", "").Split(',').Select(x => long.Parse(x)).ToList();
                var opr = pi[i + 2].Replace("  Operation: new = old ", "").Split(' ');
                var opt = OpType.Add;
                var opv = -1L;
                if(opr[0] == "+")
                {
                    opt = OpType.Add;
                    opv = long.Parse(opr[1]);
                }
                else if(opr[0] == "*" && opr[1] == "old")
                {
                    opt = OpType.Square;
                    opv = -1;
                }
                else
                {
                    opt = OpType.Multiply;
                    opv = long.Parse(opr[1]);
                }
                var tst = long.Parse(pi[i + 3].Replace("  Test: divisible by ", ""));
                var tTg = int.Parse(pi[i + 4].Replace("    If true: throw to monkey ", ""));
                var fTg = int.Parse(pi[i + 5].Replace("    If false: throw to monkey ", ""));
                yield return new Monkey(mid, itm, opt, opv, tst, tTg, fTg);
            }
        }
    }

    public enum OpType { Add, Multiply, Square }

    public record Monkey(int Name, List<long> Items, OpType Type, long OperationValue, long Test, int TrueTarget, int FalseTarget)
    {
        public long Inspections { get; set; } = 0;
        public bool Inspect(ref long item, bool part1)
        {
            item = Type switch
            {
                OpType.Add => (item + OperationValue),
                OpType.Multiply => (item * OperationValue),
                OpType.Square => (item * item),
                _ => throw new NotImplementedException(),
            };
            if (part1) item /= 3;
            Inspections++;
            return item % Test == 0;
        }
    }

    public static class Day11Extensions
    {
        public static void PrintRound(this List<Monkey> monkeys, int rnd)
        {
            C.WriteLine($"After Round {rnd}");
            monkeys.ForEach(m => C.WriteLine($"M{m.Name}: "
                + string.Join(",", m.Items)));
        }

        public static void PrintEnd(this List<Monkey> monkeys, int rnds)
        {
            C.WriteLine($"After {rnds} Rounds");
            monkeys.ForEach(m => C.WriteLine($"M{m.Name} inspected {m.Inspections} items."));
        }
    }
}