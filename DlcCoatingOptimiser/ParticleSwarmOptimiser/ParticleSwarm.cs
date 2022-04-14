using DlcCoatingOptimiser.Interfaces;
using DlcCoatingOptimiser.ParticleSwarmOptimiser.Tracing;
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
        private TrackerWriter tracker;
        private float bestScore;
        private Vector4 bestPosition;
        private float initialC1 = (float)0.8;
        private float finalC1 = (float)0.1;
        private float initialC2 = (float)0.1;
        private float finalC2 = (float)1;
        private float initialW = (float)0.8;
        private float finalW = (float)0.1;

        private bool Tracking;


        public ParticleSwarm(IEvaluator evaluator, bool tracking)
        {
            rnd = new Random();
            Evaluator = evaluator;
            Tracking = tracking;

        }

        private List<Particle> CreateSwarm(int size)
        {
            var particles = new List<Particle>();
            for (int i =0; i < size; i++)
            {
                particles.Add(new Particle(rnd, Evaluator));
            }            

            if (Tracking)
                InitialiseTracker(size);
            return particles;
        }


        public OptimisationResult RunOptimisation(float MaxIterations, double minStandardDeviation)
        {
            bestScore = 0;
            bestPosition = new Vector4(0,0,0,0);
            var particles = CreateSwarm(100);
            float c1;
            float c2;
            float w;
            int i = 0;
            var converged = false;
            while (i <= MaxIterations && converged == false)
            {
                i++;
                //will vary from near 0 to 1
                float progressFactor = i / MaxIterations;
                (c1, c2, w) = GetCoefficients(progressFactor);
                UpdateParticles(particles, c1, c2, w);
                UpdateBest(particles);
                if (GetStandardDeviation(particles) < minStandardDeviation)
                    converged = true;                    
                if (Tracking)                
                    RecordPositions(particles, i);                
            }
            if (Tracking)
                WriteResults();
            return new OptimisationResult(converged, GetStandardDeviation(particles), Evaluator.GetHardness(bestPosition), Evaluator.GetEnergyUsage(bestPosition), bestPosition);
        }

        private double GetStandardDeviation(List<Particle> swarm)
        {
            //sigma = sqrt(SIGMA((Xi - mu)^2/N)
            float averageX = swarm.Average(x=> x.Position.X);
            float averageY = swarm.Average(x => x.Position.Y);
            float averageZ = swarm.Average(x => x.Position.Z);
            float averageW = swarm.Average(x => x.Position.W);
            double xVariance = 0;
            double yVariance = 0;
            double zVariance = 0;
            double wVariance = 0;
            foreach (Particle particle in swarm)
            {
                xVariance = xVariance + (particle.Position.X - averageX)* (particle.Position.X - averageX);
                yVariance = yVariance + (particle.Position.Y - averageY) * (particle.Position.Y - averageY);
                zVariance = zVariance + (particle.Position.Z - averageZ) * (particle.Position.Z - averageZ);
                wVariance = wVariance + (particle.Position.W - averageW) * (particle.Position.W - averageW);
            }
            var N = swarm.Count();
            var standardDeviation = Math.Sqrt(xVariance/ N + yVariance / N + zVariance / N + wVariance / N);
            return standardDeviation;
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

        private void WriteResults()
        {
            tracker.WriteResults();
        }

        private void RecordPositions(List<Particle> particles, int iteration)
        {
            tracker.UpdateTrackerParticles(particles, iteration);
        }     

        private void InitialiseTracker(int swarmSize)
        {
            tracker = new TrackerWriter(swarmSize, Evaluator); 
        }
    }
}
