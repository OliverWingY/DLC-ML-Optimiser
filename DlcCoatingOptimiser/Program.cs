using System;
using System.Linq;
using DlcCoatingOptimiser.ParticleSwarmOptimiser;

namespace DlcCoatingOptimiser
{
    
    public class Program
    {
        //todo: make these args
        private static float desiredHardness = (float)5;
        private static float hardnessTolerance = (float)0.1;
        private static float maxIterations = (float)50;
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
                Console.WriteLine($"Final Hardness: {result.FinalHardness}");
                Console.WriteLine($"Final Energy usage: {result.EnergyUsage} W");
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
