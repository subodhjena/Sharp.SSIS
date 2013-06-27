using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;

namespace SSIS_API
{
    class Program
    {
        static void Main(string[] args)
        {
            // List to store the Vraiable names
            List<String> packageTasks = new List<String>();
            List<String> packageVars = new List<String>();
            List<String> packageGUIDs = new List<string>();

            //Creating objects for the Packge
            LoadPackage load = new LoadPackage();
            LoadPackageItems loadPackageTasks = new LoadPackageItems();
            LoadVariables loadVars = new LoadVariables();            
            LoadGUIDs guids = new LoadGUIDs();

            Package readPackage = load.LoadSSIS(@"/template.dtsx");

            // Reading the contents inside of the package
            packageTasks = loadPackageTasks.LoadTaskDetails(readPackage);
            foreach (String tname in packageTasks)
            {
                Console.WriteLine(tname.ToString());
            }
                     
            // Reading the Variable inside the package
            packageVars = loadVars.GetVariables(readPackage);
            foreach (String var in packageVars)
            {
                Console.WriteLine(var.ToString());
            }

            // Reading the GUID for all items inside the package
            packageGUIDs = guids.LoadGUIDS(readPackage);
            foreach (String gid in packageGUIDs)
            {
                Console.WriteLine(gid.ToString());
            }
            Console.ReadLine();

        }
    }
}
