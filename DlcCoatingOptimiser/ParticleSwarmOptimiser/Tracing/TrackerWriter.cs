using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using DlcCoatingOptimiser.Interfaces;

namespace DlcCoatingOptimiser.ParticleSwarmOptimiser.Tracing
{
    public class TrackerWriter
    {
        private List<TrackerParticle> trackerParticles;
        private IEvaluator evaluator;
        //todo: make this a config file
        private string fileLocation = @"C:\Users\44742\Documents\Uni year 3\Individual project\Tracking\";
        private int numberOfTrackers;
        public TrackerWriter(int TotalNumberParticles, IEvaluator Evaluator)
        {
            evaluator = Evaluator; 
            trackerParticles = new List<TrackerParticle>();
            numberOfTrackers = (int)Math.Round((double)TotalNumberParticles / 10);
            for (int i = 0; i < numberOfTrackers; i++)
            {
                trackerParticles.Add(new TrackerParticle { Id = i, Positions = new List<Position>() }) ;
            }
        }

        public void WriteResults()
        {
            foreach (TrackerParticle particle in trackerParticles)
            {
                IEnumerable<Position> records = particle.Positions;
                string fileName = $"{fileLocation}Tracker{particle.Id}.csv  ";
                using (var writer = new StreamWriter(fileName))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<PositionMap>();
                    csv.WriteRecords(records);
                }
            }            
        }
        public void UpdateTrackerParticles(List<Particle> particles, int iteration)
        {
            for (int i = 0; i < numberOfTrackers; i++)
            {
                trackerParticles[i].Positions.Add(GetPosition(iteration, particles[i]));
            };
        }

        private Position GetPosition(int iteration, Particle particle)
        {
            var nomalisedVector = evaluator.NormaliseParticlePosition(particle.Position);
            return new Position { Iteration = iteration, Time = nomalisedVector.X, MicrowavePower = nomalisedVector.Y, PartialPressure = nomalisedVector.Z, GasFlowRatio = nomalisedVector.W };
        }
    }
}
