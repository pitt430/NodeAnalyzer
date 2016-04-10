using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeTreeAnalyzer.Entities;
using NodeTreeAnalyzer.Logic;
using System.IO;

namespace NodeTreeAnalyzer.Logic
{
    public class FileNodeWriter : INodeWriter
    {
        private readonly INodeDescriber _nodeDescriber;
        public FileNodeWriter(INodeDescriber nodeDescriber)
        {
            _nodeDescriber = nodeDescriber;
        }
        public async Task WriteToFileAsync(Node node, string filePath)
        {
            var describer = new TextNodeDescriber();
            var txtResult = _nodeDescriber.Describe(node);
            using (StreamWriter outputFile = new StreamWriter(filePath))
            {
                await outputFile.WriteAsync(txtResult);
            }
        }
    }
}
