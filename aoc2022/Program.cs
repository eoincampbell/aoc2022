using aoc2022;
using aoc2022.Puzzles;

var days = new List<Puzzle>
{
    new Day01(),
};


bool currentDayOnly = true;

Puzzle.Header.Print();
foreach (var day in currentDayOnly ? days.OrderByDescending(x => x.Day).Take(1) : days)
{
    day.Run().Print();
}