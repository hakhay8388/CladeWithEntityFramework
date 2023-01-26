using System;
using System.Collections.Generic;
using System.Reflection;
using Bootstrapper.Core.nApplication.nConfiguration.nStartParameter;
using Bootstrapper.Core.nExceptions;
using System.Globalization;
using System.Linq.Expressions;
using Bootstrapper.Core.nCore;
using System.IO;
using Bootstrapper.Boundary.nCore.nBootType;
using Bootstrapper.Core.nApplication.nConfiguration.nApplicationSettings;

namespace Bootstrapper.Core.nApplication.nConfiguration
{
    public class cConfiguration : cCoreObject
    {
        public cApplicationSettings ApplicationSettings { get; private set; }

        private cStartParameterController StartParameterController = null;

        /*public List<string> DomainNames { get; private set; }
        public string UICultureName { get; set; }

        */

        public string BinPath { get; private set; }

        public string DefaultDataPath { get; set; }
        public string ConfigurationDataPath { get; set; }

        public string ScriptPath { get; private set; }
        public string ScriptCachePath { get; private set; }
        public string ScriptDebugSourcePath { get; private set; }
        public string LanguagePath { get; set; }
        

        public EBootType BootType { get; set; }

        public CultureInfo UICulture
        {
            get
            {
                try
                {
                    return string.IsNullOrEmpty(ApplicationSettings.UICultureName) ? CultureInfo.InvariantCulture : new CultureInfo(ApplicationSettings.UICultureName);
                }
                catch (Exception _Ex)
                {
                    App.Loggers.CoreLogger.LogError(_Ex);
                    return CultureInfo.InvariantCulture;
                }
            }
        }

        public string HomePath{ get; private set; }
		public string LogPath { get; private set; }
		public string GeneralLogPath { get; private set; }
		public string ExecutedSqlLogPath { get; private set; }
        public string SqlGlobalInfoLogPath { get; private set; }
        public string RequestPerformanceLogPath { get; private set; }
        public string BatchJobLogPath { get; private set; }


        public bool LoadDefaultDataOnStart { get; set; }
        public bool LoadBatchJobOnStart { get; set; }
        public bool LoadGlobalParamsOnStart { get; set; }




        public cConfiguration(EBootType _BootType)
            :base(null)
        {
            BootType = _BootType;
            StartParameterController = new cStartParameterController(this);
        }

        public override void Init()
        {
            base.Init();
            App.Handlers.FileHandler.MakeDirectory(App.Configuration.GeneralLogPath, true);
            App.Handlers.FileHandler.MakeDirectory(App.Configuration.ExecutedSqlLogPath, true);
            App.Handlers.FileHandler.MakeDirectory(App.Configuration.SqlGlobalInfoLogPath, true);            
            App.Handlers.FileHandler.MakeDirectory(App.Configuration.RequestPerformanceLogPath, true);
            App.Handlers.FileHandler.MakeDirectory(App.Configuration.BatchJobLogPath, true);            
            App.Handlers.FileHandler.MakeDirectory(App.Configuration.DefaultDataPath, true);
            App.Handlers.FileHandler.MakeDirectory(App.Configuration.ConfigurationDataPath, true);
            App.Handlers.FileHandler.MakeDirectory(App.Configuration.ScriptCachePath, true);
            App.Handlers.FileHandler.MakeDirectory(App.Configuration.ScriptDebugSourcePath, true);
            App.Handlers.FileHandler.MakeDirectory(App.Configuration.LanguagePath, true);

        }

        public void InnerInit(cApp _App)
        {
            App = _App;
            App.Factories.ObjectFactory.RegisterInstance(GetType(), this);

            //DomainNames = new List<string>() { "Bootstrapper" };
            //UICultureName = "tr-TR";
            HomePath = AppDomain.CurrentDomain.BaseDirectory;
            BinPath = AppDomain.CurrentDomain.BaseDirectory;

            LogPath = GetVariableName(() => LogPath);
            LogPath = Path.Combine(HomePath, LogPath);

            LoadDefaultDataOnStart = true;
            LoadBatchJobOnStart = true;
            LoadGlobalParamsOnStart = true;



            SetPaths();

            ApplicationSettings = LoadSettingFromApplicationSettings();
        }


        public cApplicationSettings LoadSettingFromApplicationSettings()
        {
            string __Path = App.Handlers.FileHandler.FindFileFromOuterDirectory(App.Configuration.HomePath, "ApplicationSettings.xml");
            cApplicationSettings __ApplicationSettings = null;
            if (__Path != null)
            {

                try
                {
                    __ApplicationSettings = App.Handlers.XmlHandler.ReadXMLToObject<cApplicationSettings>(__Path);
                }
                catch (Exception _Ex)
                {
                }

                cApplicationSettings __SampleSettings = cApplicationSettings.CreateSampleSetting();
                App.Handlers.XmlHandler.WriteObjectToXML<cApplicationSettings>(__SampleSettings, Path.Combine(Path.GetDirectoryName(__Path), "SampleSettings.xml"));
            }
            else
            {
                cApplicationSettings __SampleSettings = cApplicationSettings.CreateSampleSetting();
                App.Handlers.XmlHandler.WriteObjectToXML<cApplicationSettings>(__SampleSettings, Path.Combine(App.Configuration.HomePath, "ApplicationSettings.xml"));
                throw new Exception("Lütfen \"" + Path.Combine(App.Configuration.HomePath, "ApplicationSettings.xml") + "\" oluşturulmuş ApplicationSettings.xml dosyasını bütün alt projelerin erişebileceği root klasöre taşıyınız.");
            }
            return __ApplicationSettings;
        }


        private void SetPaths()
        {
            GeneralLogPath = GetVariableName(() => GeneralLogPath);
            GeneralLogPath = Path.Combine(LogPath, GeneralLogPath);

            ExecutedSqlLogPath = GetVariableName(() => ExecutedSqlLogPath);
            ExecutedSqlLogPath = Path.Combine(LogPath, ExecutedSqlLogPath);

            SqlGlobalInfoLogPath = GetVariableName(() => SqlGlobalInfoLogPath);
            SqlGlobalInfoLogPath = Path.Combine(LogPath, SqlGlobalInfoLogPath);

            RequestPerformanceLogPath = GetVariableName(() => RequestPerformanceLogPath);
            RequestPerformanceLogPath = Path.Combine(LogPath, RequestPerformanceLogPath);

            BatchJobLogPath = GetVariableName(() => BatchJobLogPath);
            BatchJobLogPath = Path.Combine(LogPath, BatchJobLogPath);

            DefaultDataPath = GetVariableName(() => DefaultDataPath);
            DefaultDataPath = Path.Combine(HomePath, DefaultDataPath);

            ConfigurationDataPath = GetVariableName(() => ConfigurationDataPath);
            ConfigurationDataPath = Path.Combine(HomePath, ConfigurationDataPath);

            ScriptPath = GetVariableName(() => ScriptPath);
            ScriptCachePath = GetVariableName(() => ScriptCachePath);

            ScriptCachePath = Path.Combine(HomePath, ScriptPath, ScriptCachePath);
            ScriptDebugSourcePath = GetVariableName(() => ScriptDebugSourcePath);

            ScriptDebugSourcePath = Path.Combine(HomePath, ScriptPath, ScriptDebugSourcePath);
            ScriptPath = Path.Combine(HomePath, ScriptPath);

            LanguagePath = GetVariableName(() => LanguagePath);
            LanguagePath = Path.Combine(HomePath, LanguagePath);

        }

        protected string GetVariableName<T>(Expression<Func<T>> _Expr)
        {
            var __Body = (MemberExpression)_Expr.Body;
            return __Body.Member.Name;
        }

        private void OverrideConfiguration()
        {
            Console.WriteLine("Parameters Overriding....");
            Type __Type = GetType();
            foreach (var __Item in StartParameterController.ParameterList)
            {
                PropertyInfo __FieldInfo = __Type.SearchProperty(__Item.Key.ToString());
                if (__FieldInfo != null)
                {
                    try
                    {
                        __FieldInfo.SetValue(this, Convert.ChangeType(__Item.Value, __FieldInfo.PropertyType));
                        Console.WriteLine(__Item.Key.ToString() + " : " + __Item.Value + " -> Override success...");
                    }
                    catch(Exception _Ex)
                    {
						App.Loggers.CoreLogger.LogError(_Ex);
						throw new cCoreException(App, "Parametre hatası : " + __Item.Value);
                    }

                }

            }
        }
        public string TryGetParameter(String _ParameterName)
        {
            return StartParameterController.ParameterList[_ParameterName];
        }
    }
}
