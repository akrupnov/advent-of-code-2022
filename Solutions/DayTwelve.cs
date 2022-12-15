using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions
{
        public class Field
        {
            public (Int32, Int32) Coords {get;set;}
            public ISet<Field> AdjacentFields {get; } = new HashSet<Field>();

            public Char Height {get;set;}

            public override String ToString()
            {
                return $"{Height} - {Coords}";
            }

        }

    public class DayTwelve : AbstractSolution
    {
        public DayTwelve(string input) : base(input)
        {
        }

        public override string SolvePartOne()
        {
            var lines = Input.Split(Environment.NewLine);
            var fieldDictionary = new Dictionary<(int, int), Field>();
            for(var i = 0; i < lines.Length; i++)
            {
                for(var j = 0; j < lines[i].Length; j++)
                {
                    fieldDictionary[(i, j)] = new Field(){
                        Coords = (i, j),
                        Height = lines[i][j]
                    };
                }
            }

            foreach(var de in fieldDictionary.Values)
            {
                var neighbourCoordinates = new [] {
                    (de.Coords.Item1 - 1, de.Coords.Item2),
                    (de.Coords.Item1 + 1, de.Coords.Item2),
                    (de.Coords.Item1, de.Coords.Item2 - 1),
                    (de.Coords.Item1, de.Coords.Item2 + 1)
                };

                foreach(var neighbour in neighbourCoordinates)
                {
                    if(fieldDictionary.ContainsKey(neighbour))
                    {
                        de.AdjacentFields.Add(fieldDictionary[neighbour]);
                    }

                }
            }

            var start = fieldDictionary.First(x => x.Value.Height == 'S').Value;
            start.Height = 'a';
            var target = fieldDictionary.First(x => x.Value.Height == 'E').Value;
            target.Height = 'z';

            var path = SolveDijkstra(start, target);
            return (path.Count - 1).ToString();
        }

    private static IList<Field> SolveDijkstra(Field start, Field? target)
    {
        var distances = new Dictionary<Field, Int32>();
        var transitions = new Dictionary<Field, Field>();

        var queueTracker = new List<Field>();
        distances[start] = 0;
        var workingQueue = new PriorityQueue<Field, int>();
        workingQueue.Enqueue(start, distances[start]);
        queueTracker.Add(start);
        
        while (workingQueue.Count > 0)
        {
            var u = workingQueue.Dequeue();
            queueTracker.Remove(u);
            if (u == target)
                break;
            foreach (var neighbour in u.AdjacentFields.Where(x => x.Height <= u.Height + 1))
            {
                var distanceToExplore =  distances[u] + 1;
                if (distanceToExplore < (distances.ContainsKey(neighbour) ? distances[neighbour] : int.MaxValue))
                {
                    distances[neighbour] = distanceToExplore;
                    transitions[neighbour] = u;
                    if (! queueTracker.Contains(neighbour))
                    {
                        workingQueue.Enqueue(neighbour, distanceToExplore);
                        queueTracker.Add(neighbour);
                    }
                }
            }
        }
        var path = new List<Field>();
        if (transitions.ContainsKey(target) || target == start)
        {
            while (target != null)
            {
                path.Add(target);
                target = transitions.ContainsKey(target) ? transitions[target] : null;
            }
        }
        return path;
    }

        
        public override string SolvePartTwo()
        {
            throw new NotImplementedException();
        }
    }
}