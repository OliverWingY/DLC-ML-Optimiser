using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DlcCoatingOptimiser
{
    public interface IMatlabRunner
    {
        public bool CreateSvmModel();
        public double QuerySvmModel(double DepositionTime, double MicrowavePower, double WorkingPressure, double GasFlowRateRatio);
        public bool CreateAnnModel();
        public double QueryAnnModel(double DepositionTime, double MicrowavePower, double WorkingPressure, double GasFlowRateRatio);

        public void Dispose();
    }
}