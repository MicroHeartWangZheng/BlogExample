using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Topshelf;
using WindowServer.Services;
using WindowServer.TopShelf;

namespace WindowServer.TopSelf
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new HostBuilder()
              .UseConsoleLifetime()
              .ConfigureAppConfiguration((hostBuilderContext, configuration) =>
              {
                  configuration.AddJsonFile("appsettings.json");
              })
              .ConfigureServices((context, services) =>
              {
                  services.AddScoped(typeof(IOrderService), typeof(OrderService));
                  services.AddScoped<JobManager>();
              })
              .Build();

            HostFactory.Run(x =>
            {
                x.Service<JobManager>(service =>
                {
                    service.ConstructUsing(() => host.Services.GetService<JobManager>());
                    service.WhenStarted(async s => await s.ConfigWithStartJobs());
                    service.WhenStopped(x =>
                    {
                        Console.WriteLine("停止");
                    });
                });
                x.SetDescription("描述");
                x.SetDisplayName("TopShelfExample");
                x.SetServiceName("TopShelfExampleService");
            });
        }
    }
}
