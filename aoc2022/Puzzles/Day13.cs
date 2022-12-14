//AOC2022 - Day 13
//Result 1: 5390
//Result 2: 19261
namespace aoc2022.Puzzles
{
    using MoreLinq;
    using System.Text.Json;

    internal class Day13 : Puzzle
    {
        public override int Day => 13;
        public override string Name => "Distress Signal";
        protected override object RunPart1() => Part1();
        protected override object RunPart2() => Part2();
        private readonly IEnumerable<Packet> _signals;
        private static Packet [] Ord => new Packet[] { Parse("[[2]]"), Parse("[[6]]") };
        public Day13() : base("Inputs/Day13.txt") =>
            _signals = PuzzleInput
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(Parse);

        private object Part2() =>
            _signals.Concat(Ord)
                 .OrderBy(x => x, Comparer<Packet>.Create(Compare))
                 .Select((e, i) => e == Ord[0] || e == Ord[1] ? i + 1 : 1)
                 .Product();
          
        private object Part1() =>
            _signals.Batch(2)
                .Index()
                .Select((e, i) => (Compare(e.Value.ElementAt(0), e.Value.ElementAt(1)) < 0) ? i + 1 : 0)
                .Sum();

        private int Compare(Packet left, Packet right) =>
        (left, right) switch
        {
            (Packet l, null) => +1,
            (null, Packet r) => -1,
            (IPacket l, IPacket r) => Comparer<int>.Default.Compare(l.Value, r.Value),
            (IPacket l, APacket r) => Compare(new APacket(new Packet[] { l }), r),
            (APacket l, IPacket r) => Compare(l, new APacket(new Packet[] { r })),
            (APacket l, APacket r) => l.Packets
                    .ZipLongest(r.Packets, Compare)
                    .SkipWhile(x => x == 0)
                    .FirstOrDefault(),
            _ => throw new NotImplementedException()
        };

        private record Packet;
        private record IPacket(int Value) : Packet;
        private record APacket(Packet[] Packets) : Packet;

        private static Packet Parse(string value) =>
            Parse(JsonSerializer.Deserialize<JsonElement>(value));

        private static Packet Parse(JsonElement e) =>
           (e.ValueKind == JsonValueKind.Number)
                ? new IPacket(e.GetInt32())
                : new APacket(e.EnumerateArray().Select(Parse).ToArray());
    }
}