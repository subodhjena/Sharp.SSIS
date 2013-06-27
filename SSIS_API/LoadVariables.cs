using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;

namespace SSIS_API
{
    class LoadVariables
    {
        List<String> packageVars = new List<String>();
        public List<String> GetVariables(Package pack)
        {
            try
            {
                Variables vars = pack.Variables;
                foreach (Variable var in vars)
                {
                    packageVars.Add(var.Name+" | Datatype : "+var.DataType);
                }
                return packageVars;
            }
            catch(Exception x)
            {
                Console.WriteLine("Error While Reading the Variables");
                Console.WriteLine(x.StackTrace);
                return null;
            }
        }
    }
}
