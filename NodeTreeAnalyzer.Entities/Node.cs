using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeTreeAnalyzer.Entities
{
    public abstract class Node
    {
        public string Name { get; }
        protected Node(string name)
        {
            Name = name;
        }
    }
}
