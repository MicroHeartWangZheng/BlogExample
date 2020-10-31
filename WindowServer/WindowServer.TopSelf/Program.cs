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

            IServiceProvider serviceProdiver;
            IServiceCollection services = new ServiceCollection();
            services.AddScoped(typeof(IOrderService), typeof(OrderService));

            services.AddScoped<StatisticsService>();
            serviceProdiver = services.BuildServiceProvider();

            HostFactory.Run(x =>
            {
                x.Service<StatisticsService>(service =>
                {
                    service.ConstructUsing(() => serviceProdiver.GetService<StatisticsService>());
                   
                    service.WhenStarted(orderService => orderService.Statistics());

                    service.WhenStopped(x =>
                    {
                        Console.WriteLine("停止");
                    });
                });
            });

            Console.Read();
        }
    }
}
