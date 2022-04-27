using System.Numerics;

namespace DlcCoatingOptimiser.ParticleSwarmOptimiser
{
    public class OptimisationResult
    {
        public bool Converged;
        public int Iterations;
        public double FinalHardness;
        public float EnergyUsage;
        public float DepositionTime;
        public float MicrowavePower;
        public float Pressure;
        public float GasFlowRatio;
        public double FinalStandardDeviation;
        public OptimisationResult(bool converged, int iterations, double finalStandardDeviation, double hardness,  float energyUsage, Vector4 Position)
        {
            Converged = converged;
            Iterations = iterations;
            FinalHardness = hardness;
            EnergyUsage = energyUsage;
            DepositionTime = Position.X;
            MicrowavePower = Position.Y;
            Pressure = Position.Z;
            GasFlowRatio = Position.W;
            FinalStandardDeviation = finalStandardDeviation;
        }
    }
}
