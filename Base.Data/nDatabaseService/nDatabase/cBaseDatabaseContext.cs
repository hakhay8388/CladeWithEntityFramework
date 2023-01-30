using Base.Data.nDatabaseService.nDatabase.nInterceptors;
using Bootstrapper.Boundary.nCore.nObjectLifeTime;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nAttributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDatabaseService.nDatabase
{
    public abstract class cBaseDatabaseContext : DbContext
    {
        public cApp App { get; set; }
        public cBaseDatabaseContext()
          : base()
        {
            App = cApp.App;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder _OptionsBuilder)
        {
            _OptionsBuilder.UseNpgsql(App.Configuration.ApplicationSettings.DatabaseConnectionString);
            _OptionsBuilder.LogTo(_Action=>
            {
                if (App != null)
                {
                    App.Loggers.SqlGlobalInfoLogger.LogInfo(_Action);
                }
            })
            .EnableDetailedErrors()
            .AddInterceptors(new cQueryCommandLoggerInterceptor(App));
        }


        public void Perform(Action _ServiceMethod)
        {
            Console.WriteLine("Perform Begin : " + _ServiceMethod.Method.ToString());

            InvokeTransactionalAction(() =>
            {
                _ServiceMethod.Invoke();
                return true;
            });
            Console.WriteLine("Perform End : " + _ServiceMethod.Method.ToString());
        }

        public void Perform(Func<bool> _ServiceMethod)
        {
            Console.WriteLine("Perform Begin : " + _ServiceMethod.Method.ToString());

            InvokeTransactionalAction(() =>
            {
                return _ServiceMethod.Invoke();
            });
            Console.WriteLine("Perform End : " + _ServiceMethod.Method.ToString());
        }

 
        private void InvokeTransactionalAction(Func<bool> _ServiceMethod)
        {
            IDbContextTransaction __Transaction = null;
            try
            {
                List<MethodBase> __Methods = App.Handlers.StackHandler.GetMethods("InvokeTransactionalAction", 0);

                if (__Methods.Where(__Item => __Item.DeclaringType.Name == this.GetType().Name).ToList().Count < 2)
                {
                    __Transaction = Database.BeginTransaction();
                    if (_ServiceMethod())
                    {
                        __Transaction.Commit();
                    }
                    else
                    {
                        __Transaction.Rollback();
                    }
                }
                else
                {
                    throw new Exception("ic ice aclilan transation mevcut..!");
                }
            }
            catch (Exception _Ex)
            {
                App.Loggers.SqlLogger.LogError(_Ex);
                if (__Transaction != null)
                {
                    __Transaction.Rollback();
                }
                throw _Ex;
            }
        }



    }
}
