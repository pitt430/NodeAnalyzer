using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeTreeAnalyzer.Entities;
using System.Collections;
using System.Reflection;

namespace NodeTreeAnalyzer.Logic.Describer
{
    public class TextNodeDescriber : INodeDescriber
    {
        private  int _level;
        private readonly int _indentSize;
        private readonly StringBuilder _stringBuilder;
        private int _countNodeHasChildren;
        public TextNodeDescriber(int indentSize)
        {
            _indentSize = indentSize;
            _stringBuilder = new StringBuilder("result is:").AppendLine();
            _countNodeHasChildren = 0;
        }

        public string Describe(Node node)
        {
            DescribeByNodeType(node);
            var result = _stringBuilder.ToString().TrimEnd().TrimEnd(',');
            for (var i = 0; i < _countNodeHasChildren; i++)
            {
                result+=")";
            }
            return result;

        }

        public void DescribeByNodeType(Node node)
        {
            var nodeType = node.GetType();
            var nodeTypeName = nodeType.Name;
            var nodeName = nodeType.GetProperty("Name").GetValue(node);
            _level++;

            PropertyInfo[] properties = nodeType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (properties.Length == 1)
            {
                Write("New {0}(\"{1}\"),", nodeTypeName, nodeName);
            }
            else 
            {
                Write("New {0}(\"{1}\",", nodeTypeName, nodeName);
                _countNodeHasChildren++;
            }
                       

            foreach (var propertyInfo in properties)
            {
                var propertyValue = propertyInfo.GetValue(node, null);
                if (!(propertyValue is ValueType) && !(propertyValue is string))
                {
                    var isEnumerable = typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType);
                    if (isEnumerable)
                    {
                        foreach (var item in propertyValue as IEnumerable<Node>)
                        {
                            _level++;
                            DescribeByNodeType(item);
                            _level--;
                        }
                    }
                    else
                    {
                        DescribeByNodeType(propertyValue as Node);
                    }
                }

            }
        }

        private void Write(string value, params object[] args)
        {
            var space = new string(' ', _level * _indentSize);

            if (args != null)
                value = string.Format(value, args);

            _stringBuilder.AppendLine(space + value);
        }
    }
}
