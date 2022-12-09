using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions
{
    public class DayNine : AbstractSolution
    {
        public class FieldCell
        {
            public Int32 TailVisits {get; private set;}

            public Boolean HostHead {get;set;}

            private bool hostTail;
            public Boolean HostTail 
            {
                get {return hostTail;}
                set {
                    if(value)
                    {
                        TailVisits++;
                    }
                    hostTail = value;
                }

            }
        }

        public DayNine(string input) : base(input)
        {
        }

        public override string SolvePartOne()
        {
            var field = new Dictionary<(int, int), FieldCell>();

            field[(0, 0)] = new FieldCell() { HostHead = true, HostTail = true };

            var headCoords = (0, 0);
            var tailCoords = (0, 0);

            foreach(var move in Input.Split(Environment.NewLine).Select(l => new {Direction = l.Split(' ')[0], Steps = Convert.ToInt32(l.Split(' ')[1])}))
            {
                for(var i = 0; i < move.Steps; i++)
                {
                    field[headCoords].HostHead = false;
                    
                    switch(move.Direction)
                    {
                        case "R":
                            headCoords.Item1 +=1;
                            break;
                        case "L":
                            headCoords.Item1 -=1;
                            break;
                        case "U":
                            headCoords.Item2 +=1;
                            break;
                        case "D":
                            headCoords.Item2 -=1;
                            break;
                    }

                    GetFieldCell(field, headCoords).HostHead = true;
                    if(GetDistance(headCoords, tailCoords) > 1.5)
                    {
                        field[tailCoords].HostTail = false;
                        if(headCoords.Item1 == tailCoords.Item1)
                        {
                            tailCoords.Item2 += (tailCoords.Item2 < headCoords.Item2 ? 1 : -1);
                        }
                        else if(headCoords.Item2 == tailCoords.Item2)
                        {
                            tailCoords.Item1 += (tailCoords.Item1 < headCoords.Item1 ? 1 : -1);
                        }
                        else
                        {
                            if(Math.Abs(headCoords.Item1 - tailCoords.Item1) == 2)
                            {
                                tailCoords.Item1 += (tailCoords.Item1 < headCoords.Item1 ? 1 : -1);
                                tailCoords.Item2 = headCoords.Item2;
                            }
                            else
                            {
                                tailCoords.Item2 += (tailCoords.Item2 < headCoords.Item2 ? 1 : -1);
                                tailCoords.Item1 = headCoords.Item1;
                            }
                        }

                        GetFieldCell(field, tailCoords).HostTail = true;
                        //RenderField(field);

                    }
                }

            }

            //RenderField(field);
            

            return field.Where(x => x.Value.TailVisits > 0).Count().ToString();

        }
        private static double GetDistance((int, int) first , (int, int) second)
        {
             return Math.Sqrt(Math.Pow((second.Item1 - first.Item1), 2) + Math.Pow((second.Item2 - first.Item2), 2));
        }
        private static FieldCell GetFieldCell(Dictionary<(int, int), FieldCell> field, (int, int) coords)
        {
            if(!field.ContainsKey(coords))
            {
                field[coords] = new FieldCell();
            }
            return field[coords];
        }

        private static void RenderField(Dictionary<(int, int), FieldCell> field)
        {
            var maxY = field.Max(x => x.Key.Item2);
            var maxX = field.Max(x => x.Key.Item1);
            Console.Clear();
            for (var i = 0; i <= maxY; i++)
            {
                for (var j = 0; j <= maxX; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.Write('.');
                }
            }
            foreach (var fieldEntry in field)
            {
                Console.SetCursorPosition(fieldEntry.Key.Item1, maxY - fieldEntry.Key.Item2);
                Console.Write(fieldEntry.Value.HostHead ? "H" : (fieldEntry.Value.HostTail ? "T" : fieldEntry.Value.TailVisits >0 ? "X" : "."));

            }
            if(!Debugger.IsAttached)
                Console.ReadKey();
        }

        public override string SolvePartTwo()
        {
            throw new NotImplementedException();
        }
    }
}