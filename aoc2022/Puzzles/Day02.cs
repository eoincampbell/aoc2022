namespace aoc2022.Puzzles
{
    internal class Day02 : Puzzle
    {
        public override int Day => 2;
        public override string Name => "Rock Paper Scissors";
        protected override object RunPart1() => Calc(_outcomes);
        protected override object RunPart2() => Calc(_outcomes2);

        private static Dictionary<string, int> _outcomes = new Dictionary<string, int>
        {
            {"AX", 4 }, //R R = R(1) + Draw(3)
            {"AY", 8 }, //R P = P(2) + Win(6)
            {"AZ", 3 }, //R S = S(3) + Loss(0) 
            {"BX", 1 }, //P R = R(1) + Loss(0)
            {"BY", 5 }, //P P = P(2) + Draw(3)
            {"BZ", 9 }, //P S = S(3) + Win(6)
            {"CX", 7 }, //S R = R(1) + Win(6)
            {"CY", 2 }, //S P = P(2) + Loss(0)
            {"CZ", 6 }, //S S = S(3) + Draw(3)
        };

        private static Dictionary<string, int> _outcomes2 = new Dictionary<string, int>
        {
            {"AX", 3 }, //R S = S(3) + Loss(0) 
            {"AY", 4 }, //R R = R(1) + Draw(3)
            {"AZ", 8 }, //R P = P(2) + Win(6)
            {"BX", 1 }, //P R = R(1) + Loss(0)
            {"BY", 5 }, //P P = P(2) + Draw(3)
            {"BZ", 9 }, //P S = S(3) + Win(6)
            {"CX", 2 }, //S P = P(2) + Loss(0) 
            {"CY", 6 }, //S S = S(3) + Draw(3)
            {"CZ", 7 }, //S R = R(1) +  Win(6)
        };

        public Day02() : base("Inputs/Day02.txt")
        {

        }

        

        private object Calc(Dictionary<string, int> o)
        {
            var score = 0;
            foreach(var row in PuzzleInput)
            {
                var s = row[0].ToString() + row[2].ToString();
                score += o[s];
            }
            return score;
        }
            
    }
}
