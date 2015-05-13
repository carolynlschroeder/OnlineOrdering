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
            var billingAddress = new AddressModel { AddressId = Guid.NewGuid(), AddressTypeId = 0, CustomerId = model.CustomerId };
            var shippingAddress = new AddressModel { AddressId = Guid.NewGuid(), AddressTypeId = 1, CustomerId = model.CustomerId };
            model.AddressList = new List<AddressModel> {billingAddress, shippingAddress};

            return View(model);

        }

        [HttpPost]
        public ActionResult CreateCustomer(CustomerModel customerModel)
        {
            var customer = new Customer();
            CustomerFromCustomerModel(customerModel, customer);
            foreach (var addressModel in customerModel.AddressList)
            {
                var a = new Address();
                AddressFromAddressModel(a, addressModel);
                customer.Addresses.Add(a);
            }
           

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

       

        [HttpPost]
        public ActionResult EditCustomer(CustomerModel customerModel)
        {
            var repository = new CustomerRepository();
            var customer = repository.GetCustomer(customerModel.CustomerId);
            CustomerFromCustomerModel(customerModel, customer);
            foreach (var addressModel in customerModel.AddressList)
            {
                var address = customer.Addresses.FirstOrDefault(a => a.AddressId == addressModel.AddressId);
                AddressFromAddressModel(address,addressModel);
            }

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

        public void AddressFromAddressModel(Address a, AddressModel model)
        {

            a.AddressId = model.AddressId;
            a.AddressTypeId = model.AddressTypeId;
            a.CustomerId = model.CustomerId;
            a.StreetAddress = model.StreetAddress;
            a.City = model.City;
            a.State = model.State;
            a.Zip = model.Zip;

        }

        private void CustomerFromCustomerModel(CustomerModel customerModel, Customer customer)
        {
            customer.CustomerId = customerModel.CustomerId;
            customer.FirstName = customerModel.FirstName;
            customer.LastName = customerModel.LastName;
            customer.MiddleI = customerModel.MiddleI;
            
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

            model.AddressList = new List<AddressModel>();

            foreach (var address in customer.Addresses.OrderBy(a=>a.AddressTypeId))
            {
                var addressModel = AddressModelFromAddress(address);
                model.AddressList.Add(addressModel);
            }

            return model;
        }
    }
}