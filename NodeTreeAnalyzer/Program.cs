using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeTreeAnalyzer.Entities;
using NodeTreeAnalyzer.Logic;
using NodeTreeAnalyzer.Logic.Describer;
using NodeTreeAnalyzer.Logic.Transformer;

namespace NodeTreeAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            INodeDescriber describer = new TextNodeDescriber(4);
            //var testData = new SingleChildNode("root", new TwoChildrenNode("child1", new NoChildrenNode("leaf1"), new SingleChildNode("child2", new NoChildrenNode("leaf2"))));
            var testData = new SingleChildNode("root", new TwoChildrenNode("child1", new NoChildrenNode("leaf1"), new SingleChildNode("child2", null)));
            // testData = new SingleChildNode("root", null);

            var result = describer.Describe(testData);
            Console.WriteLine(result);


            INodeDescriber describer2 = new TextNodeDescriber(4);
            INodeTransformer transformer = new TypeNodeTransformer();
            var transformedData=transformer.Transform(testData);
            var transformedresult=describer2.Describe(transformedData);
            Console.WriteLine(transformedresult);
            Console.Read();

        }
    }
}
