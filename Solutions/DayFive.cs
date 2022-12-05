using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions
{
    public class DayFive : AbstractSolution
    {
        public DayFive(string input) : base(input)
        {
        }

        public override string SolvePartOne()
        {
            var defaultCrates = Input.Split(Environment.NewLine + Environment.NewLine)[0];
            var rows = defaultCrates.Split(Environment.NewLine).Reverse().Skip(1).Select(x => x.Chunk(4)).Select(x => x.Select(y => (new String(y.ToArray())).Trim().Replace("[","").Replace("]","") ).ToArray()).ToList();
            var stacks = Enumerable.Range(0, rows.First().Count()).Select(x => new Stack<String>()).ToArray();

            foreach(var row in rows)
            {                
                for(var i = 0; i < row.Count(); i++)
                {
                    if(!String.IsNullOrEmpty(row[i]))
                    {
                        stacks[i].Push(row[i]);
                    }
                }
            }
            foreach(var instructionRow in Input.Split(Environment.NewLine + Environment.NewLine)[1]
            .Replace("move ", "")
            .Replace("from ", "")
            .Replace("to ", "")
            .Split(Environment.NewLine).Select(x => x.Split(' ').Select(s => Convert.ToInt32(s)).ToArray()) )
            {
                var instruction = new {
                    CratesToTake = instructionRow[0],
                    SourceStack = instructionRow[1] - 1,
                    TargetStack = instructionRow[2] - 1
                };
                var crane = new Stack<string>();

                for(var i = 0; i < instruction.CratesToTake; i++)
                {
                    stacks[instruction.TargetStack].Push(stacks[instruction.SourceStack].Pop());
                }
            }

            return String.Join("", stacks.Select(x => x.Pop()));
        }

        public override string SolvePartTwo()
        {
            throw new NotImplementedException();
        }
    }
}