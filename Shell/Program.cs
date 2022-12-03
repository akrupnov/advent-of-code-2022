// See https://aka.ms/new-console-template for more information
using Solutions;



Console.WriteLine("Day 1. Max total calories: " + new DayOne(File.ReadAllText("./input/day1")).SolvePartOne()) ;
Console.WriteLine("Day 1 pt. Top 3 total calories: " + new DayOne(File.ReadAllText("./input/day1")).SolvePartTwo()) ;



