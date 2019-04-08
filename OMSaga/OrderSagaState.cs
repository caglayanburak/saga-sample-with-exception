using System;
using System.Collections.Generic;
using System.Text;
using Automatonymous;

namespace OMSaga
{
    public class OrderSagaState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
         public State CurrentState { get; set; }
        public int OrderId { get; set; }
        public string OrderCode { get; set; }
    }
}
