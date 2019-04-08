using System;

namespace CommonLib
{
    public class Order : IOrderCommand
    {
        public int OrderId { get; set; }
        public string OrderCode { get; set; }
    }
    
   public interface IOrderCommand
   {
       int OrderId { get; set; }
       string OrderCode { get; set; }

    }

   public interface ICompleteTask
   {
       Guid CorrelationId { get; }
       int OrderId { get; }
       string OrderCode { get; set; }
    }

   public class CompleteTask : ICompleteTask
   {
       public Guid CorrelationId { get; set; }
       public int OrderId { get; set; }
       public string OrderCode { get; set; }
   }
}
