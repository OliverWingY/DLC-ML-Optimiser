using DlcCoatingOptimiser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;

namespace TestDlcCoatingOptimiser
{
    [TestClass]
    public class MatlabSvmRunnerTests
    {
        private MatlabSvmRunner matlab;

        [TestCleanup]
        public void Cleanup()
        {
            if (matlab is not null)
                matlab.Dispose();
        }

        [TestInitialize]
        public void Initialise()
        {
            matlab = new MatlabSvmRunner();
        }

        [TestMethod]
        public void Constructor_Should_not_throw()
        {
            //arrange
            if (matlab is not null)
                matlab.Dispose();
            //act
            Action act = () => matlab = new MatlabSvmRunner();
            //assert
            act.Should().NotThrow();
        }

        [TestMethod]
        public void Can_Create_SVM_Model()
        {
            //arrange
            //act
            var result = matlab.CreateModel();
            //assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void Can_Query_SVM_Model_given_created()
        {
            //arrange
            matlab.CreateModel();
            //act
            var result = matlab.QueryModel(45,100,0.013,100);
            //assert
            result.Should().NotBe(null);
        }

    }
    [TestClass]
    public class MatlabAnnRunnerTests
    {
        private MatlabAnnRunner matlab;

        [TestCleanup]
        public void Cleanup()
        {
            if (matlab is not null)
                matlab.Dispose();
        }

        [TestInitialize]
        public void Initialise()
        {
            matlab = new MatlabAnnRunner();
        }

        [TestMethod]
        public void Constructor_Should_not_throw()
        {
            //arrange
            if (matlab is not null)
                matlab.Dispose();
            //act
            Action act = () => matlab = new MatlabAnnRunner();
            //assert
            act.Should().NotThrow();
        }

        [TestMethod]
        public void Can_Query_Ann_Model_given_created()
        {
            //arrange
            matlab.CreateModel();
            //act
            var result = matlab.QueryModel(45, 100, 0.013, 100);
            //assert
            result.Should().NotBe(null);
        }

        [TestMethod]
        public void Can_Create_Ann_Model()
        {
            //arrange
            //act
            var result = matlab.CreateModel();
            //assert
            result.Should().BeTrue();
        }
    }
}
