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
          


        public ParticleSwarm(IEvaluator evaluator)
        {
            rnd = new Random();
            Evaluator = evaluator;
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

        public OptimisationResult RunOptimisation(float MaxIterations, double minStandardDeviation)
        {
            bestScore = 0;
            bestPosition = new Vector4(0,0,0,0);
            var particles = CreateSwarm(100);
            float c1 = 1;
            float c2 = 1;
            float w = 1;
            int i = 0;
            var converged = false;
            while (i <= MaxIterations || converged == true)
            {
                i++;
                //will vary from near 0 to 1
                float progressFactor = i / MaxIterations;
                (c1, c2, w) = GetCoefficients(progressFactor);
                UpdateParticles(particles, c1, c2, w);
                UpdateBest(particles);
                if (i % 10 == 0 && GetStandardDeviation(particles) < minStandardDeviation)
                    converged = true;                    
            }

            return new OptimisationResult(true, Evaluator.GetHardness(bestPosition), Evaluator.GetEnergyUsage(bestPosition), bestPosition);
        }

        private double GetStandardDeviation(List<Particle> swarm)
        {
            float averageX = swarm.Average(x=> x.Position.X);
            float averageY = swarm.Average(x => x.Position.Y);
            float averageZ = swarm.Average(x => x.Position.Z);
            float averageW = swarm.Average(x => x.Position.W);
            float sumX = swarm.Sum(x => x.Position.X);
            float sumY = swarm.Sum(x => x.Position.Y);
            float sumZ = swarm.Sum(x => x.Position.Z);
            float sumW = swarm.Sum(x => x.Position.W);
            var N = swarm.Count();
            return (Math.Sqrt(((sumX - averageX) * (sumX - averageX) / N) + Math.Sqrt(((sumY - averageY) * (sumY - averageY) / N)) + Math.Sqrt(((sumZ - averageZ) * (sumZ - averageZ) / N)) + Math.Sqrt(((sumW - averageW) * (sumW - averageW) / N))));
        }
         
        private void UpdateParticles( List<Particle> particles, float C1, float C2, float W)
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
