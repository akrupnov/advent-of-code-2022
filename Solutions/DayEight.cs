using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions
{

    public class DayEight : AbstractSolution
    {
        class Tree
        {
            public Int32 Height {get;set;}
            public Boolean ConfirmedVisiblity {get;set;}
            public override string ToString()
            {
                return $"H: {Height}, V: {ConfirmedVisiblity}";
            }
        }

        public DayEight(string input) : base(input)
        {
        }

        public override string SolvePartOne()
        {
            var inputLines = Input.Split(Environment.NewLine);
            Tree[][] forest = new Tree[inputLines.Length][];
            
            for(var i = 0; i < forest.Length; i++)
            {                
                forest[i] = new Tree[inputLines[i].Length];
                for(var j = 0; j < forest[i].Length; j++)
                {
                    forest[i][j] = new Tree
                    {
                        Height = Convert.ToInt32(inputLines[i][j].ToString()),
                        ConfirmedVisiblity = i == 0 || j == 0 || i == forest.Length - 1 || j == forest[i].Length - 1
                    };
                }
            }

            for(var i = 0; i < forest.Length; i++)
            {
                for(var j = 0; j < forest[i].Length; j++)
                {
                    var tree = forest[i][j];
                    if(!tree.ConfirmedVisiblity)
                    {
                        tree.ConfirmedVisiblity = GetLinesOfSight(i, j, forest).
                        Any(line => line.All(t => t.Height < tree.Height));
                    }
                }
            }

            return forest.SelectMany(x => x.Where(t => t.ConfirmedVisiblity)).Count().ToString();
        }

        private IEnumerable<IEnumerable<Tree>> GetLinesOfSight(int i, int j, Tree[][] forest)
        {
            yield return Enumerable.Range(0, i).Select(x => forest[x][j]);
            yield return Enumerable.Range(i + 1, forest.Length - i - 1).Select(x => forest[x][j]);
            yield return Enumerable.Range(0, j).Select(x => forest[i][x]);
            yield return Enumerable.Range(j + 1, forest[i].Length - j - 1).Select(x => forest[i][x]);
        }



        public override string SolvePartTwo()
        {
            throw new NotImplementedException();
        }
    }
}