using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQSender
{
    class Program
    {
        static void Main(string[] args)
        {
            IConnectionFactory conFactory = new ConnectionFactory//创建连接工厂对象
            {
                UserName = "wz",//用户名
                Password = "123456",//密码
                HostName = "192.168.1.63"//rabbitmq ip
            };

            var queue1 = "queue1";
            var queue2 = "queue2";
            var queue3 = "queue3";
            var exchange1 = "exchange1";
            var exchange2 = "exchange2";
            var routingKey1 = "routingKey1";
            var routingKey2 = "routingKey2";
            using (IConnection con = conFactory.CreateConnection())//创建连接对象
            {
                using (IModel channel = con.CreateModel())//创建连接会话对象
                {
                    channel.QueueDeclare(queue1, false, false, false, null);
                    channel.QueueDeclare(queue2, false, false, false, null);
                    channel.QueueDeclare(queue3, false, false, false, null);

                    //channel.ExchangeDeclare(exchange1, ExchangeType.Direct);
                    
                    //channel.QueueBind(queue1, exchange1, routingKey1);
                    //channel.QueueBind(queue2, exchange1, routingKey2);
                    //channel.QueueBind(queue3, exchange1, routingKey2);


                    channel.ExchangeDeclare(exchange2, ExchangeType.Fanout);
                    channel.QueueBind(queue1, exchange2, routingKey2);
                    channel.QueueBind(queue2, exchange2, routingKey2);
                    channel.QueueBind(queue3, exchange2, routingKey2);

                    while (true)
                    {
                        Console.Write("消息内容:");
                        var message = Console.ReadLine();
                        byte[] body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange2, routingKey2, null, body);
                        Console.WriteLine("成功发送消息:" + message);
                    }
                }
            }
        }
    }
}
