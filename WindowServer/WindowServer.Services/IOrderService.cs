using System.Collections.Generic;
using WindowServer.Services.Model;

namespace WindowServer.Services
{
    public interface IOrderService
    {
        public int Count(OrderSearchModel searchModel);
    }
}
