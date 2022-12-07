using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions.Shared
{
    public class Directory : Node
    {
        public ICollection<Node> Childs {get;}
        public Directory(String name) : base(name)
        {
            Childs = new HashSet<Node>();
        }
        public override int Size => Childs.Sum(c => c.Size);
    }
}