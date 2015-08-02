using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineOrdering.Models
{
    public class OrderItemModel
    {
        [Display(Name = "Order Item ID")]
        public Guid OrderItemId { get; set; }
        public Guid OrderId { get; set; }
        [Display(Name = "Product")]
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Dictionary<Guid, string> ProductDictionary { get; set; }
        [Display(Name = "Order Item Total")]
        public string OrderItemTotal { get; set; }
        public Dictionary<int, int> QuantityDictionary { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
}
}