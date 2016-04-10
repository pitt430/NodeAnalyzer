using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace NodeTreeAnalyzer.Logic
{
    public abstract class DescriberBase
    {
        private int _indentSize;
        public int IndentSize
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["IndentSize"]); }
            set { _indentSize = value; }
        }
    }
}
