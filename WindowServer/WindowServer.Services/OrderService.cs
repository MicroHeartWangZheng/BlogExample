using System;
using WindowServer.Services.Model;

namespace WindowServer.Services
{
    public class OrderService : IOrderService
    {
        public int Count(OrderSearchModel searchModel)
        {
            return new Random().Next(100, 300);
        }
    }
}
