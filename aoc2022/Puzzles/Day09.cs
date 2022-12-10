//AOC2022 - Day 09
//Result 1: 5513
//Result 2: 2427
namespace aoc2022.Puzzles
{
    using System.Drawing;

    internal class Day09 : Puzzle
    {
        public override int Day => 9;
        public override string Name => "Rope Bridge";
        protected override object RunPart1() => DragRope(2);
        protected override object RunPart2() => DragRope(10);
        public Day09() : base("Inputs/Day09.txt") { }

        private object DragRope(int length)
        {
            var rope = new Point[length]; //points default to 0,0
            var hash = new HashSet<Point> { rope[0] };

            foreach (var instruction in PuzzleInput)
            {
                var split = instruction.Split(' ');
                var dir = split[0];
                var dist = int.Parse(split[1]);

                for (int d = 0; d < dist; d++)
                {
                    rope[0] = UpdateHead(rope[0], dir);
                    for (int r = 1; r < rope.Length; r++)
                    {
                        if (IsAdjacent(rope[r - 1], rope[r]))
                            continue;

                        rope[r] = UpdateTail(rope[r], rope[r - 1]);
                        if (r == rope.Length - 1)
                            hash.Add(rope[r]);
                    }
                }
            }
            //hash.Print(rope);
            return hash.Count;
        }

        private static Point UpdateTail(Point head, Point tail) => 
            new(tail.X + Math.Sign(head.X - tail.X), 
                tail.Y + Math.Sign(head.Y - tail.Y));

        private static bool IsAdjacent(Point head, Point tail) =>
            Math.Abs(head.X - tail.X) <= 1 &&
            Math.Abs(head.Y - tail.Y) <= 1;

        private static Point UpdateHead(Point head, string dir) =>
            dir switch
            {
                "U" => new Point(head.X, head.Y + 1),
                "D" => new Point(head.X, head.Y - 1),
                "L" => new Point(head.X - 1, head.Y),
                "R" => new Point(head.X + 1, head.Y),
                _ => throw new InvalidOperationException()
            };
    }
}