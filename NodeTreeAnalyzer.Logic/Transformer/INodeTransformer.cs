using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeTreeAnalyzer.Entities;


namespace NodeTreeAnalyzer.Logic
{
    public interface INodeTransformer
    {
        Node Transform(Node node);
    }
}
