using System;
using System.Numerics;
using DlcCoatingOptimiser.Interfaces;
using System.Runtime.CompilerServices;
[assembly:InternalsVisibleTo("IntegrationTests")]

namespace DlcCoatingOptimiser.ParticleSwarmOptimiser
{
    public class Particle
    {
        //system is x y z w: Time MicrowavePower WorkingPressure GasFlowRateRatio, normalised into 1x1x1x1 space        
        internal Vector4 Position;
        private Vector4 Velocity;
        private IEvaluator Evaluator;
        public Vector4 BestPosition { get; private set; }
        public float BestScore { get; private set; }
        private Random rnd;
        public Particle(Random random, IEvaluator evaluator)
        {
            rnd = random;
            Evaluator = evaluator;
            Position = new Vector4((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble());
            Velocity = new Vector4((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble());
            BestPosition = Position;
            BestScore = evaluator.EvaluatePosition(Position);
        }

        public void UpdatePosition(float bestGlobalScore, Vector4 bestGlobalPosition, float c1, float c2, float w)
        {
            var r1 = (float)rnd.NextDouble();
            var r2 = (float)rnd.NextDouble();
            var cognitiveAcceleration = FindCognitiveAcceleration(c1, r1);
            var globalAcceleration = FindGlobalAcceleration(bestGlobalPosition, c2, r2);
            Velocity = Velocity.Multiply(w) + globalAcceleration + cognitiveAcceleration;
            Position = Position + Velocity;
            BindPosition();
            UpdateScore();
        }

        private Vector4 FindGlobalAcceleration(Vector4 bestGlobalPosition, float c2, float r2)
        {
            var acceleration = bestGlobalPosition - Position;
            return acceleration * r2 * c2;
        }

        private Vector4 FindCognitiveAcceleration(float c1, float r1)
        {
            var acceleration = BestPosition - Position;
            return acceleration * r1 * c1;
        }

        private void UpdateScore()
        {
            var currentScore = Evaluator.EvaluatePosition(Position);
            if (currentScore > BestScore)
            {
                BestPosition = Position;
                BestScore = currentScore;
            }
        }

        private void BindPosition()
        {
            if (Position.X > 1)
            {
                Position.X = 1;
                Velocity.X = 0;
            }
            else if (Position.X < 0)
            {
                Position.X = 0;
                Velocity.X = 0;
            }
            if (Position.Y > 1)
            {
                Position.Y = 1;
                Velocity.Y = 0;
            }
            else if (Position.Y < 0)
            {
                Position.Y = 0;
                Velocity.Y = 0;
            }
            if (Position.Z > 1)
            {
                Position.Z = 1;
                Velocity.Z = 0;
            }
            else if (Position.Z < 0)
            {
                Position.Z = 0;
                Velocity.Z = 0;
            }
            if (Position.Y > 1)
            {
                Position.Y = 1;
                Velocity.Y = 0;
            }
            else if (Position.Y < 0)
            {
                Position.Y = 0;
                Velocity.Y = 0;
            }
            if (Position.W > 1)
            {
                Position.W = 1;
                Velocity.W = 0;
            }
            else if (Position.W < 0)
            {
                Position.W = 0;
                Velocity.W = 0;
            }
        }
    }
}
