using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;

namespace SSIS_API
{
    class LoadPackage
    {
        Application app = new Application();
        Package pack;
        
        public Package LoadSSIS(String path)
        {
            pack = app.LoadPackage(@path, null);
            return pack;
        }
    }
}
