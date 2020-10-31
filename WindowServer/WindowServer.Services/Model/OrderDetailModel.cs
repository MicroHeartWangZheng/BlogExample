using System;
using System.Collections.Generic;
using System.Text;

namespace WindowServer.Services.Model
{
    public class OrderDetailModel
    {
        public string OrderNo { get; set; }

        /// <summary>
        /// 消费者名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 消费者手机号
        /// </summary>
        public string CustomerPhone { get; set; }


        public DateTime CreateTime { get; set; }
    }
}
