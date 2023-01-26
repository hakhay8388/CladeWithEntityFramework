using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nUtils.nImpersonatedUserUtils
{
    public class cImpersonatedUserUtils : cCoreObject
    {
        public cImpersonatedUserUtils(nApplication.cApp _App)
            : base(_App)
        {
            App.Factories.ObjectFactory.RegisterInstance<cImpersonatedUserUtils>(this);
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cImpersonatedUserUtils>(this);
        }

        public void ConnectPath(string _Directory, string _Username, string _Password)
        {

            string __Command = "NET USE " + _Directory + " /delete";
            ExecuteCommand(__Command, 5000);

            __Command = "NET USE " + _Directory + " /user:" + _Username + " " + _Password;
            ExecuteCommand(__Command, 5000);
        }
        public void DisConnectPath(string _Directory)
        {

            string __Command = "NET USE " + _Directory + " /delete";
            ExecuteCommand(__Command, 5000);
        }

        public static int ExecuteCommand(string command, int timeout)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/C " + command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = "C:\\",
            };

            var process = Process.Start(processInfo);
            process.WaitForExit(timeout);
            var exitCode = process.ExitCode;
            process.Close();
            return exitCode;
        }

    }



}
