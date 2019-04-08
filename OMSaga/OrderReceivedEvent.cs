using CommonLib;
using System;

namespace OMSaga
{
    public class OrderReceivedEvent : IOrderReceivedEvent
    {
        private readonly OrderSagaState _orderSagaState;

        public OrderReceivedEvent(OrderSagaState orderSagaState)
        {
            _orderSagaState = orderSagaState;
        }

        public Guid CorrelationId => _orderSagaState.CorrelationId;

        public string OrderCode => _orderSagaState.OrderCode;

        public int OrderId => _orderSagaState.OrderId;
    }

    public class OrderProcessedEvent : IOrderProcessedEvent
    {
        private readonly OrderSagaState _orderSagaState;

        public OrderProcessedEvent(OrderSagaState orderSagaState)
        {
            _orderSagaState = orderSagaState;
        }

        public Guid CorrelationId => _orderSagaState.CorrelationId;

        public string OrderCode => _orderSagaState.OrderCode;

        public int OrderId => _orderSagaState.OrderId;
    }

    public class OrderStockedEvent : IOrderStockEvent
    {
        private readonly OrderSagaState _orderSagaState;

        public OrderStockedEvent(OrderSagaState orderSagaState)
        {
            _orderSagaState = orderSagaState;
        }

        public Guid CorrelationId { get; set; }

        public string OrderCode { get; set; }
        public string Stock { get; }

        public int OrderId { get; set; }
    }

}
