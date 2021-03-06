﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeTreeAnalyzer.Entities;
using System.Collections;
using System.Reflection;

namespace NodeTreeAnalyzer.Logic
{
    public class TextNodeDescriber : DescriberBase, INodeDescriber
    {
        private  int _level;
        private readonly StringBuilder _stringBuilder;
        private int _countNodeHasChildren;
        public TextNodeDescriber()
        {
            _countNodeHasChildren = 0;
            _stringBuilder = new StringBuilder("result is:").AppendLine();
        }

        public string Describe(Node node)
        {
            var result = string.Empty;
            if (node != null)
            {
                DescribeByNodeType(node);
                result = _stringBuilder.ToString().TrimEnd().TrimEnd(',');
                for (var i = 0; i < _countNodeHasChildren; i++)
                {
                    result += ")";
                }
            }

            return result;

        }

        protected void DescribeByNodeType(Node node)
        {
            if (node != null)
            {
                var nodeType = node.GetType();
                var nodeTypeName = nodeType.Name;
                var nodeName = nodeType.GetProperty("Name").GetValue(node);
                _level++;

                PropertyInfo[] properties = nodeType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                if (properties.Length == 1)
                {
                    //the node is no child node if it only has one property which is Name
                    Write("New {0}(\"{1}\"),", nodeTypeName, nodeName);
                }
                else
                {
                    //the node has children nodes if it has more than one properties
                    Write("New {0}(\"{1}\",", nodeTypeName, nodeName);
                    _countNodeHasChildren++;
                }

                foreach (var propertyInfo in properties)
                {
                    var propertyValue = propertyInfo.GetValue(node, null);
                    if (!(propertyValue is ValueType) && !(propertyValue is string) && propertyValue!=null)
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
            
        }

        private void Write(string value, params object[] args)
        {
            var space = new string(' ', _level * IndentSize);

            if (args != null)
                value = string.Format(value, args);

            _stringBuilder.AppendLine(space + value);
        }
    }
}
