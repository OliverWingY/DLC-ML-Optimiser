using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DlcCoatingOptimiser
{
    public interface IMatlabRunner
    {
        public bool CreateModel();
        public double QueryModel(double DepositionTime, double MicrowavePower, double WorkingPressure, double GasFlowRateRatio);
        public void Dispose();
    }
}