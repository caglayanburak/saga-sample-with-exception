using System;
using GreenPipes;
using MassTransit;

namespace StockService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Order Stock Start");

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint(host, "trendyol_saga_order_stock", e =>
                {
                    e.UseMessageRetry((t)=>t.Immediate(5));
                    e.Consumer<OrderStockConsumer>();
                });
            });
            busControl.Start();
            Console.ReadLine();
        }
    }
}
