using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeTreeAnalyzer.Entities;
using NodeTreeAnalyzer.Logic.Describer;
using System.IO;

namespace NodeTreeAnalyzer.Logic.Writer
{
    public class FileNodeWriter : INodeWriter
    {
        public async Task WriteToFileAsync(Node node, string filePath)
        {
            var describer = new TextNodeDescriber(4);
            var txtResult = describer.Describe(node);
            using (StreamWriter outputFile = new StreamWriter(filePath))
            {
                await outputFile.WriteAsync(txtResult);
            }
        }
    }
}
