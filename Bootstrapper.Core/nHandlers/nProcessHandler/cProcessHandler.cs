using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using Bootstrapper.Core.nExceptions;
using Bootstrapper.Core.nHandlers.nStringHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Bootstrapper.Core.nHandlers.nProcessHandler
{
    public class cProcessHandler : cCoreObject
    {
        public cProcessHandler(nApplication.cApp _App)
            :base(_App)
        {
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cProcessHandler>(this);
        }

        public bool OpenModalProcess(string _ExecFileWithPath, string _Arguments)
        {
            try
            {
                if (File.Exists(_ExecFileWithPath))
                {
                    Process process = new Process();
                    process.StartInfo.FileName = _ExecFileWithPath;
                    process.StartInfo.WorkingDirectory = Path.GetDirectoryName(_ExecFileWithPath);
                    process.StartInfo.Arguments = _Arguments;
                    process.StartInfo.Verb = "runas";
                    process.Start();
                    process.WaitForExit();

                    return process.ExitCode == 0;
                }
                else if (_ExecFileWithPath.IndexOf("\\") < 1 && _ExecFileWithPath.IndexOf("/") < 1)
                {
                    Process process = new Process();
                    process.StartInfo.FileName = _ExecFileWithPath;
                    process.StartInfo.WorkingDirectory = Path.GetDirectoryName(_ExecFileWithPath);
                    process.StartInfo.Arguments = _Arguments;
                    process.StartInfo.Verb = "runas";
                    process.Start();
                    process.WaitForExit();

                    return process.ExitCode == 0;
                }
            }
            catch (Exception _Ex)
            {
				App.Loggers.CoreLogger.LogError(_Ex);
				throw _Ex;
            }

            return false;
        }
    }
}
