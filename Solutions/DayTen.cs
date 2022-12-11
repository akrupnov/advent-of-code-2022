using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions
{
    public class DayTen : AbstractSolution
    {
        public DayTen(string input) : base(input)
        {
        }

        public override string SolvePartOne()
        {
            var cycle = 0;
            var cycleRecord = new Dictionary<Int32, Int32>();
            var register = 1;

            foreach(var command in Input.Split(Environment.NewLine).Select(x => x.Split(' ')).Select(x => new {
                Name = x[0],
                Value = x.Length == 1 ? 0 : Convert.ToInt32(x[1])
            }))
            {
                if(command.Name == "noop")
                {
                    cycle++;
                    cycleRecord[cycle] = register;
                }
                else 
                {
                    cycle++;
                    cycleRecord[cycle] = register;
                    cycle++;
                    cycleRecord[cycle] = register;
                    register += command.Value;                    
                }

            }

            return (new List<Int32> {20, 60, 100, 140, 180, 220}).Sum(x => x*cycleRecord[x]).ToString();
        }

        public override string SolvePartTwo()
        {
            var cycle = 0;
            var cycleRecord = new Dictionary<Int32, Int32>();
            var register = 1;

            var spriteBuilder = new StringBuilder();
            
            foreach(var command in Input.Split(Environment.NewLine).Select(x => x.Split(' ')).Select(x => new {
                Name = x[0],
                Value = x.Length == 1 ? 0 : Convert.ToInt32(x[1])
            }))
            {
                if(command.Name == "noop")
                {
                    spriteBuilder.Append(Enumerable.Range(register - 1, 3).Contains(cycle % 40) ? "#" : ".");
                    cycle++;
                    cycleRecord[cycle] = register;
                }
                else 
                {
                    spriteBuilder.Append(Enumerable.Range(register - 1, 3).Contains(cycle % 40) ? "#" : ".");
                    cycle++;
                    cycleRecord[cycle] = register;
                    spriteBuilder.Append(Enumerable.Range(register - 1, 3).Contains(cycle % 40) ? "#" : ".");
                    cycle++;
                    cycleRecord[cycle] = register;
                    register += command.Value;                    
                }

            }


            return Environment.NewLine +  String.Join(Environment.NewLine, spriteBuilder.ToString().Chunk(40).Select(x => new String(x.ToArray())));
        }
    }
}