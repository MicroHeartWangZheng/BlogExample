using System;
using System.Collections.Generic;
using System.Text;
using WindowServer.Services;
using WindowServer.Services.Model;

namespace WindowServer.TopShelf
{
    /// <summary>
    /// 统计服务
    /// </summary>
    public class StatisticsService
    {
        private IOrderService orderService;
        public StatisticsService(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        /// <summary>
        /// 统计 并写入数据库
        /// </summary>
        public void Statistics()
        {
            var count = orderService.Count(new OrderSearchModel()
            {
                BeginTime = DateTime.Now.AddDays(-1),
                EndTime = DateTime.Now
            });

            Console.WriteLine("Count:" + count);
            Console.ReadLine();

            //todo  写入数据库
        }
    }
}
