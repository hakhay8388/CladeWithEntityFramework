using Base.Data.nConfiguration;
using Bootstrapper.Boundary.nCore.nBootType;
using Bootstrapper.Core.nApplication;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Data.nDatabaseService.nDatabase.nInterceptors
{
    public class cQueryCommandLoggerInterceptor : DbCommandInterceptor
    {

        public cApp App { get; set; }
        public cQueryCommandLoggerInterceptor(cApp _App)
        {
            App = _App;
        }

        public override DbCommand CommandCreated(CommandEndEventData _EventData, DbCommand _Result)
        {
            if (App.Configuration.ApplicationSettings.LogExecutedSqlEnabled)
            {
                Log(_Result, _EventData);
            }

            return base.CommandCreated(_EventData, _Result);
        }

        public override int NonQueryExecuted(DbCommand _Command, CommandExecutedEventData _EventData, int _Result)
        {
            if (App.Configuration.ApplicationSettings.LogExecutedSqlEnabled)
            {
                Log(_Command, _EventData);
            }

            return base.NonQueryExecuted(_Command, _EventData, _Result);
        }

        public override DbDataReader ReaderExecuted(DbCommand _Command, CommandExecutedEventData _EventData, DbDataReader _Result)
        {
            if (App.Configuration.ApplicationSettings.LogExecutedSqlEnabled)
            {
                Log(_Command, _EventData);
            }

            return base.ReaderExecuted(_Command, _EventData, _Result);
        }

        public override object ScalarExecuted(DbCommand _Command, CommandExecutedEventData _EventData, object _Result)
        {
            if (App.Configuration.ApplicationSettings.LogExecutedSqlEnabled)
            {
                Log(_Command, _EventData);
            }
            return base.ScalarExecuted(_Command, _EventData, _Result);
        }


        protected void Log(DbCommand _Command, CommandEndEventData _EventData)
        {
            List<string> __BulkLog = new List<string>();

            __BulkLog.Add(_Command.CommandText);
            if (_Command.Parameters.Count > 0)
            {
                __BulkLog.Add("//////////// PARAMETERS ///////////");
                foreach (DbParameter __Item in _Command.Parameters)
                {
                    __BulkLog.Add("\t" + __Item.ParameterName + "\t:\t" + __Item.Value);
                }
                __BulkLog.Add("/////////////////////////////////// ");
            }

            __BulkLog.Add("                   ↓↓                    ");
            if (App.Cfg<cDataConfiguration>().BootType == EBootType.Console
              || App.Cfg<cDataConfiguration>().BootType == EBootType.Batch)
            {
                __BulkLog.Add("RequestID : " + App.Cfg<cDataConfiguration>().BootType.Name);
            }
            else
            {
                __BulkLog.Add("RequestID : " + "Request ID alınacak henüz yapılmadı");
            }
            __BulkLog.Add("ElapsedTime : " + _EventData.Duration);
            __BulkLog.Add("//////////////////////////////////");
            App.Loggers.SqlLogger.LogInfo(__BulkLog);
        }


    }
}
