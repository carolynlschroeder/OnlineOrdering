using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineOrdering.Models
{
    public class AddressModel
    {
        public Guid AddressId { get; set; }
        public int AddressTypeId { get; set; }
        public Guid CustomerId { get; set; }
        [Required(ErrorMessage = "Street Address is required")]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        [Required(ErrorMessage = "Zip is required")]
        public string Zip { get; set; }
    }
}