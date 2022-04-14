using DlcCoatingOptimiser.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;


namespace DlcCoatingOptimiser.ParticleSwarmOptimiser
{
    public class Evaluator : IEvaluator
    {
        private IMatlabRunner MatlabRunner;
        private float DesiredHardness;
        private float Tolerance;
        private float auxilaryLoads = 500;
        //system is x y z w: Time MicrowavePower WorkingPressure GasFlowRateRatio
        //Ranges: Time 36-50 power 1000-1200 WorkingPressure 0.009-0.013 GasFlowRatio 40-100
        private Vector4 normalisationScaleVector = new Vector4(14, 200, (float)0.004, 60);
        private Vector4 normalisationOffsetVector = new Vector4(36,1000, (float)0.009, 40);
        public Evaluator(IMatlabRunner matlabRunner, float desiredHardness, float tolerance)
        {
            DesiredHardness = desiredHardness;
            Tolerance = tolerance;
            MatlabRunner = matlabRunner;
        }
        public float EvaluatePosition(Vector4 position)
        {            
            var trueSettings = NormaliseParticlePosition(position);
            var hardness = MatlabRunner.QueryModel((double)trueSettings.X, (double)trueSettings.Y, (double)trueSettings.Z, (double)trueSettings.W);
            //may want to redo this to include other factors and be generally more complex
            var energyUsage = GetEnergyUsage(position);
            if (hardness < DesiredHardness + Tolerance && hardness > DesiredHardness - Tolerance)
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

        public float GetEnergyUsage(Vector4 position)
        {
            var trueSettings = NormaliseParticlePosition(position);
            return trueSettings.X * 60 * trueSettings.Y + trueSettings.X * 60*auxilaryLoads; 
        }

        public Vector4 NormaliseParticlePosition(Vector4 vector)
        {
            return vector * normalisationScaleVector + normalisationOffsetVector;
        }        
    }
}
