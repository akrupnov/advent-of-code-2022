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

            var count = BFSFindNodeWithStartNode(start);
            //var path = FindShortestPathToEnd(start, new List<Field>(), new List<Field>());
            throw new Exception();
        }

        private void DijkstraSearch(Field start, ISet<Field> visited)
        {
            //Start.MinCostToStart = 0;
            var prioQueue = new List<Field>();
            prioQueue.Add(start);
            do {
                prioQueue = prioQueue.OrderBy(x => x.Height).ToList();
                var node = prioQueue.First();
                prioQueue.Remove(node);

                foreach (var childNode in node.AdjacentFields.Where(x => !visited.Contains(x) && (Convert.ToByte(x.Height) <= Convert.ToByte(node.Height) + 1 || x.Height == 'E') ).OrderBy(x => x.Height))
                {
                    if (childNode.MinCostToStart == null ||
                        node.MinCostToStart + cnn.Cost < childNode.MinCostToStart)
                    {
                        childNode.MinCostToStart = node.MinCostToStart + cnn.Cost;
                        childNode.NearestToStart = node;
                        if (!prioQueue.Contains(childNode))
                            prioQueue.Add(childNode);
                    }
                }
                node.Visited = true;
                if (node == End)
                    return;
            } while (prioQueue.Any());
        }

        private IList<Field> FindShortestPathToEnd(Field node, List<Field> visited, List<Field> pathToHere)
        {
            Console.WriteLine($"{node.ToString()}");
            pathToHere.Add(node);

            if(node.Height == 'E')
            {
                return pathToHere;
            }
            else
            {
                visited.Add(node);
            }
            var childPaths = new List<IList<Field>>();

            var adjustedAdjacent = node.AdjacentFields.Where(x => !visited.Contains(x) && (Convert.ToByte(x.Height) <= Convert.ToByte(node.Height) + 1 || x.Height == 'E') ).ToList();

            foreach(var field in adjustedAdjacent)
            {
                var path = FindShortestPathToEnd(field, visited, new List<Field>());            
                
                var d = "d;";

                if(path.Where(x => x.Height == 'E').Any())
                {
                    childPaths.Add(path);
                }
/*                else
                {
                    visited.RemoveAll(x => path.Contains(x));
                }*/
            }
            var shortestPath = childPaths.OrderBy(x => x.Count()).FirstOrDefault();
            if(shortestPath != null) 
                pathToHere.AddRange(shortestPath);
            return pathToHere;
        }

        public int BFSFindNodeWithStartNode(Field startingPoint)
            {
                if (startingPoint.Height == 'E')
                {
                    return 0;
                }

                var visited = new HashSet<Field>();
                visited.Add(startingPoint);
                
                var q = new Queue<Field>();
                
                // Add this node to the queue
                q.Enqueue(startingPoint);
                
                int count = 0;
                
                while(q.Count > 0)
                {
                    var current = q.Dequeue();
                    
                    Console.WriteLine(current);
                    if (current.Height == 'E')
                    {
                        return visited.Count();
                    }
                    
                    // Iterate through UNVISITED nodes
                    foreach(var neighbour in current.AdjacentFields.Where(x => !visited.Contains(x) && (Convert.ToByte(x.Height) <= Convert.ToByte(current.Height) + 1 || x.Height == 'E')))
                    {
                        if(neighbour.Height == 'E')
                        {
                            return visited.Count();
                        }
                        visited.Add(neighbour);
                        q.Enqueue(neighbour);
                    }
                    count++;
                }
                Console.WriteLine("Could not find node!");
                return count;
            }        
        
        public override string SolvePartTwo()
        {
            throw new NotImplementedException();
        }
    }
}