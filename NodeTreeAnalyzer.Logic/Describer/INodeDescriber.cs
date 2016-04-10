using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeTreeAnalyzer.Entities;

namespace NodeTreeAnalyzer.Logic
{
    public interface INodeDescriber
    {
        string Describe(Node node);
    }
}
