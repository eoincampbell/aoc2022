//AOC2022 - Day 02
//Result 1: 11841
//Result 2: 13022

namespace aoc2022.Puzzles
{
    internal class Day02 : Puzzle
    {
        public override int Day => 2;
        public override string Name => "Rock Paper Scissors";
        protected override object RunPart1() => Calc(true);
        protected override object RunPart2() => Calc(false);

        private const int Rock = 1;
        private const int Paper = 2;
        private const int Sciossors = 3;
        private const int Loss = 0;
        private const int Draw = 3;
        private const int Win = 6;

        private static readonly Dictionary<string, (int o1, int o2)> _outcomes = new()
        {
            {"AX", (Rock+Draw,      Sciossors+Loss)},
            {"AY", (Paper+Win,      Rock+Draw)},
            {"AZ", (Sciossors+Loss, Paper+Win)},
            {"BX", (Rock+Loss,      Rock+Loss)},
            {"BY", (Paper+Draw,     Paper+Draw)},
            {"BZ", (Sciossors+Win,  Sciossors+Win)},
            {"CX", (Rock+Win,       Paper+Loss)},
            {"CY", (Paper+Loss,     Sciossors+Draw)},
            {"CZ", (Sciossors+Draw, Rock+Win)}
        };

        public Day02() : base("Inputs/Day02.txt") { }

        private object Calc(bool c)
        {
            var s = 0;
            PuzzleInput.Select(x => new string(new[] { x[0], x[2] }))
                .ToList()
                .ForEach(f => s += c ? _outcomes[f].o1 : _outcomes[f].o2);
            return s;
        }

    }
}