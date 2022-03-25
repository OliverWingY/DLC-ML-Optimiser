using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DlcCoatingOptimiser.Interfaces
{
    public interface IEvaluator
    {
        public float EvaluatePosition(Vector4 position);
        public double GetHardness(Vector4 position);
        public float GetEnergyUsage(Vector4 Postion);
        public Vector4 NormaliseParticlePosition(Vector4 vector);
    }
}
