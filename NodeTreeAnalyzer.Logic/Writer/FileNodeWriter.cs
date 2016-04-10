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
            try
            {

                //Todo: create file and folder if not exists
                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                }
                var txtResult = _nodeDescriber.Describe(node);
                //todo: clear the file before write
                using (StreamWriter outputFile = File.AppendText(filePath))
                {
                    await outputFile.WriteAsync(txtResult);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }




        }
    }
}
