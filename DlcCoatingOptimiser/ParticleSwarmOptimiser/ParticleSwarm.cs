using DlcCoatingOptimiser.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DlcCoatingOptimiser.ParticleSwarmOptimiser
{
    public class ParticleSwarm
    {
        private readonly IMatlabRunner MatlabRunner;
        private IEvaluator Evaluator;
        private Random rnd;
        private float bestScore;
        private Vector4 bestPosition;
        private float initialC1 = 10;
        private float finalC1 = (float)0.1;
        private float initialC2 = 10;
        private float finalC2 = (float)0.1;
        private float initialW = 10;
        private float finalW = (float)0.1;



        public ParticleSwarm(IMatlabRunner matlabRunner)
        {
            rnd = new Random();
            MatlabRunner = matlabRunner;
        }

        private List<Particle> CreateSwarm(int size)
        {
            var particles = new List<Particle>();
            for (int i =0; i < size; i++)
            {
                particles.Add(new Particle(rnd, Evaluator));
            }
            return particles;
        }

        public OptimisationResult RunOptimisation(float desiredHardness, float tolerance, float MaxIterations)
        {
            bestScore = 0;
            bestPosition = new Vector4(0,0,0,0);
            Evaluator = new SvmEvaluator(MatlabRunner, desiredHardness, tolerance);
            var particles = CreateSwarm(100);
            float c1 = 1;
            float c2 = 1;
            float w = 1;
            int i = 0;
            while (i <= MaxIterations)
            {
                i++;
                //will vary from near 0 to 1
                float progressFactor = i / MaxIterations;
                (c1, c2, w) = GetCoefficients(progressFactor);
                UpdateParticles(progressFactor, particles, c1, c2, w);
                UpdateBest(particles);
            }

            return new OptimisationResult(true, Evaluator.GetHardness(bestPosition), Evaluator.GetEnergyUsage(bestPosition), bestPosition);
        }

        private void UpdateParticles(float progressFactor, List<Particle> particles, float C1, float C2, float W)
        {
            
            foreach (var particle in particles)
            {
                particle.UpdatePosition(bestScore, bestPosition, C1, C2, W);
            }
        }

        private (float C1, float C2, float W) GetCoefficients(float progressFactor)
        {
            float C1 = initialC1 + (finalC1 - initialC1) * progressFactor;
            float C2 = initialC2 + (finalC2 - initialC2) * progressFactor;
            float W = initialW + (finalW - initialW) * progressFactor;
            return (C1, C2, W);
        }

        private void UpdateBest(List<Particle> particles)
        {
            foreach (var particle in particles)
            {
                if (particle.BestScore > bestScore)
                {
                    bestScore = particle.BestScore;
                    bestPosition = particle.BestPosition;
                }
            }
        }
    }
}
