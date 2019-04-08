using MassTransit;
using System;
using GreenPipes;

namespace OrderService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Order Service Start");
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint(host, "trendyol_saga_order", e =>
                {
                    //e.Durable = false;
                    //e.ExchangeType = "fanout";
                    e.PrefetchCount = 1;
                    e.Consumer<OrderReceivedConsumer>();
                    e.Consumer<OrderReceivedCancelConsumer>();
                    //e.Consumer<OrderProcessedFaultConsumer>();
                });
            });
            busControl.Start();
            Console.ReadLine();
        }
    }
}

