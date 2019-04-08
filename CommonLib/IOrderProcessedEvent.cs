using System;

namespace CommonLib
{
    public interface IOrderProcessedEvent
    {
        Guid CorrelationId { get; }
        int OrderId { get; }
    }

    public interface IOrderProcessedCancelEvent
    {
        Guid CorrelationId { get; set; }
        int OrderId { get; set; }
    }


}
