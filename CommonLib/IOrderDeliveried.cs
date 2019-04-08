using System;

namespace CommonLib
{
    public class OrderDelivery : IOrderDeliveriedEvent
    {
        public Guid CorrelationId { get; }
        public int OrderId { get; }
        public string OrderCode { get; }
        public string Delivery { get; }
    }
    public interface IOrderDeliveriedEvent
    {
        Guid CorrelationId { get; }
        int OrderId { get; }
        string OrderCode { get; }
        string Delivery { get; }

    }
}
