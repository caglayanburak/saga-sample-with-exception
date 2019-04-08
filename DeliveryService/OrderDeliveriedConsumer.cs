using CommonLib;
using MassTransit;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace DeliveryService
{
    public class OrderDeliveriedConsumer : IConsumer<IOrderDeliveriedEvent>
    {
        public async Task Consume(ConsumeContext<IOrderDeliveriedEvent> context)
        {
            var orderCommand = context.Message;

            await Console.Out.WriteLineAsync($"DeliveryService: Order code: {orderCommand.OrderCode} Order id: {orderCommand.OrderId} is received.{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}");

            //await context.Publish<IOrderCompletedEvent>(
            //    new { CorrelationId = context.Message.CorrelationId, OrderId = orderCommand.OrderId, OrderCode = context.Message.OrderCode });
        }
    }
}
