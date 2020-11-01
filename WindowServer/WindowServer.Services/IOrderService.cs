using System.Collections.Generic;
using WindowServer.Services.Model;

namespace WindowServer.Services
{
    public interface IOrderService
    {
        public List<OrderDetailModel> GetList(OrderSearchModel searchModel);
    }
}
