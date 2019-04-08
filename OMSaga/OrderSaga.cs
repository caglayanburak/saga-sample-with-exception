using Automatonymous;
using CommonLib;
using MassTransit;
using System;
using System.Globalization;

namespace OMSaga
{
    public class OrderSaga : MassTransitStateMachine<OrderSagaState>
    {
        public State Received { get; set; }
        public State Processed { get; set; }
        public State Stocked { get; set; }
        public State Deliveried { get; set; }
        public State Completed { get; set; }

        public Event<IOrderCommand> OrderCommand { get; set; }
        public Event<IOrderProcessedEvent> OrderProcessed { get; set; }
        //public Event<ICompleteTask> TaskCompleted { get; set; }
        public Event<IOrderStockEvent> StockCompleted { get; set; }
        public Event<IOrderDeliveriedEvent> OrderDeliveryCompleted { get; set; }
        //public Event<IOrderCompletedEvent> OrderCompleted { get; set; }

        public OrderSaga()
        {
            InstanceState(s => s.CurrentState);

            Event(() => OrderCommand, x => x.CorrelateBy(
                s => s.OrderCode,
                context => context.Message.OrderCode)
                .SelectId(context => Guid.NewGuid()));

            Event(() => OrderProcessed, x => x.CorrelateById(s => s.Message.CorrelationId));
            //Event(() => TaskCompleted, x => x.CorrelateById(s => s.Message.CorrelationId));
            Event(() => StockCompleted, x => x.CorrelateById(s => s.Message.CorrelationId));
            Event(() => OrderDeliveryCompleted, x => x.CorrelateById(s => s.Message.CorrelationId));
            //Event(() => OrderCompleted, x => x.CorrelateById(s => s.Message.CorrelationId));

            Initially(
                When(OrderCommand).Then(context =>
                    {
                        context.Instance.OrderCode = context.Data.OrderCode;
                        context.Instance.OrderId = context.Data.OrderId;
                    }

                ).Then(context => Console.Out.WriteLineAsync($"{context.Data.OrderCode} order id is received..{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}"))
                    .TransitionTo(Received)
                    .Publish(context =>
                            new OrderReceivedEvent(context.Instance))
                
            );

            During(Received, When(OrderProcessed)
                .ThenAsync(
                    context =>
                        Console.Out.WriteLineAsync(
                            $"{context.Data.OrderId} order billed..{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}")
                    )
                .Catch<Exception>(ex =>
                    ex.Then(t => Console.WriteLine(t.Exception.Message)))
                .TransitionTo(Processed)
                    );

            During(Received, When(StockCompleted)
                .Then(
                    context => Console.Out.WriteLineAsync(
                        $"{context.Data.OrderCode} order stocked(Received)..{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}"))
                .Catch<Exception>(ex =>
                    ex.Then(t => Console.WriteLine(t.Exception.Message)))
                .TransitionTo(Stocked)

            );

            During(Processed, When(StockCompleted)
                .Then(
                    context => Console.Out.WriteLineAsync(
                        $"{context.Data.OrderCode} order stocked(Processed)..{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}"))

                .Catch<Exception>(ex =>
                    ex.Then(t => Console.WriteLine(t.Exception.Message))).TransitionTo(Stocked)

                );


            During(Stocked,
                When(OrderDeliveryCompleted)
                    .Then(
                        context => Console.Out.WriteLineAsync(
                            $"{context.Data.OrderCode} order deliveried ..{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}"))
                    .TransitionTo(Deliveried)
                    .Catch<Exception>(ex =>
                        ex.Then(t=>Console.WriteLine(t.Exception.Message)))
                    .Finalize());

            //During(Deliveried, When(OrderCompleted)
            //    .Then(context => Console.Out.WriteLineAsync(
            //        $"{context.Data.OrderCode} order  completed..{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}"))
            //    .TransitionTo(Completed)
            //    .Finalize());

            SetCompletedWhenFinalized();
        }
    }
}
