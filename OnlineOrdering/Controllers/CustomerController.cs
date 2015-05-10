using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineOrdering.Model;
using OnlineOrderingBusiness;
using OnlineOrderingBusiness.Repositories;

namespace OnlineOrdering.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Customers()
        {
            var repository = new CustomerRepository();
            var model = repository.GetCustomers().Select(c => new CustomerModel
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                MiddleI = c.MiddleI
            }).ToList();
            
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateCustomer()
        {
            var model = new CustomerModel();
            return View(model);

        }

        [HttpPost]
        public ActionResult CreateCustomer(CustomerModel customerModel)
        {
            var customer = new Customer
            {
                CustomerId = Guid.NewGuid(),
                FirstName = customerModel.FirstName,
                LastName = customerModel.LastName,
                MiddleI = customerModel.MiddleI
            };

            var billingAddress = AddressFromAddressModel(customerModel.BillingAddress);
            billingAddress.CustomerId = customer.CustomerId;
            var shippingAddress = AddressFromAddressModel(customerModel.ShippingAddress);
            shippingAddress.CustomerId = customer.CustomerId;
            customer.Addresses = new List<Address> {billingAddress, shippingAddress};

            var repository = new CustomerRepository();
            repository.CreateCustomer(customer);
            return RedirectToAction("Customers");

        }

        [HttpGet]
        public ActionResult EditCustomer(Guid id)
        {
            var repository = new CustomerRepository();
            var customer = repository.GetCustomer(id);
            var model = new CustomerModel
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                MiddleI = customer.MiddleI
            };

            return View(model);
        }

        private AddressModel AddressModelFromAddress(Address address)
        {
            return new AddressModel
            {
                AddressId = address.AddressId,
                AddressTypeId = address.AddressTypeId,
                CustomerId = address.CustomerId,
                StreetAddress = address.StreetAddress,
                City = address.City,
                State = address.State,
                Zip = address.Zip
            };
        }

        public Address AddressFromAddressModel(AddressModel model)
        {
            return new Address
            {
                AddressId = model.AddressId,
                AddressTypeId = model.AddressTypeId,
                CustomerId = model.CustomerId,
                StreetAddress = model.StreetAddress,
                City = model.City,
                State = model.State,
                Zip = model.Zip
            };
        }
    }
}