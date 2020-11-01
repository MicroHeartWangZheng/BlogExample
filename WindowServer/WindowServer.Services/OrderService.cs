using System;
using System.Collections.Generic;
using WindowServer.Services.Model;

namespace WindowServer.Services
{
    public class OrderService : IOrderService
    {
        public List<OrderDetailModel> GetList(OrderSearchModel searchModel)
        {
            var result = new List<OrderDetailModel>();
            for (var i = 0; i < 20; i++)
            {
                result.Add(new OrderDetailModel()
                {
                    OrderNo = i.ToString().PadLeft(9),
                    CreateTime=DateTime.Now,
                    CustomerName=$"顾客{i}",
                }) ;
            }
            return result;
        }
    }
}
