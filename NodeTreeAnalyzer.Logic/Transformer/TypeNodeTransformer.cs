using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeTreeAnalyzer.Entities;
using System.Reflection;
using System.Collections;

namespace NodeTreeAnalyzer.Logic
{
    public class TypeNodeTransformer : INodeTransformer
    {
        public Node Transform(Node node)
        {
            if (node != null)
            {
                return CreateTransformedNode(node);
            }
            return null;
        }

        public Node CreateTransformedNode(Node originNode)
        {
            var myList = new List<Node>();
            var nodeType = originNode.GetType();
            //Check the real type of node
            var properties = nodeType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var isManyChild = false;
            var nodeCount = 0;
            var nodeName = string.Empty;

            foreach (var propertyInfo in properties)
            {
                var propertyValue = propertyInfo.GetValue(originNode, null);
                if(propertyValue != null)
                {
                    if (!(propertyValue is ValueType) && !(propertyValue is string))
                    {
                        var nodeClassType = typeof(Node);
                        //if node has IEnumerable type
                        isManyChild = typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType);
                        if (isManyChild)
                        {
                            foreach (var item in propertyValue as IEnumerable<Node>)
                            {
                                if (  item != null && nodeClassType.IsAssignableFrom(item.GetType()))
                                {
                                    nodeCount++;
                                    myList.Add(item);
                                }
                            }
                        }

                        if (nodeClassType.IsAssignableFrom(propertyInfo.PropertyType))
                        {
                            nodeCount++;
                            myList.Add(propertyValue as Node);
                        }
                    }
                    else
                    {
                        nodeName = propertyValue.ToString();
                    }
                }
            }



            switch (nodeCount)
            {
                case 0:
                    originNode = new NoChildrenNode(nodeName);
                    break;
                case 1:
                    originNode = new SingleChildNode(nodeName, CreateTransformedNode(myList[0]));
                    break;
                case 2:
                    originNode = new TwoChildrenNode(nodeName, CreateTransformedNode(myList[0]), CreateTransformedNode(myList[1]));
                    break;

                default:

                    break;
            }

            return originNode;

        }
    }
}