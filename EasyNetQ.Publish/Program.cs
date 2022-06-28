using System;
using EasyNetQ.Message;

namespace EasyNetQ.Publish
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var message = new MessageModel()
            {
                Id = "1",
                Type = "type"
            };

            var bus = RabbitHutch.CreateBus("host=172.16.20.33;username=jiepeimq;password=jpmq321.0");

            bus.PubSub.Publish(message);
            Console.WriteLine("推送消息:" + message.Id);

            Console.ReadLine();
        }

    }
}
