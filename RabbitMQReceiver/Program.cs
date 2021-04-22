using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            IConnectionFactory connFactory = new ConnectionFactory
            {
                UserName = "wz",
                Password = "123456",
                HostName = "192.168.1.63"
            };
            var queue1 = "queue1";
            var queue2 = "queue2";
            var queue3 = "queue3";

            using (IConnection conn = connFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    var consumer = new EventingBasicConsumer(channel);

                    var consumer2=new EventingBasicConsumer(channel);
                    channel.BasicConsume(queue1, false, consumer);
                    channel.BasicConsume(queue2, false, consumer);
                    channel.BasicConsume(queue3, false, consumer);

                    channel.BasicConsume(queue1, false, consumer2);
                    channel.BasicConsume(queue2, false, consumer2);
                    channel.BasicConsume(queue3, false, consumer2);

                    consumer.Received += (model, ea) =>
                    {
                        byte[] message = ea.Body.ToArray();
                        Console.WriteLine($"消费者:consumer1  交换机：{ea.Exchange} 路由Key：{ea.RoutingKey} 接收到信息为:{Encoding.UTF8.GetString(message)}");
                        channel.BasicAck(ea.DeliveryTag, false);
                    };

                    consumer2.Received += (model, ea) =>
                    {
                        byte[] message = ea.Body.ToArray();
                        Console.WriteLine($"消费者:consumer2 交换机：{ea.Exchange} 路由Key：{ea.RoutingKey} 接收到信息为:{Encoding.UTF8.GetString(message)}");
                        channel.BasicAck(ea.DeliveryTag, false);
                    };

                    Console.ReadKey();
                }
            }
        }
    }
}
