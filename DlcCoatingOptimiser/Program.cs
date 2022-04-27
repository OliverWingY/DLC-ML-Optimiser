using System;
using System.Linq;
using DlcCoatingOptimiser.ParticleSwarmOptimiser;

namespace DlcCoatingOptimiser
{
    
    public class Program
    {
        //todo: make these args
        private static float desiredHardness = (float)6;
        private static float hardnessTolerance = (float)0.1;
        private static float maxIterations = (float)100;
        private static double minStandardDeviation = 0.05;
        private static IMatlabRunner MatlabRunner;
        private static ParticleSwarm ParticleSwarm;
        public static void Main(string[] args)
        {
            Initialise_App();
            CreateAnnModel();
            var evaluator = new Evaluator(MatlabRunner, desiredHardness, hardnessTolerance);
            ParticleSwarm = new ParticleSwarm(evaluator, true);
            var result = ParticleSwarm.RunOptimisation(maxIterations, minStandardDeviation);

            if (result.Converged)
            {
                Console.WriteLine($"Converged in {result.Iterations} iterations");
                Console.WriteLine($"Final Hardness: {result.FinalHardness}");
                Console.WriteLine($"Final Energy usage: {result.EnergyUsage} W");
                Console.WriteLine($"Microwave Power (W): {result.MicrowavePower.ToString("G4")}");
                Console.WriteLine($"Pressure (mbar): {result.Pressure.ToString("G4")}");
                Console.WriteLine($"Deposition Time (min): {result.DepositionTime.ToString("G2")}");
                Console.WriteLine($"Gas Flow Ratio (%): {result.GasFlowRatio.ToString("G3")}");
                
            }
            else Console.WriteLine($"Failed to converge, final standard deviation: {result.FinalStandardDeviation}");
            
            MatlabRunner.Dispose();
        }

        private static void Initialise_App()
        {
            MatlabRunner = new MatlabAnnRunner();            
        }

        private static void CreateAnnModel()
        {
            MatlabRunner.CreateModel();
        }
          
    }
}
