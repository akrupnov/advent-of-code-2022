using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions
{
    public class DayThree
    {
        private readonly String input;
        public DayThree(String input)
        {
            this.input = input;
        }
        public String SolvePartOne()
        {
            var r = input.Split(Environment.NewLine).Select(x => new Tuple<ISet<Char>, ISet<Char>>(
                x.Take(x.Length/2).ToHashSet(),
                x.Skip(x.Length/2).Take(x.Length/2).ToHashSet()
                )
                ).Select(x => x.Item1.Intersect(x.Item2).First()).Sum(x => GetCharPriority(x));
            return r.ToString();
        }

        private Int32 GetCharPriority(Char character)
        {
            if(char.IsLower(character))
            {
                return Convert.ToInt32(character) - 96;
            }
            return Convert.ToInt32(character) - 38;
        }
    }
}