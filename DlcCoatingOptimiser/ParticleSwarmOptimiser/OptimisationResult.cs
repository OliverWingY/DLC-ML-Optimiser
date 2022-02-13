using System.Numerics;

namespace DlcCoatingOptimiser.ParticleSwarmOptimiser
{
    public class OptimisationResult
    {
        public bool Converged;
        public double FinalHardness;
        public float EnergyUsage;
        public float DepositionTime;
        public float MicrowavePower;
        public float Pressure;
        public float GasFlowRatio;
        public OptimisationResult(bool converged, double hardness,  float energyUsage, Vector4 Position)
        {
            Converged = converged;
            FinalHardness = hardness;
            EnergyUsage = energyUsage;
            DepositionTime = Position.X;
            MicrowavePower = Position.Y;
            Pressure = Position.Z;
            GasFlowRatio = Position.W;
        }
    }
}
