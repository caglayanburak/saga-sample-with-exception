using System;

namespace CommonLib
{
    public interface IOrderCompletedEvent
    {
        Guid CorrelationId { get; }
        int OrderId { get; }
        string OrderCode { get; }
        string Stock { get; }
    }
}
