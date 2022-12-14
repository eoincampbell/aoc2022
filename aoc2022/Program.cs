﻿using aoc2022;
using aoc2022.Puzzles;

var days = new List<Puzzle>
{
    new Day01(),    new Day02(),
    new Day03(),    new Day04(),
    new Day05(),    new Day06(),
    new Day07(),    new Day08(),
    new Day09(),    new Day10(),
    new Day11(),    new Day12(),
    new Day13(),    new Day14()
};

var current = false;
Puzzle.Header.Print();
(current ? days.OrderByDescending(x => x.Day).Take(1) : days).ToList().ForEach(d => d.Run().Print());