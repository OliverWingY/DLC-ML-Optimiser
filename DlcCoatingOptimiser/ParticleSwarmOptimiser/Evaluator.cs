using DlcCoatingOptimiser.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DlcCoatingOptimiser.ParticleSwarmOptimiser
{
    public class Evaluator : IEvaluator
    {
        private IMatlabRunner MatlabRunner;
        private float DesiredHardness;
        private float Tolerance;
        //system is x y z w: Time MicrowavePower WorkingPressure GasFlowRateRatio
        //Ranges: Time 36-50 power 900-1200 WorkingPressure 0.011-0.013 GasFlowRatio 10-100
        private Vector4 normalisationScaleVector = new Vector4(14, 300, (float)0.002, 90);
        private Vector4 normalisationOffsetVector = new Vector4(36,900, (float)0.011, 10);
        public Evaluator(IMatlabRunner matlabRunner, float desiredHardness, float tolerance)
        {
            DesiredHardness = desiredHardness;
            Tolerance = tolerance;
            MatlabRunner = matlabRunner;
        }
        public float EvaluatePosition(Vector4 position)
        {            
            var trueSettings = NormaliseParticlePosition(position);
            //Todo; Create new interface that can be either Svm or Ann, and use QueryModel
            var hardness = MatlabRunner.QueryModel((double)trueSettings.X, (double)trueSettings.Y, (double)trueSettings.Z, (double)trueSettings.W);
            //may want to redo this to include other factors and be generally more complex
            var energyUsage = GetEnergyUsage(trueSettings);
            if (hardness > DesiredHardness + Tolerance || hardness < DesiredHardness - Tolerance)
            {
                return 1 / energyUsage;
            }
            else
                return 0;
        }    
        
        public double GetHardness(Vector4 position)
        {
            var trueSettings = NormaliseParticlePosition(position);
            return MatlabRunner.QueryModel((double)trueSettings.X, (double)trueSettings.Y, (double)trueSettings.Z, (double)trueSettings.W);
        }

        public float GetEnergyUsage(Vector4 trueSettings)
        {
            return trueSettings.X * 60 * trueSettings.Y; 
        }

        private Vector4 NormaliseParticlePosition(Vector4 vector)
        {
            return vector * normalisationScaleVector + normalisationOffsetVector;
        }        
    }
}
