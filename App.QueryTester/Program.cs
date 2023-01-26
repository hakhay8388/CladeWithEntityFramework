using Base.Data;
using Bootstrapper.Boundary.nCore.nBootType;
using Bootstrapper.Core.nApplication.nConfiguration;
using Bootstrapper.Core.nApplication;
using Microsoft.EntityFrameworkCore;
using Base.Data.nConfiguration;
using App.QueryTester;
using System.Text;
using App.QueryTester.cQueryTesters;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Program Başladı.");

        //first create configuration
        cDataConfiguration __DataConfiguration = new cDataConfiguration(EBootType.Console);

        //this is domain search layer order 
        //this application name is starting with App (TApp.TestConsoleProject) so like this 
        // if you have many layer you can add your domain from core to app layer
        //__DataConfiguration.ApplicationSettings.DomainNames.Add("GenericScaffold");



        ///__DataConfiguration.LoadDefaultDataOnStart = false;
        //__DataConfiguration.LoadGlobalParamsOnStart = true;


        // You can change other configuration at here
        //__DataConfiguration.

        cApp __App = cApp.Start<cStarter>(__DataConfiguration);


        cQueryTest __QueryTest = __App.Factories.ObjectFactory.ResolveInstance<cQueryTest>();

        __QueryTest.Start();

        Console.WriteLine("Program Sonlandı.");
    }
}
