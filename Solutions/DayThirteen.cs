using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions
{
    public class DayThirteen : AbstractSolution, IComparer<String>
    {
        private enum ElementType {Integer, List}
        private enum CompareResult {Right, Wrong, Inconclusive}
        
        private class PacketElement 
        {
            public String Value {get;set;} = String.Empty;
            public ElementType Type {get;set;}
            public Int32 EndIndex {get;set;}

            public override string ToString()
            {
                return Value;
            }

        }

        public DayThirteen(string input) : base(input)
        {
        }

        public override string SolvePartOne()
        {
            var index = 0;

            var packetStatuses = new Dictionary<Int32, Boolean>();
            foreach(var packetPair in Input
                                        .Split($"{Environment.NewLine}{Environment.NewLine}")
                                        .Select(x => x.Split(Environment.NewLine).Select(x => x.Substring(1, x.Length - 2))).ToArray())
            {
                index++;

                var left = packetPair.First();
                var right = packetPair.Skip(1).First();
                var test = ComparePackets(left, right);
                packetStatuses[index] = ComparePackets(left, right) == CompareResult.Right;
            }

            return packetStatuses.Where(x => x.Value == true).Select(x => x.Key).Sum().ToString();
        }

        public override string SolvePartTwo()
        {
            var flatInput = Input.Replace($"{Environment.NewLine}{Environment.NewLine}", Environment.NewLine).Split(Environment.NewLine).ToList();
            var dividers = new string[] {"[[2]]", "[[6]]"};
            flatInput.AddRange(dividers);
            return flatInput.OrderBy(x => x, this).Select((s, i) => new {i, s})
                .Where(t => dividers.Contains(t.s))
                .Select(t => t.i + 1)
                .ToList().Aggregate(1, (x, y) => x*y).ToString();
        }


        private CompareResult ComparePackets(String left, String right)
        {
                var currentLeftIndex = 0;
                var currentRightIndex = 0;
                
                while(true)
                {
                    var leftElement = TakeNextElement(left, currentLeftIndex);
                    var rightElement = TakeNextElement(right, currentRightIndex);
                    if(leftElement == null || rightElement == null)
                    {
                        if(leftElement == null && rightElement == null)
                        {
                            return CompareResult.Inconclusive;
                        }
                        return leftElement == null ? CompareResult.Right : CompareResult.Wrong;
                    }
                    if(leftElement.Type == ElementType.Integer 
                        && rightElement.Type == ElementType.Integer )
                    {
                        if(Convert.ToInt32(leftElement.Value) != Convert.ToInt32(rightElement.Value))
                        {
                            return  Convert.ToInt32(leftElement.Value) < Convert.ToInt32(rightElement.Value) ? CompareResult.Right : CompareResult.Wrong;
                        }
                    }
                    else
                    {
                        var listCompareResult = ComparePackets(
                            leftElement.Type == ElementType.List ? leftElement.Value.Substring(1, leftElement.Value.Length - 2) : leftElement.Value, 
                            rightElement.Type == ElementType.List ? rightElement.Value.Substring(1, rightElement.Value.Length - 2) : rightElement.Value);

                        if(listCompareResult != CompareResult.Inconclusive)
                        {
                            return listCompareResult;
                        }
                    }


                    currentLeftIndex = leftElement.EndIndex + 1;
                    currentRightIndex = rightElement.EndIndex + 1;
                }

            return CompareResult.Inconclusive;
        }

        private PacketElement TakeNextElement(String packet, Int32 startingIndex)
        {
            if(startingIndex >= packet.Length)
            {
                return null;
            }
            var retvalBuilder = new StringBuilder();
            var type = ElementType.List;
            var finalIndex = startingIndex;
            if(packet[startingIndex] != '[')
            {
                type = ElementType.Integer;

                for(var i = startingIndex; i < packet.Length; i++)
                {
                    if(packet[i] == ',')
                    {
                        finalIndex = i;
                        break;
                    }
                    retvalBuilder.Append(packet[i]);

                }
            }
            else
            {
                type = ElementType.List;
                var opened = 0;
                var closed = 0;

                for(var i = startingIndex; i < packet.Length; i++)
                {
                    retvalBuilder.Append(packet[i]);
                    opened += packet[i] == ']' ? 1 : 0;
                    closed += packet[i] == '[' ? 1 : 0;

                    if(opened == closed)
                    {
                        finalIndex = i + 1;
                        break;
                    }
                }

            }
            return new PacketElement() {
                Value = retvalBuilder.ToString(),
                Type = type,
                EndIndex = finalIndex
            };
        }

        public int Compare(string? x, string? y)
        {
            var result = ComparePackets(x, y);
            if(result == CompareResult.Inconclusive)
            {
                return 0;
            }
            if(result == CompareResult.Right)
            {
                return -1;
            }
            return 1;
        }
    }
}