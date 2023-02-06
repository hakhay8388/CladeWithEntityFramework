using Base.Data.nConfiguration;
using Base.Web.nCustomDI;
using Bootstrapper.Boundary.nCore.nBootType;
using Bootstrapper.Core.nApplication;
using Sys.Data.nConfiguration;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Web.Domain;
using Web.Domain.Controllers;
using Domain.Data.nConfiguration;

public class Program
{
    public static void Main(string[] _Args)
    {
        WebApplicationBuilder __Builder = WebApplication.CreateBuilder(_Args);


        __Builder.Services.Configure<KestrelServerOptions>(__Options =>
        {
            __Options.AllowSynchronousIO = true;
        });

        __Builder.Services.Configure<IISServerOptions>(__Options =>
        {
            __Options.AllowSynchronousIO = true;
        });

        __Builder.Services.AddControllersWithViews().AddNewtonsoftJson(
            __Options => __Options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        );



        __Builder.Services.AddSignalR(__Conf =>
        {
            __Conf.MaximumReceiveMessageSize = null;
        });

        __Builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", __Builder =>
        {
            __Builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin();
        }));

        __Builder.Services.Configure<CookiePolicyOptions>(__Options =>
        {
            // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            __Options.CheckConsentNeeded = context => false;
            __Options.MinimumSameSitePolicy = SameSiteMode.None;
        });

        __Builder.Services.AddDistributedMemoryCache();

        __Builder.Services.AddSession(__Options =>
        {
            // Set a short timeout for easy testing.
            __Options.IdleTimeout = TimeSpan.FromSeconds(10);
            __Options.Cookie.HttpOnly = true;
            // Make the session cookie essential
            __Options.Cookie.IsEssential = true;
        });




        cDomainDataConfiguration __Configuration = new cDomainDataConfiguration(EBootType.Web);


        cApp __App = cApp.Start<cStarter>(__Configuration);

        __Builder.Host.UseServiceProviderFactory(new cUnityServiceProviderFactory(__App));

        // Add services to the container.


        var __WebApp = __Builder.Build();

        // Configure the HTTP request pipeline.
        if (!__WebApp.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            __WebApp.UseHsts();
        }


        __WebApp.UseHttpsRedirection();
        __WebApp.UseStaticFiles();
        __WebApp.UseDefaultFiles();
        __WebApp.UseSession();

        __WebApp.UseRouting();

        __WebApp.UseEndpoints(endpoints =>
        {

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");
            endpoints.MapHub<SignalRHub>("/signalr");
        });

        __WebApp.Use((__Context, __Next) =>
        {
            __Context.Request.EnableBuffering();
            return __Next();
        });


        /*__WebApp.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}");*/

        __WebApp.MapFallbackToFile("index.html");

        __WebApp.UseCors("CorsPolicy");

        __WebApp.Run();
    }

}
