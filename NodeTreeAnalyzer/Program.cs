using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeTreeAnalyzer.Entities;
using NodeTreeAnalyzer.Logic;
using NodeTreeAnalyzer.Logic.Describer;

namespace NodeTreeAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            INodeDescriber describer = new TextNodeDescriber(4);
            var testData = new SingleChildNode("root", new TwoChildrenNode("child1", new NoChildrenNode("leaf1"), new SingleChildNode("child2", new NoChildrenNode("leaf2"))));
            var result = describer.Describe(testData);
            Console.WriteLine(result);
            Console.Read();

        }
    }
}
