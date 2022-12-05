using aoc2022;
using aoc2022.Puzzles;

var days = new List<Puzzle>
{
    new Day01(),
    new Day02(),
    new Day03(),
    new Day04(),
    new Day05()
};


bool currentDayOnly = false;

Puzzle.Header.Print();
foreach (var day in currentDayOnly ? days.OrderByDescending(x => x.Day).Take(1) : days)
{
    day.Run().Print();
}