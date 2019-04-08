using CommonLib;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderUI.Entities;
using System.Threading.Tasks;

namespace OrderUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IBusControl _bus;

        public OrdersController(IBusControl bus)
        {
            _bus = bus;
            _bus.Start();
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Post([FromBody]Order order)
        {
            ECommerceContext context = new ECommerceContext();
            //todo:Guid bakılacak.

            order.OrderCode = "TY" + order.OrderId;
            context.Order.Add(order);
            context.SaveChanges();
            //CreateOrder(order);
            await _bus.Publish<Order>(order);
            
            return order;
        }

        private void CreateOrder(Order orderModel)
        {
            _bus.Send(orderModel).Wait();
        }
    }
}
