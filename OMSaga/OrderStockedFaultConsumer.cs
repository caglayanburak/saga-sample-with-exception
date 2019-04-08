using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using MassTransit;

namespace OMSaga
{
    public class OrderStockedFaultConsumer : IConsumer<Fault<IOrderStockEvent>>
    {
        public async Task Consume(ConsumeContext<Fault<IOrderStockEvent>> context)
        {
            var orderCommand = context.Message;

            await Console.Out.WriteLineAsync($"Processed FAULT: StockService: FaultId: {orderCommand.FaultId} Message: {orderCommand.Message} is received.{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}");


            await context.Publish<IOrderProcessedCancelEvent>(
                new { CorrelationId = orderCommand.Message.CorrelationId, OrderId = orderCommand.Message.OrderId, OrderCode = orderCommand.Message.OrderCode });
        }
    }
}
