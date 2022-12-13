using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions
{
    public class DayEleven : AbstractSolution
    {
        public DayEleven(string input) : base(input)
        {
        }

        public override string SolvePartOne()
        {
            var monkeys = Input.Split($"{Environment.NewLine}{Environment.NewLine}")
                .Select(x => x.Split(Environment.NewLine).Select(y => y.Split(":")[1].Trim()).ToArray())
                .Select(
                    monkeyParts => new 
                    {
                        Stash = monkeyParts[1].Split(",").Select(x => Convert.ToInt64(x)).ToList(),
                        Operation =  (
                            monkeyParts[2].Split("=")[1].Trim().Split(" ")[1],
                            monkeyParts[2].Split("=")[1].Trim().Split(" ")[0],
                            monkeyParts[2].Split("=")[1].Trim().Split(" ")[2]
                        ),
                        Divider = Convert.ToInt64(monkeyParts[3].Split(" ").Last()),
                        SuccessTarget = Convert.ToInt64(monkeyParts[4].Split(" ").Last()),
                        FailureTarget = Convert.ToInt64(monkeyParts[5].Split(" ").Last()),
                    }
                ).ToArray();

            var inspections = monkeys.Select(x => 0).ToArray();

            for(var i = 0; i < 20; i++)
            {
                for(var m = 0; m < monkeys.Length; m++)
                {
                    var monkey = monkeys[m];
                    inspections[m] += monkey.Stash.Count();

                    foreach(var item in monkey.Stash)
                    {
                        Func<Int64, Int64, Int64> op = Add;
                        if (monkey.Operation.Item1 == "*")
                        {
                            op = Mult;
                        }
                        
                        var calculatedWorryLevel = 
                            Convert.ToInt64(Math.Floor(
                                    op( monkey.Operation.Item2 == "old" ? item : Convert.ToInt64(monkey.Operation.Item2),
                                        monkey.Operation.Item3 == "old" ? item : Convert.ToInt64(monkey.Operation.Item3))
                                        /3.0));
                        var target = calculatedWorryLevel % monkey.Divider == 0 ? monkey.SuccessTarget : monkey.FailureTarget;

                        monkeys[target].Stash.Add(calculatedWorryLevel);
                    }
                    monkey.Stash.Clear();
                }
            }

            return inspections.OrderByDescending(x => x).Take(2).Aggregate(1, (x, y) => x*y).ToString();
        }
        private Int64 Add(Int64 x, Int64 y){
            return x + y;
        }
        private Int64 Mult(Int64 x, Int64 y)
        {
            return x * y;
        }
        public override string SolvePartTwo()
        {
            var monkeys = Input.Split($"{Environment.NewLine}{Environment.NewLine}")
                .Select(x => x.Split(Environment.NewLine).Select(y => y.Split(":")[1].Trim()).ToArray())
                .Select(
                    monkeyParts => new 
                    {
                        Stash = monkeyParts[1].Split(",").Select(x => Convert.ToInt64(x)).ToList(),
                        Operation =  (
                            monkeyParts[2].Split("=")[1].Trim().Split(" ")[1],
                            monkeyParts[2].Split("=")[1].Trim().Split(" ")[0],
                            monkeyParts[2].Split("=")[1].Trim().Split(" ")[2]
                        ),
                        Divider = Convert.ToInt64(monkeyParts[3].Split(" ").Last()),
                        SuccessTarget = Convert.ToInt64(monkeyParts[4].Split(" ").Last()),
                        FailureTarget = Convert.ToInt64(monkeyParts[5].Split(" ").Last()),
                    }
                ).ToArray();

            var inspections = monkeys.Select(x => 0).ToArray();
            var dividersSum = monkeys.Aggregate((long)1, (x,y) => x * y.Divider);

            for(var i = 0; i < 10000; i++)
            {
                for(var m = 0; m < monkeys.Length; m++)
                {
                    var monkey = monkeys[m];

                    inspections[m] += monkey.Stash.Count();

                    foreach(var item in monkey.Stash)
                    {
                        Func<Int64, Int64, Int64> op = Add;
                        if (monkey.Operation.Item1 == "*")
                        {
                            op = Mult;
                        }
                        
                        var calculatedWorryLevel = 
                                    op( monkey.Operation.Item2 == "old" ? item : Convert.ToInt64(monkey.Operation.Item2),
                                        monkey.Operation.Item3 == "old" ? item : Convert.ToInt64(monkey.Operation.Item3))
                                        % dividersSum;
                        
                        var target = calculatedWorryLevel % monkey.Divider == 0 ? monkey.SuccessTarget : monkey.FailureTarget;
                        monkeys[target].Stash.Add(calculatedWorryLevel);
                            
                    }
                    monkey.Stash.Clear();
                }
            }

            return inspections.OrderByDescending(x => x).Take(2).Aggregate((long)1, (x, y) => x*y).ToString();
        }
    }
}