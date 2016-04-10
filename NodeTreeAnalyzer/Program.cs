using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeTreeAnalyzer.Entities;
using NodeTreeAnalyzer.Logic;
using System.IO;
using Microsoft.Practices.Unity;

namespace NodeTreeAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            //INodeDescriber describer = new TextNodeDescriber(4);
            //var testData = new SingleChildNode("root", new TwoChildrenNode("child1", new NoChildrenNode("leaf1"), new SingleChildNode("child2", new NoChildrenNode("leaf2"))));
            var testData = new SingleChildNode("root", new TwoChildrenNode("child1", new NoChildrenNode("leaf1"), new SingleChildNode("child2", null)));
            // testData = new SingleChildNode("root", null);

            //var result = describer.Describe(testData);
            //Console.WriteLine(result);


            //INodeDescriber describer2 = new TextNodeDescriber(4);
            //INodeTransformer transformer = new TypeNodeTransformer();
            //var transformedData=transformer.Transform(testData);
            //var transformedresult=describer2.Describe(transformedData);
            //Console.WriteLine(transformedresult);


            //INodeWriter writer = new FileNodeWriter();
            //var filePath = @"C:\Work\Repository\NodeTreeAnalyzer\test.txt";
            //writer.WriteToFileAsync(testData,filePath );
            //var persistedText = File.ReadAllText(filePath);
            //Console.WriteLine(persistedText);

            //configure
            var container = new UnityContainer();
            container.RegisterType(typeof(INodeDescriber), typeof(TextNodeDescriber));
            container.RegisterType(typeof(INodeTransformer), typeof(TypeNodeTransformer));
            container.RegisterType(typeof(INodeWriter), typeof(FileNodeWriter));

            var describer = container.Resolve<TextNodeDescriber>();
            Console.WriteLine(describer.Describe(testData));

            var writer = container.Resolve<FileNodeWriter>();
            var filePath = @"C:\Work\Repository\NodeTreeAnalyzer\test.txt";
            writer.WriteToFileAsync(testData, filePath);
            var persistedText = File.ReadAllText(filePath);
            Console.WriteLine(persistedText);

            Console.Read();


        }
    }
}
