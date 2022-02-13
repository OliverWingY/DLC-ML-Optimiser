using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DlcCoatingOptimiser
{
    public class MatlabRunner : IMatlabRunner
    {
        private MLApp.MLApp Matlab;
        private string repo = "D:\\Repos\\DlcCoatingOptimiser\\DlcCoatingOptimiser\\MatlabScripts";
        private string initialiseName = "InitialiseMatlabWorkspace";
        private string checkWorkspaceName = "CheckWorkspace";
        private string createSvmName = "BuildSvmModel";
        private string createAnnName = "BuildAnnModel";
        private string querySvmName = "QuerySvmModel";
        private string queryAnnName = "QueryAnnModel";

        public MatlabRunner()
        {
            Matlab = new MLApp.MLApp();
            Matlab.Execute($"cd {repo}");
            InitialiseWorkspace();
            ValidateWorkspace();       
        }

        public bool CreateAnnModel()
        {
            Matlab.Feval(createAnnName, 1, out object successful);
            return (bool)((object[])successful)[0];
        }

        public bool CreateSvmModel()
        {
            Matlab.Feval(createSvmName, 1, out object successful);
            return (bool)((object[])successful)[0];
        }

        public double QueryAnnModel(double DepositionTime, double MicrowavePower, double WorkingPressure, double GasFlowRateRatio)
        {
            Matlab.Feval(queryAnnName, 1, out object Hardness, DepositionTime, MicrowavePower, WorkingPressure, GasFlowRateRatio);
            return (double)((object[])Hardness)[0];
        }

        public double QuerySvmModel(double DepositionTime, double MicrowavePower, double WorkingPressure, double GasFlowRateRatio)
        {
            Matlab.Feval(querySvmName, 1, out object Hardness, DepositionTime, MicrowavePower, WorkingPressure, GasFlowRateRatio);
            return (double)((object[])Hardness)[0];
        }

        private bool InitialiseWorkspace()
        {
            Matlab.Feval(initialiseName, 1, out object MatlabInitialisationSuccessful);            
            return (bool)((object[])MatlabInitialisationSuccessful)[0];
        }

        private bool ValidateWorkspace()
        {
            Matlab.Feval(checkWorkspaceName, 1, out object MatlabWorkspaceValid);
            return (bool)((object[])MatlabWorkspaceValid)[0];
        }        

        public void Dispose()
        {
            Matlab.Quit();
        }
    }
}
