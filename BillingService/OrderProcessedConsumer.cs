using CommonLib;
using MassTransit;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace BillingService
{
    public class OrderProcessedConsumer : IConsumer<IOrderProcessedEvent>
    {
        public async Task Consume(ConsumeContext<IOrderProcessedEvent> context)
        {
            var orderCommand = context.Message;

            Console.Out.WriteLine($"BillingService: Order code: {orderCommand.OrderId} Order id: {orderCommand.OrderId} is received.{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}");
            

            await context.Publish<IOrderStockEvent>(
                new { CorrelationId = context.Message.CorrelationId, OrderId = orderCommand.OrderId });

        }
    }
    public class OrderProcessedCancelConsumer : IConsumer<IOrderProcessedCancelEvent>
    {
        public async Task Consume(ConsumeContext<IOrderProcessedCancelEvent> context)
        {
            var orderCommand = context.Message;

            Console.Out.WriteLine($"BillingService: Order billing rollback: {orderCommand.OrderId} Order id: {orderCommand.OrderId} is received.{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}");


            await context.Publish<IOrderReceivedCancelEvent>(
                new { CorrelationId = context.Message.CorrelationId, OrderId = orderCommand.OrderId });

        }
    }
}
