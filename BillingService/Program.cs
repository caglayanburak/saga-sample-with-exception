using System;
using GreenPipes;
using MassTransit;

namespace BillingService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Order Billing Start");

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint(host, "trendyol_saga_order_billing", e =>
                {
                    //e.Durable = false;
                    //e.ExchangeType = "fanout";
                    e.PrefetchCount = 1;
                    e.Consumer<OrderProcessedConsumer>();
                    e.Consumer<OrderProcessedCancelConsumer>();
                });
            });
            busControl.Start();
            Console.ReadLine();
        }
    }
}
