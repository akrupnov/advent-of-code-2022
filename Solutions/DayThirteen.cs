using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions
{
    public class DayThirteen : AbstractSolution
    {
        private enum ElementType {Integer, List}
        
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

                packetStatuses[index] = true;
                
                var currentLeftIndex = 0;
                var currentRightIndex = 0;
                var left = packetPair.First();
                var right = packetPair.Skip(1).First();
                
                while(currentLeftIndex < left.Length)
                {
                    var leftElement = TakeNextElement(left, currentLeftIndex);
                    var rightElement = TakeNextElement(right, currentRightIndex);
                    currentLeftIndex = leftElement.EndIndex + 1;
                    currentRightIndex = rightElement.EndIndex + 1;
                }
                
            }

            throw new Exception();
        }

        private PacketElement TakeNextElement(String packet, Int32 startingIndex)
        {
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
            return new PacketElement() {
                Value = retvalBuilder.ToString(),
                Type = type,
                EndIndex = finalIndex
            };
        }

        public override string SolvePartTwo()
        {
            throw new NotImplementedException();
        }
    }
}