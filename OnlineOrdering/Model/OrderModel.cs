using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineOrdering.Model
{
    public class OrderModel
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public Dictionary<Guid, string> CustomerDictionary { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
    }
}