using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeTreeAnalyzer.Entities;
using System.Reflection;
using System.Collections;

namespace NodeTreeAnalyzer.Logic.Transformer
{
    public class TypeNodeTransformer : INodeTransformer
    {
        public Node Transform(Node node)
        {

            return CreateTransformedNode(node);
        }

        public Node CreateTransformedNode(Node originNode)
        {
            var nodeType = originNode.GetType();
            //Check the real type of node
            var properties = nodeType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var isManyChild = false;
            var nodeCount = 0;
            var nodeName = string.Empty;
            IList<Node> children;

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
                                if (item.GetType().Equals(nodeClassType) && item != null)
                                {
                                    nodeCount++;
                                }
                            }
                        }

                        if (propertyInfo.PropertyType.IsAssignableFrom(nodeClassType))
                        {
                            nodeCount++;
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
                    originNode = new SingleChildNode(nodeName, CreateTransformedNode(properties[1].GetValue(originNode, null) as Node));
                    break;
                case 2:
                    originNode = new TwoChildrenNode(nodeName, CreateTransformedNode((properties[1].GetValue(originNode, null) as Node)), CreateTransformedNode((properties[2].GetValue(originNode, null) as Node)));
                    break;

                default:

                    break;
            }

            return originNode;

        }
    }
}