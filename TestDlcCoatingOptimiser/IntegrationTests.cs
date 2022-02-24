using DlcCoatingOptimiser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;
using DlcCoatingOptimiser.Interfaces;
using DlcCoatingOptimiser.ParticleSwarmOptimiser;
using System.Numerics;

namespace TestDlcCoatingOptimiser
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public void Particle_Swarm_Converges()
        {
            //arrange
            Mock<IEvaluator> mockEvaluator = new Mock<IEvaluator>();
            mockEvaluator.Setup(f => f.EvaluatePosition(It.IsAny<Vector4>())).Returns((Vector4 vector) => Evaluate(vector));
            mockEvaluator.Setup(f => f.GetHardness(It.IsAny<Vector4>())).Returns(1);
            mockEvaluator.Setup(f => f.GetEnergyUsage(It.IsAny<Vector4>())).Returns(1);
            var PSO = new ParticleSwarm(mockEvaluator.Object);
            //act
            var result = PSO.RunOptimisation(100, 0.1);
            //assert
            result.Converged.Should().BeTrue();
            result.MicrowavePower.Should().Be(0);
            result.Pressure.Should().Be(0);
            result.DepositionTime.Should().Be(0);
            result.GasFlowRatio.Should().Be(0);
        }

        private float Evaluate(Vector4 position)
        {
            return ((1- position.X) + (1- position.Y + (1- position.Z) + (1- position.W)));
        }


    }
}
