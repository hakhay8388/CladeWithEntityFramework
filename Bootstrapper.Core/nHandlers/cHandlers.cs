using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nApplication.nCoreLoggers;
//using Bootstrapper.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using Bootstrapper.Core.nHandlers.nAssemblyHandler;
using Bootstrapper.Core.nHandlers.nFileHandler;
using Bootstrapper.Core.nHandlers.nLambdaHandler;
using Bootstrapper.Core.nHandlers.nReflectionHandler;
using Bootstrapper.Core.nHandlers.nStringHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bootstrapper.Core.nHandlers.nHashTableHandler;
using Bootstrapper.Core.nHandlers.nDateTimeHandler;
using Bootstrapper.Core.nHandlers.nProcessHandler;
using Bootstrapper.Core.nHandlers.nHashHandler;
using Bootstrapper.Core.nHandlers.nValidationHandler;
using Bootstrapper.Core.nHandlers.nDefaultDataHandler;
using Bootstrapper.Core.nHandlers.nEmailHandler;
using Bootstrapper.Core.nHandlers.nExcelHandler;
using Bootstrapper.Core.nHandlers.nStackHandler;
using Bootstrapper.Core.nHandlers.nXmlHandler;
using Bootstrapper.Core.nHandlers.nScriptHandler;
using Bootstrapper.Core.nHandlers.nLanguageHandler;

namespace Bootstrapper.Core.nHandlers
{
    public class cHandlers : cCoreObject
    {
        public cAssemblyHandler AssemblyHandler { get; set; }
        public cLambdaHandler LambdaHandler { get; set; }
        public cReflectionHandler ReflectionHandler { get; set; }
        public cFileHandler FileHandler { get; set; }
        public cStringHandler StringHandler { get; set; }
        public cHashTableHandler HashTableHandler { get; set; }
        public cDateTimeHandler DateTimeHandler { get; set; }
        public cProcessHandler ProcessHandler { get; set; }
        public cHashHandler HashHandler { get; set; }
        public cValidationHandler ValidationHandler { get; set; }
        public cDefaultDataHandler DefaultDataHandler { get; set; }
        
        public cEmailHandler EmailHandler { get; set; }
		public cExcelHandler ExcelHandler { get; set; }
		public cStackHandler StackHandler { get; set; }
        public cXmlHandler XmlHandler { get; set; }
        public cScriptHandler ScriptHandler { get; set; }
        public cLanguageHandler LanguageHandler { get; set; }

        public cHandlers(nApplication.cApp _App)
            : base(_App)
        {
            AssemblyHandler = new cAssemblyHandler(_App);
            LambdaHandler = new cLambdaHandler(_App);
            ReflectionHandler = new cReflectionHandler(_App);
            FileHandler = new cFileHandler(_App);
            StringHandler = new cStringHandler(_App);
            HashTableHandler = new cHashTableHandler(_App);
            DateTimeHandler = new cDateTimeHandler(_App);
            ProcessHandler = new cProcessHandler(_App);
            HashHandler = new cHashHandler(_App);
            ValidationHandler = new cValidationHandler(_App);
            DefaultDataHandler = new cDefaultDataHandler(_App);
            EmailHandler = new cEmailHandler(_App);
			ExcelHandler = new cExcelHandler(_App);
			StackHandler = new cStackHandler(_App);
            XmlHandler = new cXmlHandler(_App);
            ScriptHandler = new cScriptHandler(_App);
            LanguageHandler = new cLanguageHandler(App);
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cHandlers>(this);
			StackHandler.Init();
			AssemblyHandler.Init();
            LambdaHandler.Init();
            ReflectionHandler.Init();
            FileHandler.Init();
            StringHandler.Init();
            HashTableHandler.Init();
            DateTimeHandler.Init();
            ProcessHandler.Init();
            HashHandler.Init();
            ValidationHandler.Init();
            LanguageHandler.Init();
            DefaultDataHandler.Init();
            EmailHandler.Init();
			ExcelHandler.Init();
            ScriptHandler.Init();

        }
    }
}
