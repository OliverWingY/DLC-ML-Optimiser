using DlcCoatingOptimiser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;

namespace TestDlcCoatingOptimiser
{
    [TestClass]
    public class MatlabRunnerTests
    {
        private MatlabRunner matlab;

        [TestCleanup]
        public void Cleanup()
        {
            if (matlab is not null)
                matlab.Dispose();
        }

        [TestInitialize]
        public void Initialise()
        {
            matlab = new MatlabRunner();
        }

        [TestMethod]
        public void Constructor_Should_not_throw()
        {
            //arrange
            if (matlab is not null)
                matlab.Dispose();
            //act
            Action act = () => matlab = new MatlabRunner();
            //assert
            act.Should().NotThrow();
        }

        [TestMethod]
        public void Can_Create_SVM_Model()
        {
            //arrange
            //act
            var result = matlab.CreateSvmModel();
            //assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void Can_Create_Ann_Model()
        {
            //arrange
            //act
            var result = matlab.CreateAnnModel();
            //assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void Can_Query_SVM_Model_given_created()
        {
            //arrange
            matlab.CreateSvmModel();
            //act
            var result = matlab.QuerySvmModel(45,100,0.013,100);
            //assert
            result.Should().NotBe(null);
        }

        [TestMethod]
        public void Can_Query_Ann_Model_given_created()
        {
            //arrange
            matlab.CreateAnnModel();
            //act
            var result = matlab.QueryAnnModel(45, 100, 0.013, 100);
            //assert
            result.Should().NotBe(null);
        }
    }
}
