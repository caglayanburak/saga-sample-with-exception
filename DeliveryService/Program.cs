using System;
using GreenPipes;
using MassTransit;

namespace DeliveryService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Order Delivery Start");

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint(host, "trendyol_saga_order_delivery", e =>
                {
                    e.UseMessageRetry(r => r.Immediate(0));
                    e.Consumer<OrderDeliveriedConsumer>();
                });
            });
            busControl.Start();
            Console.ReadLine();
        }
    }
}
