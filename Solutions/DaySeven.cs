using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Solutions.Shared;

namespace Solutions
{
    public class DaySeven : AbstractSolution
    {
        public DaySeven(string input) : base(input)
        {
        }

        public override string SolvePartOne()
        {
            Dictionary<string, Node> flatNodes = GetFlatNodes();

            return SumDirectoriesUnderSize((flatNodes["/"] as Shared.Directory).Childs.OfType<Shared.Directory>(), 100000).ToString();
        }

        private Dictionary<string, Node> GetFlatNodes()
        {
            Shared.Directory currentParent = null;
            var flatNodes = new Dictionary<String, Shared.Node>() { { "/", new Shared.Directory("/") } };


            foreach (var statement in Input.Split(Environment.NewLine))
            {
                var statementArguments = statement.Split(" ");
                if (statementArguments[0] == "$")
                {
                    if (statementArguments[1] == "cd")
                    {
                        if (statementArguments[2] != "..")
                        {
                            var newDirectory = currentParent != null ? $"{currentParent.Name}/{statementArguments[2]}".Replace("//", "/") : statementArguments[2];
                            currentParent = flatNodes[newDirectory] as Shared.Directory;
                        }
                        else
                        {
                            currentParent = currentParent.Parent as Shared.Directory;
                        }
                    }
                }
                else
                {
                    var nodeDescriptor = statement.Split(" ");
                    var nodeName = $"{currentParent.Name}/{nodeDescriptor[1]}".Replace("//", "/");
                    Shared.Node node;
                    if (flatNodes.ContainsKey(nodeName))
                    {
                        node = flatNodes[nodeName];
                    }
                    else
                    {
                        if (nodeDescriptor[0] == "dir")
                        {
                            node = new Shared.Directory(nodeName);
                        }
                        else
                        {
                            node = new Shared.File(nodeName, Convert.ToInt32(nodeDescriptor[0]));
                        }
                        node.Parent = currentParent;
                        currentParent.Childs.Add(node);
                        flatNodes.Add(nodeName, node);
                    }

                }
            }

            return flatNodes;
        }

        private Int32 SumDirectoriesUnderSize(IEnumerable<Shared.Directory> directories, int sizeThreshold)
        {
            if(!directories.Any())
            {
                return 0;
            }
            return directories.Where(x => x.Size <= sizeThreshold).Sum(x => x.Size) + SumDirectoriesUnderSize(directories.SelectMany(x => x.Childs.OfType<Shared.Directory>()), sizeThreshold);
        }

        public override string SolvePartTwo()
        {
            Dictionary<string, Node> flatNodes = GetFlatNodes();
            var root = (flatNodes["/"] as Shared.Directory);

            return GetOversizedDirectories(root.Childs.OfType<Shared.Directory>(), 30000000 - (70000000 - root.Size)).OrderBy(x => x.Size).First().Size.ToString();
        }

        private IEnumerable<Shared.Directory> GetOversizedDirectories(IEnumerable<Shared.Directory> directories, int sizeThreshold)
        {
            if(!directories.Any())
            {
                return directories;
            }
            return Enumerable.Concat(directories.Where(x => x.Size >= sizeThreshold), GetOversizedDirectories(directories.SelectMany(x => x.Childs.OfType<Shared.Directory>()), sizeThreshold));    
        }
    }
}