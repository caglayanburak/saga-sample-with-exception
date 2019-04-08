using System;
using GreenPipes;
using MassTransit;
using MassTransit.Saga;

namespace OMSaga
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Saga";
            var orderSaga = new OrderSaga();
            var repo = new InMemorySagaRepository<OrderSagaState>();

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint(host, "trendyol_saga_state", e =>
                {
                    e.PrefetchCount = 1;
                    //e.Durable = false;
                    //e.AutoDelete = true;
                    //e.ExchangeType = "fanout";//direct
                    e.StateMachineSaga(orderSaga, repo);
                    e.UseMessageRetry((t) => t.Immediate(5));

                    e.Consumer<OrderStockedFaultConsumer>();

                });
                cfg.UseRetry(t=>t.Immediate(5));
                   //cfg.UseInMemoryOutbox();
            });
            busControl.StartAsync();

            Console.WriteLine("Order saga started..");
            Console.ReadLine();
        }
    }
}
