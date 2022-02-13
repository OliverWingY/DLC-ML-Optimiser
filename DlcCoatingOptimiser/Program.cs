using System;
using System.Linq;
using DlcCoatingOptimiser.ParticleSwarmOptimiser;

namespace DlcCoatingOptimiser
{
    
    public class Program
    {
        private static IMatlabRunner MatlabRunner;
        private static ParticleSwarm ParticleSwarm;
        public static void Main(string[] args)
        {
            Initialise_App();
            CreateSvmModel();
            ParticleSwarm = new ParticleSwarm(MatlabRunner);
            var result = ParticleSwarm.RunOptimisation((float)3.65, (float)0.5, 1000);
            if (result.Converged)
            {
                Console.WriteLine($"Final Hardness: {result.FinalHardness}");
                Console.WriteLine($"Final Energy usage: {result.EnergyUsage} W");
            }
            else Console.WriteLine("Failed to converge");
            MatlabRunner.Dispose();
        }

        private static void Initialise_App()
        {
            MatlabRunner = new MatlabRunner();            
        }

        private static void CreateSvmModel()
        {
            MatlabRunner.CreateSvmModel();
        }
          
    }
}
