using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OnlineOrdering.Models;

namespace OnlineOrdering.Models
{
    public class OrderModel
    {
        public Guid OrderId { get; set; }
        [Required(ErrorMessage = "Customer is required")]
        [Display(Name = "Customer")]
        public Guid CustomerId { get; set; }
        [Display(Name = "Order Date")]
        public DateTime OrderDateTime { get; set; }
        public Dictionary<Guid, string> CustomerDictionary { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
        [Display(Name = "Order Total")]
        public decimal OrderTotal { get; set; }
        [Display(Name = "Order Code")]
        public string OrderCode { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
    }
}