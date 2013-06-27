
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace SSIS_API
{
    class LoadPackageItems
    {
        private List<String> _packageTaskDetails = new List<String>();
        public List<String> LoadTaskDetails(Package pack)
        {
            try
            {
                Executables pExecs = pack.Executables;
                foreach (Executable pExec in pExecs)
                {
                        if (pExec.GetType().ToString() == "Microsoft.SqlServer.Dts.Runtime.TaskHost")
                        {
                            TaskHost taskHost = (TaskHost)pExec;
                            _packageTaskDetails.Add(taskHost.Name);
                        }
                        else if (pExec.GetType().ToString() == "Microsoft.SqlServer.Dts.Runtime.Sequence")
                        {
                            Sequence seq = (Sequence)pExec;
                            Executables seqExecs = seq.Executables;
                            foreach (Executable seqExec in seqExecs)
                            {
                                TaskHost seqTaskHost = (TaskHost)seqExec;
                                if (seqTaskHost.InnerObject is Microsoft.SqlServer.Dts.Pipeline.Wrapper.MainPipe)
                                {
                                    MainPipe pipe = (MainPipe)seqTaskHost.InnerObject;
                                    foreach (IDTSComponentMetaData100 comp in pipe.ComponentMetaDataCollection)
                                    {
                                        _packageTaskDetails.Add(seq.Name + " -> " + seqTaskHost.Name + " -> " + comp.Name);
                                    }
                                }
                                else
                                {
                                    _packageTaskDetails.Add(seq.Name + " -> " + seqTaskHost.Name);
                                }
                            }
                        }
                }
                return _packageTaskDetails;
            }
            catch(Exception x)
            {
                Console.WriteLine("Got Exception while reading the Package");
                Console.WriteLine(x.StackTrace);
                return null;
            }
        }
    }
}
