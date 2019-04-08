using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    public interface IOrderReceivedEvent
    {
        Guid CorrelationId { get; }
        int OrderId { get; }
        string OrderCode { get; }
    }

    public interface IOrderReceivedCancelEvent
    {
        Guid CorrelationId { get; set; }
        int OrderId { get; set; }
        string OrderCode { get; set; }
    }
}
