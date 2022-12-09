using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions.Shared
{
    #pragma warning disable 8618

    public abstract class Node
    {
        public Node(String name)
        {
            this.Name = name;
            
        }
        public abstract Int32 Size {get;}
        public String Name {get;set;}

        public Node Parent {get;set;}
    }
}