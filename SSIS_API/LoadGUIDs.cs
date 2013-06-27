using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;

namespace SSIS_API
{
    class LoadGUIDs
    {
        public List<string> listGuidsToReplace = new List<string>();

        public List<String> LoadGUIDS(Package pack)
        {
                //capture list of GUIDs to replace
                listGuidsToReplace = new List<string>();
                AddGuid(pack.ID+" \\ "+pack.Name);
                RecurseExecutablesAndCaptureGuids(pack);
                foreach (DtsEventHandler h in pack.EventHandlers)
                {
                    AddGuid(h.ID+" \\ "+h.Name);
                    RecurseExecutablesAndCaptureGuids(h);
                }
                foreach (ConnectionManager cm in pack.Connections)
                {
                    AddGuid(cm.ID+" \\ "+cm.Name);
                }
                foreach (Microsoft.SqlServer.Dts.Runtime.Configuration conf in pack.Configurations)
                {
                    AddGuid(conf.ID+" \\"+conf.Name);
                }
                return listGuidsToReplace;
        }


        void RecurseExecutablesAndCaptureGuids(IDTSSequence parentExecutable)
        {
            foreach (Variable v in ((DtsContainer)parentExecutable).Variables)
            {
                //don't replace system variables since they're not in the XML
                //don't replace parameters which show as variables since they have a different ID and you won't find the variable's ID in the XML to replace
                if (!v.SystemVariable)
                {
                    AddGuid(v.ID+" \\ "+v.Name);
                }
            }
            foreach (Executable e in parentExecutable.Executables)
            {
                AddGuid(((DtsContainer)e).ID);
                if (e is IDTSSequence)
                {
                    RecurseExecutablesAndCaptureGuids((IDTSSequence)e);
                }
            }
        }

        void AddGuid(string guid)
        {
            if (!listGuidsToReplace.Contains(guid))
            {
                listGuidsToReplace.Add(guid);
            }     
        }
    }
}
