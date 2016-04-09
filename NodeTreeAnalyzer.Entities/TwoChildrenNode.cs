using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeTreeAnalyzer.Entities
{
    public class TwoChildrenNode:Node
    {
        public Node FirstNode { get; }
        public Node SecondNode { get; }
        public TwoChildrenNode(string name,Node firstNode,Node secondNode) : base(name)
        {
            FirstNode = firstNode;
            SecondNode = secondNode;
        }
    }
}
