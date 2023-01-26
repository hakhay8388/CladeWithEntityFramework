using App.BatchJobService.nBatchJobService;
using Base.Data.nConfiguration;
using Base.Web.nCustomDI;
using Bootstrapper.Boundary.nCore.nBootType;
using Bootstrapper.Core.nApplication;
using Data.Domain.nConfiguration;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Text;
using Web.Domain;

public class Program
{
    public static void Main(string[] _Args)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        WebApplicationBuilder __Builder = WebApplication.CreateBuilder(_Args);



        cDomainDataConfiguration __Configuration = new cDomainDataConfiguration(EBootType.Web);


        cApp __App = cApp.Start<cStarter>(__Configuration);

        __Builder.Host.UseServiceProviderFactory(new cUnityServiceProviderFactory(__App));

        // Add services to the container.


        var __WebApp = __Builder.Build();

        __WebApp.UseRouting();

        __WebApp.UseEndpoints(endpoints =>
        {

            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Batch Job Service Started...");
            });
        });


        cBatchJobService __BatchJobService = __App.Factories.ObjectFactory.ResolveInstance<cBatchJobService>();

        __BatchJobService.Start();

        __WebApp.Run();
        __BatchJobService.Stop();
    }

}
