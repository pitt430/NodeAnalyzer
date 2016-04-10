using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NodeTreeAnalyzer.Entities;
using NodeTreeAnalyzer.Logic;
using Microsoft.Practices.Unity;

namespace NodeTreeAnalyzer.UnitTest
{
    [TestFixture]
    public class Describer_Test
    {
        private UnityContainer _container;
        [SetUp]
        public void SetupContainer()
        {
            _container = new UnityContainer();
            _container.RegisterType(typeof(INodeDescriber), typeof(TextNodeDescriber));
            var describer = _container.Resolve<TextNodeDescriber>();
        }

        [Test]
        public void Describe_NULL_OutputEmpty()
        {
            var describer = _container.Resolve<TextNodeDescriber>();
            var result = describer.Describe(null);
            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void Describe_NoChildNode_OutputNoChildNode()
        {
            var describer = _container.Resolve<TextNodeDescriber>();
            var testData =new NoChildrenNode("leaf1");
            var result = describer.Describe(testData);
            Assert.IsTrue(result!=null && result.Contains("leaf1"));
        }

        [Test]
        public void Describe_SingleChildWithOneLeaf_SingleChildWithOneLeaf()
        {
            var describer = _container.Resolve<TextNodeDescriber>();
            var testData = new SingleChildNode("root", new NoChildrenNode("leaf1"));
            var result = describer.Describe(null);
            Assert.IsTrue(result!=null && result.Contains("root") && result.Contains("leaf1"));
        }
    }
}
