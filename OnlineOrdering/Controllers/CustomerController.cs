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
            model.CustomerId = Guid.NewGuid();
            model.BillingAddress = new AddressModel { AddressId = Guid.NewGuid(), AddressTypeId = 0, CustomerId = model.CustomerId };
            model.ShippingAddress = new AddressModel { AddressId = Guid.NewGuid(), AddressTypeId = 1, CustomerId = model.CustomerId };
            return View(model);

        }

        [HttpPost]
        public ActionResult CreateCustomer(CustomerModel customerModel)
        {
            var customer = new Customer();
            CustomerFromCustomerModel(customerModel, customer);

            var repository = new CustomerRepository();
            repository.CreateCustomer(customer);
            return RedirectToAction("Customers");

        }

        [HttpGet]
        public ActionResult EditCustomer(Guid id)
        {
            var repository = new CustomerRepository();
            var customer = repository.GetCustomer(id);
            var model = CustomerModelFromCustomer(customer);

            return View(model);
        }

       

        public ActionResult EditCustomer(CustomerModel customerModel)
        {
            var repository = new CustomerRepository();
            var customer = repository.GetCustomer(customerModel.CustomerId);
            CustomerFromCustomerModel(customerModel, customer);

            repository.EditCustomer(customer);
            return RedirectToAction("Customers");
        }

        public ActionResult DeleteCustomer(Guid id)
        {
            var repository = new CustomerRepository();
            repository.DeleteCustomer(id);
            return RedirectToAction("Customers");
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

        private void CustomerFromCustomerModel(CustomerModel customerModel, Customer customer)
        {
            customer.CustomerId = customerModel.CustomerId;
            customer.FirstName = customerModel.FirstName;
            customer.LastName = customerModel.LastName;
            customer.MiddleI = customerModel.MiddleI;
            
            var billingAddress = AddressFromAddressModel(customerModel.BillingAddress);
            var shippingAddress = AddressFromAddressModel(customerModel.ShippingAddress);
            customer.Addresses = new List<Address> { billingAddress, shippingAddress };
        }

        private CustomerModel CustomerModelFromCustomer(Customer customer)
        {
            var model = new CustomerModel
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                MiddleI = customer.MiddleI
            };
            model.BillingAddress = AddressModelFromAddress(customer.Addresses.FirstOrDefault(a => a.AddressTypeId == 0));
            model.ShippingAddress = AddressModelFromAddress(customer.Addresses.FirstOrDefault(a => a.AddressTypeId == 1));
            return model;
        }
    }
}