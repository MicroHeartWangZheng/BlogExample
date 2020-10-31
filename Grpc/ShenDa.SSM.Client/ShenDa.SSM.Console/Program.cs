using Grpc.Core;
using Grpc.Net.Client;
using ShenDa.SSM.Grpc;
using System;
using System.Threading;
using System.Threading.Tasks;
using static ShenDa.SSM.Grpc.SsmService;

namespace ShenDa.SSM.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new SsmServiceClient(channel);

            await HealthCheck(client);
        }


        public static async Task HealthCheck(SsmServiceClient client)
        {
            var response = await client.HealthAsync(new EmptyRequest());

            System.Console.WriteLine(response.Success ? "健康" : "连接失败");
        }
        public static async Task AddUser(SsmServiceClient client)
        {
            var cts = new CancellationTokenSource(5000);
            var userAdd = client.User_Add();

            //发送请求
            for (var i = 0; i < 10; i++)
            {
                Thread.Sleep(300);
                await userAdd.RequestStream.WriteAsync(new UserAddRequest()
                {
                    Id = i,
                    Age = "18",
                    Name = $"Name-{i}"
                }); ;
            }

            //处理返回
            await Task.Run(async () =>
            {
                await foreach (var item in userAdd.ResponseStream.ReadAllAsync())
                {
                    System.Console.WriteLine($"Id:{item.Id}  是否保存成功:{item.Message}");
                }
            });
        }
    }
}
