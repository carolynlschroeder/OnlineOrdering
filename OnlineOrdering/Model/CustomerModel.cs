using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineOrdering.Model
{
    public class CustomerModel
    {
        public CustomerModel()
        {
            BillingAddress = new AddressModel {AddressId = Guid.NewGuid(), AddressTypeId= 0};
            ShippingAddress = new AddressModel {AddressId = Guid.NewGuid(), AddressTypeId = 1};
        }
        public Guid CustomerId { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Middle Initial is required")]
        [Display(Name = "Middle Initial")]
        public string MiddleI { get; set; }
        public AddressModel BillingAddress { get; set; }
        public AddressModel ShippingAddress { get; set; }
    }
}