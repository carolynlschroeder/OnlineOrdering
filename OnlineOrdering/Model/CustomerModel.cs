using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineOrdering.Model
{
    public class CustomerModel
    {
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
        public List<AddressModel> AddressList { get; set; }
    }
}