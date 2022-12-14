using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions
{
    public class DayFourteen : AbstractSolution
    {
        private enum SpaceType {Empty, Rock, Sand, EverFloatingSand}

        private class CaveSpace
        {
            public SpaceType SpaceType {get;set;} = SpaceType.Empty;
            public (Int32, Int32) Coords {get;set;}

            public override string ToString()
            {
                return $"[{Coords}] - {SpaceType}";
            }
        }

        public DayFourteen(string input) : base(input)
        {
        }

        public override string SolvePartOne()
        {
            var wallDefinitions = Input.Split(Environment.NewLine).
                    Select(x => x.Split(" -> ").Select(x => x.Split(",")).Select(
                            p => (Convert.ToInt32(p[0]), Convert.ToInt32(p[1]))
                            ) );
            var minX = wallDefinitions.Select(x => x.Min(d => d.Item1)).Min(x => x);
            var maxX = wallDefinitions.Select(x => x.Max(d => d.Item1)).Max(x => x);
            var maxY = wallDefinitions.Select(x => x.Max(d => d.Item2)).Max(x => x);

            
            var cave = Enumerable.Range(0, maxY + 1).
                Select((row, ri) => Enumerable.Range(0, maxX - minX +1).Select(
                    (c, ci) => new CaveSpace()
                    {
                        Coords = (ri, ci)
                    }
                    ).ToArray()
                ).ToArray();


            foreach(var wd in wallDefinitions.Select(x => x.ToArray()))
            {
                for(var i = 0; i < wd.Length - 1; i++)
                {
                    if(wd[i].Item1 == wd[i + 1].Item1)
                    {
                        var yes = new List<Int32>(){ wd[i].Item2, wd[i + 1].Item2};

                        for(var c = yes.Min(); c <= yes.Max(); c++)
                        {
                            cave[c][wd[i].Item1 - minX].SpaceType = SpaceType.Rock;
                        }
                    }
                    else
                    {
                        var xes = new List<Int32>() {wd[i].Item1, wd[i + 1].Item1};
                        for(var c = xes.Min(); c <= xes.Max(); c++)
                        {
                            cave[wd[i].Item2][c - minX].SpaceType = SpaceType.Rock;
                        }
                    }
                }
            }
            
            var startCoord = (0, 500 - minX);
            var sandPieces = 1;
            
            var coord = startCoord;
            while(true)
            {
                try
                {
                    if(true)
                    {
                        if(cave[coord.Item1 + 1][coord.Item2].SpaceType == SpaceType.Empty)
                        {
                            cave[coord.Item1][coord.Item2].SpaceType = SpaceType.Empty;
                            coord = (coord.Item1 + 1, coord.Item2);
                        }
                        else if(cave[coord.Item1 + 1][coord.Item2 - 1].SpaceType == SpaceType.Empty)
                        {
                            cave[coord.Item1][coord.Item2].SpaceType = SpaceType.Empty;
                            coord = (coord.Item1 + 1, coord.Item2 - 1);
                        }
                        else if(cave[coord.Item1 + 1][coord.Item2 + 1].SpaceType == SpaceType.Empty)
                        {
                            cave[coord.Item1][coord.Item2].SpaceType = SpaceType.Empty;
                            coord = (coord.Item1 + 1, coord.Item2 + 1);
                        }
                        else
                        {
                            sandPieces++;
                            coord = startCoord;
                        }

                    }

                    cave[coord.Item1][coord.Item2].SpaceType = SpaceType.Sand;
                    DrawCave(cave, sandPieces);
                }
                catch(IndexOutOfRangeException)
                {
                    return (sandPieces - 1).ToString();
                }

            }
        }

        private void DrawCave(CaveSpace[][] cave, int sandPieces)
        {
            //return;
            Thread.Sleep(10);
            Console.Clear();
            Console.SetCursorPosition(70, 70);
            Console.Write(sandPieces);
            for(int y = 0; y < cave.Length; y++)
            {
                for(int x = 0; x < cave[y].Length; x++)
                {
                    Console.SetCursorPosition(x, y);
                    switch(cave[y][x].SpaceType)
                    {
                        case SpaceType.Rock:
                            Console.Write("#");
                            break;
                        case SpaceType.Empty:
                            Console.Write(".");
                            break;
                        case SpaceType.Sand:
                            Console.Write("o");
                            break;
                        case SpaceType.EverFloatingSand:
                            Console.Write("~");
                            break;
                    }
                }
            }
        }
        private void DrawCave(Dictionary<(Int32, Int32), SpaceType> cave, int sandPieces)
        {
            return;
            //Thread.Sleep(1);
            Console.Clear();
            Console.SetCursorPosition(70, 70);
            Console.Write(sandPieces);
            var minX = cave.Min(x => x.Key.Item1);
            foreach(var caveField in cave)
            {
                Console.SetCursorPosition(caveField.Key.Item1 - minX, caveField.Key.Item2);
                switch(caveField.Value)
                {
                    case SpaceType.Rock:
                        Console.Write("#");
                        break;
                    case SpaceType.Empty:
                        Console.Write(".");
                        break;
                    case SpaceType.Sand:
                        Console.Write("o");
                        break;
                    case SpaceType.EverFloatingSand:
                        Console.Write("~");
                        break;
                }
            }
            
        }

        public override string SolvePartTwo()
        {
            var wallDefinitions = Input.Split(Environment.NewLine).
                    Select(x => x.Split(" -> ").Select(x => x.Split(",")).Select(
                            p => (Convert.ToInt32(p[0]), Convert.ToInt32(p[1]))
                            ) );
            var minX = wallDefinitions.Select(x => x.Min(d => d.Item1)).Min(x => x);
            var maxX = wallDefinitions.Select(x => x.Max(d => d.Item1)).Max(x => x);
            var maxY = wallDefinitions.Select(x => x.Max(d => d.Item2)).Max(x => x);

            
            var cave = new Dictionary<(int, int), SpaceType>();


            foreach(var wd in wallDefinitions.Select(x => x.ToArray()))
            {
                for(var i = 0; i < wd.Length - 1; i++)
                {
                    if(wd[i].Item1 == wd[i + 1].Item1)
                    {
                        var yes = new List<Int32>(){ wd[i].Item2, wd[i + 1].Item2};

                        for(var c = yes.Min(); c <= yes.Max(); c++)
                        {
                            cave[(wd[i].Item1, c)] = SpaceType.Rock;
                        }
                    }
                    else
                    {
                        var xes = new List<Int32>() {wd[i].Item1, wd[i + 1].Item1};
                        for(var c = xes.Min(); c <= xes.Max(); c++)
                        {
                            cave[(c, wd[i].Item2)] = SpaceType.Rock;
                        }
                    }
                }
            }
            
            var startCoord = (500, 0);
            cave[startCoord] = SpaceType.Empty;
            var sandPieces = 1;
            
            var coord = startCoord;
            while(true)
            {
                if(true)
                {
                    for(var i = coord.Item1 - 1; i <= coord.Item1 + 1; i++)
                    {
                        cave[(i, maxY + 2)] = SpaceType.Rock;
                    }

                    var bottomCoord = (coord.Item1, coord.Item2 + 1);
                    var leftDiagCoord = (coord.Item1 - 1, coord.Item2 + 1);
                    var rightDiagCoord = (coord.Item1 + 1, coord.Item2 + 1);

                    if(!cave.ContainsKey(bottomCoord) || cave[bottomCoord] == SpaceType.Empty)
                    {
                        cave[coord] = SpaceType.Empty;
                        coord = bottomCoord;
                    }
                    else if(!cave.ContainsKey(leftDiagCoord) || cave[leftDiagCoord] == SpaceType.Empty)
                    {
                        cave[coord] = SpaceType.Empty;
                        coord = leftDiagCoord;
                    }
                    else if(!cave.ContainsKey(rightDiagCoord) || cave[rightDiagCoord] == SpaceType.Empty)
                    {
                        cave[coord] = SpaceType.Empty;
                        coord = rightDiagCoord;
                    }
                    else
                    {
                        sandPieces++;
                        if(coord == startCoord)
                        {
                            return (sandPieces - 1).ToString();
                        }
                        coord = startCoord;
                    }
                    
                    cave[coord] = SpaceType.Sand;
                    DrawCave(cave, sandPieces);
                }
            }        
        }
    }
}