using EasyNetQ.Message;
using System;

namespace EasyNetQ.Subscribe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var bus = RabbitHutch.CreateBus("host=172.16.20.33;username=jiepeimq;password=jpmq321.0");

            while (true)
            {
                bus.PubSub.Subscribe<MessageModel>("sub_id", message =>
                {
                    Console.WriteLine(message.Id);
                });
            }

            Console.ReadKey();
        }
    }
}
