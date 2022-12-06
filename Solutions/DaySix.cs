using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions
{
    public class DaySix : AbstractSolution
    {
        public DaySix(string input) : base(input)
        {
        }

        public override string SolvePartOne()
        {
            var slidingSet = new HashSet<Char>();
            var sequenceCounter = 0;

            for(var i = 0; i < Input.Length; i++)
            {
                sequenceCounter++;

                slidingSet.Add(Input[i]);
                if(sequenceCounter % 4 == 0 && slidingSet.Count == 4)
                {
                    return (i + 1).ToString();
                }
                else if(sequenceCounter > 0 && sequenceCounter % 4 == 0)
                {
                    i = i - 3;
                    sequenceCounter = 0;
                    slidingSet.Clear();
                }
            }

            throw new Exception("Unable to find a solution, I am sorry");
        }

        public override string SolvePartTwo()
        {
            var slidingSet = new HashSet<Char>();
            var sequenceCounter = 0;

            for(var i = 0; i < Input.Length; i++)
            {
                sequenceCounter++;

                slidingSet.Add(Input[i]);
                if(sequenceCounter % 14 == 0 && slidingSet.Count == 14)
                {
                    return (i + 1).ToString();
                }
                else if(sequenceCounter > 0 && sequenceCounter % 14 == 0)
                {
                    i = i - 13;
                    sequenceCounter = 0;
                    slidingSet.Clear();
                }
            }

            throw new Exception("Unable to find a solution, I am sorry");
        }
    }
}