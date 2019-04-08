using System;

namespace CommonLib
{
    public class OrderStockEvent : IOrderStockEvent
    {
        public Guid CorrelationId { get; set; }
        public int OrderId { get; set; }
        public string OrderCode { get; set; }
        public string Stock { get; set; }
    }
    public interface IOrderStockEvent
    {
        Guid CorrelationId { get; }
        int OrderId { get; }
        string OrderCode { get; }
        string Stock { get; }
    }
}
