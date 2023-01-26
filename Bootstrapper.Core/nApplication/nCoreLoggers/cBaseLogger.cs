using System;
using System.Collections.Generic;
using System.IO;
using Bootstrapper.Core.nCore;

namespace Bootstrapper.Core.nApplication.nCoreLoggers
{
    public abstract class cBaseLogger : cCoreObject
    {
        public cBaseLogger(cApp _App)
            : base(_App)
        {
        }
        protected abstract string LogPath();
        protected abstract bool IsEnabled();

        protected string ControlledLogPath()
        {
            return App.Handlers.FileHandler.MakeDirectory(LogPath(), true);
        }


        protected string PrepereString(List<string> _Values)
        {
            string __Result = "";

            _Values.ForEach(__Item =>
            {
                __Result += "Time : " + App.Handlers.DateTimeHandler.GetNow();
                __Result += new String('\t', 1);
                __Result += __Item + "\n";
            });
            return __Result;
        }

        //////////////////////////////////////////
        /// Writer  //////////////////////////////
        //////////////////////////////////////////
        protected void WriteToFile(string _FileName, string _Value)
        {
            App.Handlers.FileHandler.AppendStringWithNewThread(_Value, _FileName);
        }

        protected void WriteTo(string _Value, string _FileName)
        {
            if (App.Configuration.ApplicationSettings.LogToFile)
            {
                WriteToFile(Path.Combine(ControlledLogPath(), _FileName + ".log"), _Value);
            }

            if (App.Configuration.ApplicationSettings.LogToConsole)
            {
                Console.WriteLine(_Value);
            }
        }

        //////////////////////////////////////////
        /////////////////////////////////////////////
        /////////////////////////////////////////////


        //////////////////////////////////////////
        /// Info Logger //////////////////////////
        //////////////////////////////////////////

        public void LogInfo(string _Value, params object[] _Args)
        {
            if (App.Configuration.ApplicationSettings.LogInfoEnabled && IsEnabled())
            {
                _Value = PrepereString(new List<string>() { _Value.FormatEx(_Args) });
                WriteTo(_Value, LogFileName);
            }
        }

        public void LogInfo(List<string> _BulkValue)
        {
            if (App.Configuration.ApplicationSettings.LogInfoEnabled && IsEnabled())
            {
                lock (this)
                {
                    string __Value = PrepereString(_BulkValue);
                    WriteTo(__Value, LogFileName);
                }
            }
        }


        //////////////////////////////////////////
        //////////////////////////////////////////
        //////////////////////////////////////////


        //////////////////////////////////////////
        /// Debug Logger //////////////////////////
        //////////////////////////////////////////

        public void DebugLog(string _Value, params object[] _Args)
        {
            if (App.Configuration.ApplicationSettings.LogDebugEnabled && IsEnabled())
            {
                _Value = PrepereString(new List<string>() { _Value.FormatEx(_Args) });
                WriteTo(_Value, DebugLogFileName);
            }
        }

        public void DebugLog(List<string> _BulkValue)
        {
            if (App.Configuration.ApplicationSettings.LogDebugEnabled && IsEnabled())
            {
                lock (this)
                {
                    string __Value = PrepereString(_BulkValue);
                    WriteTo(__Value, DebugLogFileName);
                }
            }
        }

        //////////////////////////////////////////
        //////////////////////////////////////////
        //////////////////////////////////////////


        //////////////////////////////////////////
        /// Error Logger //////////////////////////
        //////////////////////////////////////////

        protected string PrepereExceptionString(string _Value, params object[] _Args)
        {
            _Value = _Value.FormatEx(_Args);
            string __Result = "";
            __Result += new String('\n', 3);
            __Result = "Time : " + App.Handlers.DateTimeHandler.GetNow();
            __Result += new String('\n', 2);
            __Result += new String('#', 100);
            __Result += new String('\n', 2);
            __Result += _Value;
            __Result += new String('\n', 2);
            __Result += new String('#', 100);
            __Result += new String('\n', 2);
            return __Result;
        }

        public void LogError(string _Value, params object[] _Args)
        {
            if (App.Configuration.ApplicationSettings.LogExceptionEnabled && IsEnabled())
            {
                _Value = PrepereExceptionString(_Value, _Args);
                WriteTo(_Value, ErrorLogFileName);
            }
        }

        public void LogError(Exception _Ex, Action<string> _Function = null)
        {
            if (App.Configuration.ApplicationSettings.LogExceptionEnabled && IsEnabled())
            {
#if !DEBUG
var __RavenClient = new RavenClient("https://f0e36791227e431fa63b44e81991a3d8@o1052346.ingest.sentry.io/6037674");
                __RavenClient.CaptureException(_Ex);
#endif


                string __ExceptionString = string.Format(_Ex.Message + "\n {0} \n {1} \n {2}", _Ex.StackTrace, _Ex.Source, _Ex.InnerException);
                LogError(__ExceptionString);
                if (_Function != null)
                {
                    _Function(__ExceptionString);
                }
            }

        }

        public void LogError(List<string> _BulkValueBeforeError, Exception _Ex, List<string> _BulkValueAfterError)
        {
            if (App.Configuration.ApplicationSettings.LogExceptionEnabled && IsEnabled())
            {
                lock (this)
                {
                    if (_BulkValueBeforeError != null)
                    {
                        string __Value = PrepereString(_BulkValueBeforeError);
                        WriteTo(__Value, ErrorLogFileName);
                    }
                    if (_Ex != null)
                    {
                        LogError(_Ex.Message + "\n {0} \n {1} \n {2}", _Ex.StackTrace, _Ex.Source, _Ex.InnerException);
                    }
                    if (_BulkValueAfterError != null)
                    {
                        string __Value = PrepereString(_BulkValueAfterError);
                        WriteTo(__Value, ErrorLogFileName);
                    }
                }
            }
        }


        //////////////////////////////////////////
        //////////////////////////////////////////
        //////////////////////////////////////////


        protected string LogFileName
        {
            get
            {
                return "Log_" + DateTime.Now.Hour + "h_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
            }
        }

        protected string DebugLogFileName
        {
            get
            {
                return LogFileName + ".DEBUG";
            }
        }

        protected string ErrorLogFileName
        {
            get
            {
                return LogFileName + ".ERROR";
            }
        }
    }
}
