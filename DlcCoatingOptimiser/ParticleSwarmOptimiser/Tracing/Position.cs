using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DlcCoatingOptimiser.ParticleSwarmOptimiser.Tracing
{
    internal class Position
    {
        internal float Iteration;
        internal float MicrowavePower;
        internal float Time;
        internal float PartialPressure;
        internal float GasFlowRatio;
    }

    internal sealed class PositionMap : ClassMap<Position>
    {
        public PositionMap()
        {
            Map(m => m.Iteration);
            Map(m => m.MicrowavePower);
            Map(m => m.Time);
            Map(m => m.PartialPressure);
            Map(m => m.GasFlowRatio);
        }
    }
}
