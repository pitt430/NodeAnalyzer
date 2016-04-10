using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using NodeTreeAnalyzer.Entities;
using NodeTreeAnalyzer.Logic;
using NUnit.Framework;

namespace NodeTreeAnalyzer.UnitTest
{
    [TestFixture]
    public class Transformer_Test
    {
        private UnityContainer _container;
        [SetUp]
        public void Init()
        {
            _container = new UnityContainer();
            _container.RegisterType(typeof(INodeTransformer), typeof(TypeNodeTransformer));
        }

        [Test]
        public void Transform_NULL_OutputEmpty()
        {
            var transformer = _container.Resolve<TypeNodeTransformer>();
            var result = transformer.Transform(null);
            Assert.AreEqual(null, result);
        }

        [Test]
        public void Transform_NoChildNode_ReturnChildNode()
        {
            var Transformr = _container.Resolve<TypeNodeTransformer>();
            var testData = new NoChildrenNode("leaf1");
            var result = Transformr.Transform(testData);
            Assert.IsTrue(result != null && result.GetType().Name== "NoChildrenNode");
        }

        [Test]
        public void Transform_SingleChildWithNull_ReturnNoChildNode()
        {
            var Transformr = _container.Resolve<TypeNodeTransformer>();
            var testData = new SingleChildNode("root", null);
            var result = Transformr.Transform(testData);
            Assert.IsTrue(result != null && result.GetType().Name == "NoChildrenNode");
        }

        [Test]
        public void Transform_TwoChildWithNull_ReturnNoChildNode()
        {
            var Transformr = _container.Resolve<TypeNodeTransformer>();
            var testData = new TwoChildrenNode("root",
                null,
                null);
            var result = Transformr.Transform(testData);
            Assert.IsTrue(result != null && result.GetType().Name == "NoChildrenNode");
        }

        [Test]
        public void Transform_ManyChildWithNull_ReturnNoChildNode()
        {
            var Transformr = _container.Resolve<TypeNodeTransformer>();
            var testData = new ManyChildrenNode("root",
                null,
                null, null);
            var result = Transformr.Transform(testData);
            Assert.IsTrue(result != null && result.GetType().Name == "NoChildrenNode");
        }

        [Test]
        public void Transform_ManyChildWithOne_ReturnSingleChildNode()
        {
            var Transformr = _container.Resolve<TypeNodeTransformer>();
            var testData = new ManyChildrenNode("root", new NoChildrenNode("leaf1"));
            var result = Transformr.Transform(testData);
            Assert.IsTrue(result != null && result.GetType().Name == "SingleChildNode");
        }

        [Test]
        public void Transform_ManyChildWithTwo_ReturnTwoChildNode()
        {
            var Transformr = _container.Resolve<TypeNodeTransformer>();
            var testData = new ManyChildrenNode("root", new NoChildrenNode("leaf1"), new NoChildrenNode("leaf2"));
            var result = Transformr.Transform(testData);
            Assert.IsTrue(result != null && result.GetType().Name == "TwoChildrenNode");
        }

        [Test]
        public void Transform_ManyChildWithThree_ReturnManyChildNode()
        {
            var Transformr = _container.Resolve<TypeNodeTransformer>();
            var testData = new ManyChildrenNode("root", new NoChildrenNode("leaf1"), new NoChildrenNode("leaf2"),new NoChildrenNode("leaf3"));
            var result = Transformr.Transform(testData);
            Assert.IsTrue(result != null && result.GetType().Name == "ManyChildrenNode");
        }

    }
}
