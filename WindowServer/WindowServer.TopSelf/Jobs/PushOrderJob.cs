using Quartz;
using System.Threading.Tasks;
using WindowServer.Services;
using WindowServer.Services.Model;

namespace WindowServer.TopShelf.Jobs
{
    public class PushOrderJob : IJob
    {
        private IOrderService _orderService;

        public PushOrderJob(IOrderService orderService)
        {
            this._orderService = orderService;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            var orders = _orderService.GetList(new OrderSearchModel());
            //push order
        }
    }
}
