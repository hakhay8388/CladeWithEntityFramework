using Base.Data;
using Bootstrapper.Boundary.nCore.nBootType;
using Bootstrapper.Core.nApplication.nConfiguration;
using Bootstrapper.Core.nApplication;
using GenericScaffold;
using Microsoft.EntityFrameworkCore;
using Base.Data.nConfiguration;
using Domain.Data.nConfiguration;

public static class Program
{
    public static void Main()
    {
        //first create configuration
        cDomainDataConfiguration __DataConfiguration = new cDomainDataConfiguration(EBootType.Console);

        //this is domain search layer order 
        //this application name is starting with App (TApp.TestConsoleProject) so like this 
        // if you have many layer you can add your domain from core to app layer
        //__DataConfiguration.ApplicationSettings.DomainNames.Add("GenericScaffold");


        // You can change other configuration at here
        //__DataConfiguration.

        cApp __App = cApp.Start<cStarter>(__DataConfiguration);


    }
}
