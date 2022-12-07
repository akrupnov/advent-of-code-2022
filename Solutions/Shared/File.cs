using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutions.Shared
{
    public class File : Node
    {
        private readonly Int32 size;
        public File(String name, Int32 size) : base(name)
        {
            this.size = size;            
        }

        public override int Size => size;
    }
}