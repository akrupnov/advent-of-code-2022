using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions
{

    public class DayFour : AbstractSolution
    {
        public DayFour(string input) : base(input)
        {
        }

        public override string SolvePartOne()
        {
            var r = Input.Split(Environment.NewLine).Select(
                x => 
                {
                    var assgnmts = x.Split(",");
                    var firstRange = assgnmts[0].Split("-").Select(n => Convert.ToInt32(n)).ToArray();
                    var secondRange = assgnmts[1].Split("-").Select(n => Convert.ToInt32(n)).ToArray();
                    return new {
                        FirstElfAssignment = Enumerable.Range(firstRange[0], firstRange[1] - firstRange[0] + 1).ToHashSet(),
                        SecondElfAssignment = Enumerable.Range(secondRange[0], secondRange[1] - secondRange[0] + 1).ToHashSet()

                    };
                }
            ).Count(x => x.FirstElfAssignment.IsSubsetOf(x.SecondElfAssignment) || x.FirstElfAssignment.IsSupersetOf(x.SecondElfAssignment));

            return r.ToString();
        }

        public override string SolvePartTwo()
        {
            var r = Input.Split(Environment.NewLine).Select(
                x => 
                {
                    var assgnmts = x.Split(",");
                    var firstRange = assgnmts[0].Split("-").Select(n => Convert.ToInt32(n)).ToArray();
                    var secondRange = assgnmts[1].Split("-").Select(n => Convert.ToInt32(n)).ToArray();
                    return new {
                        FirstElfAssignment = Enumerable.Range(firstRange[0], firstRange[1] - firstRange[0] + 1).ToHashSet(),
                        SecondElfAssignment = Enumerable.Range(secondRange[0], secondRange[1] - secondRange[0] + 1).ToHashSet()

                    };
                }
            ).Count(x => x.FirstElfAssignment.Overlaps(x.SecondElfAssignment));

            return r.ToString();
        }
    }
}