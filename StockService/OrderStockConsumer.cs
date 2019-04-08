using CommonLib;
using MassTransit;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace StockService
{
    public class OrderStockConsumer : IConsumer<IOrderStockEvent>
    {
        public async Task Consume(ConsumeContext<IOrderStockEvent> context)
        {
            var orderCommand = context.Message;

            await Console.Out.WriteLineAsync($"StockService: Order code: {orderCommand.OrderCode} Order id: {orderCommand.OrderId} is received.{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}");
            
            throw new Exception("hata!!!");

            //await context.Publish<IOrderDeliveriedEvent>(
            //    new { CorrelationId = context.Message.CorrelationId, OrderId = orderCommand.OrderId, OrderCode = context.Message.OrderCode });
        }
    }
}
