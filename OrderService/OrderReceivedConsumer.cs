using CommonLib;
using MassTransit;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace OrderService
{
    class OrderReceivedConsumer : IConsumer<IOrderReceivedEvent>
    {
        public async Task Consume(ConsumeContext<IOrderReceivedEvent> context)
        {
            var orderCommand = context.Message;

            Console.Out.WriteLine($"OrderService:Order code: {orderCommand.OrderCode} Order CorrelationId: {orderCommand.CorrelationId} is received.{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}");

            //throw new Exception("test");
            //await sendEndpoint.Send<MyCommand>(...);

            await context.Publish<IOrderProcessedEvent>(
              new { CorrelationId = context.Message.CorrelationId, OrderId = orderCommand.OrderId, OrderCode = orderCommand.OrderCode });
        }
    }

    class OrderReceivedCancelConsumer : IConsumer<IOrderReceivedCancelEvent>
    {
        public async Task Consume(ConsumeContext<IOrderReceivedCancelEvent> context)
        {
            var orderCommand = context.Message;

            Console.Out.WriteLine($"OrderService:Order rollback: {orderCommand.OrderCode} Order CorrelationId: {orderCommand.CorrelationId} is received.{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}");

            //await context.Publish<IOrderProcessedEvent>(
            //    new { CorrelationId = context.Message.CorrelationId, OrderId = orderCommand.OrderId, OrderCode = orderCommand.OrderCode });
        }
    }
}
