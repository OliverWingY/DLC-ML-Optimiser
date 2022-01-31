using System;
using System.Linq;

namespace DlcCoatingOptimiser
{
    
    public class Program
    {
        private static MLApp.MLApp Matlab;
        public static void Main(string[] args)
        {
            Initialise_App();

        }

        private static void Initialise_App()
        {
            Matlab = new MLApp.MLApp();
            Matlab.Execute("cd D:\\Repos\\DlcCoatingOptimiser\\MatlabScripts");
            Matlab.Feval("InitialiseMatlabWorkspace",1, out object MatlabInitialisationSuccessful);
            Matlab.Feval("CheckWorkspace", 1, out object WorkspaceIsInitialised);

            if ((bool)((object[])MatlabInitialisationSuccessful)[0])
                Console.WriteLine("Matlab app successfully initialised");
            if ((bool)((object[])WorkspaceIsInitialised)[0])
                Console.WriteLine("Matlab Workspace initialised");
            

        }
    }
}
