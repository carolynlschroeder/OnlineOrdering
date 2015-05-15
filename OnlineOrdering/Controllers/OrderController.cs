using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineOrdering.Model;
using OnlineOrderingBusiness.Repositories;

namespace OnlineOrdering.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult CreateOrder()
        {
            var repository = new CustomerRepository();
            var customerDictionary =
                repository.GetCustomers()
                    .Select(c => new {CustomerId = c.CustomerId, CustomerName = c.FirstName + " " + c.LastName})
                    .ToDictionary(x => x.CustomerId, x => x.CustomerName);
            var model = new OrderModel
            {
                OrderId = Guid.NewGuid(),
                OrderDateTime = DateTime.Now,
                CustomerDictionary = customerDictionary
            };
            return View(model);
        }

        public ActionResult AddItem()
        {
            return View();
        }
    }
}